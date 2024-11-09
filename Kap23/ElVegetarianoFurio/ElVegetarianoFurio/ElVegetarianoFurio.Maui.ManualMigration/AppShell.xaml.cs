using ElVegetarianoFurio.Maui.ManualMigration.Menu;

namespace ElVegetarianoFurio.Maui.ManualMigration
{
  public partial class AppShell : Shell
  {
    public AppShell()
    {
      InitializeComponent();
      Routing.RegisterRoute(nameof(CategoryPage), typeof(CategoryPage));
      Routing.RegisterRoute(nameof(DishPage), typeof(DishPage));
    }
  }
}
