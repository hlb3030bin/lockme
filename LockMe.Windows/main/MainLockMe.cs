using System;
using System.Configuration;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using LockMe.Base.OxBase;
using NLog;

namespace LockMe.Windows.Main
{
    public partial class MainLockMe : Form
    {
        private ChromiumWebBrowser browser;

        //private LockMeConfigSettings meSettings;

        private readonly ILogger logger = LogManager.GetCurrentClassLogger();

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
            var ScreenArea = GetScreenArea();
            var width = ScreenArea.Width;
            var height = ScreenArea.Height;
            meSettings.Size = new Size(meSettings.Width, height);
            meSettings.Location = new Point((width - meSettings.Width) / 2, 0);
        }

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            IWebBrowser browser = (ChromiumWebBrowser) sender;
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

        public void Locations() //控件相对于屏幕位置
        {
            var ScreenArea = GetScreenArea();
            var width = ScreenArea.Width;
            var height = ScreenArea.Height;
            bt_power.Location = new Point(x_extent, height - y_extent);
            bt_setting.Location = new Point(width - (x_extent + bt_setting.Width), height - y_extent);
            panel_power.Location = new Point(x_extent, bt_power.Location.Y - panel_power.Height - 10);
        }

        private Rectangle GetScreenArea()
        {
            return Screen.GetBounds(this);
        }

        private void BorderShow()
        {
            if (int.Parse(ConfigurationManager.AppSettings["IS_BORDER"]) == 1)
                FormBorderStyle = FormBorderStyle.Sizable;
        }


        /// <summary>
        ///     全屏
        /// </summary>
        private void FullScreen()
        {
            SetVisibleCore(true);
        }

        private void bt_setting_Click(object sender, EventArgs e)
        {
            meSettings.LoadAppSettings();
            meSettings.Visible = !meSettings.Visible;
        }

        private void bt_poweroff_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show("确定要关机？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
                SysWindowsMan.ShutDown();
        }

        private void bt_reboot_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show("确定要重启？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
                SysWindowsMan.Reboot();
        }

        private void bt_power_Click(object sender, EventArgs e)
        {
            panel_power.Visible = !panel_power.Visible;
        }
       
    }
}