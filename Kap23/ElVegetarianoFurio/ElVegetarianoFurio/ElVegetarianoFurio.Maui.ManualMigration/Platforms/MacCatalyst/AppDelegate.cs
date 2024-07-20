using Foundation;

namespace ElVegetarianoFurio.Maui.ManualMigration
{
  [Register("AppDelegate")]
  public class AppDelegate : MauiUIApplicationDelegate
  {
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
  }
}
