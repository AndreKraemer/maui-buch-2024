namespace ElVegetarianoFurio.Maui.ManualMigration.Menu;

[QueryProperty(nameof(CategoryId), nameof(CategoryId))]
public partial class CategoryPage : ContentPage
{
  private readonly CategoryViewModel _viewModel;

  public CategoryPage(CategoryViewModel viewModel)
  {
    InitializeComponent();
    _viewModel = viewModel;
    BindingContext = _viewModel;
  }

  public CategoryPage(string categoryId)
  {
    InitializeComponent();
    CategoryId = categoryId;
    var viewModel = ((App)Application.Current).ServiceProvider.GetService<CategoryViewModel>();
    _viewModel = viewModel;
    BindingContext = _viewModel;
  }

  public string CategoryId
  {
    get;
    set;
  }

  protected override async void OnAppearing()
  {
    if (int.TryParse(CategoryId, out var category))
    {
      _viewModel.CategoryId = category;
    }

    await _viewModel.Initialize();
    base.OnAppearing();
  }
}