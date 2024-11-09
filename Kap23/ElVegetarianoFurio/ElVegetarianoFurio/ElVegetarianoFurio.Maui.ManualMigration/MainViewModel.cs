using ElVegetarianoFurio.Maui.ManualMigration.Core;
using ElVegetarianoFurio.Maui.ManualMigration.Data;
using ElVegetarianoFurio.Maui.ManualMigration.Menu;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ElVegetarianoFurio.Maui.ManualMigration
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;

        public ICommand LoadDataCommand { get; }
        public ICommand NavigateToCategoryCommand { get;  }

        public ObservableCollection<Category> Categories { get; } =
            new ObservableCollection<Category>();

        public MainViewModel(IDataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            LoadDataCommand = new Command(async () => await LoadData());
            NavigateToCategoryCommand = new Command(NavigateToCategory);
            Title = "Start";
        }

        private async Task LoadData()
        {

            try
            {
                IsBusy = true;
                Categories.Clear();
                var categories = await _dataService.GetCategoriesAsync();

                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void NavigateToCategory(object arg)
        {
            if (arg is Category category)
            {
                await _navigationService.GoToAsync($"{nameof(CategoryPage)}?CategoryId={category.Id}");
            }
        }

        public override async Task Initialize()
        {
            await LoadData();
            await base.Initialize();
        }
    }
}