namespace EzCheckout.Api.Tests;

using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;

using Xunit;

public class ValuesControllerTests {
    private static readonly JsonSerializerOptions JsonSerializerOptions = new() {
        PropertyNameCaseInsensitive = true
    };

    [Fact]
    public async Task TestGet() {
        LambdaEntryPoint lambdaFunction = new();

        string requestStr = File.ReadAllText("./SampleRequests/ValuesController-Get.json");
        APIGatewayProxyRequest? request = JsonSerializer.Deserialize<APIGatewayProxyRequest>(requestStr, JsonSerializerOptions);
        TestLambdaContext context = new();
        APIGatewayProxyResponse response = await lambdaFunction.FunctionHandlerAsync(request, context);

        Assert.Equal(200, response.StatusCode);
        Assert.Equal("[\"value1\",\"value2\"]", response.Body);
        Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
        Assert.Equal("application/json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
    }
}