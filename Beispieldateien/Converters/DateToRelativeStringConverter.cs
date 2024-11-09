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

        var today = DateTime.Today;
        var difference = (date - today).Days;

        switch (difference)
        {
            case 0:
                return "Heute";
            case 1:
                return "Morgen";
            case -1:
                return "Gestern";
            default:
                return difference switch
                {
                    < -7 => string.Format("Vor {0} Tagen", Math.Abs(difference)),
                    < 0 => "Letzte Woche",
                    < 7 => string.Format("In {0} Tagen", difference),
                    < 14 => "Nächste Woche",
                    < 21 => "In zwei Wochen",
                    < 28 => "In drei Wochen",
                    _ => "In über einem Monat"
                };
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}