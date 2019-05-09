using System;
using System.Configuration;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using LockMe.Base.OxBase;
using NLog;

namespace LockMe.Windows.Main
{
    public partial class MainLockMe : Form
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
                logger.Error(ex, $@"------异常InitBrowser:{ex.Message}:{ex.GetBaseException().StackTrace}------");
            }
           
        }

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            IWebBrowser browser = (ChromiumWebBrowser)sender;
            var url = browser.Address;
            var isloading = args.IsLoading;
            this.InvokeOnUiThreadIfRequired(() => BrowerEventHandler.SetIsLoading(browser, isloading, url));
        }

        public MainLockMe()
        {
            try
            {             
                InitializeComponent();
                BorderShow();
                SysInitGodMan.Instance().SysInit();
                InitBrowser();
            
            }
            catch (Exception ex)
            {
       
                logger.Error(ex, $@"------Error.LockMe():{ex.Message}:{ex.GetBaseException().StackTrace}-----");
            }
        }
        private void LockMeLoad(object sender, EventArgs e)
        {
            FullScreen();
        }
        private void BorderShow()
        {
            if (Int32.Parse(ConfigurationManager.AppSettings["IS_BORDER"]) == 1)
            {
                FormBorderStyle = FormBorderStyle.Sizable;

            }
        }


        /// <summary>
        /// 全屏
        /// </summary>
        private void FullScreen()
        {
            SetVisibleCore(true);
        }

    }
}