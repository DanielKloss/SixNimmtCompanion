using System;
using Windows.UI.Xaml.Data;

namespace SixNimmtCompanion.UI.Converters
{
    public class booleanToGameOptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
            {
                return "Points";
            }
            else
            {
                return "Rounds";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
