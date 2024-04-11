using DontLetMeExpire.ViewModels;

namespace DontLetMeExpire;

public partial class MainPage : ContentPage
{
  private readonly MainViewModel _viewModel;

  public MainPage(MainViewModel viewModel)
  {
    InitializeComponent();
    BindingContext = _viewModel = viewModel;
  }

  protected override async void OnNavigatedTo(NavigatedToEventArgs args)
  {
    await _viewModel.InitializeAsync();
    base.OnNavigatedTo(args);
  }
}
