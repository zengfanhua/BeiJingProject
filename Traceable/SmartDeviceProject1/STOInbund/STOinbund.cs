using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartDeviceProject1.Common;
using SmartDeviceProject1.Packaging;
using Datalogic_Preprocessor;
using SmartDeviceProject1.WebReference;

namespace SmartDeviceProject1.STOInbund
{
    /// <summary>
    ///     收-STO调拨单
    /// </summary>
    public partial class STOinbund : Form
    {
        private DataTable _bill;
        private DataSet _dataSet;
        private DataTable _table;
        private string user;
        public STOinbund()
        {
            InitializeComponent();
        }
        public STOinbund(string username)
        {
            this.user = username;
            InitializeComponent();
        }

        public int k = 0;
        /// <summary>
        ///     确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConform_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboxOrder.Text.Trim() == "")
                {
                    MessageBox.Show("请输入交货单号！");
                    return;
                }
                if (!validation.IsNumber(cboxOrder.Text) || cboxOrder.Text.Length < 10)
                {
                    MessageBox.Show("交货单号有误，请输入10位整数！");
                    return;
                }
                string edatu = dtpEdatu.Value.ToString("yyyyMMdd");
                string WK = GlobalState.GetConfigInfo("工厂");
                string Org = GlobalState.GetConfigInfo("采购组织");
                var model = new Zmmi501
                {
                    Vbeln = cboxOrder.Text,
                    Werks = WK,
                    Ekorg = Org,
                    Edatu = edatu
                };
                //_bill.Rows.Add(model.Vbeln, model.Edatu);
                SmartDeviceProject1.WebReference.Zsdmsg01[] outmess = new SmartDeviceProject1.WebReference.Zsdmsg01[10];
                model.Return = outmess;
                Zmmi501Response response = GlobalState.GetWebReference().Zmmi501(model);
                SmartDeviceProject1.WebReference.Zsdmsg01[] message = response.Return;
                for (int i = 0; i < message.Length; i++)
                {
                    if (response.Return[i].Ztype == "E")
                    {
                        MessageBox.Show(response.Return[i].Message);
                        return;
                    }

                }
                if (response.Mtype == "S")
                {
                    cboxOrder.Items.Add(cboxOrder.Text);
                    CommonAPI.Load(_table, GetType().Name);
                    //判断缓存是否符合条件
                    if (_table.Rows.Count > 0)
                    {
                        DataRow[] dr = _table.Select("交货单号='" + cboxOrder.Text.Trim() + "' and 操作ID='" + GlobalState.UserID + "'");
                        if (dr.Count() > 0)
                        {
                            if (MessageBox.Show("是否加载已保存的数据！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                dtpEdatu.Value = CommonAPI.ParseTime(_table.Rows[0]["过账日期"].ToString());
                                cboxOrder.Enabled = false;
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
                        //cboxOrder.Enabled = false;
                        tcMain.SelectedIndex = 1;
                        txtBarcode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zmmi501");
            }
        }
        public void DeleteXML()
        {
            _table.Rows.Clear();
            dataGrid1.DataSource = null;
            CommonAPI.Remove(this.GetType().Name);
        }
        /// <summary>
        ///     返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGoBack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                //CommonAPI.GoBack(this);
            }
            catch { }
        }

        /// <summary>
        ///     确认收货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUploadDraw_Click(object sender, EventArgs e)
        {
            try
            {
                Zrohgrbapi[] ltZfertzctemp = (from DataRow row in _table.Rows
                                              select new Zrohgrbapi
                                               {
                                                   Zmeng2 = Convert.ToDecimal(row["实际数量"]),
                                                   Werks = GlobalState.GetConfigInfo("工厂"),
                                                   Matnr = "0000000000" + Convert.ToString(row["物料号"]),
                                                   Zvlot = Convert.ToString(row["供应商批次"]),
                                                   //Charg = praseProduct.Charg,
                                                   Meins = Convert.ToString(row["单位"]),
                                                   Zpnum = Convert.ToString(row["包号"]),
                                                   Menge = Convert.ToDecimal(row["数量"]),
                                                   Zpdte = Convert.ToString(row["生产日期"])
                                               }).ToArray();
                var model = new Zmmi504
                {
                    Vbeln = cboxOrder.Text,
                    Zmove = "X",
                    Itab = ltZfertzctemp,
                    Itac = null,
                    Zgrop = GlobalState.UserID
                };

                Zmmi504Response response = GlobalState.GetWebReference().Zmmi504(model);
                if (string.Compare(response.Return.Ztype, "S", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _table.Rows.Clear();
                    dataGrid1.DataSource = null;
                    CommonAPI.Remove(GetType().Name);
                    CommonAPI.Message(response.Return.Message);
                    tcMain.SelectedIndex = 1;
                }
                else
                {
                    CommonAPI.Message(response.Return.Message);
                }

            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zmmi504");
            }
        }

        /// <summary>
        ///     取消收货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelDraw_Click(object sender, EventArgs e)
        {
            try
            {
                dataGrid1.DataSource = null;
                _table.Rows.Clear();
                tcMain.SelectedIndex = 1;
                txtBarcode.Focus();
                CommonAPI.Remove(GetType().Name);
            }
            catch { }
        }
        public ProductInfo praseProduct = new ProductInfo();

        /// <summary>
        ///     产品名称:红牛/691/普通/饮料罐||版面:精装1段||批次:1014905148||包号:34||数量:80||罐||生产线:制罐八线||生产日期:20141015||生产时间:19:00||检验结果:合格||检验员:张三||30000000||2040
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != '\r') return;
                praseProduct = new ProductInfo();
                praseProduct = CommonAPI.RawMaterials(txtBarcode.Text);
                if (praseProduct == null)
                {
                    txtBarcode.Text = string.Empty;
                    txtBarcode.Focus();
                    CommonAPI.Message("扫描条码错误！");
                    return;
                }
                txtMetrialNumber.Text = praseProduct.Matnr;
                txtBatchNumber.Text = praseProduct.Charg;
                txtUnit.Text = praseProduct.Meins;
                txtPackNumber.Text = praseProduct.Zpnum;
                txtQty.Text = praseProduct.Menge.ToString();
                txtQty.Focus();
            }
            catch { }

        }

        private void STOinbund_Load(object sender, EventArgs e)
        {
            try
            {
                _table = new DataTable(GetType().Name);
                _table.Columns.Add("交货单号", typeof(string));
                _table.Columns.Add("过账日期", typeof(decimal));
                _table.Columns.Add("物料号", typeof(string));
                _table.Columns.Add("实际数量", typeof(string));
                _table.Columns.Add("数量", typeof(decimal));
                _table.Columns.Add("单位", typeof(string));
                _table.Columns.Add("供应商批次", typeof(string));
                _table.Columns.Add("包号", typeof(string));
                _table.Columns.Add("生产日期", typeof(string));
                _table.Columns.Add("操作ID", typeof(string));

                //_bill = new DataTable("bill");
                //_bill.Columns.Add("Vbeln", typeof(string));
                //_bill.Columns.Add("Edatu", typeof(string));

                //_dataSet = new DataSet("dummp");
                //_dataSet.Tables.Add(_table);
                //_dataSet.Tables.Add(_bill);

                //if (CommonAPI.Load(_table, GetType().Name))
                //{
                //    k = 1;
                //    //cboxOrder.Text = _table.Rows[0]["过账日期"].ToString();
                //    //dtpEdatu.Value = CommonAPI.ParseTime(_table.Rows[0]["过账日期"].ToString());
                //    //cboxOrder.Enabled = false;
                //    dataGrid1.DataSource = _table;
                //    tcMain.SelectedIndex = 2;
                //}
                //CommonAPI.Fill(dataGrid1, _table);
            }
            catch (Exception ex)
            {

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
            praseProduct = new ProductInfo();
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
                if (_table.Rows.Count > 0)
                {
                    if (praseProduct != null)
                    {
                        DataRow[] dr = _table.Select("物料号='" + praseProduct.Matnr + "' and 批次='" + praseProduct.Charg + "' and 包号='" + praseProduct.Zpnum + "'");
                        if (dr.Count() > 0)
                        {
                            MessageBox.Show("有物料、批次、包号重复的数据！");
                            clear();
                            return;
                        }
                    }
                }
                decimal d = decimal.Parse(txtQty.Text.Trim());
                var item = new Zmmi502
                {
                    Vbeln = cboxOrder.Text,
                    Zbarcode = new Zrohgrbapi()
                    {
                        Zmeng2 = Math.Round(d, 2),
                        Werks = GlobalState.GetConfigInfo("工厂"),
                        Matnr = "0000000000" + praseProduct.Matnr,
                        Zvlot = praseProduct.Charg,
                        //Charg = praseProduct.Charg,
                        Meins = praseProduct.Meins,
                        Zpnum = praseProduct.Zpnum,
                        Menge = praseProduct.Menge,
                        Zpdte = praseProduct.Zpdte
                    },
                    Zgrop = GlobalState.UserID
                };
                //_table.Columns.Add("交货单号", typeof(string));
                //_table.Columns.Add("过账日期", typeof(decimal));
                //_table.Columns.Add("物料号", typeof(string));
                //_table.Columns.Add("实际数量", typeof(string));
                //_table.Columns.Add("数量", typeof(decimal));
                //_table.Columns.Add("单位", typeof(string));
                //_table.Columns.Add("供应商批次", typeof(string));
                //_table.Columns.Add("包号", typeof(string));
                //_table.Columns.Add("生产日期", typeof(string));
                //_table.Columns.Add("操作ID", typeof(string));
                Zmmi502Response response = GlobalState.GetWebReference().Zmmi502(item);
                if (string.Compare(response.Return.Ztype, "S", StringComparison.OrdinalIgnoreCase) == 0)
                {

                    _table.Rows.Add(cboxOrder.Text.Trim(), dtpEdatu.Value.ToString("yyyyMMdd"), praseProduct.Matnr, txtQty.Text.Trim(), praseProduct.Menge, praseProduct.Meins, praseProduct.Charg, praseProduct.Zpnum, praseProduct.Zpdte, GlobalState.UserID
             );
                    CommonAPI.Save(_table, GetType().Name);
                    dataGrid1.DataSource = null;
                    dataGrid1.DataSource = _table;
                    txtBarcode.Text = "";
                    txtBarcode.SelectAll();
                    txtBarcode.Focus();
                    btnCancelDraw.Enabled = true;
                    btnConform.Enabled = true;
                    clear();
                }
                else
                {
                    CommonAPI.Message(response.Return.Message);
                }

            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zmmi502");
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
                        MessageBox.Show("请检查内部订单！");
                        cboxOrder.Focus();
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

        private void button3_Click(object sender, EventArgs e)
        {

            cboxOrder.Text = "";
            cboxOrder.Focus();
            //cboxOrder.Enabled = true;
            //cboxOrder.Text = "";
            //cboxOrder.Focus();
            //_table.Rows.Clear();
            //dataGrid1.DataSource = null;
            //CommonAPI.Remove(GetType().Name);
        }
    }
}