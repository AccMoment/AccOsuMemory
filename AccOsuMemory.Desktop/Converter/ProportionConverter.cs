using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace AccOsuMemory.Desktop.Converter;

public class ProportionConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not double w || double.IsNaN(w)) return value;
        var proportionStr = parameter?.ToString();
        if (string.IsNullOrWhiteSpace(proportionStr)) return value;
        if (double.TryParse(proportionStr, out var proportion))
        {
            return w * proportion / 100;
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}