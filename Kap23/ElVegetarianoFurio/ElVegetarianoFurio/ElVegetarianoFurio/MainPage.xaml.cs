using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace ElVegetarianoFurio
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = Startup.ServiceProvider.GetService<MainViewModel>();
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            await _viewModel.Initialize();
            base.OnAppearing();
        }
    }
}
