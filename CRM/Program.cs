using CRM.Database;
using CRM.Models;
using CRM.Services;
using CRM.Utilities;
using CRM.Utilities.Converters;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Net;

namespace CRM
{
    public static class Program
    {
        private static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
                });
            builder.Services.AddAuthentication(Cookies.Identity)
                .AddCookie(Cookies.Identity, options =>
                {
                    options.Cookie.Name = Cookies.Identity;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.MustBeAdmin, policy => policy.RequireRole(nameof(Role.Admin)));
                options.AddPolicy(Policies.MustBeUser , policy => policy.RequireRole(nameof(Role.User)));
            });
            builder.Services.AddScoped<ToDoItemsService>();
            builder.Services.AddScoped<PeopleService>();
        }

        private static void ConfigureDatabase(this WebApplicationBuilder builder)
        {
            string? connectionStringName = builder.Configuration["UseConnectionString"];
            if (connectionStringName == null) { throw new ConfigurationErrorsException("'UseConnectionString' must be specified"); }

            string? connectionString = builder.Configuration.GetConnectionString(connectionStringName);
            if (connectionString == null) { throw new ConfigurationErrorsException($"Could not find {connectionStringName} connection string"); }

            builder.Services.AddDbContext<CRMDbContext>(options => options.UseMySQL(connectionString));
        }

        private static async Task MigrateDatabase(this WebApplicationBuilder builder)
        {
            try
            {
                using var scope = builder.Services.BuildServiceProvider().CreateScope();
                await scope.ServiceProvider.GetRequiredService<CRMDbContext>().Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while connecting to the database. Probably wrong configuration.", ex);
            }
        }

        private static void ConfigureMiddlewarePipeline(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
        }

        private static void ConfigureRoutes(this WebApplication app)
        {
            app.MapControllerRoute(
                name: "todoitems",
                pattern: "/todoitems",
                defaults: new { controller = "Home", action = "ToDoItems" });
            app.MapControllerRoute(
                name: "default",
                pattern: "/",
                defaults: new { controller = "Home", action = "ToDoItems" });
            app.MapControllers().RequireAuthorization();
        }

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureDatabase();
            var dbMigrationTask = builder.MigrateDatabase();
            builder.ConfigureServices();

            var app = builder.Build();
            app.ConfigureMiddlewarePipeline();
            app.ConfigureRoutes();

            await dbMigrationTask;
            app.Run();
        }
    }
}