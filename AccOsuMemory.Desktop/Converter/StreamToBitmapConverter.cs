using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using AccOsuMemory.Desktop.Utils;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;

namespace AccOsuMemory.Desktop.Converter;

public class StreamToBitmapConverter:IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            null => null,
            string s when targetType.IsAssignableFrom(typeof(Bitmap)) => new Bitmap(HttpUtil.HttpClient.GetStreamAsync(s).Result),
            _ => throw new NotSupportedException()
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}