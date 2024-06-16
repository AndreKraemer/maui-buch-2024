using DontLetMeExpire.Services;
using DontLetMeExpire.Views;
using System.Windows.Input;

namespace DontLetMeExpire.ViewModels;

public class MainViewModel : ViewModelBase
{
  private readonly INavigationService _navigationService;
  private readonly IItemService _itemService;
  private int _stockCount;
  private int _expiringSoonCount;
  private int _expiresTodayCount;
  private int _expiredCount;

  public MainViewModel(INavigationService navigationService, IItemService itemService)
  {
    _itemService = itemService;
    _navigationService = navigationService;
    NavigateToAddItemCommand = new Command(async () => await NavigateToAddItem());
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

  /// <summary>
  /// Initialisiert das ViewModel asynchron.
  /// </summary>
  public async Task InitializeAsync()
  {
    StockCount = (await _itemService.GetAsync()).Count();
    ExpiringSoonCount = (await _itemService.GetExpiresSoonAsync()).Count();
    ExpiresTodayCount = (await _itemService.GetExpiresTodayAsync()).Count();
    ExpiredCount = (await _itemService.GetExpiredAsync()).Count();
  }

  private async Task NavigateToAddItem()
  {
    await _navigationService.GoToAsync(nameof(ItemPage));
  }
}
