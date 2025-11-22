namespace EzCheckout.Infrastructure;

using System;

using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.RDS;
using Amazon.CDK.AWS.SecretsManager;

using Constructs;

internal sealed class DatabaseConstruct : Construct {
    internal DatabaseConstruct(Construct scope, string id)
            : base(scope, id) {

        // 1. Create a VPC with public and private subnets
        Vpc = new(
            scope: this,
            id: "EzCheckoutVpc",
            props: new VpcProps {
                VpcName = "ezcheckout-vpc",
                MaxAzs = 2,
                NatGateways = 1,
                SubnetConfiguration = [
                    // Subnet for public resources (e.g., load balancers)
                    new SubnetConfiguration {
                        Name = "PublicSubnet",
                        SubnetType = SubnetType.PUBLIC,
                        CidrMask = 24
                    },
                    // Subnet for private resources (e.g., databases, application servers)
                    new SubnetConfiguration {
                        Name = "PrivateSubnet",
                        SubnetType = SubnetType.PRIVATE_WITH_EGRESS,
                        CidrMask = 24
                    }
                ]
            });

        // 2. Create a Security Group for the database
        DatabaseSecurityGroup = new(
            scope: this,
            id: "DatabaseSecurityGroup",
            props: new SecurityGroupProps {
                Vpc = Vpc,
                Description = "Security group for EzCheckout database",
                AllowAllOutbound = true
            });

        // 3. Create Aurora Serverless v2 Database Cluster
        DatabaseCluster dbCluster = new(
            scope: this,
            id: "EzCheckoutDatabaseCluster",
            props: new DatabaseClusterProps() {
                Engine = DatabaseClusterEngine.AuroraPostgres(new AuroraPostgresClusterEngineProps() {
                    Version = AuroraPostgresEngineVersion.VER_16_3
                }),
                Writer = ClusterInstance.ServerlessV2("writer"),
                // Use low capacity for development purposes
                ServerlessV2MinCapacity = 0.5,
                ServerlessV2MaxCapacity = 1.5,
                DefaultDatabaseName = "ezcheckoutdb",
                // Add to vpc and private subnet
                Vpc = Vpc,
                VpcSubnets = new SubnetSelection {
                    SubnetType = SubnetType.PRIVATE_WITH_EGRESS
                },
                // Attach the security group
                SecurityGroups = [DatabaseSecurityGroup],

                RemovalPolicy = Amazon.CDK.RemovalPolicy.DESTROY, // For development purposes
            });
        if(dbCluster.Secret is null) {
            throw new InvalidOperationException("DatabaseCluster.Secret is null. Ensure credentials are auto-generated.");
        }
        DatabaseSecret = dbCluster.Secret;
    }

    internal ISecret DatabaseSecret { get; }
    internal Vpc Vpc { get; }
    internal SecurityGroup DatabaseSecurityGroup { get; }
}
