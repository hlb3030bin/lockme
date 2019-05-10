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
using LockMe.Base.OxBase;


namespace LockMe.Windows.mycontrol
{
    public partial class LockMeConfigSettings : UserControl
    {
        public LockMeConfigSettings()
        {
            InitializeComponent();

        }

        private void save_sets_Click(object sender, EventArgs e)
        {
            SaveAppSettings();
            MessageBox.Show("保存配置成功重启APP生效!");
            this.Visible = !this.Visible;
            new SysAdminRunApp().AdminRunapp();
        }

        public void SaveAppSettings()
        {
            string home_url = Get_home_url();
            string enter_url = Get_enter_url();
            string user_id = Get_user_id();
            string pass_word = Get_pass_word();
            string run_app = Get_run_app();

            string is_task = Get_is_task();
            string is_border = Get_is_border();
            string auto_login = Get_auto_login();
            string auto_run = Get_auto_run();

            Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["HOME_URL"].Value = home_url;
            config.AppSettings.Settings["ENTER_URL"].Value = enter_url;
            config.AppSettings.Settings["USER_ID"].Value = user_id;
            config.AppSettings.Settings["PSSS_WORD"].Value = pass_word;
            config.AppSettings.Settings["RUN_APP"].Value = run_app;

            config.AppSettings.Settings["IS_TASK"].Value = is_task;
            config.AppSettings.Settings["IS_BORDER"].Value = is_border;
            config.AppSettings.Settings["AUTO_LOGIN"].Value = auto_login;
            config.AppSettings.Settings["AUTO_RUN"].Value = auto_run;
            // 保存
            config.Save();
            // 刷新
            ConfigurationManager.RefreshSection("appSettings");
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
            Set_home_url(home_url);
            Set_enter_url(enter_url);
            Set_user_id(user_id);
            Set_pass_word(pass_word);
            Set_run_app(run_app);

            Set_is_task(is_task);
            Set_is_border(is_border);
            Set_auto_login(auto_login);
            Set_auto_run(auto_run);

        }

        #region 
        public string Get_home_url()
        {
            return tx_home_url.Text;
        }
        public void Set_home_url(string tvalue)
        {
            tx_home_url.Text = tvalue;
        }
        public string Get_enter_url()
        {
            return tx_enter_url.Text;
        }
        public void Set_enter_url(string tvalue)
        {
            tx_enter_url.Text = tvalue;
        }
        public string Get_user_id()
        {
            return tx_user_id.Text;
        }
        public void Set_user_id(string tvalue)
        {
            tx_user_id.Text = tvalue;
        }
        public string Get_pass_word()
        {
            return tx_pass_word.Text;
        }
        public void Set_pass_word(string tvalue)
        {
            tx_pass_word.Text = tvalue;
        }
        public string Get_run_app()
        {
            return nu_run_app.Text;
        }
        public void Set_run_app(string tvalue)
        {
            nu_run_app.Text = tvalue;
        }
        public string Get_is_task()
        {
            if (radio_task_1.Checked)
                return "1";
            if (radio_task_0.Checked)
                return "0";
            return "";
        }
        public void Set_is_task(string tvalue)
        {
            if (tvalue == "1")
            {
                radio_task_1.Checked = true;
                radio_task_0.Checked = false;
            }
            else
            {
                radio_task_1.Checked = false;
                radio_task_0.Checked = true;
            }
        }
        public string Get_is_border()
        {
            if (radio_border_1.Checked)
                return "1";
            if (radio_border_0.Checked)
                return "0";
            return "";
        }
        public void Set_is_border(string tvalue)
        {
            if (tvalue == "1")
            {
                radio_border_1.Checked = true;
                radio_border_0.Checked = false;
            }
            else
            {
                radio_border_1.Checked = false;
                radio_border_0.Checked = true;
            }
        }
        public string Get_auto_login()
        {
            if (radio_login_1.Checked)
                return "1";
            if (radio_login_0.Checked)
                return "0";
            return "";
        }
        public void Set_auto_login(string tvalue)
        {
            if (tvalue == "1")
            {
                radio_login_1.Checked = true;
                radio_login_0.Checked = false;
            }
            else
            {
                radio_login_1.Checked = false;
                radio_login_0.Checked = true;
            }
        }
        public string Get_auto_run()
        {
            if (radio_auto_1.Checked)
                return "1";
            if (radio_auto_0.Checked)
                return "0";
            return "";
        }
        public void Set_auto_run(string tvalue)
        {
            if (tvalue == "1")
            {
                radio_auto_1.Checked = true;
                radio_auto_0.Checked = false;
            }
            else
            {
                radio_auto_1.Checked = false;
                radio_auto_0.Checked = true;
            }
        }
        #endregion
    }
}
