

namespace ElVegetarianoFurio.Maui.ManualMigration;

public partial class MainPage : ContentPage
{
  private readonly MainViewModel _viewModel;

  public MainPage(MainViewModel viewModel)
  {
    InitializeComponent();
    _viewModel = viewModel;
    BindingContext = _viewModel;
  }

  protected override async void OnAppearing()
  {
    await _viewModel.Initialize();
    base.OnAppearing();
  }
}