using CommunityToolkit.Maui.Maps;

namespace ViewsSample;

public static class MauiProgram
{
  // Get your Bing Maps Key from the Bing Maps Dev Center
  // https://learn.microsoft.com/en-us/bingmaps/getting-started/bing-maps-dev-center-help/getting-a-bing-maps-key
  const string YourBingMapsKey = "<Your Bing Maps Key Here>";

  // To run this sample under android, you need to get a Goole Maps Key at:
  // https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/map?view=net-maui-10.0#get-a-google-maps-api-key
  // and replace the value of com.google.android.geo.API_KEY in Platforms/Android/AndroidManifest.xml with your Google Maps Key.

  public static MauiApp CreateMauiApp()
  {
    var builder = MauiApp.CreateBuilder();
    builder
      .UseMauiApp<App>()
      .ConfigureFonts(fonts =>
      {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
      })
#if WINDOWS
			// Initialize the .NET MAUI Community Toolkit Maps by adding the below line of code
			.UseMauiCommunityToolkitMaps(YourBingMapsKey);
#else
            // For all other platforms
            .UseMauiMaps();
#endif

#if WINDOWS
    Microsoft.Maui.Handlers.DatePickerHandler.Mapper
      .AppendToMapping("FixFirstDayOfWeek", (handler, view) =>
    {
      handler.PlatformView.FirstDayOfWeek = (Windows.Globalization.DayOfWeek)(int)
          System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
    });

    Microsoft.Maui.Handlers.TimePickerHandler.Mapper
      .AppendToMapping("TimeFormatFix", (handler, view) =>
    {

      // The commnted solution works only once. If you select a time for a specific TimePicker instance, 
      // the ClockIdentifier will be reset to 12HourClock because the Maui
      // TimePicker View format property doesn't contain an uppercase or lowercase "h".
      // Please note that this behavior may change in future updates.
      // if (Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern.Contains("h"))
      //{
      //  handler.PlatformView.ClockIdentifier = "12HourClock";
      //}
      //else
      //{
      //  handler.PlatformView.ClockIdentifier = "24HourClock";
      //}

      // change the format property of the Maui TimePicker if
      // it's value equals to it's default value.
      // Currently the default value is "t",
      // which is the format string for a short time pattern.
      // Setting the format property leads to the correct ClockIdentifier
      // of 12HourClock or 24HourClock under Windows.
      if (view is TimePicker timePicker
              && timePicker.Format ==
                  TimePicker.FormatProperty.DefaultValue as string)
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
