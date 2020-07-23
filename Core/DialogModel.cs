using Magic.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Magic.Core
{
    /// <summary>
    /// 窗口对话框基本模型
    /// </summary>
    public class DialogModel : BaseModel
    {
        public DialogModel()
        {
            Title = "";
            this.SubmitCommand = base.CreatedCommand(event_submit, submit_check);
            this.ApplyCommand = base.CreatedCommand(event_apply, apply_check);
            this.CancelCommand = base.CreatedCommand(event_cancel, cancel_check);
            this.ExitCommand = base.CreatedCommand(event_exit, exit_check);

        }

        /// <summary>
        /// 窗口标题
        /// </summary>
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



        public virtual void InitializeModel()
        {

        }

        #region dispose


        /// <summary>
        /// 销毁
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion

        #region dialog event

        /// <summary>
        /// 提交按钮事件
        /// </summary>
        protected virtual void event_submit(Object param)
        {
            this.DialogResult = true;
        }

        protected virtual Boolean submit_check()
        {
            return true;
        }

        /// <summary>
        /// 应用按钮事件
        /// </summary>
        protected virtual void event_apply(Object param)
        {

        }

        protected virtual Boolean apply_check()
        {
            return true;
        }
        /// <summary>
        /// 取消按钮事件
        /// </summary>
        protected virtual void event_cancel(Object param)
        {
            this.DialogResult = false;
        }

        protected virtual Boolean cancel_check()
        {
            return true;
        }
        /// <summary>
        /// 取消按钮事件
        /// </summary>
        protected virtual void event_exit(Object param)
        {
            this.DialogResult = true;
        }

        protected virtual Boolean exit_check()
        {
            return true;
        }
        /// <summary>
        /// 确定选择,需要时在派生类中重写
        /// </summary>
        public ICommand SubmitCommand { get; protected set; }

        /// <summary>
        /// 应用,需要时在派生类中重写
        /// </summary>
        public ICommand ApplyCommand { get; protected set; }

        /// <summary>
        /// 取消选择,需要时在派生类中重写
        /// </summary>
        public ICommand CancelCommand { get; protected set; }

        /// <summary>
        /// 取消选择,需要时在派生类中重写
        /// </summary>
        public ICommand ExitCommand { get; protected set; }


        /// <summary>
        /// 关闭当前对话框并返回结果
        /// </summary>
        protected Boolean DialogResult
        {
            set
            {
                if (OnClose != null)
                {
                    OnClose.Invoke(this, new WindowDestroyArgs() { DialogResult = value });
                }
            }
        }

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        public event EventHandler<WindowDestroyArgs> OnClose;
        #endregion
    }
}
