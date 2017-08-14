using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CefSharp;
using CefSharp.WinForms;

namespace CefTest
{
    /// <summary>
    /// 控制弹出窗体
    /// </summary>
    public class TestLifeSpanHandler : ILifeSpanHandler
    {
        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            //如果浏览器已经被处理，那么我们只会让默认行为发生，我们需要允许弹出窗口关闭
            if (browser.IsDisposed || browser.IsPopup){
                return false;
            }

            //CEF的默认行为（返回false）将发送一个OS关闭通知(比如:WM_CLOSE).关于此方法的详细信息，请参阅文档。
            //这里返回true，来处理关闭你自己（而不会发送WM_CLOSE）。
            return true;
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser){   
        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser){
        }

        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser){
            //设置 newBrowser为空，除非您尝试在ChromiumWebBrowser的新实例中托管中弹出窗口
            //只能在WPF/OffScreen中使用
            newBrowser = null;

            var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;
            chromiumWebBrowser.Load(targetUrl);

            return true;///返回true来取消创建弹出
        }
    }
}
