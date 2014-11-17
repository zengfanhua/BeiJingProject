using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartDeviceProject1.SingleSales;
using SmartDeviceProject1.WareSales;
using SmartDeviceProject1.AllocatioinSale;
using SmartDeviceProject1.Return;

namespace SmartDeviceProject1
{
    public partial class Warn : Form
    {
        private string function;
        public Warn()
        {
            InitializeComponent();
        }
        private string UserName;

        public Warn(string funame, string user)
        {
            this.UserName = user;
            this.function = funame;
            InitializeComponent();
        }

        private void Warn_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (function == "4002")
            {
                SaleNoForm info = new SaleNoForm(UserName, function);
                info.ShowDialog();
            }
            if (function == "4004")
            {
                SaleNoForm info = new SaleNoForm(UserName, function);
                info.ShowDialog();
            }
            if (function == "4006")
            {
                SaleNoForm info = new SaleNoForm(UserName, function);
                info.ShowDialog();
            }
            if (function == "4009")
            {
                SaleNoForm info = new SaleNoForm(UserName, function);
                info.ShowDialog();
            }
        }
    }
}