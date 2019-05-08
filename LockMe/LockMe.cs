using System;
using System.Configuration;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using NLog;

namespace LockMe
{
    public partial class LockMe : Form
    {
        public ChromiumWebBrowser browser;

        private  ILogger logger = LogManager.GetCurrentClassLogger();

        public void InitBrowser()
        {
            try
            {            
                browser = new ChromiumWebBrowser(ConfigurationManager.AppSettings["HOME_URL"]);
                browser.MenuHandler = new MenuHandler();
                browser.DownloadHandler = new MyDownLoadFile();
                browser.LoadingStateChanged += OnLoadingStateChanged;
                browser.Dock = DockStyle.Fill;
                Controls.Add(browser);
            }
            catch (Exception ex)
            {
                logger.Trace(ex, $@"------异常InitBrowser------");
            }
           
        }

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            IWebBrowser browser = (ChromiumWebBrowser)sender;
            var url = browser.Address;
            var isloading = args.IsLoading;
            this.InvokeOnUiThreadIfRequired(() => BrowerEventHandler.SetIsLoading(browser, isloading, url));
        }

        public LockMe()
        {
            try
            {             
                InitializeComponent();
                SysInitGodMan.Instance().SysInit();
                InitBrowser();
            
            }
            catch (Exception ex)
            {
                logger.Trace(ex, $@"------Trace.LockMe()-----");
                logger.Error(ex, $@"------Error.LockMe()-----");
            }
        }
        private void LockMeLoad(object sender, EventArgs e)
        {
            FullScreen();
        }
        /// <summary>
        /// 全屏
        /// </summary>
        private void FullScreen()
        {
            
            if (ConfigurationManager.AppSettings["IS_BORDER"] == "Y")
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
               
            }
            SetVisibleCore(true);
        }

    }
}