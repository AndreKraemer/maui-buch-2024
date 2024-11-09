using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace ElVegetarianoFurio.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(DishId), nameof(DishId))]
    public partial class DishPage : ContentPage
    {
        private readonly DishViewModel _viewModel;

        public DishPage()
        {
            InitializeComponent();
            _viewModel = Startup.ServiceProvider.GetService<DishViewModel>();
            BindingContext = _viewModel;
        }

        public string DishId
        {
            get;
            set;
        }

        protected override async void OnAppearing()
        {
            if (int.TryParse(DishId, out var dish))
            {
                _viewModel.DishId = dish;
            }

            await _viewModel.Initialize();
            base.OnAppearing();
        }
    }
}