using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Magic.Styles.Windows
{
    public class ServiceCollection : FreezableCollection<IService>
    {
        public ServiceCollection()
        {
            
        }


        //protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        //{
        //    base.OnCollectionChanged(e);

        //    // new



        //    // old
        //}


        internal void Initialize(Window _window)
        {
            window = _window;
            foreach (var service in this)
            {
                try
                {
                    service.Attach(window);
                }
                catch (Exception)
                {
                }
            }
        }
        internal void UnInitialize()
        {
            foreach (var service in this)
            {
                try
                {
                    service.UnAttach();
                }
                catch (Exception)
                {
                }
            }
        }
        internal IntPtr HandleMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            foreach (var service in this)
            {
                try
                {
                    var result = service.RouteMessage(hwnd, msg, wParam, lParam, ref handled);
                    if (handled)
                    {
                        return result;
                    }
                }
                catch (Exception)
                {
                }
            }
            return IntPtr.Zero;
        }
        private Window window { get; set; }
    }
}
