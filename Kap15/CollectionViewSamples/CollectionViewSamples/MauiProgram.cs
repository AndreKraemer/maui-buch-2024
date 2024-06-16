using CollectionViewSamples.Models;
using CollectionViewSamples.Services;
using CollectionViewSamples.ViewModels;
using CollectionViewSamples.Views;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace CollectionViewSamples
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
        });

#if DEBUG
  		builder.Logging.AddDebug();
#endif
      builder.Services.AddSingleton<IDataStore<Person>, MockDataStore>();
      builder.Services.AddTransient<BindableLayoutSamplePage>();
      builder.Services.AddTransient<FirstSampleViewModel>();
      builder.Services.AddTransient<SelectSampleViewModel>();
      builder.Services.AddTransient<RefreshSampleViewModel>();
      builder.Services.AddTransient<MenuSampleViewModel>();
      builder.Services.AddTransient<GroupSampleViewModel>();
      builder.Services.AddTransient<BindableLayoutSampleViewModel>();
      builder.Services.AddTransient<FirstSamplePage>();
      builder.Services.AddTransient<SelectSamplePage>();
      builder.Services.AddTransient<RefreshSamplePage>();
      builder.Services.AddTransient<MenuSamplePage>();
      builder.Services.AddTransient<GroupSamplePage>();


      return builder.Build();
    }
  }
}
