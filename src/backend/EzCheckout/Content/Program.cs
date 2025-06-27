namespace EzCheckout;

using EzCheckout.Content.Core.Models.Identity;
using EzCheckout.Data;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Npgsql;

using System;
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
        builder.Services.AddControllers();

        builder.Services.AddDbContext<EzCheckoutContext>(options => {
            // Connect to the database specified in the configuration file
            options.UseNpgsql(
                connection: new NpgsqlConnection(
                    builder.Configuration.GetConnectionString("EzCheckoutContext")),
                npgsqlOptionsAction: (npgsqlBuilder) => {
                    // At the moment , we are not using any specific options
                });
        });

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 12;
        })
        .AddEntityFrameworkStores<EzCheckoutContext>()
        .AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(options => {
            // Configure the cookie settings
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
            options.LoginPath = "/api/auth/login";
            options.LogoutPath = "/api/auth/logout";
            options.SlidingExpiration = true;

            options.Events = new CookieAuthenticationEvents() {
                OnRedirectToLogin = context => {
                    // Handle redirect to login
                    context.Response.StatusCode = 401; // Unauthorized
                    return Task.CompletedTask;
                },
                OnRedirectToAccessDenied = context => {
                    // Handle redirect to access denied
                    context.Response.StatusCode = 403; // Forbidden
                    return Task.CompletedTask;
                }
            };
        });

        builder.Services.AddLogging(loggingBuilder => {
            // Configure logging
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
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
