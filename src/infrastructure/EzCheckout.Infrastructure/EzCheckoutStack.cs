namespace EzCheckout.Infrastructure;
using Amazon.CDK;

using Constructs;

public class EzCheckoutCdkStack : Stack {
    internal EzCheckoutCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props) {
        DatabaseConstruct databaseConstruct = new(this, "Database");
        AuthConstruct authConstruct = new (this, "Auth");
        FrontendConstruct frontendConstruct = new(this, "Frontend");
        BackendConstruct backendConstruct = new (this, "Backend", new BackendConstructProps(
            Vpc: databaseConstruct.Vpc,
            DatabaseSecurityGroup: databaseConstruct.DatabaseSecurityGroup,
            DatabaseSecret: databaseConstruct.DatabaseSecret,
            UserPool: authConstruct.UserPool,
            DistributionDomainName: frontendConstruct.DistributionDomainName));
    }
}
