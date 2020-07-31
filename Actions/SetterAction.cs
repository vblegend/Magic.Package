using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace Magic.Actions
{
    public class SetterAction : TargetedTriggerAction<FrameworkElement>
    {

        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(object), typeof(SetterAction), new PropertyMetadata());
        public object Target
        {
            get
            {
                return base.GetValue(TargetProperty);
            }
            set
            {
                base.SetValue(TargetProperty, value);
            }
        }




        public static readonly DependencyProperty PropertyProperty = DependencyProperty.Register("Property", typeof(DependencyProperty), typeof(SetterAction), new PropertyMetadata());

        public DependencyProperty Property
        {
            get
            {
                return (DependencyProperty)base.GetValue(PropertyProperty);
            }
            set
            {
                base.SetValue(PropertyProperty, value);
            }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(SetterAction), new PropertyMetadata());


        public object Value
        {
            get
            {
                return (object)base.GetValue(ValueProperty);
            }
            set
            {
                base.SetValue(ValueProperty, value);
            }
        }




        protected override void Invoke(object parameter)
        {
            Object obj = Value;
            TypeConverter typeConverter = TypeDescriptor.GetConverter(Property.PropertyType);
            if (typeConverter != null && typeConverter.CanConvertFrom(this.Value.GetType()))
            {
                obj = typeConverter.ConvertFrom(this.Value);
            }
            else
            {
                typeConverter = TypeDescriptor.GetConverter(this.Value.GetType());
                if (typeConverter != null && typeConverter.CanConvertTo(Property.PropertyType))
                {
                    obj = typeConverter.ConvertTo(this.Value, Property.PropertyType);
                }
            }


            if (Target is DependencyObject dd)
            {
                dd.SetValue(Property, obj);
            }

            
        }
    }
}
