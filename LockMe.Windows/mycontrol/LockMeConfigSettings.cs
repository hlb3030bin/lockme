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

        }

        private void save_sets_Click(object sender, EventArgs e)
        {
            this.Visible = !this.Visible;
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

        public string Get_home_url()
        {
            return tx_home_url.Text;
        }
        public string Get_enter_url()
        {
            return tx_enter_url.Text;
        }
        public string Get_user_id()
        {
            return tx_user_id.Text;
        }
        public string Get_pass_word()
        {
            return tx_pass_word.Text;
        }
        public string Get_run_app()
        {
            return nu_run_app.Text;
        }
        public string Get_is_task()
        {
            return "";
        }

        public string Get_is_border()
        {
            return "";
        }

        public string Get_auto_login()
        {
            return "";
        }
        public string Get_auto_run()
        {
            return "";
        }
    }
}
