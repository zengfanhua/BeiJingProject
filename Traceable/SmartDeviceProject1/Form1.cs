using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Collections;
using SmartDeviceProject1.Common;

namespace SmartDeviceProject1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string org;
        private string fac;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    MessageBox.Show("用户名不能为空！");
                    return;
                }
                if (string.IsNullOrEmpty(textBox2.Text.Trim()))
                {
                    MessageBox.Show("密码不能为空！");
                    return;
                }
                string name = textBox1.Text.Trim();
                string pwd = textBox2.Text.Trim();
                string gc = fac;
                //try
                //{
                //    RegistryKey hklm = Registry.LocalMachine;
                //    RegistryKey software = hklm.OpenSubKey("Software");
                //    RegistryKey intecmac = software.OpenSubKey("Intermec");
                //    RegistryKey ssclient = intecmac.OpenSubKey("SSClient");
                //    SN = ssclient.GetValue("UniqueID").ToString();
                //    bool flag = GlobalState.IsExist(SN);
                //    if (flag == false)
                //    {
                //        MessageBox.Show("未知的设备！");
                //        return;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("未知的设备！");
                //    return;
                //}
                Packaging.ZuserChack io = new Packaging.ZuserChack();
                io.Zusrd = name;
                io.Zpasw = pwd;
                io.Werks = gc;
                io.Znumb = DeviceID;
                Packaging.ZuserCheck[] zout = new Packaging.ZuserCheck[20];
                io.Zuser = zout;
                Packaging.ZuserChackResponse response = GlobalState.GetWebServiceSD().ZuserChack(io);
                if (response.Return.Ztype == "E")
                {
                    MessageBox.Show(response.Return.Message);
                    return;
                }
                Packaging.ZuserCheck[] returnmsg = response.Zuser;
                string menu = "";
                for (int i = 0; i < returnmsg.Length; i++)
                {
                    menu += returnmsg[i].Zfunc.ToString() + ",";
                }
                if (!string.IsNullOrEmpty(menu))
                {
                    GlobalState.UserID = name;
                    Main main = new Main(menu, name, org, fac);
                    main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("该用户没有可访问的功能菜单!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("网络异常：" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Packaging.ZuserCancel para = new SmartDeviceProject1.Packaging.ZuserCancel();
                para.Zusrd = textBox1.Text.Trim();
                para.Zpasw = textBox2.Text.Trim();
                para.Werks = fac;
                para.Znumb = DeviceID;
                Packaging.ZuserCancelResponse response = GlobalState.GetWebServiceSD().ZuserCancel(para);
                if (response.Return.Ztype == "E")
                {
                    MessageBox.Show(response.Return.Message);
                    // return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Application.Exit();
        }
        private string SN;
        private string DeviceID;
        private void Form1_Load(object sender, EventArgs e)
        {
            fac = GlobalState.GetConfigInfo("工厂");
            org = GlobalState.GetConfigInfo("销售组织");
            DeviceID = GlobalState.GetConfigInfo("设备编号");
        }
    }
}