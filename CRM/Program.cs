using CRM.Database;
using CRM.Database.DbContexts;
using CRM.Services;
using CRM.Services.Util;

namespace CRM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options => 
                {
                    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
                });

            // Configure DB
            builder.Services.AddSingleton<CRMDbContext, MySqlDbContext>();
            builder.Services.AddSingleton<DatabaseService>();

            // Configure services
            builder.Services.AddSingleton<IToDoItemsService, ToDoItemsServiceDebug>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "calendar",
                pattern: "/calendar",
                defaults: new { controller = "Home", action = "Index" });
            app.MapControllerRoute(
                name: "default",
                pattern: "/",
                defaults: new { controller = "Home", action = "Index" });

            app.Run();
        }
    }
}