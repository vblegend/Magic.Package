using System;
using System.Windows;
using System.Windows.Data;

namespace Magic.Converter
{
    /// <summary>
    /// Boolean to visible converter
    /// </summary>
    public class DescriptionConverter : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public DescriptionConverter()
        {

        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var S = value.GetType();
                if (S.BaseType == typeof(Enum))
                {
                    Enum ivalue = (Enum)value;
                    return ivalue.GetDescription();
                }
            }
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

}
