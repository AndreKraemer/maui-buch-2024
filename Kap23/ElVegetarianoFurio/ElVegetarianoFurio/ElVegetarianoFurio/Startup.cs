using System;
using ElVegetarianoFurio.Core;
using ElVegetarianoFurio.Data;
using ElVegetarianoFurio.Menu;
using ElVegetarianoFurio.Profile;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Xamarin.Essentials;

namespace ElVegetarianoFurio
{
    public static class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static App Init(Action<HostBuilderContext, IServiceCollection> configurePlattformServices)
        {
            var host = new HostBuilder()
                .ConfigureHostConfiguration(c =>
                {
                    // Konfigurationsdatei auslesen
                    c.AddCommandLine(new [] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                    c.AddJsonFile(new EmbeddedFileProvider(typeof(Startup).Assembly, 
                                        typeof(Startup).Namespace),
                                        "appsettings.json", false, false);
                })
                .ConfigureServices((c, x) =>
                {
                    configurePlattformServices(c, x);   // Plattformspezifische Konfiguration
                    ConfigureServices(c, x);            // Plattformübergreifende Konfiguration
                })
                .Build();


            ServiceProvider = host.Services;
            return new App();
        }

        static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            // Umgebungsabhängige Konfiguration durchführen
            if (ctx.HostingEnvironment.IsDevelopment())
            {
                // services.AddSingleton<IDataService, DummyDataService>();
            }
            else
            {
                // services.AddSingleton<IDataService, DataService>();
            }
            
            services.AddTransient<AppShell>();
            services.AddSingleton<INavigationService, NavigationService>();
            
            services.AddTransient<ProfileViewModel>();
            services.AddSingleton<IProfileService, ProfileService>();
           
            //services.AddSingleton<IDataService, DummyDataService>();
            services.AddSingleton<IDataService, WebApiDataService>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<CategoryViewModel>();
            services.AddTransient<DishViewModel>();

        }
    }
}
