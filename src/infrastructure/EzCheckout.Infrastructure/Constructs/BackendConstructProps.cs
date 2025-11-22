namespace EzCheckout.Infrastructure;

using Amazon.CDK.AWS.Cognito;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.SecretsManager;

/// <summary>
/// Represents the configuration properties required to construct a backend component.
/// </summary>
internal readonly record struct BackendConstructProps(
    Vpc Vpc,
    SecurityGroup DatabaseSecurityGroup,
    ISecret DatabaseSecret,
    UserPool UserPool,
    string DistributionDomainName);
