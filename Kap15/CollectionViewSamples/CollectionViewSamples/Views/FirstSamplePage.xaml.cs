using CollectionViewSamples.ViewModels;
using Microsoft.Maui.Controls;


namespace CollectionViewSamples.Views
{

    public partial class FirstSamplePage : ContentPage
    {
        private FirstSampleViewModel _viewModel;

        public FirstSamplePage(FirstSampleViewModel viewModel)
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