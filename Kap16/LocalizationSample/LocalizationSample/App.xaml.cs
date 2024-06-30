using System.Globalization;

namespace LocalizationSample;

public partial class App : Application
{
  public App()
  {
    InitializeComponent();

    MainPage = new AppShell();
    // Aktuelle UI-Kultur auf Französisch setzen
    // CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
  }
}
