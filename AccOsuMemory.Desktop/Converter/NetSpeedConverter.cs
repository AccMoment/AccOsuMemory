using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AccOsuMemory.Desktop.Converter;

public class NetSpeedConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        long.TryParse(value?.ToString(), out var netSpeed);
        var format = netSpeed switch
        {
            <= 0 => "{1:f2}%",
            <= 1024L => "{1:f2}%" + $"({netSpeed}B/S)",
            <= 1024 * 1024 => "{1:f2}%" + $"({netSpeed / 1024}KB/S)",
            _ => "{1:f2}%" + $"({netSpeed / (1024 * 1024)}MB/S)",
        };
        return format;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}