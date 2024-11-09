namespace DontLetMeExpire.Services;

public interface ISettingsService
{
  IEnumerable<KeyValuePair<string, string>> GetLanguages();

  IEnumerable<KeyValuePair<string, string>> GetThemes();

  string GetLanguage();

  string GetTheme();

  void SetLanguage(string language);

  void SetTheme(string theme);
}