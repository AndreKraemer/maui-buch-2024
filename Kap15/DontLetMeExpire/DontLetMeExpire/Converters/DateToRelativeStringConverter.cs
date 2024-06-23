using System.Globalization;
namespace DontLetMeExpire.Converters;

public class DateToRelativeStringConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is not DateTime date)
    {
      throw new ArgumentException("Expected date value");
    }

    // Differenz zwischen dem aktuellen und
    // dem �bergebenen Datum berechnen
    var today = DateTime.Today;
    var difference = (date - today).Days;

    // Unterschied in nat�rliche Sprache umwandeln
    // und zur�ckgeben
    return difference switch
    {
      0 => "Heute",
      1 => "Morgen",
      -1 => "Gestern",
      < -7 => string.Format("Vor {0} Tagen", Math.Abs(difference)),
      < 0 => "Letzte Woche",
      < 7 => string.Format("In {0} Tagen", difference),
      < 14 => "N�chste Woche",
      < 21 => "In zwei Wochen",
      < 28 => "In drei Wochen",
      _ => "In �ber einem Monat"
    };
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}