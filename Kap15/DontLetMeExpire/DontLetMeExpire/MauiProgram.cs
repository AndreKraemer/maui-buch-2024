using CommunityToolkit.Maui;
using DontLetMeExpire.Services;
using DontLetMeExpire.ViewModels;
using DontLetMeExpire.Views;
using Microsoft.Extensions.Logging;

namespace DontLetMeExpire
{
  public static class MauiProgram
  {
    public static MauiApp CreateMauiApp()
    {
      var builder = MauiApp.CreateBuilder();
      builder
        .UseMauiApp<App>()
        .UseMauiCommunityToolkit()
        .ConfigureFonts(fonts =>
        {
          fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
          fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

#if DEBUG
  		builder.Logging.AddDebug();
#endif
      builder.Services.AddSingleton<IStorageLocationService, DummyStorageLocationService>();
      builder.Services.AddSingleton<IItemService, DummyItemService>();
      builder.Services.AddSingleton<INavigationService, NavigationService>();
      builder.Services.AddTransient<MainViewModel>();
      builder.Services.AddTransient<MainPage>();
      builder.Services.AddTransient<ItemViewModel>();
      builder.Services.AddTransient<ItemPage>();
      return builder.Build();
    }
  }
}
