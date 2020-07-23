using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Magic.Core
{
    public class WindowDestroyArgs : RoutedEventArgs
    {
        /// <summary>
        /// Window.DialogResult 返回值
        /// </summary>
        public Boolean DialogResult { get; set; }


    }

    public interface IDialog
    {

        /// <summary>
        /// 初始化模型
        /// </summary>
        void InitializeModel();
    }
}
