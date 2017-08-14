using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;

namespace CefTest
{
    public class CookieVisitor : ICookieVisitor
    {
        public event Action<CefSharp.Cookie> SendCookie;
        public bool Visit(CefSharp.Cookie cookie, int count, int total, ref bool deleteCookie)
        {
            deleteCookie = false;
            if (SendCookie != null)
            {
                SendCookie(cookie);
            }

            return true;
        }

        #region
        //readonly List<Tuple<string, string>> cookies = new List<Tuple<string, string>>();
        //readonly Action<IEnumerable<Tuple<string, string>>> useAllCookies;

        //public CookieVisitor(Action<IEnumerable<Tuple<string, string>>> useAllCookies) {
        //    this.useAllCookies = useAllCookies;
        //}


        //public bool Visit(Cookie cookie, int count, int total, ref bool deleteCookie)
        //{
        //    cookies.Add(new Tuple<string, string>(cookie.Name, cookie.Value));

        //    if (count == total - 1)
        //        useAllCookies(cookies);

        //    return true;
        //}
        #endregion

    }
}
