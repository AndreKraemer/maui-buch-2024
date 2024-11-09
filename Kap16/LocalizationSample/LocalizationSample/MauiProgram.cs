using Microsoft.Extensions.Logging;

namespace LocalizationSample
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
      // use the correct first day of the week
      Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("FixFirstDayOfWeek", (handler, view) =>
      {
        handler.PlatformView.FirstDayOfWeek = (Windows.Globalization.DayOfWeek)(int)
            System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
      });
      Microsoft.Maui.Handlers.TimePickerHandler.Mapper
      .AppendToMapping("TimeFormatFix", (handler, view) =>
      {
        // change the format property of the Maui TimePicker if it's value equals to it's
        // default value. Currently the default value is "t", which is the format string for a
        // short time pattern
        if (view is TimePicker timePicker
                && timePicker.Format == TimePicker.FormatProperty.DefaultValue as string)
        {
          timePicker.Format =
              Thread.CurrentThread.CurrentCulture
                .DateTimeFormat.ShortTimePattern;
        }
      });

#endif
      return builder.Build();
    }
  }
}
