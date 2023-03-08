using System;
using System.Windows.Forms;
using LockMe.Base.OxBase;
using LockMe.Windows.Main;
using NLog;

namespace LockMe
{
    static class Program
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainLockMe());
            new SysAdminRunApp().RunApp(new MainLockMe());
            return;
            //#region 去掉
            ////获得当前登录的Windows用户标示
            //System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            //System.Security.Principal.WindowsPrincipal principal =
            //    new System.Security.Principal.WindowsPrincipal(identity);

            //logger.Info("------判断是否管理员------");
            ////判断当前登录用户是否为管理员
            //if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            //{
            //    logger.Info("------是管理员------");
            //    logger.Info("------ Application.Run(new LockMe());------");

            //    //如果是管理员，则直接运行
            //    Application.Run(new LockMe());
            //}
            //else
            //{
            //    //创建启动对象
            //    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //    startInfo.UseShellExecute = true;
            //    startInfo.WorkingDirectory = Environment.CurrentDirectory;
            //    startInfo.FileName = Application.ExecutablePath;
            //    //设置启动动作,确保以管理员身份运行
            //    startInfo.Verb = "runas";
            //    try
            //    {
            //        logger.Info("------管理员方式执行------");
            //        logger.Info("------System.Diagnostics.Process.Start(startInfo);------");
            //        System.Diagnostics.Process.Start(startInfo);
            //    }
            //    catch (Exception ex)
            //    {
            //        logger.Info("------异常结束------");
            //        logger.Info($@"------{ex.Message}------");
            //        logger.Info($@"------{ex.StackTrace}------");
            //        return;
            //    }

            //    logger.Info("------ Application.Exit();------");

            //    //退出
            //    Application.Exit();
            //    //return;             
            //}
            //#endregion
        }
    }
}
