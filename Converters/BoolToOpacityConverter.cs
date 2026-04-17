using Avalonia.Data.Converters;
using System;

namespace AndroidPadSimulator.Converters;

public class BoolToOpacityConverter : IValueConverter
{
    public static BoolToOpacityConverter Instance { get; } = new BoolToOpacityConverter();

    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        return value is bool b && b ? 1.0 : 0.0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class BoolToTransformConverter : IValueConverter
{
    public static BoolToTransformConverter Instance { get; } = new BoolToTransformConverter();

    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        return value is bool b && b ? new Avalonia.Media.TranslateTransform(0, 0) : new Avalonia.Media.TranslateTransform(0, 50);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
