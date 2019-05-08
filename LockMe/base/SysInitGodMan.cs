using System;
using System.Configuration;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace LockMe
{
    public class SysInitGodMan
    {
        public  void SysInit()
        {

            InitCefSettings();
            InitHook();
            InitTmKill();
            InitAutoRun();

        }

        public static SysInitGodMan Instance()
        {
            return  new SysInitGodMan();
        }
        private  void InitCefSettings()
        {
            var settings = new CefSettings();
            settings.CachePath = "cache";
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            settings.UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.79 Safari/537.36";
            settings.Locale = "zh-CN";
            settings.AcceptLanguageList = "zh-CN";
            settings.CefCommandLineArgs.Add("--ignore-urlfetcher-cert-requests", "1");
            settings.CefCommandLineArgs.Add("--ignore-certificate-errors", "1");
            Cef.Initialize(settings);
        }
        private readonly SysHookMan hook = new SysHookMan();
        /// <summary>
        /// 初始钩子函数
        /// </summary>
        private void InitHook()
        {
            hook.HookStart();
        }
        private void InitAutoRun()
        {
            int if_auto_run = Int32.Parse(ConfigurationManager.AppSettings["AUTO_RUN"]);
            string strFilePath = Application.ExecutablePath;
            string strFileName = System.IO.Path.GetFileName(strFilePath);

            bool isau = SysAutoRunMan.IsAutoRun(strFilePath, strFileName);
            if (if_auto_run == 1)
            {
                if (isau)
                    return;
                SysAutoRunMan.SetAutoRun(strFilePath + " -autostart", strFileName, true);
            }
            else
            {
                if (!isau)
                    return;
                SysAutoRunMan.SetAutoRun(strFilePath + " -autostart", strFileName, false);
            }

        }


        /// <summary>
        /// 任务管理器
        /// </summary>
        private void InitTmKill()
        {
            //管理任务管理器的方法
            //0：启用任务管理器 1：禁用任务管理器
            SysTaskMan.TaskManager();
        }
    }
}