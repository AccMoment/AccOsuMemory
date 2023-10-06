using System;
using System.Globalization;
using AccOsuMemory.Core.Models.SayoModels.Enum;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Microsoft.Extensions.Configuration;

namespace AccOsuMemory.Desktop.Converter;

public class BatchDownloadTypeBooleanConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is BatchDownloadType b1)
        {
            if (parameter is BatchDownloadType b2)
            {
                return b1 == b2;
            }
        }

        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}