using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Magic.Commands
{
	public sealed class ActionCommand : ICommand
	{
		private Action action;
		private Func<Boolean> canexecute;
		private Action<Object> objectAction;
       // private EventHandler CanExecuteChanged;


        /// <summary>
        /// 检查命令是否可以执行的事件，
        /// 在UI事件发生导致控件状态或数据发生变化时触发
        /// </summary>
        //      event EventHandler ICommand.CanExecuteChanged
        //{
        //	add
        //	{
        //		this.CanExecuteChanged += value;
        //	}
        //	remove
        //	{
        //		this.CanExecuteChanged -= value;
        //	}
        //}

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canexecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (canexecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }


        public ActionCommand(Action action)
		{
			this.action = action;
		}

		public ActionCommand(Action action, Func<Boolean> canexecute)
		{
			this.action = action;
			this.canexecute = canexecute;
		}


		public ActionCommand(Action<Object> objectAction)
		{
			this.objectAction = objectAction;
		}

		public ActionCommand(Action<Object> objectAction, Func<Boolean> canexecute)
		{
			this.objectAction = objectAction;
			this.canexecute = canexecute;
		}

		bool ICommand.CanExecute(Object parameter)
		{
			return canexecute == null ? true : canexecute();
		}

		public void Execute(Object parameter)
		{
			if (this.objectAction != null)
			{
				this.objectAction(parameter);
				return;
			}
            if (action != null)
            {
				this.action();
			}
		}
	}
}
