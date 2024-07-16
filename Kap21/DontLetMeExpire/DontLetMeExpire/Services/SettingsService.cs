using DontLetMeExpire.Resources.Strings;

namespace DontLetMeExpire.Services;

public class SettingsService : ISettingsService
{
  private readonly IPreferences _preferences;

  public SettingsService(IPreferences preferences)
  {
    _preferences = preferences;
  }

  // Liefert eine lokalisierte Liste der verf�gbaren Sprachen
  public IEnumerable<KeyValuePair<string, string>> GetLanguages()
  {
    // lokalisierte Namen der Sprachen zu einer Liste hinzuf�gen.
    var languages = new List<KeyValuePair<string, string>>(4)
        {
            new("en", AppResources.English),
            new("de", AppResources.German)
        };

    // lokalisierte Liste alphabetisch sortieren
    languages.Sort((a, b) => string.Compare(a.Value, b.Value, StringComparison.Ordinal));

    // Standard-Eintrag an der ersten Position hinzuf�gen.
    languages.Insert(0, new KeyValuePair<string, string>(string.Empty, AppResources.Default));

    return languages;
  }

  // Liefert eine lokalisierte Liste der verf�gbaren Themes
  public IEnumerable<KeyValuePair<string, string>> GetThemes()
  {

    // lokalisierte Theme-Namen zu einer Liste hinzuf�gen.
    var themes = new List<KeyValuePair<string, string>>(4)
        {
            new(nameof(AppTheme.Dark), AppResources.Dark),
            new(nameof(AppTheme.Light), AppResources.Light)
        };

    // lokalisierte Liste alphabetisch sortieren
    themes.Sort((a, b) => string.Compare(a.Value, b.Value, StringComparison.Ordinal));
    
    // Standard-Eintrag an der ersten Position hinzuf�gen.
    themes.Insert(0, new KeyValuePair<string, string>(string.Empty, AppResources.Default));

    return themes;
  }

  // Ausgew�hlte Sprache aus den Benutzereinstellungen abrufen
  public string GetLanguage()
  {
    return _preferences.Get(nameof(AppResources.Language), string.Empty);
  }

  // Ausgew�hltes Theme aus den Benutzereinstellungen abrufen
  public string GetTheme()
  {
    return _preferences.Get(nameof(AppResources.Theme), string.Empty);
  }

  // Ausgew�hlte Sprache in den Benutzereinstellungen speichern
  public void SetLanguage(string language)
  {
    _preferences.Set(nameof(AppResources.Language), language);
  }

  // Ausgew�hltes Theme in den Benutzereinstellungen speichern
  public void SetTheme(string theme)
  {
    _preferences.Set(nameof(AppResources.Theme), theme);

    // Theme nach dem Speichern direkt umstellen
    Application.Current!.UserAppTheme = theme switch
    {
      nameof(AppTheme.Dark) => AppTheme.Dark,
      nameof(AppTheme.Light) => AppTheme.Light,
      _ => Application.Current.UserAppTheme = AppTheme.Unspecified
    };
  }
}