using CustomControlsDemo.Controls;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Platform;

namespace CustomControlsDemo
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

      // plattformspezifische Anpassung der Steuerelemente, nachdem
      // die Eigenschaft Progress einer Progressbar verändert wurde
      Microsoft.Maui.Handlers.ProgressBarHandler.Mapper
  .AppendToMapping(nameof(IProgress.Progress), (handler, view) =>
  {
    if (view.Progress > 0.3)
    {
#if ANDROID
      handler.PlatformView.ProgressTintList =
        Android.Content.Res.ColorStateList.ValueOf(Colors.Green.ToPlatform());
#elif IOS || MACCATALYST
      handler.PlatformView.ProgressTintColor = Colors.Blue.ToPlatform();
#elif WINDOWS
      handler.PlatformView.Foreground = Colors.DarkOrange.ToPlatform();
#endif
    }
  });
      //builder.ConfigureMauiHandlers(handlers =>
      //{
      //  handlers.AddHandler<ProgressBar, CircularProgressBarHandler>();
      //});

      return builder.Build();
    }
  }
}
