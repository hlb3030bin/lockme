﻿using System;
using System.Windows.Forms;
using NLog;

namespace LockMe.Base.OxBase
{
    public class SysAdminRunApp
    {
        private   ILogger logger = LogManager.GetCurrentClassLogger();

        public  void RunApp(Form appform)
        {

            logger.Info("------判断是否管理员------");
            //判断当前登录用户是否为管理员
            if (!SysWindowsMan.CheckRoleAdministrator())
            {
                AdminRunapp();
            }
            else
            {
                logger.Info("------是管理员------");
                logger.Info("------ Application.Run(new LockMe());------");

                //如果是管理员，则直接运行
                Application.Run(appform);
            }
        }

        /// <summary>
        /// 管理员运行
        /// </summary>
        public  void AdminRunapp()
        {
            //创建启动对象
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Application.ExecutablePath;
            //设置启动动作,确保以管理员身份运行
            startInfo.Verb = "runas";
            try
            {
                logger.Info("------管理员方式执行------");
                logger.Info("------System.Diagnostics.Process.Start(startInfo);------");
                System.Diagnostics.Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                logger.Info("------异常结束------");
                logger.Info($@"------{ex.Message}------");
                logger.Info($@"------{ex.StackTrace}------");
                return;
            }
            logger.Info("------ Application.Exit();------");
            //退出
            Application.Exit();
        }
    }
}