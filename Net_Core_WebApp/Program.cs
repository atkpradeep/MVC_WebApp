using Net_Core_WebApp.Contract;
using Net_Core_WebApp.Service;
using NLog.Extensions.Logging;

namespace Net_Core_WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddNLog();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<IJsonFileWrapper, JsonFileWrapper>();
            builder.Services.AddLogging();
            var app = builder.Build();


            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
