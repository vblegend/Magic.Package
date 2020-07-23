using Magic.Commands;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Magic.Core
{
    /// <summary>
    /// 线程安全的属性基本模型
    /// </summary>

    public abstract class BaseModel : INotifyPropertyChanged, IDisposable
    {
        protected BaseModel()
        {
            _propertys = new ConcurrentDictionary<string, object>();
        }
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 属性改变
        /// </summary>
        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 获取属性的值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="name">属性名，缺省为当前方法名</param>
        /// <returns></returns>
        protected T GetValue<T>([CallerMemberName]String name = "")
        {
            _propertys.TryGetValue(name, out Object value);
            if (value is T tvalue)
            {
                return tvalue;
            }
            return default(T);
        }

        /// <summary>
        /// 设置属性的值，并触发属性变更通知
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="name">属性名，缺省为当前方法名</param>
        protected void SetValue<T>(T value, [CallerMemberName]string name = "")
        {
            _propertys.AddOrUpdate(name, value, (k, v) => value);
            this.RaisePropertyChanged(name);
        }

        /// <summary>
        /// 移除一个属性
        /// </summary>
        /// <param name="name"></param>
        protected void RemoveProperty([CallerMemberName]string name = "")
        {
            _propertys.TryRemove(name, out Object value);
        }


        public virtual void Dispose()
        {
            _propertys.Clear();
            _propertys = null;
            this.IsDisposed = true;
        }

        private ConcurrentDictionary<String, Object> _propertys { get; set; }



        /// <summary>
        /// 是否已销毁
        /// </summary>
        public Boolean IsDisposed { get; private set; }
        #endregion







        /// <summary>
        /// 创建命令
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        /// <returns></returns>
        public ICommand CreatedCommand(Action<Object> execute, Func<bool> canExecute)
        {
            return new ActionCommand(execute, canExecute);
        }


        /// <summary>
        /// 创建命令
        /// </summary>
        /// <param name="execute"></param>
        /// <returns></returns>
        public ICommand CreatedCommand(Action<Object> execute)
        {
            return new ActionCommand(execute);
        }

    }
}