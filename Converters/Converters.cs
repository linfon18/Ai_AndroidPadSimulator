using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace AndroidPadSimulator.Converters;

public class IntToBoolConverter : IValueConverter
{
    public static IntToBoolConverter Instance { get; } = new IntToBoolConverter();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int intValue && parameter is string paramString)
        {
            if (int.TryParse(paramString, out int targetValue))
            {
                return intValue == targetValue;
            }
        }
        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class TabIndexToColorConverter : IValueConverter
{
    public static TabIndexToColorConverter Instance { get; } = new TabIndexToColorConverter();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int intValue && parameter is string paramString)
        {
            if (int.TryParse(paramString, out int targetValue))
            {
                return intValue == targetValue ? new SolidColorBrush(Color.Parse("#4CAF50")) : new SolidColorBrush(Color.Parse("#80FFFFFF"));
            }
        }
        return new SolidColorBrush(Colors.Gray);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class CallTypeToColorConverter : IValueConverter
{
    public static CallTypeToColorConverter Instance { get; } = new CallTypeToColorConverter();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ViewModels.CallType callType)
        {
            return callType switch
            {
                ViewModels.CallType.Incoming => new SolidColorBrush(Color.Parse("#4CAF50")),
                ViewModels.CallType.Outgoing => new SolidColorBrush(Color.Parse("#2196F3")),
                ViewModels.CallType.Missed => new SolidColorBrush(Color.Parse("#F44336")),
                _ => new SolidColorBrush(Colors.Gray)
            };
        }
        return new SolidColorBrush(Colors.Gray);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class FirstCharConverter : IValueConverter
{
    public static FirstCharConverter Instance { get; } = new FirstCharConverter();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string str && !string.IsNullOrEmpty(str))
        {
            return str[0].ToString().ToUpper();
        }
        return "?";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
