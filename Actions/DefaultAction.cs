using System;
using System.Windows;
using System.Windows.Interactivity;

namespace Magic.Actions
{
    public class DefaultAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty ActionProperty = DependencyProperty.Register("Action", typeof(Action<Object>), typeof(DefaultAction), new PropertyMetadata(null));
        /// <summary>
        /// 要执行的 Action 
        /// </summary>
        public Action<Object> Action
        {
            get { return (Action<Object>)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }



        public static readonly DependencyProperty ParameterProperty = DependencyProperty.Register("Parameter", typeof(Object), typeof(DefaultAction), new PropertyMetadata(null));
        /// <summary>
        /// Action 参数
        /// </summary>
        public Object Parameter
        {
            get { return (Object)GetValue(ParameterProperty); }
            set { SetValue(ParameterProperty, value); }
        }


        protected override void Invoke(object parameter)
        {
            if (Action != null)
            {
                Action.Invoke(Parameter);
            }
        }
    }
}
