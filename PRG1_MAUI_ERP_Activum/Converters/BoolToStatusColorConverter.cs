using System.Globalization;

namespace PRG1_MAUI_ERP_Activum.Converters;

/// <summary>
/// Konverterar bool (IsActive) till grön eller röd färg.
/// Används i InsuranceRegisterPage för att färgsätta statuskolumnen.
/// </summary>
public class BoolToStatusColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isActive)
            return isActive ? Colors.Green : Colors.Red;
        return Colors.Gray;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
