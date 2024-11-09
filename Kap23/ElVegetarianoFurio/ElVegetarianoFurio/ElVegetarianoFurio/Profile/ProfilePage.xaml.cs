using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElVegetarianoFurio.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private readonly ProfileViewModel _viewModel;

        public ProfilePage()
        {
            _viewModel = Startup.ServiceProvider.GetService<ProfileViewModel>();
            InitializeComponent();
            BindingContext = _viewModel;
        }
 
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.Initialize();
        }
    }
}