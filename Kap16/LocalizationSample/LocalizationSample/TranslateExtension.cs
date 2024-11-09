namespace LocalizationSample;

// Markup Extension für die dynamische Übersetzung von Texten
public class TranslateExtension : IMarkupExtension<BindingBase>
{
  // Key ist der Schlüssel, der in der Ressourcendatei definiert ist
  public string Key { get; set; }
  
  // Resource ist der vollqualifizierte Name der Ressource
  // inklusive Namespace und Klassennamen
  public string Resource { get; set; }

  // Gibt das Binding für die angegebene Ressource und den angegebenen Key zurück
  public BindingBase ProvideValue(IServiceProvider serviceProvider)
  {
    if (string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Resource))
    {
      return null;
    }

    // Binding erstellen und zurückgeben
    return new Binding
    {
      Mode = BindingMode.OneWay,
      Path = $"[{Key}]",
      Source = TranslateSource.For(Resource)
    };    
  }

  object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
  {
    return ProvideValue(serviceProvider);
  }
}
