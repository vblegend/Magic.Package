using Magic.Uitity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Magic.Styles.Windows
{
    public class FixFullScreenService : IService
    {
        protected override IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WinApi.WM_GETMINMAXINFO)
            {
                WinApi.GetMinMaxInfo(hwnd, lParam);
                handled = true;
            }
            return base.WindowProc(hwnd, msg, wParam, lParam, ref handled);
        }

    }
}
