using System;
using System.Threading;
using System.Windows;

namespace Magic
{
    /// <summary>
    /// 异步任务的UI回调
    /// </summary>
    public static class AsyncTask
    {
        /// <summary>
        /// 线程同步上下文
        /// </summary>
        public static SynchronizationContext Context { get; private set; }

        /// <summary>
        /// UI线程ID
        /// </summary>
        public static Int32 mainThreadId { get; private set; }

        /// <summary>
        /// 初始化UI线程
        /// </summary>
        private static void initContext()
        {
            if (Context == null)
            {
                Application.Current.Dispatcher.Invoke((Action)delegate ()
                {
                    mainThreadId = Thread.CurrentThread.ManagedThreadId;
                    Context = SynchronizationContext.Current;
                });
            }
        }





        /// <summary>
        /// 获取当前操作线程是否处于UI线程内
        /// </summary>
        public static Boolean IsUIThread
        {
            get
            {
                return Context != null &&  SynchronizationContext.Current == Context;
            }
        }

        /// <summary>
        /// 提交到UI线程执行委托（异步）
        /// </summary>
        /// <param name="callback"></param>
        public static void PostToUI(Action callback)
        {
            try
            {
                if (AsyncTask.IsUIThread)
                {
                    callback();
                    callback = null;
                }
                else
                {
                    initContext();
                    if (Context == null)
                    {
                        callback = null;
                        throw new Exception("SynchronizationContext.Current was not initialized");
                    }
                    Context.Post((ncallback) =>
                    {
                        ((Action)ncallback)();
                        ncallback = null;
                    }, callback.Clone());
                    callback = null;
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 切换到UI线程执行委托（同步）
        /// </summary>
        /// <param name="callback"></param>
        public static void SwitchToUI(Action callback)
        {
            try
            {
                if (AsyncTask.IsUIThread)
                {
                    callback();
                    callback = null;
                }
                else
                {
                    initContext();
                    if (Context == null)
                    {
                        callback = null;
                        throw new Exception("SynchronizationContext.Current was not initialized");
                    }
                    Context.Send((ncallback) =>
                    {
                        ((Action)ncallback)();
                        ncallback = null;
                    }, callback.Clone());
                    callback = null;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
