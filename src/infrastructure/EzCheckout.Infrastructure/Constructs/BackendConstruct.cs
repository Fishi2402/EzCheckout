namespace EzCheckout.Infrastructure;

using System.Collections.Generic;

using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.Lambda;

using Constructs;

internal sealed class BackendConstruct : Construct {
    internal BackendConstruct(Construct scope, string id, BackendConstructProps props)
            : base(scope, id) {
        // 1. Security group for Lambda function
        SecurityGroup lambdaSecurityGroup = new(
            scope: this,
            id: "BackendLambdaSecurityGroup",
            props: new SecurityGroupProps {
                Vpc = props.Vpc,
                Description = "Security group for EzCheckout backend Lambda functions",
                AllowAllOutbound = true
            });

        // 2. Allow Lambda to connect to the database
        props.DatabaseSecurityGroup.AddIngressRule(
            peer: lambdaSecurityGroup,
            connection: Port.Tcp(5432), // PostgreSQL default port
            description: "Allow Lambda functions to connect to the database"
        );

        // 3. ASP.NET Core Lambda function
        Function lambdaFunction = new(
            scope: this,
            id: "EzCheckoutBackendLambdaFunction",
            props: new FunctionProps {
                FunctionName = "ezcheckout-backend-lambda",
                Runtime = Runtime.DOTNET_8,
                Handler = "EzCheckout.Api::EzCheckout.Api.LambdaEntryPoint::FunctionHandlerAsync",
                Code = Code.FromAsset("../backend/EzCheckout.Api/bin/Release/net8.0/win-x64/publish"),

                // Place in private subnet
                Vpc = props.Vpc,
                VpcSubnets = new SubnetSelection {
                    SubnetType = SubnetType.PRIVATE_WITH_EGRESS
                },
                SecurityGroups = [lambdaSecurityGroup],

                // Resource allocation
                MemorySize = 512,
                Timeout = Duration.Seconds(30),

                // Pass configuration for .NET application
                Environment = new Dictionary<string, string> {
                    { "DB_SECRET_ARN", props.DatabaseSecret.SecretArn },
                    { "COGNITO_USER_POOL_ID", props.UserPool.UserPoolId }
                }
            });

        // 4. Grant Lambda function access to the database secret
        props.DatabaseSecret.GrantRead(lambdaFunction);

        // 5. Create API gateway
        RestApi api = new(
            scope: this,
            id: "EzCheckoutApiGateway",
            props: new RestApiProps {
                RestApiName = "EzCheckout API",
                Description = "API Gateway for EzCheckout backend services",
                // Enable CORS
                DefaultCorsPreflightOptions = new CorsOptions {
                    AllowOrigins = [$"https://{props.DistributionDomainName}"],
                    AllowMethods = Cors.ALL_METHODS,
                    AllowHeaders = ["Content-Type", "Authorization", "X-Amz-Date"]
                }
            });

        // 6. Create Cognito Authorizer
        CognitoUserPoolsAuthorizer cognitoAuthorizer = new(
            scope: this,
            id: "EzCheckoutCognitoAuthorizer",
            props: new CognitoUserPoolsAuthorizerProps {
                CognitoUserPools = [props.UserPool],
                IdentitySource = "method.request.header.Authorization"
            });

        // 7. Integrate Lambda with API Gateway
        LambdaIntegration lambdaIntegration = new(lambdaFunction);

        // 8. Create API Gateway route -> We send everything to the Lambda function (.NET)
        api.Root.AddProxy(new ProxyResourceOptions() {
            AnyMethod = true,
            DefaultIntegration = lambdaIntegration,
            DefaultMethodOptions = new MethodOptions {
                AuthorizationType = AuthorizationType.COGNITO,
                Authorizer = cognitoAuthorizer
            }
        });

        // 9. Output the API endpoint URL
        new CfnOutput(this, "ApiEndpointUrl", new CfnOutputProps {
            Value = api.Url,
            Description = "The endpoint URL of the EzCheckout API Gateway."
        });
    }
}
