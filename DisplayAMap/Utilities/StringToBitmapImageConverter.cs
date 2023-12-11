using DisplayAMap.API.Results;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DisplayAMap.WPF.Utilities
{
    public class StringToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string imageUrl)
            {
                try
                {
                    return new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error converting URL '{imageUrl}': {ex.Message}");
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
