using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CefTest
{
    /// <summary>
    /// 亚马逊登录绑定对象
    /// </summary>
    public class AmazonEventBoundObject
    {
        public string ABName{ get; set; }

        public string ABPassword { get; set; }

        /// <summary>
        /// 当事件传递过来后触发
        /// </summary>
        public event Action<string, object> EventArrived;

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="eventData">调用者提供的有关事件的数据</param>
        public void RaiseEvent(string eventName, object eventData = null)
        {
            if (EventArrived != null)
            {
                EventArrived(eventName, eventData);
            }
        }
    }
}
