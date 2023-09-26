using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AccOsuMemory.Desktop.Converter;

public class StringToTimeSpanConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TimeSpan ts)
        {
            return ts.TotalSeconds.ToString("F");
        }
        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!double.TryParse(value?.ToString(), out var result)) return TimeSpan.FromSeconds(5);
        if (result > TimeSpan.MaxValue.TotalSeconds || result < 0) return TimeSpan.FromSeconds(5);
        return TimeSpan.FromSeconds(result);
    }
}