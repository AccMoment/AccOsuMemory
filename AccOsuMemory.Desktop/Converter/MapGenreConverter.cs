using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace AccOsuMemory.Desktop.Converter;

public class MapGenreConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        int.TryParse(value?.ToString(), out var result);
        return result switch
        {
            0 => "Any",
            1 => "Unspecified",
            2 => "Video Game",
            3 => "Anime",
            4 => "Rock",
            5 => "Pop",
            6 => "Other",
            7 => "Novelty",
            9 => "Hip Hop",
            10 => "Electronic",
            _ => ""
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}