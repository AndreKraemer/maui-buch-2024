using System.Globalization;
using DontLetMeExpire.Resources.Strings;
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
    // dem übergebenen Datum berechnen
    var today = DateTime.Today;
    var difference = (date - today).Days;

    // Unterschied in natürliche Sprache umwandeln
    // und zurückgeben
    return difference switch
    {
      0 => AppResources.Today,
      1 => AppResources.Tomorrow,
      -1 => AppResources.Yesterday,
      < -7 => string.Format(AppResources.XDaysAgo, Math.Abs(difference)),
      < 0 => AppResources.LastWeek,
      < 7 => string.Format(AppResources.InXDays, difference),
      < 14 => AppResources.NextWeek,
      < 21 => AppResources.InTwoWeeks,
      < 28 => AppResources.InThreeWeeks,
      _ => AppResources.InMoreThanAMonth
    };
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}