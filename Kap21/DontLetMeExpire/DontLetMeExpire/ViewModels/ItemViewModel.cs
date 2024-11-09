using DontLetMeExpire.Models;
using DontLetMeExpire.OpenFoodFacts;
using DontLetMeExpire.Resources.Strings;
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
  private string _searchText;
  private bool _showBarcodeScanner;
  private string _title;
  private readonly IItemService _itemService;
  private readonly INavigationService _navigationService;
  private readonly IStorageLocationService _storageLocationService;
  private readonly IOpenFoodFactsApiClient _openFoodFactsApiClient;

  public ItemViewModel(INavigationService navigationService,
                        IStorageLocationService storageLocationService,
                        IItemService itemService,
                        IOpenFoodFactsApiClient openFoodFactsApiClient)
  {
    SaveCommand = new Command(async () => await SaveAsync());
    ScanBarcodeCommand = new Command(ScanBarcode);
    SearchBarcodeCommand = new Command(async () => await SearchBarcode());
    TakePhotoCommand = new Command(async () => await TakePhoto());
    DeletePhotoCommand = new Command(DeletePhoto);
    _navigationService = navigationService;
    _storageLocationService = storageLocationService;
    _openFoodFactsApiClient = openFoodFactsApiClient;
    _itemService = itemService;
  }

  public ObservableCollection<StorageLocation> StorageLocations { get; set; } = [];

  public Command SaveCommand { get; }

  public Command ScanBarcodeCommand { get; }

  public Command SearchBarcodeCommand { get; }

  public Command TakePhotoCommand { get; }

  public Command DeletePhotoCommand { get; }
  public string Title
  {
    get => _title;
    set => SetProperty(ref _title, value);
  }

  public string SearchText
  {
    get => _searchText;
    set => SetProperty(ref _searchText, value);
  }


  public string Id
  {
    get => _id;
    set => SetProperty(ref _id, value);
  }

  /// <summary>
  /// Der Name des Elements.
  /// </summary>
  [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "RequiredErrorMessage")]
  [Length(1, 50, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "LengthErrorMessage")]
  [Display(Name = "Name", ResourceType = typeof(AppResources))]

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
  [Range(1, 10000, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "RangeErrorMessage")]
  [Display(Name = "Amount", ResourceType = typeof(AppResources))]
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

  public bool ShowBarcodeScanner
  {
    get => _showBarcodeScanner;
    set => SetProperty(ref _showBarcodeScanner, value);
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
      Title = AppResources.NewEntry;
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

  private void DeletePhoto()
  {
    Image = string.Empty;
  }

  private async Task TakePhoto()
  {
    if (MediaPicker.Default.IsCaptureSupported)
    {
      var photo = await MediaPicker.Default.CapturePhotoAsync();

      if (photo != null)
      {
        // save the file into local storage
        var localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

        await using var sourceStream = await photo.OpenReadAsync();
        await using var localFileStream = File.OpenWrite(localFilePath);

        await sourceStream.CopyToAsync(localFileStream);
        Image = localFilePath;
      }
    }
  }

  private void ScanBarcode()
  {
    ShowBarcodeScanner = !ShowBarcodeScanner;
  }

  private async Task SearchBarcode()
  {
    var response = await _openFoodFactsApiClient.GetProductByCodeAsync(SearchText);
    if (response is { Status: 1, Product: not null })
    {
      Name = response.Product.ProductName!;

      // Prüfen ob ein Bild vorhanden ist
      if (!string.IsNullOrEmpty(response.Product.ImageUrl))
      {
        // Dateinamen und Pfad generieren
        var fileName = $"{response.Code}.jpg";
        var localFilePath = Path.Combine(FileSystem.CacheDirectory, fileName);

        // Prüfen ob das Bild bereits heruntergeladen wurde
        if (!File.Exists(localFilePath))
        {
          // Falls das Bild lokale noch nicht existiert,
          // wird es heruntergeladen und gespeichert
          var imageBytes = await _openFoodFactsApiClient.DownloadImage(response.Product.ImageUrl);
          await File.WriteAllBytesAsync(localFilePath, imageBytes);
        }

        // Bildpfad zur Anzeige in der View setzen
        Image = localFilePath;
      }

    }
    else
    {
      // Zu einem späteren Zeitpunkt Fehlermeldung anzeigen
    }
  }
}

