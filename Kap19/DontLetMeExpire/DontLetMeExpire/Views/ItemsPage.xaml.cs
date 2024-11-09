using DontLetMeExpire.ViewModels;

namespace DontLetMeExpire.Views;

[QueryProperty(nameof(DisplayMode), nameof(DisplayMode))]
[QueryProperty(nameof(LocationId), nameof(LocationId))]
public partial class ItemsPage : ContentPage
{
  private readonly ItemsViewModel _viewModel;

  public ItemsPage(ItemsViewModel viewModel)
  {
    InitializeComponent();
    BindingContext = _viewModel = viewModel;
  }

  public string DisplayMode { get; set; }
  public string LocationId { get; set; }

  protected override async void OnNavigatedTo(NavigatedToEventArgs args)
  {
    base.OnNavigatedTo(args);
    await _viewModel.InitializeAsync(DisplayMode, LocationId);
  }
}