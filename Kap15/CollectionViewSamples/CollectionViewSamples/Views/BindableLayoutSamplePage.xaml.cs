using CollectionViewSamples.ViewModels;

namespace CollectionViewSamples.Views;

public partial class BindableLayoutSamplePage : ContentPage
{
  private BindableLayoutSampleViewModel _viewModel;

  public BindableLayoutSamplePage(BindableLayoutSampleViewModel viewModel)
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