using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElVegetarianoFurio
{
    public class ResourceImageSourceConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string source && !string.IsNullOrEmpty(source))
            {
                source = string.Format(source, value);
            }
            else
            {
                source = value.ToString();
            }

            return ImageSource.FromResource(source, typeof(ResourceImageSourceConverter).GetTypeInfo().Assembly);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}