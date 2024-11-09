using DontLetMeExpire.ViewModels;

namespace DontLetMeExpire.Views;

public partial class SettingsPage : ContentPage
{
  private readonly SettingsViewModel _viewModel;

  public SettingsPage(SettingsViewModel viewModel)
  {
    BindingContext = _viewModel = viewModel;
    InitializeComponent();
  }

  protected override async void OnNavigatedTo(NavigatedToEventArgs args)
  {
    await _viewModel.InitializeAsync();
    base.OnNavigatedTo(args);
  }
}