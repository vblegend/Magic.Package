using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace Magic.Actions
{


    /// <summary>
    /// 事件动作委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    /// <param name="parameter"></param>
    public delegate void EventActionDelegate(FrameworkElement sender, RoutedEventArgs args);
    /// <summary>
    /// 基于事件的动作
    /// </summary>
    public class EventAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty ActionProperty = DependencyProperty.Register("Action", typeof(EventActionDelegate), typeof(EventAction), new PropertyMetadata(null));

        /// <summary>
        /// 要执行的 Action 
        /// </summary>
        public EventActionDelegate Action
        {
            get { return (EventActionDelegate)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }


        protected override void Invoke(object parameter)
        {
            Console.WriteLine(this);
            if (Action != null)
            {
                Action.Invoke(this.AssociatedObject, parameter as RoutedEventArgs);
            }
        }
    }
}
