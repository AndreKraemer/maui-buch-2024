using CollectionViewSamples.ViewModels;
using Microsoft.Maui.Controls;


namespace CollectionViewSamples.Views
{

    public partial class SelectSamplePage : ContentPage
    {
        private SelectSampleViewModel _viewModel;

        public SelectSamplePage(SelectSampleViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            await _viewModel.Initialize();
            base.OnNavigatedTo(args);
        }
    }
}