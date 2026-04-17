using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace AndroidPadSimulator.Converters;

public class BoolToColorConverter : IValueConverter
{
    public static BoolToColorConverter Instance { get; } = new BoolToColorConverter();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue && parameter is string colorString)
        {
            var colors = colorString.Split('|');
            if (colors.Length == 2)
            {
                var color = boolValue ? colors[0] : colors[1];
                return new SolidColorBrush(Color.Parse(color));
            }
        }
        return new SolidColorBrush(Colors.Gray);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class BoolToTextConverter : IValueConverter
{
    public static BoolToTextConverter Instance { get; } = new BoolToTextConverter();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue && parameter is string textString)
        {
            var texts = textString.Split('|');
            if (texts.Length == 2)
            {
                return boolValue ? texts[0] : texts[1];
            }
        }
        return "";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
