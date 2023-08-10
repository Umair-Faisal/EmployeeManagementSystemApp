using Backend.Collections;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.ObjectModel;

namespace Frontend.Converters;

public class CustomCollectionToObservableCollectionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var collection = (CustomCollection<object>)value;
        ObservableCollection<object> result = new ObservableCollection<object>();
        foreach (var item in collection) { result.Add(item); }
        return new ObservableCollection<object>(collection);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        ObservableCollection<object> observable = (ObservableCollection<object>)value;
        CustomCollection<object> result = new CustomCollection<object>();
        foreach (var item in observable) { result.Add(item); }
        return result;
    }
}
