using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;


namespace CefTest
{
    public partial class Form1 : Form
    {
        ChromiumWebBrowser browser = null;
        StringBuilder cookies = new StringBuilder();

        public Form1()
        {
            InitializeComponent();
            InitBrowser();
        }

        /// <summary>
        /// 初始化浏览器
        /// </summary>
        public void InitBrowser() {

            var browerSetting = new CefSettings();
           
            //指定cookie的存储地方
            //browerSetting.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)+@"\mycef";

            //在Win7或更高版本上使用DPI高分辨率
            Cef.EnableHighDPISupport();

            Cef.Initialize(browerSetting);

            browser = new ChromiumWebBrowser("https://sellercentral.amazon.com/gp/homepage.html");
            browser.LifeSpanHandler = new TestLifeSpanHandler();

            //在页面右键菜单中启用或关闭开发者工具，仅限于调试目的
            browser.MenuHandler = new MenuHandler();
            

            var eventObject = new AmazonEventBindingObject();
            eventObject.ABName = "你的账号";
            eventObject.ABPassword = "你的密码";
            eventObject.EventArrived += OnJavascriptEventArrived;
            browser.RegisterJsObject("amazonBoundEvent", eventObject);
            
            browser.FrameLoadEnd += Browser_FrameLoadEnd;
            

            this.Controls.Add(browser);

            browser.Dock = DockStyle.Fill;
        }

        public bool setCookies(string domain, string name, string value, DateTime ExpiresTime) {

            var cookieManager = Cef.GetGlobalCookieManager();

            var setTask = cookieManager.SetCookieAsync("https://" + domain, new CefSharp.Cookie() {
                Domain = domain,
                Name = name,
                Value = value,
                Expires = ExpiresTime
            });

            setTask.Wait();


            if (setTask.IsCompleted) {

            }

            return setTask.Result;


            //存储cookie
            //var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Google\Chrome\User Data\Default\Cookies";
            //bool flag = cookieManager.SetStoragePath(AppDomain.CurrentDomain.BaseDirectory, true);
        }


        /// <summary>
        /// 页面加载完整后的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            var cookieManager = Cef.GetGlobalCookieManager();
            
            //读取Cookie
            CookieVisitor visitor = new CookieVisitor();
            visitor.SendCookie += visitor_SendCookie;
            cookieManager.VisitAllCookies(visitor);

            #region
            //读取
            //var visitor = new CookieVisitor(all_cookies => {
            //    var sb = new StringBuilder();
            //    foreach (var nameValue in all_cookies)
            //        sb.AppendLine(nameValue.Item1 + " = " + nameValue.Item2);

            //    BeginInvoke(new MethodInvoker(() => {
            //        MessageBox.Show(sb.ToString());
            //    }));
            //});
            //cookieManager.VisitAllCookies(visitor);
            #endregion


            //执行Javascript代码
            if (e.Frame.IsMain) {
                browser.ExecuteScriptAsync(@"

 (function () {

    if (window.amazonBoundEvent) {
        var unameElem = document.getElementById('ap_email');
        var passwordElem = document.getElementById('ap_password');

        
        unameElem.value = window.amazonBoundEvent.aBName;
        passwordElem.value = window.amazonBoundEvent.aBPassword;
    }
    


    var elem = document.getElementById('signInSubmit');

    if (elem) {
        elem.addEventListener('click', function (e) {
            if (!window.amazonBoundEvent) {
                console.log('window.amazonBoundEvent does not exist.');
                return;
            }

            var uname = document.getElementById('ap_email').value;
            var password = document.getElementById('ap_password').value;

            if (uname == "" && password == "")
                return;

            if (uname == "" || password == "")
                return;

            window.amazonBoundEvent.raiseEvent('click', { unameField: uname, upasswordField: password });
        });
        //console.log(`Added click listener to ${elem.id}.`);
    }
})();
        ");


            }
            

        }

        private void visitor_SendCookie(CefSharp.Cookie obj)
        {
            //此处处理cookie
            cookies.Append(obj.Domain.TrimStart('.') + "^" + obj.Name + "^" + obj.Value + "$");
        }

        /// <summary>
        /// 当页面上的Javascript事件触发时，执行
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="eventData"></param>
        private static void OnJavascriptEventArrived(string eventName, object eventData)
        {
            switch (eventName)
            {
                case "click":
                    {
                        var message = eventData.ToString();
                        var dataDictionary = eventData as Dictionary<string, object>;
                        if (dataDictionary != null)
                        {
                            var result = string.Join(", ", dataDictionary.Select(pair => pair.Key + "=" + pair.Value));
                            message = "event data: " + result;
                        }
                        MessageBox.Show(message, "Javascript event arrived", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
            }
        }
    }
}
