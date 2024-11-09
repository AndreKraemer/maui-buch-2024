using DontLetMeExpire.Models;
using DontLetMeExpire.Resources.Strings;
using DontLetMeExpire.Services;
using DontLetMeExpire.Views;
using System.Collections.ObjectModel;

namespace DontLetMeExpire.ViewModels;
public class ItemsViewModel : ViewModelBase
{
  private string _title;
  private readonly IItemService _itemService;
  private readonly INavigationService _navigationService;
  private readonly IStorageLocationService _storageLocationService;

  public ItemsViewModel(INavigationService navigationService, 
                        IItemService itemService, 
                        IStorageLocationService storageLocationService)
  {
    _navigationService = navigationService;
    _itemService = itemService;
    _storageLocationService = storageLocationService;
    NavigateToDetailsCommand = new Command<Item>(async (item) => await NavigateToDetails(item));
  }

  public Command<Item> NavigateToDetailsCommand { get; set; }
  public ObservableCollection<Item> Items { get; } = [];

  public string Title
  {
    get => _title;
    set => SetProperty(ref _title, value);
  }

  public async Task InitializeAsync(string displayMode, string locationId)
  {
    IEnumerable<Item> items;

    switch (displayMode)
    {
      case "Stock":
        items = await _itemService.GetAsync();
        Title = AppResources.MyStock;
        break;
      case "ExpiringSoon":
        items = await _itemService.GetExpiresSoonAsync();
        Title = AppResources.ExpiringSoon;
        break;
      case "ExpiresToday":
        items = await _itemService.GetExpiresTodayAsync();
        Title = AppResources.ExpiresToday;
        break;
      case "Expired":
        items = await _itemService.GetExpiredAsync();
        Title = AppResources.Expired;
        break;
      case "Location":
        items = await _itemService.GetByLocationAsync(locationId);
        var location = await _storageLocationService.GetByIdAsync(locationId);
        Title = location?.Name ?? AppResources.StorageLocation;
        break;
      default:
        items = await _itemService.GetAsync();
        break;
    }

    Items.Clear();
    foreach (var item in items)
    {
      Items.Add(item);
    }
  }

  private async Task NavigateToDetails(Item? item)
  {
    var navigationParams = new Dictionary<string, object>
      {
          {"ItemId", item?.Id}
      };
    await _navigationService.GoToAsync(nameof(ItemPage), navigationParams);
  }
}
