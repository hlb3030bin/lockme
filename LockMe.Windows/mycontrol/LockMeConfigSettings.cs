using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LockMe.Windows.mycontrol
{
    public partial class LockMeConfigSettings : UserControl
    {
        public LockMeConfigSettings()
        {
            InitializeComponent();
            //BackgroundImage = (Image)SysMeResource.GetResourceObject("bg");

        }

        private void save_sets_Click(object sender, EventArgs e)
        {
            //Configuration config =
            //    ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //config.AppSettings.Settings["username"].Value = "admin2";
            //// 保存
            //config.Save();
            //// 刷新
            //ConfigurationManager.RefreshSection("appSettings");
        }

        public void LoadAppSettings()
        {
            NameValueCollection appsettings = ConfigurationManager.AppSettings;
            string home_url = ConfigurationManager.AppSettings["HOME_URL"];
            string enter_url = ConfigurationManager.AppSettings["ENTER_URL"];
            string user_id = ConfigurationManager.AppSettings["USER_ID"];
            string pass_word = ConfigurationManager.AppSettings["PSSS_WORD"];
            string run_app = ConfigurationManager.AppSettings["RUN_APP"];

            string is_task = ConfigurationManager.AppSettings["IS_TASK"];
            string is_border = ConfigurationManager.AppSettings["IS_BORDER"];
            string auto_login = ConfigurationManager.AppSettings["AUTO_LOGIN"];
            string auto_run = ConfigurationManager.AppSettings["AUTO_RUN"];

            tx_home_url.Text = home_url;
            tx_enter_url.Text = enter_url;
            tx_user_id.Text = user_id;
            tx_pass_word.Text = pass_word;
            nu_run_app.Text = run_app;
        }
    }
}
