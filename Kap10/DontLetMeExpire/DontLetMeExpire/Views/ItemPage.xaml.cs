using DontLetMeExpire.ViewModels;

namespace DontLetMeExpire.Views;

public partial class ItemPage : ContentPage
{
  private ItemViewModel _viewModel;

  public ItemPage()
  {
    InitializeComponent();
    BindingContext = _viewModel = new ItemViewModel();
  }

  override protected async void OnNavigatedTo(NavigatedToEventArgs args)
  {
    await _viewModel.InitializeAsync();
    base.OnNavigatedTo(args);
  }
}