using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace AccOsuMemory.Desktop.Converter;

public class PrecisionConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (double.TryParse(value?.ToString(), out var result))
        {
            var specificPrecision = parameter?.ToString();
            if (string.IsNullOrWhiteSpace(specificPrecision))
                return result.ToString("F");
            else
            {
                if (int.TryParse(specificPrecision, out var _))
                {
                    return result.ToString($"F{specificPrecision}");
                }
                else
                {
                    return result.ToString("F");
                }
            }
        }

        return 0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}