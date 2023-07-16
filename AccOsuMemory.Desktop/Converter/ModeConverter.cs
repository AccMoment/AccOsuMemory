using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AccOsuMemory.Desktop.Converter;

public class ModeConverter: IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        int.TryParse(value?.ToString(), out var result);
        return result switch
        {
            1=> "avares://AccOsuMemory/Assets/Images/osu-std.svg",
            2=> "avares://AccOsuMemory/Assets/Images/osu-taiko.svg",
            4=> "avares://AccOsuMemory/Assets/Images/osu-catch.svg",
            8=> "avares://AccOsuMemory/Assets/Images/osu-mania.svg",
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}