using ElVegetarianoFurio.Maui.AutomaticMigration.Core;
using ElVegetarianoFurio.Maui.AutomaticMigration.Data;
using ElVegetarianoFurio.Maui.AutomaticMigration.Menu;
using ElVegetarianoFurio.Maui.AutomaticMigration.Profile;
using Microsoft.Extensions.Logging;
using MonkeyCache.FileStore;

namespace ElVegetarianoFurio.Maui.AutomaticMigration
{
  public static class MauiProgram
  {
    public static MauiApp CreateMauiApp()
    {
      var builder = MauiApp.CreateBuilder();
      builder
        .UseMauiApp<App>()
        .ConfigureFonts(fonts =>
        {
          fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
          fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
          fonts.AddFont("Font Awesome 5 Free-Solid-900.otf", "Fa-Solid");
          fonts.AddFont("ShadowsIntoLight-Regular.ttf", "Shadows");
          fonts.AddFont("Sofia-Regular.ttf", "Sofia");
        });

#if DEBUG
      builder.Logging.AddDebug();
#endif
      Barrel.ApplicationId = "ElVegetarianoFurio";

      builder.Services.AddTransient<AppShell>();
      builder.Services.AddSingleton<INavigationService, NavigationService>();

      builder.Services.AddTransient<ProfileViewModel>();
      builder.Services.AddTransient<ProfilePage>();
      builder.Services.AddSingleton<IProfileService, ProfileService>();

      //services.AddSingleton<IDataService, DummyDataService>();
      builder.Services.AddSingleton<IDataService, WebApiDataService>();
      builder.Services.AddTransient<MainViewModel>();
      builder.Services.AddTransient<MainPage>();
      builder.Services.AddTransient<CategoryViewModel>();
      builder.Services.AddTransient<CategoryPage>();
      builder.Services.AddTransient<DishViewModel>();
      builder.Services.AddTransient<DishPage>();

      return builder.Build();
    }
  }
}
