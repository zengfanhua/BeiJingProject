using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartDeviceProject1.Common;
using SmartDeviceProject1.Packaging;
using SmartDeviceProject1.WebReference;
using Datalogic_Preprocessor;

namespace SmartDeviceProject1.Parts.OrderOutbund
{
    /// <summary>
    ///     内部订单领料
    /// </summary>
    public partial class Orderoutbound : Form
    {
        private DataTable _table;
        private string user;
        public Orderoutbound()
        {
            InitializeComponent();
        }
        public Orderoutbound(string username)
        {
            this.user = username;
            InitializeComponent();
        }
        public int k = 0;
        public string InOrder = string.Empty;
        public string ReservedNumber = string.Empty;
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboxCenter.Text.Trim() == "")
                {
                    cboxCenter.Focus();
                    MessageBox.Show("请输入成本中心");
                    return;
                }
                if (!validation.IsNumber(cboxCenter.Text.Trim()) || cboxCenter.Text.Trim().Length != 10)
                {
                    MessageBox.Show("成本中心有误，请输入10位整数！");
                    cboxCenter.Focus();
                    return;
                }
                if (dateTimePicker1.Text.Trim() == "")
                {
                    dateTimePicker1.Focus();
                    MessageBox.Show("请输入过账日期");
                    return;
                }
                if (cboxOrder.Text.Trim() == "")
                {
                    cboxOrder.Focus();
                    MessageBox.Show("请输入内部订单号");
                    return;
                }
                if (!validation.IsNumber(cboxOrder.Text.Trim()) || cboxOrder.Text.Trim().Length != 12)
                {
                    cboxOrder.Focus();
                    MessageBox.Show("内部订单号有误，请输入12位整数！");
                    return;
                }
                if (txtLocation.Text.Trim() == "")
                {
                    txtLocation.Focus();
                    MessageBox.Show("请输入库位");
                    return;
                }
                if (!validation.IsNumber(txtLocation.Text.Trim()) || txtLocation.Text.Trim().Length != 4)
                {
                    txtLocation.Focus();
                    MessageBox.Show("库位有误，请输入4位整数！");
                    return;
                }
                ReservedNumber = cboxCenter.Text.Trim();
                string str1 = "";
                if (InOrder.Length < 10)
                {
                    for (int i = 0; i < 10 - ReservedNumber.Length; i++)
                    {
                        str1 += "0";
                    }
                    ReservedNumber = str1 + ReservedNumber;
                }
                string dateTime = dateTimePicker1.Value.ToString("yyyyMMdd");
                InOrder = cboxOrder.Text.Trim();
                string str = "";
                if (InOrder.Length < 12)
                {
                    for (int i = 0; i < 12 - InOrder.Length; i++)
                    {
                        str += "0";
                    }
                    InOrder = str + InOrder;
                }
                string WK = GlobalState.GetConfigInfo("工厂");
                string Location = txtLocation.Text.Trim();
                var item = new SmartDeviceProject1.WebReference.Zmmi505 { Aufnr = InOrder, Werks = WK, Kostl = ReservedNumber, Lgort = Location, Datum = dateTime };

                WebReference.Zsdmsg01[] outmess = new SmartDeviceProject1.WebReference.Zsdmsg01[10];
                item.Return = outmess;
                SmartDeviceProject1.WebReference.Zmmi505Response response = GlobalState.GetWebReference().Zmmi505(item);
                SmartDeviceProject1.WebReference.Zsdmsg01[] message = response.Return;
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
                        cboxCenter.Items.Add(cboxCenter.Text);
                        CommonAPI.Load(_table, GetType().Name);
                        //判断缓存是否符合条件
                        if (_table.Rows.Count > 0)
                        {
                            DataRow[] dr = _table.Select("订单号='" + cboxOrder.Text.Trim() + "' and 操作ID='" + GlobalState.UserID + "'");
                            if (dr.Count() == 0)
                            {
                                if (MessageBox.Show("扫描的订单号或者用户ID与存储的数据不符，是否删除保存的数据！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    _table.Rows.Clear();
                                    dataGrid1.DataSource = null;
                                    CommonAPI.Remove(this.GetType().Name);
                                }
                            }
                        }
                    }

                }
                if (response.Zaufnr != "")
                {
                    k = 1;
                    tcMain.SelectedIndex = 1;
                    txtBarcode.Focus();
                }

                // CommonAPI.Remove(GetType().Name);

            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zppi505");
            }

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
        public ProductInfo model = new ProductInfo();
        public MaterialInfo model1 = new MaterialInfo();
        /// <summary>
        ///     扫描物料条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (e.KeyChar == (char)Keys.Return)
                {
                    if (txtBarcode.Text.Trim() == "") return;
                    model = new ProductInfo();
                    model1 = new MaterialInfo();
                    model = CommonAPI.RawMaterials(txtBarcode.Text.Trim());
                    model1 = CommonAPI.PraseMaterial(txtBarcode.Text.Trim());
                    if (model == null)
                    {
                        //  model1 = CommonAPI.PraseMaterial(txtBarcode.Text);
                        if (model1 == null)
                        {

                            txtBarcode.Text = string.Empty;
                            txtBarcode.Focus();
                            CommonAPI.Message("扫描条码错误！");
                            return;
                        }
                        else
                        {
                            txtMetrialNumber.Text = model1.Matnr;
                            txtBatchNumber.Text = model1.Charg;
                            txtUnit.Text = model1.Meins;
                            txtPackNumber.Text = model1.Zpnum;
                            txtQty.Text = model1.Menge.ToString();
                            txtQty.Focus();
                        }
                    }
                    else
                    {
                        txtMetrialNumber.Text = model.Matnr;
                        txtBatchNumber.Text = model.Charg;
                        txtUnit.Text = model.Meins;
                        txtPackNumber.Text = model.Zpnum;
                        txtQty.Text = model.Menge.ToString();
                        txtQty.Focus();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("扫描格式不正确！"); }

        }
        public string factory;
        private void Orderoutbound_Load(object sender, EventArgs e)
        {
            try
            {
                factory = GlobalState.GetConfigInfo("工厂");
                _table = new DataTable(GetType().Name);
                _table.Columns.Add("工厂");
                _table.Columns.Add("订单号");
                _table.Columns.Add("成本中心");
                _table.Columns.Add("物料号");
                _table.Columns.Add("批次号");
                _table.Columns.Add("包号");
                _table.Columns.Add("库存地点");
                _table.Columns.Add("数量");
                _table.Columns.Add("单位");
                _table.Columns.Add("操作ID");
                CommonAPI.Sequence(btnConfirm_Click);
                if (CommonAPI.Load(_table, GetType().Name))
                {
                    var dataRow = _table.Rows[0];
                    dataGrid1.DataSource = _table;
                    tcMain.SelectedIndex = 2;
                    return;
                }
            }
            catch { }
        }

        private void btnUploadDraw_Click(object sender, EventArgs e)
        {
            try
            {
                WebReference.Zrohgiinfo[] ltZfertzctemp = (from DataRow row in _table.Rows
                                                           select new WebReference.Zrohgiinfo
                                              {
                                                  Werks = Convert.ToString(row["工厂"]),
                                                  Aufnr = Convert.ToString(row["订单号"]),
                                                  Kostl = Convert.ToString(row["成本中心"]),
                                                  Matnr = "0000000000" + Convert.ToString(row["物料号"]),
                                                  Charg = Convert.ToString(row["批次号"]),
                                                  Zpnum = Convert.ToString(row["包号"]),
                                                  Lgort = Convert.ToString(row["库存地点"]),
                                                  Menge = Convert.ToDecimal(row["数量"]),
                                                  Meins = Convert.ToString(row["单位"]),
                                                  Zgrop = Convert.ToString(row["操作ID"])
                                              }).ToArray();
                string WK = GlobalState.GetConfigInfo("工厂");
                var model = new WebReference.Zmmi506 { Aufnr = InOrder, Werks = WK, Kostl = ReservedNumber, Datum = dateTimePicker1.Value.ToString("yyyyMMdd"), Lgort = txtLocation.Text.Trim(), Itab = ltZfertzctemp };

                WebReference.Zsdmsg01[] outmess = new SmartDeviceProject1.WebReference.Zsdmsg01[10];
                model.Return = outmess;
                WebReference.Zmmi506Response response = GlobalState.GetWebReference().Zmmi506(model);
                SmartDeviceProject1.WebReference.Zsdmsg01[] message = response.Return;
                for (int i = 0; i < message.Length; i++)
                {
                    //str += message[i].Message.ToString() + "\r";
                    if (response.Return[i].Ztype == "E")
                    {
                        MessageBox.Show(response.Return[i].Message);
                        return;
                    }
                    else
                    {
                        CommonAPI.Remove(GetType().Name);
                        dataGrid1.DataSource = null;
                        _table.Rows.Clear();
                        tcMain.SelectedIndex = 1;
                        clear();
                        MessageBox.Show(response.Return[i].Message);
                        return;
                    }

                }

            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zmmi506");
            }
        }

        private void btnCancelDraw_Click(object sender, EventArgs e)
        {
            dataGrid1.DataSource = null;
            _table.Rows.Clear();
            tcMain.SelectedIndex = 1;
            clear();
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
                        MessageBox.Show("请检查内部订单！");
                        cboxCenter.Focus();
                        return;
                    }
                    else
                    {
                        txtBarcode.Focus();
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

        private void button1_Click(object sender, EventArgs e)
        {
            cboxCenter.Text = "";
            cboxOrder.Text = "";
            InOrder = "";
            ReservedNumber = "";
            txtLocation.Text = "";
            cboxCenter.Focus();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!validation.IsNumber(txtQty.Text.Trim()))
                {
                    MessageBox.Show("数量不是数字！");
                    if (model != null)
                    {
                        txtQty.Text = model.Menge.ToString();
                    }
                    else if (model1 != null)
                    {
                        txtQty.Text = model1.Menge.ToString();
                    }
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
            model = new ProductInfo();
            model1 = new MaterialInfo();
            txtMetrialNumber.Text = "";
            txtBatchNumber.Text = "";
            txtUnit.Text = "";
            txtPackNumber.Text = "";
            txtQty.Text = "0";
            txtBarcode.Text = "";
            txtBarcode.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
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
                    if (model != null)
                    {
                        DataRow[] dr = _table.Select("物料号='" + model.Matnr + "' and 批次号='" + model.Charg + "' and 包号='" + model.Zpnum + "'");
                        if (dr.Count() > 0)
                        {
                            MessageBox.Show("有物料、批次、包号重复的数据！");
                            clear();
                            return;
                        }
                    }
                    else if (model1 != null)
                    {
                        DataRow[] dr = _table.Select("物料号='" + model1.Matnr + "' and 批次号='" + model1.Charg + "' and 包号='" + model1.Zpnum + "'");
                        if (dr.Count() > 0)
                        {
                            MessageBox.Show("有物料、批次、包号重复的数据！");
                            clear();
                            return;
                        }
                    }

                }
                //Bwart = Convert.ToString(row["移动类型"]),
                //物料、批次、包号不能重复
                if (model != null)
                {
                    _table.Rows.Add(factory, InOrder, ReservedNumber, model.Matnr, model.Charg, model.Zpnum, txtLocation.Text, model.Menge.ToString(), model.Meins, GlobalState.UserID);
                }
                else if (model1 != null)
                {
                    _table.Rows.Add(factory, InOrder, ReservedNumber, model1.Matnr, model1.Charg, model1.Zpnum, txtLocation.Text, model1.Menge.ToString(), model1.Meins, GlobalState.UserID);
                }
                CommonAPI.Save(_table, GetType().Name);
                dataGrid1.DataSource = null;
                dataGrid1.DataSource = _table;
                txtBarcode.Text = "";
                txtBarcode.SelectAll();
                txtBarcode.Focus();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}