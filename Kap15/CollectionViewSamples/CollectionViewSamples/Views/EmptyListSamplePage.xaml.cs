using CollectionViewSamples.ViewModels;

namespace CollectionViewSamples.Views;

public partial class EmptyListSamplePage : ContentPage
{

  private EmptyListSampleViewModel _viewModel;

  public EmptyListSamplePage(EmptyListSampleViewModel viewModel)
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