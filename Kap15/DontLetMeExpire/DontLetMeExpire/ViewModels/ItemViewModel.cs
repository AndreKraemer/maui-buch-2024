using DontLetMeExpire.Models;
using DontLetMeExpire.Services;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DontLetMeExpire.ViewModels;

public class ItemViewModel : ValidationViewModelBase
{
  private string _name;
  private DateTime _expirationDate;
  private StorageLocation _location;
  private decimal _amount;
  private string _id;
  private string _image;
  private string _title;
  private readonly IItemService _itemService;
  private readonly INavigationService _navigationService;
  private readonly IStorageLocationService _storageLocationService;


  public ItemViewModel(INavigationService navigationService,
                        IStorageLocationService storageLocationService,
                        IItemService itemService)
  {
    SaveCommand = new Command(async () => await SaveAsync());
    _navigationService = navigationService;
    _storageLocationService = storageLocationService;
    _itemService = itemService;
  }

  public ObservableCollection<StorageLocation> StorageLocations { get; set; } = [];

  public Command SaveCommand { get; private set; }

  public string Title
  {
    get => _title;
    set => SetProperty(ref _title, value);
  }

  public string Id
  {
    get => _id;
    set => SetProperty(ref _id, value);
  }

  /// <summary>
  /// Der Name des Elements.
  /// </summary>
  [Required]
  [Length(1, 50)]
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
  [Range(1, 10000)]
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
  public async Task InitializeAsync(string? itemId = null)
  {
    // Speicherorte laden
    var locations = await _storageLocationService.GetAsync();

    // Die Liste der Speicherorte aktualisieren
    StorageLocations.Clear();
    foreach (var location in locations)
    {
      StorageLocations.Add(location);
    }
    if (!string.IsNullOrEmpty(itemId))
    {
      var item = await _itemService.GetByIdAsync(itemId);
      if (item != null)
      {
        Id = item.Id;
        Name = item.Name;
        ExpirationDate = item.ExpirationDate;
        SelectedLocation = StorageLocations.FirstOrDefault(x => x.Id == item.StorageLocationId);
        Image = item.Image;
        Amount = item.Amount;
        Title = item.Name;
      }

    }
    else
    {
      Id = string.Empty;
      Name = string.Empty;
      ExpirationDate = DateTime.Today;
      SelectedLocation = StorageLocations.First();
      Image = string.Empty;
      Amount = 1;
      Title = "Neuer Eintrag";
    }
  }

  /// <summary>
  /// Speichert das Element asynchron.
  /// </summary>
  private async Task SaveAsync()
  {
    if (!Validate())
    {
      return;
    }
    // Neues Element mit den
    // Daten des ViewModels erstellen
    var item = new Item
    {
      Id = Id,
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

    await _navigationService.GoToAsync("..");
  }

  private bool CanSave()
  {
    return !string.IsNullOrEmpty(Name)
      && Amount > 0;
  }
}
