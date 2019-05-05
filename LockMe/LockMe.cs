using System;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Microsoft.Win32;
using NLog;
using Timer = System.Windows.Forms.Timer;

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
                Cef.Initialize(InitSettings());
                browser = new ChromiumWebBrowser(ConfigurationManager.AppSettings["HOME_URL"]);
                browser.MenuHandler = new MenuHandler();
                browser.DownloadHandler = new MyDownLoadFile();

                Controls.Add(browser);
                browser.Dock = DockStyle.Fill;
                browser.LoadingStateChanged += OnLoadingStateChanged;
            }
            catch (Exception ex)
            {
                logger.TraceException($@"------异常InitBrowser------", ex);
            }
            

        }
        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            if (args.Address == ConfigurationManager.AppSettings["ENTER_URL"])
            {
                string scp = $@" var e = document.createEvent('MouseEvents'); 
                                 e.initEvent('click', true, true);
                                document.getElementsByClassName('storeapp-icon')[0].dispatchEvent(e);";


                browser.ExecuteScriptAsync(scp);
            }

            this.InvokeOnUiThreadIfRequired(() =>{} );
        }

        private void SetIsLoading(bool isLoading,string url)
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
                int appid = Int32.Parse(ConfigurationManager.AppSettings["RUN_APP"])-1;
                Thread.Sleep(1000);
                string scp = $@" var e = document.createEvent('MouseEvents'); 
                                 e.initEvent('click', true, true);
                                document.getElementsByClassName('storeapp-icon')[{appid}].dispatchEvent(e);";


                browser.ExecuteScriptAsync(scp);
            }
            //appInfoOpenBtn

        }

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            IWebBrowser browser = (ChromiumWebBrowser)sender;

            var url = browser.Address;
            this.InvokeOnUiThreadIfRequired(() => SetIsLoading(args.IsLoading, url));
        }
        public CefSettings InitSettings()
        {
            var settings = new CefSettings();
            settings.CachePath = "cache";
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            //settings.CefCommandLineArgs.Add("no-proxy-server", "1");
            settings.UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.79 Safari/537.36";
            settings.Locale = "zh-CN";
            settings.AcceptLanguageList = "zh-CN";
            settings.CefCommandLineArgs.Add("--ignore-urlfetcher-cert-requests", "1");
            settings.CefCommandLineArgs.Add("--ignore-certificate-errors", "1");

            //var proxy = ProxyConfig.GetProxyInformation();
            return settings;
        }

        public LockMe()
        {
            try
            {
                InitializeComponent();
                InitBrowser();
                InitTmKill();
                InitAutoRun();

            }
            catch (Exception ex)
            {
                logger.TraceException($@"------异常-----", ex);
            }

        }

        private void InitAutoRun()
        {
            int if_auto_run = Int32.Parse(ConfigurationManager.AppSettings["AUTO_RUN"]);
            string strFilePath = Application.ExecutablePath;
            string strFileName = System.IO.Path.GetFileName(strFilePath);


            bool isau = SystemHelper.IsAutoRun(strFilePath, strFileName);
            if (if_auto_run == 1)
            {
                if (isau)
                    return;
                SystemHelper.SetAutoRun(strFilePath + " -autostart", strFileName, true);
            }
            else
            {
                if (!isau)
                    return;
                SystemHelper.SetAutoRun(strFilePath + " -autostart", strFileName, false);
            }

        }


        private Timer tm_kill;

        private void InitTmKill()
        {          
            ManageTaskManager(Int32.Parse(ConfigurationManager.AppSettings["IS_TASK"]));
        }

        /// <summary>        
        /// 管理任务管理器的方法
        /// </summary>
        /// <param name="arg">0：启用任务管理器 1：禁用任务管理器</param>
        private void ManageTaskManager(int arg)
        {
            var currentUser = Registry.CurrentUser;
            var system = currentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
            //如果system项不存在就创建这个项
            if (system == null)
                system = currentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            system.SetValue("DisableTaskmgr", arg, RegistryValueKind.DWord);
            currentUser.Close();
        }

        private readonly Hook hook = new Hook();

        private void Form1_Load(object sender, EventArgs e)
        {
            FullScreen();
            hook.Hook_Start();
        }

        private void FullScreen() //全屏
        {
            
            if (ConfigurationManager.AppSettings["IS_BORDER"] == "Y")
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
               
            }
            SetVisibleCore(true);
        }
    }
}