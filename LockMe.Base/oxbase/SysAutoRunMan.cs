using System;
using Microsoft.Win32;
using NLog;

namespace LockMe.Base.OxBase
{
    public sealed class SysAutoRunMan
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 设置程序开机启动
        /// </summary>
        /// <param name="strAppPath">应用程序exe所在文件夹</param>
        /// <param name="strAppName">应用程序exe名称</param>
        /// <param name="bIsAutoRun">自动运行状态</param>
        public static void SetAutoRun(string strAppPath, string strAppName, bool bIsAutoRun)
        {
            //LogMan.WriteBegin();
            logger.Info($@"------ 写注册表开机启动------");
            try
            {
                if (string.IsNullOrWhiteSpace(strAppPath)
                    || string.IsNullOrWhiteSpace(strAppName))
                {
                    logger.Info($@"------ 异常：应用程序路径或名称为空------");
                    //throw new Exception("！");
                }

                RegistryKey reg = Registry.LocalMachine;
                RegistryKey run = reg.CreateSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\");

                if (bIsAutoRun)
                {
                    logger.Info($@"------run.SetValue(strAppName, strAppPath);------");
                    run.SetValue(strAppName, strAppPath);
                }
                else
                {
                    if (null != run.GetValue(strAppName))
                    {
                        logger.Info($@"------run.DeleteValue(strAppName);------");
                        run.DeleteValue(strAppName);
                    }
                }

                run.Close();
                reg.Close();
            }
            catch (Exception ex)
            {
                logger.Trace(ex, $@"------Trace.-异常开机启动-----");
                logger.Error(ex, $@"------Error.-异常开机启动-----");
                //throw new Exception(ex.Message, ex);
            }

        }

        /// <summary>
        /// 判断是否开机启动
        /// </summary>
        /// <param name="strAppPath">应用程序路径</param>
        /// <param name="strAppName">应用程序名称</param>
        /// <returns></returns>
        public static bool IsAutoRun(string strAppPath, string strAppName)
        {
            try
            {
                RegistryKey reg = Registry.LocalMachine;
                RegistryKey software = reg.OpenSubKey(@"SOFTWARE");
                RegistryKey run = reg.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\");
                object key = run.GetValue(strAppName);
                software.Close();
                run.Close();
                if (null == key || !strAppPath.Equals(key.ToString()))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {

                logger.Trace(ex, $@"------Trace.-异常注册表-----");
                logger.Error(ex, $@"------Error.-异常注册表-----");
                throw new Exception(ex.Message, ex);
            }
        }
    }
}