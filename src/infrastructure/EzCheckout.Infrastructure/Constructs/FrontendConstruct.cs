namespace EzCheckout.Infrastructure;

using Amazon.CDK.AWS.CloudFront;
using Amazon.CDK.AWS.CloudFront.Origins;
using Amazon.CDK.AWS.S3;

using Constructs;

/// <summary>
/// Represents a construct for the frontend components of the EzCheckout infrastructure.
/// </summary>
internal sealed class FrontendConstruct : Construct {
    internal FrontendConstruct(Construct scope, string id) 
            : base(scope, id)
    {
        // 1. Create the S3 bucket for hosting the frontend application.
        Bucket hostingBucket = new (
            scope: this,
            id: "FrontendBucket",
            props: new BucketProps() {
                WebsiteIndexDocument = "index.html",
                WebsiteErrorDocument = "index.html",
                BlockPublicAccess = BlockPublicAccess.BLOCK_ALL, // Secure by default
                RemovalPolicy = Amazon.CDK.RemovalPolicy.DESTROY, // For development purposes
                AutoDeleteObjects = true // For development purposes
        });

        // 2. Create Cloud Front Distribution
        Distribution distribution = new(
            scope: this,
            id: "CloudFrontDistribution",
            props: new DistributionProps() {
                DefaultBehavior = new BehaviorOptions() {
                    Origin = S3BucketOrigin.WithOriginAccessControl(hostingBucket),
                    ViewerProtocolPolicy = ViewerProtocolPolicy.REDIRECT_TO_HTTPS,
                    AllowedMethods = AllowedMethods.ALLOW_GET_HEAD_OPTIONS
                },
                // All React-Router routes should point to index.html
                ErrorResponses = new IErrorResponse[] {
                    new ErrorResponse {
                        HttpStatus = 404,
                        ResponseHttpStatus = 200,
                        ResponsePagePath = "/index.html",
                    },
                    new ErrorResponse {
                        HttpStatus = 403,
                        ResponseHttpStatus = 200,
                        ResponsePagePath = "/index.html",
                    }
                },
                DefaultRootObject = "index.html",
                PriceClass = PriceClass.PRICE_CLASS_100
            });

        // 3. Expose the URL of the distribution
        DistributionDomainName = distribution.DistributionDomainName;

        // 4. Output the bucket name and distribution domain name for reference
        new Amazon.CDK.CfnOutput(
            scope: this,
            id: "FrontendBucketNameOutput",
            props: new Amazon.CDK.CfnOutputProps {
                Value = hostingBucket.BucketName,
                Description = "The name of the S3 bucket hosting the frontend application."
            });
        new Amazon.CDK.CfnOutput(
            scope: this,
            id: "CloudFrontDistributionDomainNameOutput",
            props: new Amazon.CDK.CfnOutputProps {
                Value = distribution.DistributionDomainName,
                Description = "The domain name of the CloudFront distribution."
            });
    }

    internal string DistributionDomainName { get; }
}
