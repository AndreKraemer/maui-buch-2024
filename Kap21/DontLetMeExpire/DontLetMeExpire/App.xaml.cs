using DontLetMeExpire.Services;
using System.Globalization;

namespace DontLetMeExpire
{
  public partial class App : Application
  {
    public App(ISettingsService settingsService)
    {
      InitializeComponent();
      SetLanguage(settingsService.GetLanguage());
      SetTheme(settingsService.GetTheme());
      MainPage = new AppShell();
    }

    private void SetLanguage(string language)
    {
      if (string.IsNullOrEmpty(language))
      {
        return;
      }

      var cultureInfo = new CultureInfo(language);
      Thread.CurrentThread.CurrentCulture = cultureInfo;
      Thread.CurrentThread.CurrentUICulture = cultureInfo;
    }

    private void SetTheme(string theme)
    {
      if (string.IsNullOrEmpty(theme))
      {
        return;
      }

      UserAppTheme = theme == nameof(AppTheme.Dark) ? AppTheme.Dark : AppTheme.Light;
    }
  }
}
