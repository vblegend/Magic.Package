using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Core
{
    /// <summary>
    /// 拖拽的数据对象
    /// </summary>
    public class DragObject
    {
        /// <summary>
        /// 创建一个拖拽的数据
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <param name="data">数据内容</param>
        public DragObject(String type, Object data)
        {
            this.Type = type;
            this.Data = data;
        }

        public void SetData(Object data)
        {
            this.Data = data;
        }

        /// <summary>
        /// 拖拽的数据类型
        /// </summary>
        public String Type { get; private set; }

        /// <summary>
        /// 拖拽的数据内容
        /// </summary>
        public Object Data { get; private set; }
    }
}
