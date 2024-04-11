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

  private readonly IStorageLocationService _storageLocationService;
  private readonly IItemService _itemService;

  public ItemViewModel(IStorageLocationService storageLocationService, 
                       IItemService itemService)
  {
    SaveCommand = new Command(async () => await SaveAsync(), CanSave);
    _storageLocationService = storageLocationService;
    _itemService = itemService;
  }

  public ObservableCollection<StorageLocation> StorageLocations { get; set; } = [];

  /// <summary>
  /// 
  /// </summary>
  public Command SaveCommand { get; private set; }

  /// <summary>
  /// Der Name des Elements.
  /// </summary>
  public string Name
  {
    get => _name;
    set => SetProperty(ref _name, value, SaveCommand.ChangeCanExecute);
  }

  /// <summary>
  /// Das Ablaufdatum des Elements.
  /// </summary>
  public DateTime ExpirationDate
  {
    get => _expirationDate;
    set => SetProperty(ref _expirationDate, value);
  }

  /// <summary>
  /// Der Speicherort des Elements.
  /// </summary>
  public StorageLocation SelectedLocation
  {
    get => _location;
    set => SetProperty(ref _location, value);
  }

  /// <summary>
  /// Die Menge des Elements.
  /// </summary>
  public decimal Amount
  {
    get => _amount;
    set => SetProperty(ref _amount, value, SaveCommand.ChangeCanExecute);
  }

  /// <summary>
  /// Das Bild des Elements.
  /// </summary>
  public string Image
  {
    get => _image;
    set => SetProperty(ref _image, value);
  }

  /// <summary>
  /// Initialisiert das ViewModel asynchron.
  /// </summary>
  public async Task InitializeAsync()
  {
    // Speicherorte laden
    var locations = await _storageLocationService.GetAsync();
    

    // Die Liste der Speicherorte aktualisieren
    StorageLocations.Clear();
    foreach (var location in locations)
    {
      StorageLocations.Add(location);
    }
    SelectedLocation = StorageLocations.FirstOrDefault();
  }

  /// <summary>
  /// Speichert das Element asynchron.
  /// </summary>
  private async Task SaveAsync()
  {
    // Neues Element mit den
    // Daten des ViewModels erstellen
    var item = new Item
    {
      Name = Name,
      ExpirationDate = ExpirationDate,
      StorageLocationId = SelectedLocation.Id,
      Amount = Amount,
      Image = Image
    };

    // Element speichern
    await _itemService.SaveAsync(item);

    // Daten für die Anzeige zurücksetzen
    Name = string.Empty;
    ExpirationDate = DateTime.Today;
    Amount = 0;
    SelectedLocation = StorageLocations.First();
  }

  private bool CanSave()
  {
    return !string.IsNullOrEmpty(Name) 
      && Amount > 0;
  }


}
