using DontLetMeExpire.Models;
using DontLetMeExpire.Services;
using DontLetMeExpire.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DontLetMeExpire.ViewModels;

public class MainViewModel : ViewModelBase
{
  private readonly INavigationService _navigationService;
  private readonly IItemService _itemService;
  private readonly IStorageLocationService _storageLocationService;
  private int _stockCount;
  private int _expiringSoonCount;
  private int _expiresTodayCount;
  private int _expiredCount;

  public MainViewModel(INavigationService navigationService, IItemService itemService, IStorageLocationService storageLocationService)
  {
    _itemService = itemService;
    _storageLocationService = storageLocationService;
    _navigationService = navigationService;
    CreateSuggestedLocationsCommand = new Command(async () => await CreateSuggestedLocations());
    CreateSuggestedLocationsAndStockCommand = new Command(async () => await CreateSuggestedLocationsAndStock());
    NavigateToAddItemCommand = new Command(async () => await NavigateToAddItem(), CanAddNewItem);
    NavigateToStockCommand = new Command(async () => await NavigateToStock());
    NavigateToExpiringSoonCommand = new Command(async () => await NavigateToExpiringSoon());
    NavigateToExpiresTodayCommand = new Command(async () => await NavigateToExpiresToday());
    NavigateToExpiredCommand = new Command(async () => await NavigateToExpired());
    NavigateToLocationCommand = new Command<string>(async (locationId) => await NavigateToLocation(locationId));
  }



  public ICommand NavigateToAddItemCommand { get; }

  /// <summary>
  /// Die Anzahl aller gelagerten Artikel.
  /// </summary>
  public int StockCount
  {
    get => _stockCount;
    set => SetProperty(ref _stockCount, value);
  }

  /// <summary>
  /// Die Anzahl der Artikel, die bald ablaufen.
  /// </summary>
  public int ExpiringSoonCount
  {
    get => _expiringSoonCount;
    set => SetProperty(ref _expiringSoonCount, value);
  }

  /// <summary>
  /// Die Anzahl der Artikel, die heute ablaufen.
  /// </summary>
  public int ExpiresTodayCount
  {
    get => _expiresTodayCount;
    set => SetProperty(ref _expiresTodayCount, value);
  }

  /// <summary>
  /// Die Anzahl der abgelaufenen Artikel.
  /// </summary>
  public int ExpiredCount
  {
    get => _expiredCount;
    set => SetProperty(ref _expiredCount, value);
  }


  public ICommand NavigateToStockCommand { get; }
  public ICommand NavigateToExpiringSoonCommand { get; }
  public ICommand NavigateToExpiresTodayCommand { get; }
  public ICommand NavigateToExpiredCommand { get; }

  public ICommand CreateSuggestedLocationsCommand { get; }
  public ICommand CreateSuggestedLocationsAndStockCommand { get; }
  public Command<string> NavigateToLocationCommand { get; }

  public ObservableCollection<StorageLocationWithItemCount> StorageLocations { get; } = [];

  /// <summary>
  /// Initialisiert das ViewModel asynchron.
  /// </summary>
  public async Task InitializeAsync()
  {
    var locations = await _storageLocationService.GetWithItemCountAsync();

    StorageLocations.Clear();
    foreach (var location in locations)
    {
      StorageLocations.Add(location);
    }

    StockCount = (await _itemService.GetAsync()).Count();
    ExpiringSoonCount = (await _itemService.GetExpiresSoonAsync()).Count();
    ExpiresTodayCount = (await _itemService.GetExpiresTodayAsync()).Count();
    ExpiredCount = (await _itemService.GetExpiredAsync()).Count();
    ((Command)NavigateToAddItemCommand).ChangeCanExecute();
  }

  private async Task NavigateToStock()
  {
    await NavigateToItemsPage("Stock");
  }

  private async Task NavigateToExpiringSoon()
  {
    await NavigateToItemsPage("ExpiringSoon");
  }

  private async Task NavigateToExpiresToday()
  {
    await NavigateToItemsPage("ExpiresToday");
  }

  private async Task NavigateToExpired()
  {
    await NavigateToItemsPage("Expired");
  }

  private async Task NavigateToLocation(string locationId)
  {
    await NavigateToItemsPage("Location", locationId);
  }

  private async Task NavigateToItemsPage(string displayMode, string? locationId = null)
  {
    var navigationParams = new Dictionary<string, object>
    {
      { "DisplayMode", displayMode },
      { "LocationId", locationId }
    };
    await _navigationService.GoToAsync(nameof(ItemsPage), navigationParams);
  }

  private async Task NavigateToAddItem()
  {
    await _navigationService.GoToAsync(nameof(ItemPage));
  }

  private bool CanAddNewItem()
  {
    return StorageLocations.Any();
  }

  private async Task CreateSuggestedLocations()
  {
    foreach (var location in DummyData.Locations)
    {
      await _storageLocationService.SaveAsync(location);
    }
    await InitializeAsync();
  }

  private async Task CreateSuggestedLocationsAndStock()
  {

    foreach (var location in DummyData.Locations)
    {
      await _storageLocationService.SaveAsync(location);
    }

    foreach (var item in DummyData.Items)
    {
      await _itemService.SaveAsync(item);
    }

    await InitializeAsync();
  }
}
