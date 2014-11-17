using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartDeviceProject1.Input;
using SmartDeviceProject1.Common;
using SmartDeviceProject1.Webservice1;
using Datalogic_Preprocessor;

namespace SmartDeviceProject1.Input
{
    public partial class InputMaterial : Form
    {
        public string ProductionOrder { get; set; }

        public string ProductionLine { get; set; }

        public ProductStatus CurrentState { get; set; }

        public string fac { get; set; }

        public string user { get; set; }

        public InputMaterial()
        {
            InitializeComponent();
        }

        public InputMaterial(string username)
        {
            this.user = username;
            InitializeComponent();
        }

        public void CloseForm()
        {
            this.Close();
        }

        private void btnReturn2_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void btnReturn1_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void InputMaterial_Load(object sender, EventArgs e)
        {
            PageInit();

            fac = GlobalState.GetConfigInfo("工厂");

            CurrentState = ProductStatus.Init;
        }

        public DataTable GridDT
        {
            get;
            set;
        }
        private void PageInit()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("物料号", "物料号");
            dic.Add("批次号", "批次号");
            dic.Add("单位", "单位");
            dic.Add("本包数量", "本包数量");
            dic.Add("包号", "包号");
            dic.Add("数量", "数量");
            GridDT = CreateInitDT(dic);

            this.dataGrid1.DataSource = GridDT;
        }
        /// <summary>
        ///  初始化 DT列
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public DataTable CreateInitDT(Dictionary<string, string> dic)
        {
            DataTable table = new DataTable();
            foreach (string key in dic.Keys)
            {
                DataColumn col = new DataColumn(key, typeof(string));
                col.Caption = dic[key].ToString();
                table.Columns.Add(col);
                continue;
            }
            return table;
        }

        private void btnConformOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboxOrder.Text.Trim()))
            {
                MessageBox.Show("请输入生产订单号！");
                return;
            }
            if (!validation.IsNumber(cboxOrder.Text) || cboxOrder.Text.Length > 12)
            {
                MessageBox.Show("生产订单号为不大于12位的整数！");
                return;
            }
            int j = int.Parse(cboxOrder.Text.Trim());
            string order = j.ToString("000000000000");
            labOrder.Text = order.ToString();
            try
            {
                Webservice1.Zppi501 item = new SmartDeviceProject1.Webservice1.Zppi501();
                item.Aufnr = order;

                Webservice1.Zplinemaster[] dt = new SmartDeviceProject1.Webservice1.Zplinemaster[10];
                Webservice1.Zsdmsg01[] outmess = new SmartDeviceProject1.Webservice1.Zsdmsg01[10];
                item.Return = outmess;
                // dt[0] = new SmartDeviceProject1.Webservice1.Zplinemaster();
                item.LtZplinemaster = dt;
                Webservice1.Zppi501Response response = GlobalState.GetWebService().Zppi501(item);
                Webservice1.Zsdmsg01[] m = response.Return;
                if (fac != response.Werks)
                {
                    MessageBox.Show("工厂不一致！");
                    cboxOrder.Text = "";
                    return;
                }
                CurrentState = ProductStatus.LineConfirmed;
                string mess = "";

                for (int i = 0; i < m.Length; i++)
                {
                    mess += response.Return[i].Message;
                }
                if (!string.IsNullOrEmpty(mess))
                {
                    if (mess != "生产订单可以使用")
                    {
                        MessageBox.Show(mess);
                        return;
                    }
                }

                //  string str = InputLogic.ConfirmOrder(order, fac);
                cboxOrder.Items.Add(cboxOrder.Text);
                ProductionOrder = order.ToString();
                this.tcMain.SelectedIndex = 1;
                txtBarcode.Focus();

            }
            catch (BusinessException businessException)
            {
                MessageBox.Show(businessException.Message);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnConformLine_Click(object sender, EventArgs e)
        {

        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcMain.SelectedIndex == 1)
            {
                if (CurrentState != ProductStatus.LineConfirmed)
                {
                    MessageBox.Show("请确认生产订单号及生产线。");
                    tcMain.SelectedIndex = 0;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelMaterial();
        }

        private void CancelMaterial()
        {
            txtBarcode.Text = string.Empty;
            txtBatchNumber.Text = string.Empty;
            txtPackNumber.Text = string.Empty;
            txtPackQty.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtUnit.Text = string.Empty;
            txtMetrialNumber.Text = string.Empty;

            txtBarcode.Focus();
        }

        private string ExtractBarcode(string barcode)
        {
            try
            {
                string str = "";
                string[] info = GlobalState.ExtractBarcode(barcode);
                if (info.Length == 19)
                {
                    txtMetrialNumber.Text = info[2];
                    txtBatchNumber.Text = info[10];
                    txtPackNumber.Text = info[6];
                    txtQty.Text = info[7];
                    txtPackQty.Text = info[7];
                    txtUnit.Text = info[8];
                    str = "";
                }
                else
                {
                    txtMetrialNumber.Text = info[11];
                    txtBatchNumber.Text = info[2].Split(':')[1];
                    txtPackNumber.Text = info[3].Split(':')[1];
                    txtQty.Text = info[4].Split(':')[1];
                    txtPackQty.Text = info[4].Split(':')[1];
                    txtUnit.Text = info[5];
                    str = "X";
                }
                return str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void btnConfirmInput_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMetrialNumber.Text.Trim()))
            {
                MessageBox.Show("请扫描物料条码！");
                return;
            }
            if (txtUnit.Text.Trim() == "吨" || txtUnit.Text.Trim() == "T" || txtUnit.Text.Trim() == "t" || txtUnit.Text.Trim() == "千克" || txtUnit.Text.Trim() == "KG" || txtUnit.Text.Trim() == "kg")
            {
                //
            }
            else
            {
                if (txtQty.Text.Trim().Contains('.'))
                {
                    MessageBox.Show("数量有误，请输入整数！");
                    return;
                }
            }
            decimal quantity = decimal.Parse(txtQty.Text.Trim());
            if (quantity <= 0)
            {
                MessageBox.Show("投料数量不能小于零！");
                return;
            }
            if (quantity > decimal.Parse(amount))
            {
                MessageBox.Show("数量不能大于" + amount);
                return;
            }

            int porder = int.Parse(ProductionOrder);
            int mater = int.Parse(txtMetrialNumber.Text.Trim());
            MaterialModel model = new MaterialModel();
            model.Aufnr = porder.ToString("000000000000");
            model.Charg = txtBatchNumber.Text.Trim();
            model.Matnr = mater.ToString("000000000000000000");
            model.Meins = txtUnit.Text.Trim();
            model.Menge = Math.Round(quantity, 2);
            model.Werks = fac;
            model.Zgrop = GlobalState.UserID;
            model.Zmengd = decimal.Parse(txtPackQty.Text.Trim());
            model.Zplin = "默认生产线";
            model.Zpnum = txtPackNumber.Text.Trim();
            bool isSuccess = InputLogic.InputMaterial(model, sto);
            if (!isSuccess)
            {
                return;
            }
            else
            {
                DataRow row = GridDT.NewRow();
                row["物料号"] = txtMetrialNumber.Text.Trim();
                row["批次号"] = txtBatchNumber.Text.Trim();
                row["单位"] = txtUnit.Text.Trim();
                row["本包数量"] = txtPackQty.Text.Trim();
                row["包号"] = txtPackNumber.Text.Trim();
                row["数量"] = txtQty.Text.Trim();
                GridDT.Rows.Add(row);

                CancelMaterial();
            }
        }
        private string sto;
        public string amount;
        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                try
                {
                    sto = ExtractBarcode(txtBarcode.Text.Trim());
                    int mater = int.Parse(txtMetrialNumber.Text.Trim());
                    int porder = int.Parse(ProductionOrder);
                    decimal mal = decimal.Parse(txtQty.Text.Trim());
                    Zppi510 para = new Zppi510();
                    para.Aufnr = porder.ToString("000000000000");
                    para.Werks = fac;
                    para.Zplin = "默认生产线";
                    para.Charg = txtBatchNumber.Text.Trim();
                    para.Zpnum = txtPackNumber.Text.Trim();
                    para.Matnr = mater.ToString("000000000000000000");
                    para.Meins = txtUnit.Text.Trim();
                    para.Menge = Math.Round(mal, 2);
                    para.Zmengd = decimal.Parse(txtPackQty.Text.Trim());
                    para.Zgrop = GlobalState.UserID;
                    para.Zsto = sto;
                    para.Return = new Zsdmsg01[1];
                    Zppi510Response response = GlobalState.GetWebService().Zppi510(para);
                    amount = response.Zmenge.ToString();
                    string text = "";
                    string val = "";
                    for (int i = 0; i < response.Return.Length; i++)
                    {
                        text += response.Return[i].Message + "\r";
                        val = response.Return[i].Ztype;
                    }
                    if (val == "E")
                    {
                        MessageBox.Show(text);
                        CancelMaterial();
                        return;
                    }
                    else { txtQty.Text = amount; }
                    txtQty.Focus();
                }
                catch (Exception ex)
                {
                    CommonAPI.Error(ex, "Zppi510");
                }
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                btnConfirmInput.Focus();
        }


    }
}