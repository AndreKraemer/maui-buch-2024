namespace ElVegetarianoFurio.Maui.ManualMigration.Menu
{
    [QueryProperty(nameof(DishId), nameof(DishId))]
    public partial class DishPage : ContentPage
    {
        private readonly DishViewModel _viewModel;

        public DishPage(DishViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
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