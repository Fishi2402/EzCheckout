namespace EzCheckout.Infrastructure;

using Amazon.CDK;

internal sealed class Program {
    public static void Main() {
        App app = new();
        EzCheckoutCdkStack stack = new (app, "EzCheckoutInfrastructureStack");
        app.Synth();
    }
}
