using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Common
{
    /// <summary>
    /// 全局变量控制器
    /// </summary>
    public class GlobalVariablesController
    {

        static GlobalVariablesController()
        {
            keyValuePairs = new ConcurrentDictionary<string, object>();
        }
        
        /// <summary>
        /// 设置一个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="Data"></param>
        public static void SetValue<T>(String name, T Data) where T : class
        {
            keyValuePairs.AddOrUpdate(name, Data, (e, x) => { return Data; });
        }

        /// <summary>
        /// 读取一个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static T GetValue<T>(String name, T DefaultValue = default(T)) where T : class
        {
            Object outval = DefaultValue;
            keyValuePairs.TryGetValue(name, out outval);
            return outval as T;
        }




        private static ConcurrentDictionary<String, Object> keyValuePairs { get; set; }
    }
}
