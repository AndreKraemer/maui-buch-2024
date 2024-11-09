using System;
using ElVegetarianoFurio.Profile;
using MonkeyCache.FileStore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElVegetarianoFurio
{
    public partial class App : Application
    {
        public App()
        {
            var shell = Startup.ServiceProvider.GetService<AppShell>();
            InitializeComponent();
            Barrel.ApplicationId = "ElVegetarianoFurio";
            MainPage = shell;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
