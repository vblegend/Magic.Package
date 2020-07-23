using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Magic.Converter
{
    /// <summary>
    /// 
    /// {Binding xxx, Converter={convert:EnumValueConverter},ConverterParameter={x:Type enumtype:BusinessDimension}}
    /// </summary>
    public class EnumValueConverter : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        public EnumValueConverter()
        {

        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || parameter == null || value.GetType().BaseType != typeof(Enum))
            {
                return Binding.DoNothing;
            }
            return (Int32)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(parameter is Type))
            {
                return Binding.DoNothing;
            }

            var valueType = value.GetType();
            var tType = parameter as Type;
            if (valueType == typeof(Int32) && tType.BaseType == typeof(Enum))
            {
                return Enum.Parse(tType, value.ToString());
            }
            return Binding.DoNothing;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {

            return this;
        }
    }

}
