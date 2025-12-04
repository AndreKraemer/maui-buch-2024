using Microsoft.Maui.Controls.StyleSheets;
using System.Reflection;

namespace DontLetMeExpire
{
  public partial class App : Application
  {
    public App()
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
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
      var window = new Window(new AppShell());
      return window;
    }
  }
}
