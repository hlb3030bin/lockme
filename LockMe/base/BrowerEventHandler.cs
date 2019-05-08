using System;
using System.Configuration;
using System.Threading;
using CefSharp;

namespace LockMe
{
    public class BrowerEventHandler
    {

        public static  void SetIsLoading(IWebBrowser browser,bool isLoading, string url)
        {
            if (isLoading)
                return;
            if (url == ConfigurationManager.AppSettings["HOME_URL"])
            {

                string user = ConfigurationManager.AppSettings["USER_ID"];
                string pass = ConfigurationManager.AppSettings["PSSS_WORD"];
                string scp = $@"document.getElementById('Enter user name').value='{user}';
                            document.getElementById('passwd').value='{pass}';
                            document.getElementById('Log_On').click();";

                Thread.Sleep(1000);
                browser.ExecuteScriptAsync(scp);
            }
            if (url == ConfigurationManager.AppSettings["ENTER_URL"])
            {
                int appid = Int32.Parse(ConfigurationManager.AppSettings["RUN_APP"]) - 1;
                Thread.Sleep(1000);
                string scp = $@" var e = document.createEvent('MouseEvents'); 
                                 e.initEvent('click', true, true);
                                document.getElementsByClassName('storeapp-icon')[{appid}].dispatchEvent(e);";


                browser.ExecuteScriptAsync(scp);
            }
        }
    }
}