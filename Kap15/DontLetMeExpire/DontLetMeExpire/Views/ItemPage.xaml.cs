using DontLetMeExpire.ViewModels;

namespace DontLetMeExpire.Views;
[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class ItemPage : ContentPage
{
  private readonly ItemViewModel _viewModel;

  public ItemPage(ItemViewModel viewModel)
  {
    InitializeComponent();
    BindingContext = _viewModel = viewModel;
  }

  public string? ItemId { get; set; }

  override protected async void OnNavigatedTo(NavigatedToEventArgs args)
  {
    await _viewModel.InitializeAsync(ItemId);
    base.OnNavigatedTo(args);
  }
}