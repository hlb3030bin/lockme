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


    }
}