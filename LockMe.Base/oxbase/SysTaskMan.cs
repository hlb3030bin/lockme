using System;
using System.Configuration;
using Microsoft.Win32;

namespace LockMe.Base.OxBase
{
    public class SysTaskMan
    {
        public static void TaskManager()
        {
            int arg = Int32.Parse(ConfigurationManager.AppSettings["IS_TASK"]);

            var currentUser = Registry.CurrentUser;
            var system = currentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
            //如果system项不存在就创建这个项
            if (system == null)
                system = currentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            system.SetValue("DisableTaskmgr", arg, RegistryValueKind.DWord);
            currentUser.Close();
        }
    }
}