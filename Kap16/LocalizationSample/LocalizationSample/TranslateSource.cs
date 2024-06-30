using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace LocalizationSample;

public class TranslateSource : INotifyPropertyChanged
{
  // Je Resource-Datei wird eine Instanz von TranslateSource erstellt
  // und in diesem Dictionary gespeichert
  private static readonly ConcurrentDictionary<string, TranslateSource> _instances =
      new();

  // Gibt die Instanz von TranslateSource für die angegebene Ressource zurück
  public static TranslateSource For(string resource)
  {
    return _instances.GetOrAdd(resource, new TranslateSource(resource));
  }

  // ResourceManager für eine spezifische Resource der von der aktuellen Instanz
  // verwendet wird
  private readonly ResourceManager _resourceManager;

  // Konstruktor
  private TranslateSource(string resource)
  {
    _resourceManager = new ResourceManager(resource, Assembly.GetExecutingAssembly());
  }

  // Indexer, um auf die lokalisierten Zeichenfolgen zuzugreifen
  public string this[string key] => _resourceManager.GetString(key, CultureInfo.CurrentUICulture);

  // Event, das ausgelöst wird, wenn sich die Sprache ändert
  // Wird genutzt, um das Binding zu aktualisieren ohne die Seite neu zu laden
  public event PropertyChangedEventHandler? PropertyChanged;

  // Löst das PropertyChanged-Event aus sobald die Sprache geändert wird
  public void NotifyLanguageChanged()
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
  }
}
