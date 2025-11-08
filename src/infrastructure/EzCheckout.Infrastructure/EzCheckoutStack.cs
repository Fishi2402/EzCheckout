namespace EzCheckout.Infrastructure;
using Amazon.CDK;

using Constructs;

public class EzCheckoutCdkStack : Stack {
    internal EzCheckoutCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props) {
        DatabaseConstruct databaseConstruct = new(this, "Database");
        AuthConstruct authConstruct = new (this, "Auth");
        BackendConstruct backendConstruct = new (this, "Backend");
        FrontendConstruct frontendConstruct = new (this, "Frontend");
    }
}
