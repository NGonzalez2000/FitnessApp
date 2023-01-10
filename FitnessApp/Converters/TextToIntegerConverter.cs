using System;
using System.Globalization;
using System.Windows.Data;

namespace FitnessApp.Converters;

internal class TextToIntegerConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int? ret = value as int?;
        ret ??= 0;
        return ret.ToString()!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string? s = value as string;

        if (string.IsNullOrEmpty(s)) return 0;

        s = s.TrimStart('0');

        if(s.Length == 0)return 0;

        if (!char.IsDigit(s[^1])) s = s[..^1];

        _ = int.TryParse(s, out int ret);

        return ret;
    }
}
