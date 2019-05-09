using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using LockMe.Base.OxBase;
using LockMe.Windows.mycontrol;
using NLog;
namespace LockMe.Windows.Main
{
    public partial class MainLockMe : Form
    {
        private ChromiumWebBrowser browser;

        //private LockMeConfigSettings meSettings;

        private  ILogger logger = LogManager.GetCurrentClassLogger();

        private const int x_extent = 100;

        private const int y_extent = 120;

        private void InitBrowser()
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

        private void InitMeSettings()
        {
            meSettings.Visible = false;
            Rectangle ScreenArea = GetScreenArea();
            int width = ScreenArea.Width;
            int height = ScreenArea.Height;
            meSettings.Size= new System.Drawing.Size(meSettings.Width, height);
            meSettings.Location = new Point((width- meSettings.Width)/2, 0);
           
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
                InitMeSettings();
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
            Locations();
           


        }
        public void Locations()//控件相对于屏幕位置
        {
            Rectangle ScreenArea = GetScreenArea();
            int width = ScreenArea.Width; 
            int height = ScreenArea.Height;
            bt_power.Location = new Point(x_extent, height- y_extent);
            bt_setting.Location = new Point(width - (x_extent+ bt_setting.Width), height - y_extent);
          
        }

        private Rectangle GetScreenArea()
        {
            return Screen.GetBounds(this);
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

        private void bt_setting_Click(object sender, EventArgs e)
        {
            meSettings.Visible = !meSettings.Visible;
        }
    }
}