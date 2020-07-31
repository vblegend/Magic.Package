using System;
using System.Windows;
using System.Windows.Data;

namespace Magic.Converter
{
    /// <summary>
    /// Boolean to visible converter
    /// </summary>
    public class DoubleStringConverter : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public DoubleStringConverter()
        {

        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is String val)
            {
                var defdecimals = 2;
                Double result = 0;
                if (!Double.TryParse(val, out result)) return DBNull.Value;
                if (parameter is Int32 ccc) defdecimals = ccc;
                return Math.Round(result, defdecimals);
            }
            return DBNull.Value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString();
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

}
