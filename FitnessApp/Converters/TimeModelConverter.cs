using Fitness.ClassLibrary.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FitnessApp.Converters;

internal class TimeModelConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is TimeModel time)
        {
            return $"{time.Minutes}:{string.Format("{0:00}",time.Seconds)}";
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        TimeModel time = new();
        if(value is string s)
        {
            string temp = s.Replace(":", "");
            if (!char.IsDigit(s[^1])) temp = temp[..^1];
            int.TryParse(temp[..^2],out int minutes);
            int.TryParse(temp[^2..],out int seconds);
            time.Minutes = minutes;
            time.Seconds = seconds;
        }
        return time;
    }
}
