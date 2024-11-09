using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElVegetarianoFurio.Menu;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElVegetarianoFurio
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