using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartDeviceProject1.AllocatioinSale;
using SmartDeviceProject1.WareSales;

namespace SmartDeviceProject1
{
    public partial class Zmtont : Form
    {
        public string s1, s2, s3, s4, s5, s6, s7, type;
        public Zmtont()
        {
            InitializeComponent();
        }
        public Zmtont(string str1, string str2, string str3, string str4, string str5, string str6, string str7, string str8)
        {
            this.s1 = str1; this.s2 = str2; this.s3 = str3; this.s4 = str4; this.s5 = str5; this.s6 = str6; this.s7 = str7; this.type = str8;
            InitializeComponent();
        }

        private void Zmtont_Load(object sender, EventArgs e)
        {
            textBox1.Text = s1; textBox2.Text = s2; textBox3.Text = s3; textBox4.Text = s4; textBox5.Text = s5; textBox6.Text = s6; textBox7.Text = s7;
        }
        //确认
        private void button1_Click(object sender, EventArgs e)
        {
            if (type == "AllocationSaleInfo")
            {
                AllocationSaleInfo info = (AllocationSaleInfo)this.Owner;
                info.IsClear = "否";
                this.Close();
            }
            if (type == "WaleSalesInfo")
            {
                WaleSalesInfo info = (WaleSalesInfo)this.Owner;
                info.IsClear = "否";
                this.Close();
            }
        }
        //取消
        private void button2_Click(object sender, EventArgs e)
        {
            if (type == "AllocationSaleInfo")
            {
                AllocationSaleInfo info = (AllocationSaleInfo)this.Owner;
                info.IsClear = "是";
                this.Close();
            }
            if (type == "WaleSalesInfo")
            {
                WaleSalesInfo info = (WaleSalesInfo)this.Owner;
                info.IsClear = "是";
                this.Close();
            }
        }
    }
}