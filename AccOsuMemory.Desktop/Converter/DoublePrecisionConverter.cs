using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AccOsuMemory.Desktop.Converter;

public class DoublePrecisionConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (double.TryParse(value?.ToString(), out var result))
        {
            return result.ToString("F");
        }

        return 0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}