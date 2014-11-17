using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartDeviceProject1.Common;
using SmartDeviceProject1.Packaging;
using SmartDeviceProject1.WebReference;

namespace SmartDeviceProject1.parts
{
    /// <summary>
    ///     备件收货
    /// </summary>
    public partial class PartIn : Form
    {
        private DataTable _table;
        private string user;
        private string fac;
        public PartIn()
        {
            InitializeComponent();
        }
        public PartIn(string username)
        {
            this.user = username;
            InitializeComponent();
        }
        private void tbxBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r') return;

            ProductInfo praseProduct = CommonAPI.PraseProduct(tbxBarcode.Text);
            if (praseProduct == null)
            {
                tbxBarcode.Text = string.Empty;
                tbxBarcode.Focus();
                CommonAPI.Message("扫描条码错误！");
                return;
            }
            _table.Rows.Add(praseProduct.Matnr, praseProduct.Meins, "收货", fac, user);

            CommonAPI.Save(_table, GetType().Name);
            dataGrid1.DataSource = null;
            dataGrid1.DataSource = _table;
        }

        /// <summary>
        ///     确认收货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConform_Click(object sender, EventArgs e)
        {
            try
            {
                IEnumerable<Zpmi501> models =
                    from DataRow row in _table.Rows
                    select new Zpmi501
                    {
                        Matnr = row["物料编号"].ToString(),
                        Meins = row["计量单位"].ToString(),
                        Ztext = "收货",
                        Werks = row["默认工厂"].ToString(),
                        Zusrd = row["操作ID"].ToString()
                    };
                foreach (Zpmi501 model in models)
                {
                    GlobalState.GetWebReference().Zpmi501(model);
                }
                tbxBarcode.Text = "";
                CommonAPI.Remove(GetType().Name);
            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zpmi501");
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            CommonAPI.GoBack(this);
        }

        private void PartIn_Load(object sender, EventArgs e)
        {
            fac = GlobalState.GetConfigInfo("工厂");
            _table = new DataTable();
            _table.Columns.Add("物料编号", typeof(string));
            _table.Columns.Add("计量单位", typeof(string));
            _table.Columns.Add("收货", typeof(string));
            _table.Columns.Add("默认工厂", typeof(string));
            _table.Columns.Add("操作ID", typeof(string));
        }
    }
}