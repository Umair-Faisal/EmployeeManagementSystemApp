using Microsoft.UI.Xaml.Data;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Frontend.Converters;

public class DecimalToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        try
        {
            return decimal.Parse(value.ToString());
        }
        catch
        {
            return null;
        }
    }
}
