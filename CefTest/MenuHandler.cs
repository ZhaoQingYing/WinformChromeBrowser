
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CefSharp;

namespace CefTest
{
    public class MenuHandler : IContextMenuHandler
    {
        private const int ShowDevTools = 26501;
        private const int CloseDevTools = 26502;

        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            //To disable the menu then call clear
            model.Clear();

            //Removing existing menu item
            //bool removed = model.Remove(CefMenuCommand.ViewSource); // Remove "View Source" option
           

            //Add new custom menu items
            //model.AddItem((CefMenuCommand)ShowDevTools, "Show DevTools");
            //model.AddItem((CefMenuCommand)CloseDevTools, "Close DevTools");
        }

        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            //if ((int)commandId == ShowDevTools)
            //{
            //    browser.ShowDevTools();
            //}
            //if ((int)commandId == CloseDevTools)
            //{
            //    browser.CloseDevTools();
            //}
            return false;
        }

        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            
        }

        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            return false;
        }
    }
}
