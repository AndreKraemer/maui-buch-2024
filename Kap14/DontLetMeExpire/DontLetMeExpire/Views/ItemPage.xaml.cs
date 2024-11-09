using DontLetMeExpire.ViewModels;

namespace DontLetMeExpire.Views;

public partial class ItemPage : ContentPage
{
  private ItemViewModel _viewModel;

  public ItemPage(ItemViewModel viewModel)
  {
    InitializeComponent();
    BindingContext = _viewModel = viewModel;
  }

  override protected async void OnNavigatedTo(NavigatedToEventArgs args)
  {
    await _viewModel.InitializeAsync();
    base.OnNavigatedTo(args);
  }
}