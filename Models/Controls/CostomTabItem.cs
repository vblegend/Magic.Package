using Magic.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Magic.Core;

namespace Magic.Models.Controls
{
    public delegate void CostomTabItemCloseHandle(CostomTabItem item);



    public abstract class CostomTabItem : BaseModel
    {

        public CostomTabItem()
        {
            this.IsChanged = false;
            this.CloseCommand = new ActionCommand(event_close);
        }


        public String Title
        {
            get
            {
                return this.GetValue<String>();
            }
            set
            {
                this.SetValue(value);
            }
        }

        public String ToolTip 
        {
            get
            {
                return this.GetValue<String>();
            }
            set
            {
                this.SetValue(value);
            }
        }




        public Boolean IsChanged
        {
            get
            {
                return this.GetValue<Boolean>();
            }
            protected set
            {
                this.SetValue(value);
            }
        }

        public FrameworkElement Page
        {
            get
            {
                return this.GetValue<FrameworkElement>();
            }
            set
            {
                this.SetValue(value);
            }
        }


        /// <summary>
        /// 关闭命令
        /// </summary>
        public ICommand CloseCommand { get; protected set; }

        

        protected virtual void event_close(Object param)
        {
            onClose?.Invoke(this);
        }

        /// <summary>
        /// 选项卡关闭事件
        /// </summary>
        public CostomTabItemCloseHandle onClose;
    }
}
