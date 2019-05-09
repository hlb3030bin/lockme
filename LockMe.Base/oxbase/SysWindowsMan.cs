﻿using System;
using System.Runtime.InteropServices;

namespace LockMe.Base.OxBase
{
    public class SysWindowsMan
    {

        public static bool CheckRoleAdministrator()
        {
            bool badmin = false;
            //获得当前登录的Windows用户标示
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal =
                new System.Security.Principal.WindowsPrincipal(identity);

            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                badmin = true;
            }

            return badmin;
        }

        //C#关机代码
        //这个结构体将会传递给API。使用StructLayout
        //（...特性，确保其中成员是按顺序排列的，C#编译器不会对其进行调整）
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        //以下使用DLLImport特性导入了所需的Windows API。
        //导入这些方法必须是static extern的，并且没有方法体。
        //调用这些方法就相当于调用Windows API。
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValueA
            (string host, string name, ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool
            AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev,
                IntPtr relen);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool ExitWindowsEx(int flg, int rea);

        //C#关机代码
        //以下定义了在调用WinAPI时需要的常数。
        //这些常数通常可以从Platform SDK的包含文件（头文件）中找到。
        public const int SE_PRIVILEGE_ENABLED = 0x00000002;
        public const int TOKEN_QUERY = 0x00000008;
        public const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        public const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        public const int EWX_LOGOFF = 0x00000000;
        public const int EWX_SHUTDOWN = 0x00000001;
        public const int EWX_REBOOT = 0x00000002;
        public const int EWX_FORCE = 0x00000004;
        public const int EWX_POWEROFF = 0x00000008;

        public const int EWX_FORCEIFHUNG = 0x00000010;
        // 通过调用WinAPI实现关机，主要代码再最后一行ExitWindowsEx  //这调用了同名的WinAPI，正好是关机用的。


        public static void DoExitWin(int flg)
        {
            bool ok;
            TokPriv1Luid tp;
            var hproc = GetCurrentProcess();
            var htok = IntPtr.Zero;
            ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            ok = LookupPrivilegeValueA(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            ok = ExitWindowsEx(flg, 0);
        }
        public static void Reboot()
        {
            SysWindowsMan.DoExitWin(SysWindowsMan.EWX_FORCE | SysWindowsMan.EWX_REBOOT);
        }

        public static void ShutDown()
        {
            SysWindowsMan.DoExitWin(SysWindowsMan.EWX_FORCE | SysWindowsMan.EWX_POWEROFF);
        }

        public static void LogOff()
        {
            SysWindowsMan.DoExitWin(SysWindowsMan.EWX_FORCE | SysWindowsMan.EWX_LOGOFF);
        }

    }
}