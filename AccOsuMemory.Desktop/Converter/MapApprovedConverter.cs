using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace AccOsuMemory.Desktop.Converter;

public class MapApprovedConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        int.TryParse(value?.ToString(), out var result);
        return result switch
        {
            -2 => "GRAVEYARD",
            -1 => "WIP",
            0 => "PENDING",
            1 => "RANKED",
            2 => "APPROVED",
            3 => "QUALIFIED",
            4 => "LOVED",
            _ => ""
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}