using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChineseConverter
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当窗口加载时发生
        /// </summary>
        private void AboutForm_Load(object sender, EventArgs e)
        {
            lblAppName.Text = "简繁转换器 " + Application.ProductVersion;
        }

        /// <summary>
        /// 当单击程序链接标签时发生
        /// </summary>
        private void lnkAppUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo proInfo = new System.Diagnostics.ProcessStartInfo(lnkAppUrl.Text);
                System.Diagnostics.Process pro = System.Diagnostics.Process.Start(proInfo);
            }
            catch (Exception) { }
        }
    }
}
