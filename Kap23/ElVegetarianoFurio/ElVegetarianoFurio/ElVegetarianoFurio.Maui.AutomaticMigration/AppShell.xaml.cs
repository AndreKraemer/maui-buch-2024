using ElVegetarianoFurio.Maui.AutomaticMigration.Menu;

namespace ElVegetarianoFurio.Maui.AutomaticMigration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CategoryPage),typeof(CategoryPage));
            Routing.RegisterRoute(nameof(DishPage),typeof(DishPage));
        }
    }
}