namespace EzCheckout.Infrastructure;

using Amazon.CDK;
using Amazon.CDK.AWS.Cognito;

using Constructs;

internal sealed class AuthConstruct : Construct {
    internal AuthConstruct(Construct scope, string id)
            : base(scope, id)
    {
        // 1. Create the user-pool
        UserPool = new (
            scope: this,
            id: "EzCheckoutUserPool",
            props: new UserPoolProps() {
                UserPoolName = "ezcheckout-user-pool",
                // Allow that users can sign up by themselves
                SelfSignUpEnabled = true,
                // Allow sign-in with email address
                SignInAliases = new SignInAliases {
                    Email = true
                },
                // Automatically verify email addresses
                AutoVerify = new AutoVerifiedAttrs {
                    Email = true
                },
                StandardAttributes = new StandardAttributes {
                    GivenName = new StandardAttribute {
                        Required = false,
                        Mutable = true
                    },
                    FamilyName = new StandardAttribute {
                        Required = false,
                        Mutable = true
                    },
                    Email = new StandardAttribute {
                        Required = true,
                        Mutable = true
                    }
                },
                PasswordPolicy = new PasswordPolicy {
                    MinLength = 8,
                    RequireLowercase = true,
                    RequireUppercase = true,
                    RequireDigits = true,
                    RequireSymbols = true
                },
                // Account recovery via email only
                AccountRecovery = AccountRecovery.EMAIL_ONLY,
                // MFA is optional, but recommended
                Mfa = Mfa.OPTIONAL,
                RemovalPolicy = RemovalPolicy.DESTROY // For development purposes
            });

        // 2. Create the user-pool client
        UserPoolClient userPoolClient = new (
            scope: this,
            id: "EzCheckoutUserPoolClient",
            props: new UserPoolClientProps() {
                UserPool = UserPool,
                UserPoolClientName = "ezcheckout-user-pool-client",
                GenerateSecret = false,
                SupportedIdentityProviders = [
                    UserPoolClientIdentityProvider.COGNITO
                ],

                // OAuth 2.0 settings
                OAuth = new OAuthSettings {
                    Flows = new OAuthFlows {
                        AuthorizationCodeGrant = true,
                        ImplicitCodeGrant = false
                    },
                    CallbackUrls = [
                        "http://localhost:3000/callback", // For local development
                    ],
                    LogoutUrls = [
                        "http://localhost:3000/" // For local development
                    ],
                }
            });

        // 3. Output the user-pool ID and user-pool client ID for reference
        new CfnOutput(this, "UserPoolId", new CfnOutputProps {
            Value = UserPool.UserPoolId,
            Description = "The ID of the Cognito User Pool for EzCheckout."
        });

        new CfnOutput(this, "UserPoolClientId", new CfnOutputProps {
            Value = userPoolClient.UserPoolClientId,
            Description = "The ID of the Cognito User Pool Client for EzCheckout."
        });
    }

    internal UserPool UserPool { get; }
}
