using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartDeviceProject1.Common;
using SmartDeviceProject1.Webservice1;
using Datalogic_Preprocessor;
using SmartDeviceProject1.Draw;

namespace SmartDeviceProject1.move
{
    /// <summary>
    ///     转库-线边仓到成品仓
    /// </summary>
    public partial class LinetoWH : Form
    {
        private DataTable _table;
        private string user;

        public LinetoWH()
        {
            InitializeComponent();
        }
        public LinetoWH(string username)
        {
            this.user = username;
            InitializeComponent();
        }
        /// <summary>
        ///     返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                //CommonAPI.GoBack(this);
            }
            catch { }
        }


        /// <summary>
        ///     预留号确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cboxOrder.Text))
                {
                    MessageBox.Show("请输入预留号！");
                    return;
                }
                if (!validation.IsNumber(cboxOrder.Text) || cboxOrder.Text.Length > 10)
                {
                    MessageBox.Show("预留号为不大于10位的整数！");
                    return;
                }
                var item = new Zppi506 { Rsnum = cboxOrder.Text };
                Webservice1.Zsdmsg01[] outmess = new SmartDeviceProject1.Webservice1.Zsdmsg01[10];
                item.Return = outmess;
                Zppi506Response response = GlobalState.GetWebService().Zppi506(item);
                SmartDeviceProject1.Webservice1.Zsdmsg01[] message = response.Return;
                for (int i = 0; i < message.Length; i++)
                {
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
                        cboxOrder.Items.Add(cboxOrder.Text);
                        CommonAPI.Load(_table, GetType().Name);
                        //判断缓存是否符合条件
                        if (_table.Rows.Count > 0)
                        {
                            DataRow[] dr = _table.Select("预留号='" + cboxOrder.Text.Trim() + "' and 操作ID='" + GlobalState.UserID + "'");
                            if (dr.Count() > 0)
                            {
                                if (MessageBox.Show("是否加载已保存的数据！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    txtZlonu.Text = _table.Rows[0]["发货库位"].ToString();
                                    txtZzcck.Text = _table.Rows[0]["接收库位"].ToString();
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
                            txtZlonu.Text = response.Fhkw;
                            txtZzcck.Text = response.Jskw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zppi506");
            }
        }
        public void DeleteXML()
        {
            _table.Rows.Clear();
            dataGrid1.DataSource = null;
            CommonAPI.Remove(this.GetType().Name);
        }

        public ProductInfo praseProduct = new ProductInfo();
        /// <summary>
        ///     产品名称:红牛/691/普通/饮料罐||版面:精装1段||批次:101490515S||包号:2||数量:7980||罐||生产线:制罐八线||生产日期:20141015||生产时间:19:00||检验结果:合格||检验员:张三||30000000||2040
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != '\r') return;
                if (txtBarcode.Text.Trim() == "") return;
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
            catch (Exception ex)
            {
                MessageBox.Show("扫码格式不正确！");
            }


        }
        public string pickUserName;
        public string pickUserPwd;
        /// <summary>
        ///     提交转储
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUploadDraw_Click(object sender, EventArgs e)
        {
            try
            {
                Zsfertzctemp[] ltZfertzctemp = (from DataRow row in _table.Rows
                                                select new Zsfertzctemp
                                                {
                                                    Rsnum = Convert.ToString(row["预留号"]),
                                                    Werks = Convert.ToString(row["工厂"]),
                                                    Matnr = "0000000000" + Convert.ToString(row["物料号"]),
                                                    Charg = Convert.ToString(row["批次"]),
                                                    //Meins = Convert.ToString(row["单位"]),
                                                    //Zenmng = Convert.ToDecimal(row["数量"]),
                                                    Zpnum = Convert.ToString(row["包号"]),
                                                    //Zlonu = Convert.ToString(row["发货库位"]),
                                                    //Zzcck = Convert.ToString(row["接收库位"]),
                                                    Zusrd = GlobalState.UserID
                                                    //Mandt = DateTime.Now.ToString("yyyyMMdd")
                                                }).ToArray();
                var zppi5091 = new Zppi509
                {
                    LtZfertzctemp = ltZfertzctemp,
                    Zusrd = GlobalState.UserID,
                    Ztjid = "账号",
                    Zpasw = "密码",
                    Werks = factory
                };


                Webservice1.Zsdmsg01[] outmess = new SmartDeviceProject1.Webservice1.Zsdmsg01[10];
                zppi5091.Return = outmess;
                Zppi509Response response = GlobalState.GetWebService().Zppi509(zppi5091);

                SmartDeviceProject1.Webservice1.Zsdmsg01[] message = response.Return;
                for (int i = 0; i < message.Length; i++)
                {
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
                        if (response.Return[i].Message == "同意提交ID或密码正确")
                        {
                            //
                        }
                        else
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
            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zppi509");
            }
        }

        private void btnCancelDraw_Click(object sender, EventArgs e)
        {
            try
            {
                Zsfertzctemp[] ltZfertzctemp = (from DataRow row in _table.Rows
                                                select new Zsfertzctemp
                                                {
                                                    Rsnum = Convert.ToString(row["预留号"]),
                                                    Werks = Convert.ToString(row["工厂"]),
                                                    Matnr = "0000000000" + Convert.ToString(row["物料号"]),
                                                    Charg = Convert.ToString(row["批次"]),
                                                    //Meins = Convert.ToString(row["单位"]),
                                                    //Zenmng = Convert.ToDecimal(row["数量"]),
                                                    Zpnum = Convert.ToString(row["包号"]),
                                                    //Zlonu = Convert.ToString(row["发货库位"]),
                                                    //Zzcck = Convert.ToString(row["接收库位"]),
                                                    Zusrd = GlobalState.UserID
                                                }).ToArray();
                var zppi5091 = new Zppi508
                {
                    LtZfertzctemp = ltZfertzctemp
                };


                Webservice1.Zsdmsg01[] outmess = new SmartDeviceProject1.Webservice1.Zsdmsg01[10];
                zppi5091.Return = outmess;
                Zppi508Response response = GlobalState.GetWebService().Zppi508(zppi5091);
                SmartDeviceProject1.Webservice1.Zsdmsg01[] message = response.Return;
                for (int i = 0; i < message.Length; i++)
                {
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
                CommonAPI.Error(ex, "Zppi508");
            }
        }
        public string factory;
        private void LinetoWH_Load(object sender, EventArgs e)
        {
            try
            {
                factory = GlobalState.GetConfigInfo("工厂");
                _table = new DataTable(GetType().Name);
                _table.Columns.Add("工厂", typeof(string));
                _table.Columns.Add("预留号", typeof(string));
                _table.Columns.Add("物料号", typeof(string));
                _table.Columns.Add("批次", typeof(string));
                _table.Columns.Add("数量", typeof(decimal));
                _table.Columns.Add("单位", typeof(string));
                _table.Columns.Add("包号", typeof(string));
                _table.Columns.Add("发货库位", typeof(string));
                _table.Columns.Add("接收库位", typeof(string));
                _table.Columns.Add("操作ID", typeof(string));
                //  CommonAPI.Sequence(btnConfirm_Click, cboxOrder, btnConfirm);
                //if (CommonAPI.Load(_table, GetType().Name))
                //{
                //    var dataRow = _table.Rows[0];
                //    cboxOrder.Text = dataRow["预留号"].ToString();
                //    txtZlonu.Text = dataRow["发货库位"].ToString();
                //    txtZzcck.Text = dataRow["接收库位"].ToString();
                //    dataGrid1.DataSource = _table;
                //    tcMain.SelectedIndex = 2;
                //    return;
                //}

                //tabPage = tcMain.TabPages[2];

                //tcMain.TabPages.RemoveAt(2);
                cboxOrder.Focus();
            }
            catch { }
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tcMain.SelectedIndex == 0)
                {
                    dtpEdatu.Enabled = true;
                }
                if (tcMain.SelectedIndex == 1)
                {
                    if (txtZlonu.Text.Trim() == "")
                    {
                        tcMain.SelectedIndex = 0;
                        MessageBox.Show("请输入扫描预留号！");
                        cboxOrder.Focus();
                        return;
                    }
                    if (txtZzcck.Text.Trim() == "")
                    {
                        tcMain.SelectedIndex = 0;
                        MessageBox.Show("请输入接收库位！");
                        txtZzcck.Focus();
                        return;
                    }
                    if (!validation.IsNumber(txtZzcck.Text.Trim()) || txtZzcck.Text.Trim().Length != 4)
                    {
                        tcMain.SelectedIndex = 0;
                        MessageBox.Show("接收库位有误，请输入4位整数！");
                        txtZzcck.Focus();
                        return;
                    }
                    txtBarcode.Focus();

                }
                if (tcMain.SelectedIndex == 2)
                {
                    if (dataGrid1.DataSource == null)
                    {
                        if (txtZlonu.Text.Trim() != "" && txtZzcck.Text.Trim() != "")
                        {
                            tcMain.SelectedIndex = 1;
                            MessageBox.Show("请扫描物料条码！");
                            txtBarcode.Focus();
                            return;
                        }
                        if (txtZlonu.Text.Trim() == "")
                        {
                            tcMain.SelectedIndex = 0;
                            MessageBox.Show("请输入扫描预留号！");
                            cboxOrder.Focus();
                            return;
                        }
                        if (txtZzcck.Text.Trim() == "")
                        {
                            tcMain.SelectedIndex = 0;
                            MessageBox.Show("请输入接收库位！");
                            txtZzcck.Focus();
                            return;
                        }

                        return;
                    }
                }
            }
            catch { }
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
                        DataRow[] dr = _table.Select("物料号='" + praseProduct.Matnr + "' and 批次='" + praseProduct.Charg + "' and 包号='" + praseProduct.Zpnum + "'");
                        if (dr.Count() > 0)
                        {
                            MessageBox.Show("有物料、批次、包号重复的数据！");
                            clear();
                            return;
                        }
                    }
                }
                string edatu = dtpEdatu.Value.ToString("yyyyMMdd");
                if (praseProduct.Matnr == null)
                {
                    txtBarcode.Text = "";
                    txtBarcode.Focus();
                    MessageBox.Show("请扫描物料条码");
                    return;
                }
                if (txtQty.Text.Trim() == "0")
                {
                    MessageBox.Show("数量不能为0");
                    txtQty.Focus();
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
                decimal d = decimal.Parse(txtQty.Text.Trim());
                var item = new Zppi507
                {
                    Zzcte = edatu,
                    Werks = praseProduct.Werks,
                    Rsnum = cboxOrder.Text,
                    Matnr = "0000000000" + praseProduct.Matnr,
                    Charg = praseProduct.Charg,
                    Meins = praseProduct.Meins,
                    Zpnum = praseProduct.Zpnum,
                    Menge = Math.Round(d, 2),
                    Zlonu = txtZlonu.Text,
                    Zzcck = txtZzcck.Text,
                    Zusrd = GlobalState.UserID
                };

                Webservice1.Zsdmsg01[] outmess = new SmartDeviceProject1.Webservice1.Zsdmsg01[10];
                item.Return = outmess;
                Zppi507Response response = GlobalState.GetWebService().Zppi507(item);
                SmartDeviceProject1.Webservice1.Zsdmsg01[] message = response.Return;
                for (int i = 0; i < message.Length; i++)
                {
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
                        _table.Rows.Add(item.Werks, item.Rsnum, item.Matnr.Substring(10, 8), item.Charg, item.Menge, item.Meins, item.Zpnum,
                            item.Zlonu, item.Zzcck, GlobalState.UserID);

                        CommonAPI.Save(_table, GetType().Name);

                        dataGrid1.DataSource = null;
                        dataGrid1.DataSource = _table;
                        txtBarcode.Text = "";

                        txtBarcode.SelectAll();
                        txtBarcode.Focus();
                        btnCancelDraw.Enabled = true;
                        btnConfirm.Enabled = true;
                        clear();
                    }


                }
                //TODO:建议客户返回点啥吧
            }
            catch (Exception ex)
            {

                CommonAPI.Error(ex, "Zppi507");
                return;
            }

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

        private void dtpEdatu_ValueChanged(object sender, EventArgs e)
        {
            dtpEdatu.Enabled = false;
        }
    }
}