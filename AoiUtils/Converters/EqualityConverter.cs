using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace AoiUtils.Converters;

public class EqualityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString() == parameter?.ToString();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // When RadioButton is checked (value is true), return the parameter (e.g., "WinGet")
        // When RadioButton is unchecked (value is false), do nothing so we don't null the property
        if (value is bool b && b)
        {
            return parameter;
        }
        return BindingOperations.DoNothing;
    }
}
