using System.Globalization;

namespace LocalizationSample;

public partial class App : Application
{
  public App()
  {
    InitializeComponent();
    // Aktuelle UI-Kultur auf Französisch setzen
    // CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
  }

  protected override Window CreateWindow(IActivationState? activationState)
  {
    var window = new Window(new AppShell());
#if WINDOWS
    window.Width = 500;
#endif
    return window;
  }
}
