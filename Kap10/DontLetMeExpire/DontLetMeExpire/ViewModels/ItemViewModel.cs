using DontLetMeExpire.Models;
using DontLetMeExpire.Services;
using System.Collections.ObjectModel;

namespace DontLetMeExpire.ViewModels;

public class ItemViewModel : ViewModelBase
{
  private string _name;
  private DateTime _expirationDate;
  private StorageLocation _location;
  private decimal _amount;
  private string _image;

  private DummyStorageLocationService _storageLocationService = new();
  private DummyItemService _itemService = new();


  public ItemViewModel()
  {
    SaveCommand = new Command(async () => await Save());
  }

  public ObservableCollection<StorageLocation> StorageLocations { get; set; } = [];

  public Command SaveCommand { get; private set; }


  public string Name
  {
    get => _name;
    set => SetProperty(ref _name, value);
  }

  public DateTime ExpirationDate
  {
    get => _expirationDate;
    set => SetProperty(ref _expirationDate, value);
  }

  public StorageLocation SelectedLocation
  {
    get => _location;
    set => SetProperty(ref _location, value);
  }

  public decimal Amount
  {
    get => _amount;
    set => SetProperty(ref _amount, value);
  }

  public string Image
  {
    get => _image;
    set => SetProperty(ref _image, value);
  }

  public async Task InitializeAsync()
  {
    var locations = await _storageLocationService.GetAsync();
    SelectedLocation = null;
    StorageLocations.Clear();
    foreach (var location in locations) StorageLocations.Add(location);

  }

  private async Task Save()
  {
    var item = new Item
    {
      Name = Name,
      ExpirationDate = ExpirationDate,
      StorageLocationId = SelectedLocation.Id,
      Amount = Amount,
      Image = Image
    };
    await _itemService.SaveAsync(item);
    Name = string.Empty;
    ExpirationDate = DateTime.Today;
    Amount = 0;
    SelectedLocation = StorageLocations.First();
  }

}
