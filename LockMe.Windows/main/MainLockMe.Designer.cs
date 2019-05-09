namespace LockMe.Windows.Main
{
    partial class MainLockMe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainLockMe));
            this.bt_power = new System.Windows.Forms.Button();
            this.bt_setting = new System.Windows.Forms.Button();
            this.meSettings = new LockMe.Windows.mycontrol.LockMeConfigSettings();
            this.SuspendLayout();
            // 
            // bt_power
            // 
            this.bt_power.BackColor = System.Drawing.Color.Silver;
            this.bt_power.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bt_power.BackgroundImage")));
            this.bt_power.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_power.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_power.Location = new System.Drawing.Point(3, 396);
            this.bt_power.Name = "bt_power";
            this.bt_power.Size = new System.Drawing.Size(42, 42);
            this.bt_power.TabIndex = 12;
            this.bt_power.UseVisualStyleBackColor = false;
            // 
            // bt_setting
            // 
            this.bt_setting.BackColor = System.Drawing.Color.Silver;
            this.bt_setting.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bt_setting.BackgroundImage")));
            this.bt_setting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_setting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_setting.FlatAppearance.BorderSize = 0;
            this.bt_setting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_setting.Location = new System.Drawing.Point(746, 396);
            this.bt_setting.Name = "bt_setting";
            this.bt_setting.Size = new System.Drawing.Size(42, 42);
            this.bt_setting.TabIndex = 13;
            this.bt_setting.UseVisualStyleBackColor = false;
            this.bt_setting.Click += new System.EventHandler(this.bt_setting_Click);
            // 
            // meSettings
            // 
            this.meSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.meSettings.Location = new System.Drawing.Point(28, 0);
            this.meSettings.Name = "meSettings";
            this.meSettings.Size = new System.Drawing.Size(760, 367);
            this.meSettings.TabIndex = 14;
            // 
            // MainLockMe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.meSettings);
            this.Controls.Add(this.bt_setting);
            this.Controls.Add(this.bt_power);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainLockMe";
            this.Text = "看什么看!";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LockMeLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_power;
        private System.Windows.Forms.Button bt_setting;
        private mycontrol.LockMeConfigSettings meSettings;
    }
}