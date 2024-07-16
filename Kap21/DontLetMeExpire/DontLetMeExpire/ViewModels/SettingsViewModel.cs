using System.Collections.ObjectModel;
using DontLetMeExpire.Resources.Strings;
using DontLetMeExpire.Services;

namespace DontLetMeExpire.ViewModels;

public class SettingsViewModel : ViewModelBase
{
  private readonly IAlertService _alertService;
  private readonly IItemService _itemService;
  private readonly IStorageLocationService _storageLocationService;
  private readonly ISettingsService _settingsService;
  private KeyValuePair<string, string> _selectedLanguage;
  private KeyValuePair<string, string> _selectedTheme;

  public SettingsViewModel(ISettingsService settingsService, IAlertService alertService,
      IItemService itemService, IStorageLocationService storageLocationService)
  {
    _settingsService = settingsService;
    _alertService = alertService;
    _itemService = itemService;
    _storageLocationService = storageLocationService;
    SaveCommand = new Command(async () => await Save(), CanSave);
    ResetAppCommand = new Command(async () => await ResetApp());
  }

  public Command ResetAppCommand { get; set; }

  public Command SaveCommand { get; }
  public ObservableCollection<KeyValuePair<string, string>> Languages { get; } = [];

  public ObservableCollection<KeyValuePair<string, string>> Themes { get; } = [];

  // Ausgew�hlte Sprache
  public KeyValuePair<string, string> SelectedLanguage
  {
    get => _selectedLanguage;
    set
    {
      SetProperty(ref _selectedLanguage, value);
      SaveCommand.ChangeCanExecute();
    }
  }

  // Ausgew�hltes Theme
  public KeyValuePair<string, string> SelectedTheme
  {
    get => _selectedTheme;
    set
    {
      SetProperty(ref _selectedTheme, value);
      SaveCommand.ChangeCanExecute();
    }
  }


  public Task InitializeAsync()
  {
    var languages = _settingsService.GetLanguages();
    Languages.Clear();
    SelectedLanguage = new KeyValuePair<string, string>();
    foreach (var language in languages) Languages.Add(language);

    SelectedLanguage = Languages.FirstOrDefault(x => x.Key == _settingsService.GetLanguage());

    var themes = _settingsService.GetThemes();
    Themes.Clear();
    SelectedTheme = new KeyValuePair<string, string>();
    foreach (var theme in themes) Themes.Add(theme);

    SelectedTheme = Themes.FirstOrDefault(x => x.Key == _settingsService.GetTheme());

    return Task.CompletedTask;
  }

  // Speichern der Einstellungen
  private async Task Save()
  {
    // pr�fen, ob die Sprache ge�ndert wurde
    var languageChanged = _settingsService.GetLanguage() != SelectedLanguage.Key;

    // Sprache und Theme speichern
    _settingsService.SetLanguage(SelectedLanguage.Key);
    _settingsService.SetTheme(SelectedTheme.Key);

    // Hinweis auf Neustart anzeigen, falls die Sprache ge�ndert wurde
    if (languageChanged)
    {
      await _alertService.DisplayAlert(AppResources.RestartApp, AppResources.RestartAppLanguageChange, AppResources.Ok);
    }

    SaveCommand.ChangeCanExecute();
  }

  private bool CanSave()
  {
    return _settingsService.GetLanguage() != SelectedLanguage.Key ||
           _settingsService.GetTheme() != SelectedTheme.Key;
  }

  // Zur�cksetzen der App
  private async Task ResetApp()
  {
    // Warnung darstellen, dass beim Zur�cksetzen alle Daten verloren gehen.
    if (await _alertService.DisplayAlert(AppResources.Warning, AppResources.ResetAppWarnining, AppResources.Ok,
            AppResources.Cancel))
    {
      // Alle Vorratseintr�ge l�schen
      await _itemService.DeleteAllAsync();

      // Alle Lagerorte l�schen
      await _storageLocationService.DeleteAllAsync();

      // Ausgew�hlte Sprache und ausgew�hltes Thema auf Standardeintrag zur�cksetzen
      SelectedLanguage = Languages.First();
      SelectedTheme = Themes.First();
      await Save();
    }
  }
}