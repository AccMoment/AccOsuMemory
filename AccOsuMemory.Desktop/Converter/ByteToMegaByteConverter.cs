using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace AccOsuMemory.Desktop.Converter;

public class ByteToMegaByteConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        long.TryParse(value?.ToString(), out var result);

        return $"{System.Convert.ToDouble(result) / 1024 / 1024:F2}Mb";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}