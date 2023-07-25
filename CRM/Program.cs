namespace CRM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

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
                pattern: "calendar/{year:int?}/{month:int?}",
                defaults: new { controller = "Calendar", action = "Calendar" });
            app.MapControllerRoute(
                name: "default",
                pattern: "/",
                defaults: new { controller = "Calendar", action = "Calendar" });

            app.Run();
        }
    }
}