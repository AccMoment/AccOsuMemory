using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AccOsuMemory.Desktop.Converter;

public class MapLanguageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        int.TryParse(value?.ToString(), out var result);
        return result switch
        {
            0 => "Any",
            1 => "Other",
            2 => "English",
            3 => "Japanese",
            4 => "Chinese",
            5 => "Instrumental",
            6 => "Korean",
            7 => "French",
            8 => "German",
            9 => "Swedish",
            10 => "Spanish",
            11 => "Italian",
            _ => ""
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}