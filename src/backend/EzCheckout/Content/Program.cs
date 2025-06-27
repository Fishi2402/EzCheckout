namespace EzCheckout;

using EzCheckout.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Npgsql;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Represents the entry point of the application.
/// </summary>
public class Program {
    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result
    /// parameter contains a <see cref="int"/> with the exit code of the application.</returns>
    public static async Task<int> Main() {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        _ = builder.Services.AddControllers();

        _ = builder.Services.AddDbContext<EzCheckoutContext>(options => {
            // Connect to the database specified in the configuration file
            _ = options.UseNpgsql(
                connection: new NpgsqlConnection(
                    builder.Configuration.GetConnectionString("EzCheckoutContext")),
                npgsqlOptionsAction: (npgsqlBuilder) => {
                    // At the moment , we are not using any specific options
                });
        });

        WebApplication app = builder.Build();

        using (IServiceScope scope = app.Services.CreateScope()) {
            EzCheckoutContext context = scope.ServiceProvider.GetRequiredService<EzCheckoutContext>();
            // Update the database to the latest migration
            await context.Database.MigrateAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        _ = app.MapControllers();

        await app.RunAsync();
        return 0;
    }
}
