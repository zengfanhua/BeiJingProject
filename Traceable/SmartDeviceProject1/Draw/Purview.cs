using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartDeviceProject1.Draw
{
    public partial class Purview : Form
    {
        public Purview()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtName.Text.Trim();
            string userpwd = txtPwd.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("账号不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(userpwd))
            {
                MessageBox.Show("密码不能为空！");
                return;
            }
            DrawMaterial dm = (DrawMaterial)this.Owner;
            dm.pickUserName = username;
            dm.pickUserPwd = userpwd;
            this.Close();
        }
    }
}