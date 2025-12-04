using DontLetMeExpire.Services;
using Microsoft.Maui.Controls.StyleSheets;
using System.Globalization;
using System.Reflection;

namespace DontLetMeExpire
{
  public partial class App : Application
  {
    public App(ISettingsService settingsService)
    {
      InitializeComponent();
      // Fix for loading embedded stylesheet when XamlSourceGenerator is used
      var assembly = Assembly.GetExecutingAssembly();
      var resourceName = "DontLetMeExpire.Resources.Styles.mystyles.css";

      using var stream = assembly.GetManifestResourceStream(resourceName);
      if (stream != null)
      {
        using var reader = new StreamReader(stream);
        Resources.Add(StyleSheet.FromReader(reader));
      }
      SetLanguage(settingsService.GetLanguage());
      SetTheme(settingsService.GetTheme());
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
      var window = new Window(new AppShell());
      return window;
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
