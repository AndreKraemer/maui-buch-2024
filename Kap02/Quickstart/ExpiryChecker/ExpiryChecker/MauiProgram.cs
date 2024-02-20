using Microsoft.Extensions.Logging;


namespace ExpiryChecker
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

#if WINDOWS
      Microsoft.Maui.Handlers.DatePickerHandler.Mapper
        .AppendToMapping("FixFirstDayOfWeek", (handler, view) =>
      {
        handler.PlatformView.FirstDayOfWeek = (Windows.Globalization.DayOfWeek)(int)
            System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
      });
#endif

      return builder.Build();
    }
  }
}
