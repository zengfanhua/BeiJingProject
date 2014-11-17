using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartDeviceProject1.Common;
using SmartDeviceProject1.Webservice1;
using Datalogic_Preprocessor;

namespace SmartDeviceProject1.Purchase
{
    /// <summary>
    ///     采购收货【收-标准和寄售】
    /// </summary>
    public partial class PurchaseInbund : Form
    {
        private DataTable _table;
        private string user;
        public PurchaseInbund()
        {
            InitializeComponent();
        }
        public PurchaseInbund(string username)
        {
            this.user = username;
            InitializeComponent();
        }

        public int k = 0;
        /// <summary>
        ///     确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboxOrder.Text.Trim() == "")
                {
                    cboxOrder.Focus();
                    MessageBox.Show("请输入采购订单号");
                    return;
                }
                if (txtOrganization.Text.Trim() == "")
                {
                    txtOrganization.Focus();
                    MessageBox.Show("请输入采购组织");
                    return;
                }
                //string OrganizationTwo = GlobalState.GetConfigInfo("销售组织");
                //if (OrganizationTwo != txtOrganization.Text.Trim())
                //{
                //    txtOrganization.Focus();
                //    MessageBox.Show("采购组织与后台文件不符！");
                //    return;
                //}
                if (dateTimePicker1.Text.Trim() == "")
                {
                    dateTimePicker1.Focus();
                    MessageBox.Show("请输入过账日期");
                    return;
                }

                if (txtLocation.Text.Trim() == "")
                {
                    txtLocation.Focus();
                    MessageBox.Show("请输入库位");
                    return;
                }
                string Organization = txtOrganization.Text.Trim();
                string dateTime = dateTimePicker1.Value.ToString("yyyyMMdd");
                string BillNumber = cboxOrder.Text.Trim();
                string Location = txtLocation.Text.Trim();
                var model = new Zppi518
                {
                    Budat = dateTime,
                    Ebeln = BillNumber,
                    Ekorg = Organization,
                    Lgort = Location
                };

                Webservice1.Zsdmsg01[] outmess = new SmartDeviceProject1.Webservice1.Zsdmsg01[10];
                model.Return = outmess;
                Zppi518Response response = GlobalState.GetWebService().Zppi518(model);
                SmartDeviceProject1.Webservice1.Zsdmsg01[] message = response.Return;
                for (int i = 0; i < message.Length; i++)
                {
                    //str += message[i].Message.ToString() + "\r";
                    if (response.Return[i].Ztype == "E")
                    {
                        MessageBox.Show(response.Return[i].Message);
                        return;
                    }
                    if (response.Return[i].Ztype == "S")
                    {
                        cboxOrder.Items.Add(cboxOrder.Text);
                        CommonAPI.Load(_table, GetType().Name);
                        //判断缓存是否符合条件
                        if (_table.Rows.Count > 0)
                        {
                            DataRow[] dr = _table.Select("采购凭证号='" + cboxOrder.Text.Trim() + "' and 操作ID='" + GlobalState.UserID + "'");
                            if (dr.Count() > 0)
                            {
                                if (MessageBox.Show("是否加载已保存数据！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    this.dataGrid1.DataSource = _table;
                                }
                                else
                                {
                                    DeleteXML();
                                }
                            }
                            else
                            {
                                DeleteXML();
                            }
                        }
                        else
                        {
                            k = 1;
                            tcMain.SelectedIndex = 1;
                            txtBarcode.Focus();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("采购订单输入错误！", "Zppi518");
            }
        }
        public void DeleteXML()
        {
            _table.Rows.Clear();
            dataGrid1.DataSource = null;
            CommonAPI.Remove(this.GetType().Name);
        }
        private void btnGoBack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                //CommonAPI.GoBack(this);
            }
            catch { }
        }

        private void btnUploadDraw_Click(object sender, EventArgs e)
        {
            try
            {
                Zrohgrtemp[] zrohgrtemps =
                    (from DataRow row in _table.Rows
                     select new Zrohgrtemp
                     {
                         //Zbudat = DateTime.Parse(row["过账日期"].ToString()),
                         //Ekorg = CommonAPI.Ekorg,
                         //Zlgort = row["库存地点"].ToString()
                         //Zwlot = row["库存地点"].ToString(),
                         //Zbudat = DateTime.Parse(row["过账日期"].ToString()),
                         //Cgmeins = row["计量单位"].ToString(),
                         //Cgmenge = Convert.ToDecimal(row["数量"].ToString()),
                         //Charg = row["批次"].ToString(),
                         //Maktx = "",
                         //Mandt = "",
                         //Werks = CommonAPI.Werks,
                         //Zisco = "",
                         //Zlgort = "",
                         //Zpdte = DateTime.Parse(row["生产日期"].ToString()),
                         //Zvnam = "",
                         //Ekorg = row["采购组织"].ToString(),
                         //Ebeln = row["采购凭证号 "].ToString(),
                         //Matnr = row["物料号 "].ToString(),
                         //Zbanm = row["版面"].ToString(),
                         Ebeln = row["采购凭证号"].ToString(),
                         Ebelp = "000" + row["项目编号"].ToString(),
                         Zvlot = row["供应商批号"].ToString(),
                         Zpnum = row["包号"].ToString()
                         //Bmenge = Convert.ToInt32(row["基本数量"]),
                         //Bmeins = row["基本计量单位"].ToString(),
                         //Zmatn = row["素铁物料编号"].ToString(),
                         //Ztdxl = row["铁镀锡量"].ToString(),
                         //Ztydz = row["铁硬度 "].ToString(),
                         //Zltcj = row["来铁厂家"].ToString(),
                         //Szmeng = Convert.ToDecimal(row["素铁重量"]),
                         //Szmeni = row["素铁重量单位"].ToString(),
                         //Ztres = row["素铁资源号 "].ToString(),
                         //Zgrop = row["打印操作ID"].ToString()
                     }).ToArray();

                var model = new Zppi520 { LtZrohgrtemp = zrohgrtemps };
                Webservice1.Zsdmsg01[] outmess = new SmartDeviceProject1.Webservice1.Zsdmsg01[10];
                model.Return = outmess;
                Zppi520Response response = GlobalState.GetWebService().Zppi520(model);
                SmartDeviceProject1.Webservice1.Zsdmsg01[] message = response.Return;
                for (int i = 0; i < message.Length; i++)
                {
                    //str += message[i].Message.ToString() + "\r";
                    if (response.Return[i].Ztype == "E")
                    {
                        MessageBox.Show(response.Return[i].Message);
                        return;
                    }
                    else if (response.Return[i].Ztype == "W")
                    {
                        MessageBox.Show(response.Return[i].Message);
                    }
                    else if (response.Return[i].Ztype == "S")
                    {
                        CommonAPI.Remove(this.GetType().Name);
                        dataGrid1.DataSource = null;
                        _table.Rows.Clear();
                        txtBarcode.Text = "";
                        txtBarcode.Focus();
                        tcMain.SelectedIndex = 1;
                    }


                }
            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zppi520");
            }
        }

        private void btnCancelDraw_Click(object sender, EventArgs e)
        {
            try
            {
                Zrohgrtemp[] zrohgrtemps =
                    (from DataRow row in _table.Rows
                     select new Zrohgrtemp
                     {
                         Ebeln = row["采购凭证号"].ToString(),
                         Ebelp = "000" + row["项目编号"].ToString(),
                         Zvlot = row["供应商批号"].ToString(),
                         Zpnum = row["包号"].ToString()
                         //Zwlot = row["库存地点"].ToString(),
                         //Zbudat = DateTime.Parse(row["过账日期"].ToString()),
                         //Cgmeins = row["计量单位"].ToString(),
                         //Cgmenge = Convert.ToDecimal(row["数量"].ToString()),
                         //Charg = row["批次"].ToString(),
                         ////Maktx="",
                         ////Mandt="",
                         //Werks = CommonAPI.Werks,
                         ////Zisco="",
                         ////Zlgort="",
                         //Zpdte = DateTime.Parse(row["生产日期"].ToString()),
                         ////Zvnam="",
                         //Ekorg = row["采购组织"].ToString(),
                         //Ebeln = row["采购凭证号 "].ToString(),
                         //Ebelp = row["采购凭证的项目编号 "].ToString(),
                         //Matnr = row["物料号 "].ToString(),
                         //Zbanm = row["版面"].ToString(),
                         //Zvlot = row["供应商批次 "].ToString(),
                         //Zpnum = row["包号"].ToString(),
                         //Bmenge = Convert.ToInt32(row["基本数量"]),
                         //Bmeins = row["基本计量单位"].ToString(),
                         //Zmatn = row["素铁物料编号"].ToString(),
                         //Ztdxl = row["铁镀锡量"].ToString(),
                         //Ztydz = row["铁硬度 "].ToString(),
                         //Zltcj = row["来铁厂家"].ToString(),
                         //Szmeng = Convert.ToDecimal(row["素铁重量"]),
                         //Szmeni = row["素铁重量单位"].ToString(),
                         //Ztres = row["素铁资源号 "].ToString(),
                         //Zgrop = row["打印操作ID"].ToString()
                     }).ToArray();

                var model = new Zppi521 { LtZrohgrtemp = zrohgrtemps };
                Webservice1.Zsdmsg01[] outmess = new SmartDeviceProject1.Webservice1.Zsdmsg01[10];
                model.Return = outmess;
                Zppi521Response response = GlobalState.GetWebService().Zppi521(model);
                SmartDeviceProject1.Webservice1.Zsdmsg01[] message = response.Return;
                for (int i = 0; i < message.Length; i++)
                {
                    //str += message[i].Message.ToString() + "\r";
                    if (response.Return[i].Ztype == "E")
                    {
                        MessageBox.Show(response.Return[i].Message);
                        return;
                    }
                    else if (response.Return[i].Ztype == "W")
                    {
                        MessageBox.Show(response.Return[i].Message);
                    }
                    else if (response.Return[i].Ztype == "S")
                    {
                        CommonAPI.Remove(this.GetType().Name);
                        dataGrid1.DataSource = null;
                        _table.Rows.Clear();
                        txtBarcode.Text = "";
                        txtBarcode.Focus();
                        tcMain.SelectedIndex = 1;
                    }


                }
            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zppi521");
            }
        }

        private void PurchaseInbund_Load(object sender, EventArgs e)
        {
            try
            {
                dateTimePicker1.Value = DateTime.Today;
                _table = new DataTable(GetType().Name);
                _table.Columns.Add("采购订单号", typeof(string));
                _table.Columns.Add("采购组织", typeof(string));
                _table.Columns.Add("过账日期", typeof(string));
                _table.Columns.Add("库存地点", typeof(string));
                _table.Columns.Add("采购凭证号", typeof(string));
                _table.Columns.Add("项目编号", typeof(string));
                _table.Columns.Add("物料编码", typeof(string));
                _table.Columns.Add("版面", typeof(string));
                _table.Columns.Add("供应商批号", typeof(string));
                _table.Columns.Add("包号", typeof(string));
                _table.Columns.Add("数量", typeof(string));
                _table.Columns.Add("计量单位", typeof(string));
                _table.Columns.Add("生产日期", typeof(string));
                _table.Columns.Add("素铁物料编号", typeof(string));
                _table.Columns.Add("铁镀锡量", typeof(string));
                _table.Columns.Add("铁硬度", typeof(string));
                _table.Columns.Add("来铁厂家", typeof(string));
                _table.Columns.Add("素铁重量", typeof(string));
                _table.Columns.Add("素铁重量单位", typeof(string));
                _table.Columns.Add("素铁资源号", typeof(string));
                _table.Columns.Add("操作ID", typeof(string));
                //if (CommonAPI.Load(_table, GetType().Name))
                //{
                //    var dataRow = _table.Rows[0];
                //    dataGrid1.DataSource = _table;
                //    tcMain.SelectedIndex = 2;
                //    return;
                //}
                string OrganizationTwo = GlobalState.GetConfigInfo("采购组织");
                txtOrganization.Text = OrganizationTwo;
            }
            catch { }
        }
        public MaterialInfo praseProduct = new MaterialInfo();
        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != '\r') return;
                if (txtBarcode.Text.Trim() == "") return;
                praseProduct = new MaterialInfo();
                praseProduct = CommonAPI.PraseMaterial(txtBarcode.Text);
                if (praseProduct == null)
                {
                    txtBarcode.Text = string.Empty;
                    txtBarcode.Focus();
                    CommonAPI.Message("扫描条码错误！");
                    return;
                }
                txtMetrialNumber.Text = praseProduct.Matnr;
                txtBatchNumber.Text = praseProduct.Zvlot;
                txtUnit.Text = praseProduct.Meins;
                txtPackNumber.Text = praseProduct.Zpnum;
                txtQty.Text = praseProduct.Menge.ToString();
                txtQty.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("扫码格式不正确！");
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!validation.IsNumber(txtQty.Text.Trim()))
                {
                    MessageBox.Show("数量不是数字！");
                    txtQty.Text = praseProduct.Menge.ToString();
                    return;
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
        }

        public void clear()
        {
            praseProduct = new MaterialInfo();
            txtMetrialNumber.Text = "";
            txtBatchNumber.Text = "";
            txtUnit.Text = "";
            txtPackNumber.Text = "";
            txtQty.Text = "0";
            txtBarcode.Text = "";
            txtBarcode.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBarcode.Text.Trim() == "" && txtMetrialNumber.Text.Trim() == "")
                {
                    MessageBox.Show("请扫描条码！");
                    txtBarcode.Focus();
                    return;
                }
                if (_table.Rows.Count > 0)
                {
                    if (praseProduct != null)
                    {
                        DataRow[] dr = _table.Select("物料编码 ='" + praseProduct.Matnr + "' and 供应商批号='" + praseProduct.Zvlot + "' and 包号='" + praseProduct.Zpnum + "'");
                        if (dr.Count() > 0)
                        {
                            MessageBox.Show("有物料、批次、包号重复的数据！");
                            clear();
                            return;
                        }
                    }
                }
                var item = new Zppi519
                {
                    Matnr = "0000000000" + praseProduct.Matnr,//物料号
                    Zpnum = praseProduct.Zpnum,//包号、桶号
                    Budat = dateTimePicker1.Value.ToString("yyyyMMdd"),//过账日期
                    Ekorg = GlobalState.GetConfigInfo("销售组织"),//采购组织
                    Lgort = txtLocation.Text.Trim(),//库位
                    EbelnInput = cboxOrder.Text.Trim(),//pDA采购凭证
                    Ebeln = praseProduct.Ebeln,//采购凭证号 
                    Ebelp = praseProduct.Ebelp, //row["采购凭证的项目编号 "].ToString(),
                    Zbanm = praseProduct.Zbanm, //row["版面"].ToString(),
                    Zvlot = praseProduct.Zvlot, //row["供应商批次 "].ToString(),
                    Bmenge = decimal.Parse(txtQty.Text.Trim()),//基本数量
                    Bmeins = praseProduct.Meins, //,row["基本计量单位"].ToString(),
                    Zpdte = praseProduct.Zpdte, //row["生产日期"].ToString(),
                    Zmatn = praseProduct.Zmatn, //,row["素铁物料编号"].ToString(),
                    Ztdxl = praseProduct.Ztdxl, //, row["铁镀锡量"].ToString(),
                    Ztydz = praseProduct.Ztydz, //,row["铁硬度 "].ToString(),
                    Zltcj = praseProduct.Zltcj, //,row["来铁厂家"].ToString(),
                    Szmeng = praseProduct.Szmeng, //Convert.ToDecimal(row["素铁重量"]),
                    Szmeni = praseProduct.Szment, //,row["素铁重量单位"].ToString(),
                    Ztres = praseProduct.Ztres, //,row["素铁资源号 "].ToString(),
                    Zgrop = GlobalState.UserID //,row["打印操作ID"].ToString()
                };

                Webservice1.Zsdmsg01[] outmess = new SmartDeviceProject1.Webservice1.Zsdmsg01[10];
                item.Return = outmess;
                Zppi519Response response = GlobalState.GetWebService().Zppi519(item);
                SmartDeviceProject1.Webservice1.Zsdmsg01[] message = response.Return;
                for (int i = 0; i < message.Length; i++)
                {
                    //str += message[i].Message.ToString() + "\r";
                    if (response.Return[i].Ztype == "E")
                    {
                        MessageBox.Show(response.Return[i].Message);
                        return;
                    }
                    if (response.Return[i].Ztype == "W")
                    {
                        MessageBox.Show(response.Return[i].Message);
                        return;
                    }
                    else if (response.Return[i].Ztype == "S")
                    {
                        _table.Rows.Add(cboxOrder.Text.Trim(), txtOrganization.Text.Trim(), dateTimePicker1.Value.ToString("yyyy-MM-dd"), txtLocation.Text.Trim(), item.Ebeln, item.Ebelp, item.Matnr.Substring(10, 8), item.Zbanm, item.Zvlot, item.Zpnum, item.Bmenge, item.Bmeins, item.Zpdte, item.Zmatn, item.Ztdxl, item.Ztydz, item.Zltcj, item.Szmeng, item.Szmeni, item.Ztres, GlobalState.UserID);
                        //    item.Zbarcode.Menge, item.Zbarcode.Meins, item.Zbarcode.Zpnum);
                        CommonAPI.Save(_table, GetType().Name);
                        dataGrid1.DataSource = null;
                        dataGrid1.DataSource = _table;
                        txtBarcode.Text = "";
                        txtBarcode.Focus();

                        btnCancelDraw.Enabled = true;
                        btnConform.Enabled = true;
                        clear();
                    }

                }

            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zppi519");
                return;
            }

        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tcMain.SelectedIndex == 1 || tcMain.SelectedIndex == 2)
                {
                    if (k == 0)
                    {
                        tcMain.SelectedIndex = 0;
                        MessageBox.Show("请检查采购订单！");
                        cboxOrder.Focus();
                        return;
                    }
                }
                if (tcMain.SelectedIndex == 2)
                {
                    if (dataGrid1.DataSource == null)
                    {
                        tcMain.SelectedIndex = 1;
                        MessageBox.Show("请扫描物料条码！");
                        txtBarcode.Focus();
                        return;
                    }
                }
            }
            catch { }
        }
    }
}