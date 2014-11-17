using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartDeviceProject1.Common;
using Datalogic_Preprocessor;

namespace SmartDeviceProject1.Return
{
    public partial class PurcharseOrder : Form
    {
        private string user;
        private string factory;
        private string org;
        private string IsCheck;
        public PurcharseOrder()
        {
            InitializeComponent();
        }
        public PurcharseOrder(string username)
        {
            this.user = username;
            InitializeComponent();
        }
        //load
        private void PurcharseOrder_Load(object sender, EventArgs e)
        {
            PageInit();
            CreateGrid();
            IsCheck = GlobalState.GetConfigInfo("是否检查批次");
            factory = GlobalState.GetConfigInfo("工厂");
            org = GlobalState.GetConfigInfo("销售组织");
            txtPoint.Text = factory;
        }
        //订单返回
        private void button8_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    Packaging.Zsdi527 io = new SmartDeviceProject1.Packaging.Zsdi527();
            //    io.Itab = new SmartDeviceProject1.Packaging.Zsdi503[1];
            //    io.Ernam = user;
            //    io.Itac = itab;
            //    Packaging.Zsdi527Response response = GlobalState.GetWebServiceSD().Zsdi527(io);

            //    PageInit();
            //    ClearCache();
            //    cboxOrder.Text = "";
            //    txtStoreCroy.Text = "";
            //    txtStorePro.Text = "";
            //    cboxOrder.Enabled = true;
            //    txtDate.Enabled = true;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            this.Close();
        }
        //订单撤销
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                itab = (from DataRow row in GridDT.Rows
                        select new Packaging.Zsdi518
                        {
                            Matnr = row["物料"].ToString(),
                            Lgort = row["产品存位"].ToString(),
                            Charg = row["批次"].ToString(),
                            Zpnum = row["包"].ToString(),
                            Zmeng1 = decimal.Parse(row["每包数量"].ToString()),
                            Zmeng2 = decimal.Parse(row["包发货数量"].ToString()),
                            Zbanm = row["版面"].ToString(),
                            Ernam = row["操作ID"].ToString(),
                            Vbeln = cboxOrder.Text.Trim(),
                            Werks = row["扫码工厂"].ToString()

                        }).ToArray();
                Packaging.Zsdi527 io = new SmartDeviceProject1.Packaging.Zsdi527();
                io.Itab = new SmartDeviceProject1.Packaging.Zsdi503[1];
                io.Ernam = user;
                io.Itac = itab;
                Packaging.Zsdi527Response response = GlobalState.GetWebServiceSD().Zsdi527(io);
                ClearCache();
                cboxOrder.Text = "";
                txtStoreCroy.Text = "";
                txtStorePro.Text = "";
                cboxOrder.Enabled = true;
                txtDate.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string actualDate;//实际发货日期
        public string store;//库存点
        public string buyDoc;//采购凭证
        public string selectDate;//选择日期
        //订单确认
        private void button1_Click(object sender, EventArgs e)
        {
            string order = cboxOrder.Text.Trim();
            if (string.IsNullOrEmpty(order))
            {
                MessageBox.Show("订单不能为空！");
                return;
            }
            if (!validation.IsNumber(order) || order.Length != 10)
            {
                MessageBox.Show("采购订单有误，请输入10位整数！");
                return;
            }
            try
            {
                Packaging.Zsdi522 para = new SmartDeviceProject1.Packaging.Zsdi522();
                para.Ebeln = order;
                para.Werks = factory;
                para.Return = new SmartDeviceProject1.Packaging.Zsdmsg01[1];
                para.Edatu = DateTime.Parse(txtDate.Text).ToString("yyyyMMdd");
                Packaging.Zsdi522Response response = GlobalState.GetWebServiceSD().Zsdi522(para);
                Packaging.Zsdmsg01[] mess = response.Return;
                string str = "";
                string val = "";
                for (int i = 0; i < mess.Length; i++)
                {
                    str += mess[i].Message + "\r";
                    val += mess[i].Ztype;
                }
                if (val.Contains("E"))
                {
                    MessageBox.Show(str);
                    return;
                }
                else if (val == "W")
                {
                    if (MessageBox.Show(str, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        CommonAPI.Load(GridDT, GetType().Name);

                        //判断缓存是否符合条件
                        if (GridDT.Rows.Count > 0)
                        {
                            DataRow[] dr = GridDT.Select("采购订单='" + cboxOrder.Text.Trim() + "' and 操作ID='" + user + "'");
                            if (dr.Count() > 0)
                            {
                                if (MessageBox.Show("是否加载已保存数据！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    txtStorePro.Text = GridDT.Rows[0]["产品存位"].ToString();
                                    txtStoreCroy.Text = GridDT.Rows[0]["托盘存位"].ToString();
                                    this.dataGrid1.DataSource = GridDT;
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
                        cboxOrder.Items.Add(cboxOrder.Text);
                        actualDate = response.LEdatu;
                        store = response.LVstel;
                        buyDoc = response.LEbeln;
                        selectDate = response.LData;
                        cboxOrder.Enabled = false;
                        txtDate.Enabled = false;
                        panel2.Visible = true;
                    }
                }
                else
                {
                    CommonAPI.Load(GridDT, GetType().Name);

                    //判断缓存是否符合条件
                    if (GridDT.Rows.Count > 0)
                    {
                        DataRow[] dr = GridDT.Select("采购订单='" + cboxOrder.Text.Trim() + "' and 操作ID='" + user + "'");
                        if (dr.Count() > 0)
                        {
                            if (MessageBox.Show("是否加载已保存数据！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                txtStorePro.Text = GridDT.Rows[0]["产品存位"].ToString();
                                txtStoreCroy.Text = GridDT.Rows[0]["托盘存位"].ToString();
                                this.dataGrid1.DataSource = GridDT;
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
                    cboxOrder.Items.Add(cboxOrder.Text);
                    actualDate = response.LEdatu;
                    store = response.LVstel;
                    buyDoc = response.LEbeln;
                    selectDate = response.LData;
                    cboxOrder.Enabled = false;
                    txtDate.Enabled = false;
                    panel2.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void DeleteXML()
        {
            GridDT.Rows.Clear();
            dataGrid1.DataSource = GridDT;
            CommonAPI.Remove(this.GetType().Name);
        }


        //开始扫描
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStorePro.Text.Trim()))
            {
                MessageBox.Show("产品存位不能为空！");
                return;
            }
            if (!validation.IsNumber(txtStorePro.Text.Trim()) || txtStorePro.Text.Trim().Length != 4)
            {
                MessageBox.Show("产品存位有误，请输入4位整数！");
                return;
            }
            if (string.IsNullOrEmpty(txtStoreCroy.Text.Trim()))
            {
                MessageBox.Show("托盘/盖板存位不能为空！");
                return;
            }
            if (!validation.IsNumber(txtStorePro.Text.Trim()) || txtStorePro.Text.Trim().Length != 4)
            {
                MessageBox.Show("托盘盖板有误，请输入4位整数！");
                return;
            }
            this.tabControl1.SelectedIndex = 1;
            txtBarcode.Focus();
        }
        private string material = "";
        private string batch = "";
        private string bnum = "";
        private string amount = "";
        private string layout = "";
        private string vbeln;//销售订单号
        private string posnr;//行项目
        private string proline;
        private string totalcount;
        private string remark;
        private string date;
        private string unit;
        private string codefac;
        //条码扫描
        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                // 二维码
                string barcode = txtBarcode.Text.Trim();

                if (barcode != null)
                {

                    #region 二维码解析
                    string[] code = barcode.Replace("||", ",").Split(',');
                    if (code.Length == 15)// 成品带销售订单
                    {
                        //物料编号 
                        int ing = int.Parse(code[11].ToString());
                        material = ing.ToString("000000000000000000");
                        //物料描述
                        remark = code[0].Split(':')[1].ToString();
                        //版面
                        layout = code[1].Split(':')[1].ToString();
                        //供应商批次
                        batch = code[2].Split(':')[1].ToString();
                        //生产日期
                        date = code[7].Split(':')[1].ToString();
                        //包号
                        bnum = code[3].Split(':')[1].ToString();
                        //数量
                        amount = code[4].Split(':')[1].ToString();
                        //计量单位
                        unit = code[5].ToString();
                        //生产线
                        proline = code[6].Split(':')[1].ToString();
                        //行项目
                        posnr = code[14].ToString();
                        //销售订单
                        vbeln = code[13].ToString();
                        //工厂
                        codefac = code[12].ToString();
                    }
                    if (code.Length == 13) //成品不带销售订单
                    {
                        //物料编号 
                        int ing = int.Parse(code[11].ToString());
                        material = ing.ToString("000000000000000000");
                        //物料描述
                        remark = code[0].Split(':')[1].ToString();
                        //版面
                        layout = code[1].Split(':')[1].ToString();
                        //供应商批次
                        batch = code[2].Split(':')[1].ToString();
                        //生产日期
                        date = code[7].Split(':')[1].ToString();
                        //包号
                        bnum = code[3].Split(':')[1].ToString();
                        //数量
                        amount = code[4].Split(':')[1].ToString();
                        //计量单位
                        unit = code[5].ToString();
                        //生产线
                        proline = code[6].Split(':')[1].ToString();
                        //行项目
                        posnr = "";
                        //销售订单
                        vbeln = "";
                        //工厂
                        codefac = code[12].ToString();
                    }
                    if (code.Length == 19) //原材料
                    {
                        //物料号
                        int m = int.Parse(code[2].ToString());
                        material = m.ToString("000000000000000000");
                        //物料描述
                        remark = code[3].ToString();
                        //版面
                        layout = code[4].ToString();
                        //供应商批次
                        batch = code[5].ToString();
                        //生产日期
                        date = code[9].ToString();
                        //包号
                        bnum = code[6].ToString();
                        //数量
                        amount = code[7].ToString();
                        //单位 
                        unit = code[8].ToString();
                        //生产线
                        proline = "";
                        //行项目
                        posnr = code[1].ToString();
                        //销售订单
                        vbeln = code[0].ToString();
                        //工厂
                        codefac = factory;
                    }
                    #endregion

                    #region 判断物料是否合规

                    Packaging.Zsdi502 para = new SmartDeviceProject1.Packaging.Zsdi502();
                    para.Vstel = factory;
                    para.Matnr = material;
                    para.Zfsla = txtStorePro.Text.Trim();
                    para.Charg = batch;
                    para.Zpnum = bnum;
                    para.Zdate = date;
                    para.Zdatum = selectDate;
                    para.Edatu = actualDate;
                    para.Zbanm = layout;
                    // para.Zmeng = decimal.Parse(amount);
                    para.Zfslb = txtStoreCroy.Text.Trim();
                    para.Vbeln = vbeln;
                    Packaging.Zsdi523 io = new SmartDeviceProject1.Packaging.Zsdi523();
                    io.Ebeln = cboxOrder.Text.Trim();
                    io.Zbarcode = para;
                    Packaging.Zsdi523Response response = GlobalState.GetWebServiceSD().Zsdi523(io);
                    Packaging.Zsdmsg01 mess = response.Return;
                    string str = mess.Message;
                    if (!string.IsNullOrEmpty(str))
                    {
                        MessageBox.Show(str);
                        txtBarcode.Text = "";
                        return;

                    }
                    else
                    {
                        txtBarcode.Text = "";
                        txtMaterial.Text = material;
                        txtLayout.Text = layout;
                        txtBatch.Text = batch;
                        txtAmount.Text = amount;
                        txtPack.Text = "1";
                        totalcount = (decimal.Parse(txtPack.Text.Trim()) * decimal.Parse(amount)).ToString();//发货数量
                        txtQuantity.Text = totalcount;
                        txtBH.Text = bnum;
                        button9.Focus();
                    }

                    #endregion
                }
            }
        }
        //扫描撤销
        private void button5_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void Clear()
        {
            txtBH.Text = "";
            txtAmount.Text = "";
            txtBatch.Text = "";
            txtLayout.Text = "";
            txtMaterial.Text = "";
            txtQuantity.Text = "";
            txtPack.Text = "";
            txtBarcode.Text = "";
            txtBarcode.Focus();

        }
        //扫描返回
        private void button4_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0;
        }
        //扫描确认
        private void button9_Click(object sender, EventArgs e)
        {
            SaveScanInfo();
        }
        public DataTable GridDT
        {
            get;
            set;
        }
        private void PageInit()
        {
            GridDT = new DataTable(GetType().Name);
            GridDT.Columns.Add("物料", typeof(string));
            GridDT.Columns.Add("版面", typeof(string));
            GridDT.Columns.Add("批次", typeof(string));
            GridDT.Columns.Add("包", typeof(string));
            GridDT.Columns.Add("每包数量", typeof(string));
            GridDT.Columns.Add("包发货数量", typeof(string));
            GridDT.Columns.Add("采购订单", typeof(string));
            GridDT.Columns.Add("交货工厂", typeof(string));
            GridDT.Columns.Add("实际发货日期", typeof(string));
            GridDT.Columns.Add("产品存位", typeof(string));
            GridDT.Columns.Add("托盘存位", typeof(string));
            GridDT.Columns.Add("操作ID", typeof(string));
            GridDT.Columns.Add("销售订单", typeof(string));
            GridDT.Columns.Add("销售订单行项目", typeof(string));
            GridDT.Columns.Add("扫码工厂", typeof(string));
        }

        List<Packaging.Zsdi504> listl504 = new List<SmartDeviceProject1.Packaging.Zsdi504>();

        public Packaging.Zsdi518[] itab;

        public Packaging.Zsdi504[] l504 = new SmartDeviceProject1.Packaging.Zsdi504[25];

        public void SaveScanInfo()
        {
            try
            {
                //检查包号唯一性
                if (GridDT.Rows.Count > 0)
                {
                    DataRow[] dr = GridDT.Select("物料='" + material.Substring(10, 8) + "' and 批次='" + batch + "' and 包='" + bnum + "'");
                    if (dr.Count() > 0)
                    {
                        MessageBox.Show("包号重复扫描！");
                        Clear();
                        return;
                    }
                }
                Packaging.Zsdi518 para = new SmartDeviceProject1.Packaging.Zsdi518();
                para.Vbeln = vbeln;
                para.Matnr = material;
                para.Lgort = txtStorePro.Text.Trim();
                para.Charg = batch;
                para.Zpnum = bnum;
                para.Zmeng1 = decimal.Parse(amount);
                //实际每包发货数量
                para.Zmeng2 = decimal.Parse(totalcount);
                para.Ernam = user;
                para.Werks = codefac;
                para.Posnr = posnr;
                // para.Zbanm = layout;
                //生产线
                // para.Zline = proline;
                Packaging.Zsdi524 para2 = new SmartDeviceProject1.Packaging.Zsdi524();
                para2.Zbarcode = para;
                para2.Ernam = user;
                // para2.Ztest = IsCheck;
                para2.Ebeln = cboxOrder.Text.Trim();
                Packaging.Zsdi524Response ret = GlobalState.GetWebServiceSD().Zsdi524(para2);
                Packaging.Zsdmsg01 message = ret.Return;
                string s = message.Message;
                string val = message.Ztype;
                if (val == "E")
                {
                    MessageBox.Show(s);
                    return;
                }
                if (val == "W")
                {
                    MessageBox.Show(s);
                    string[] a = s.Split(':');
                    string str = a[1].ToString().Split('.')[0].Replace(",", "");
                    totalcount = txtQuantity.Text = str;
                    GridDT.Rows.Add(material.Substring(10, 8), layout, batch, bnum, amount, totalcount, cboxOrder.Text.Trim(), cboxOrder.Text.Trim(),
                       DateTime.Parse(txtDate.Text).ToString("yyyyMMdd"), txtStorePro.Text.Trim(), txtStoreCroy.Text.Trim(),
                       user, vbeln, posnr, codefac);
                    Packaging.Zsdi524 item = new SmartDeviceProject1.Packaging.Zsdi524();
                    item.Zbarcode = para;
                    item.Ztest = IsCheck;
                    item.Ernam = user;
                    item.Ebeln = buyDoc;
                    GlobalState.GetWebServiceSD().Zsdi524(item);
                    CommonAPI.Save(GridDT, GetType().Name);
                    // dataGrid1.DataSource = null;
                    dataGrid1.DataSource = GridDT;
                    Clear();
                }
                else
                {
                    GridDT.Rows.Add(material.Substring(10, 8), layout, batch, bnum, amount, totalcount, cboxOrder.Text.Trim(), cboxOrder.Text.Trim(),
                       DateTime.Parse(txtDate.Text).ToString("yyyyMMdd"), txtStorePro.Text.Trim(), txtStoreCroy.Text.Trim(),
                       user, vbeln, posnr, codefac);
                    CommonAPI.Save(GridDT, GetType().Name);
                    // dataGrid1.DataSource = null;
                    dataGrid1.DataSource = GridDT;
                    Clear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //更改包
        private void txtPack_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtPack.Text != "")
                {
                    string bao = txtPack.Text.Trim();
                    string sl = txtAmount.Text.Trim();
                    totalcount = txtQuantity.Text = (decimal.Parse(bao) * decimal.Parse(sl)).ToString();
                    //
                    SaveScanInfo();
                }
            }
        }
        //修改包发货数量
        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (!validation.IsNumber(txtQuantity.Text.Trim()))
                {
                    MessageBox.Show("发货数量必须为整数！");
                    return;
                }
                totalcount = txtQuantity.Text.Trim();
                int up = int.Parse(totalcount);
                int ol = int.Parse(txtAmount.Text.Trim());
                if (up > ol)
                {
                    MessageBox.Show("实际发货数量不能大于每包数量！");
                    return;
                }
                //验证发货数量
                SaveScanInfo();
            }
        }
        //发货明细返回
        private void button10_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 1;
        }
        //发货明细撤销
        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                itab = (from DataRow row in GridDT.Rows
                        select new Packaging.Zsdi518
                        {
                            Matnr = "0000000000" + row["物料"].ToString(),
                            Lgort = row["产品存位"].ToString(),
                            Charg = row["批次"].ToString(),
                            Zpnum = row["包"].ToString(),
                            Zmeng1 = decimal.Parse(row["每包数量"].ToString()),
                            Zmeng2 = decimal.Parse(row["包发货数量"].ToString()),
                            Zbanm = row["版面"].ToString(),
                            Ernam = row["操作ID"].ToString(),
                            Vbeln = cboxOrder.Text.Trim(),
                            Werks = row["扫码工厂"].ToString()

                        }).ToArray();
                Packaging.Zsdi527 io = new SmartDeviceProject1.Packaging.Zsdi527();
                io.Itac = itab;
                io.Ernam = user;
                io.Itab = new SmartDeviceProject1.Packaging.Zsdi503[1];
                Packaging.Zsdi527Response response = GlobalState.GetWebServiceSD().Zsdi527(io);
                ClearCache();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        Packaging.Zsdi504[] message;
        //发货明细提交 
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                itab = (from DataRow row in GridDT.Rows
                        select new Packaging.Zsdi518
                        {
                            Matnr = "0000000000" + row["物料"].ToString(),
                            Lgort = row["产品存位"].ToString(),
                            Charg = row["批次"].ToString(),
                            Zpnum = row["包"].ToString(),
                            Zmeng1 = decimal.Parse(row["每包数量"].ToString()),
                            Zmeng2 = decimal.Parse(row["包发货数量"].ToString()),
                            Zbanm = row["版面"].ToString(),
                            Ernam = row["操作ID"].ToString(),
                            Vbeln = row["销售订单"].ToString(),
                            Posnr = row["销售订单行项目"].ToString(),
                            Werks = row["扫码工厂"].ToString()
                        }).ToArray();
                Packaging.Zsdi525 io = new SmartDeviceProject1.Packaging.Zsdi525();
                io.Ebeln = cboxOrder.Text.Trim();
                io.Lgort = txtStoreCroy.Text.Trim();
                io.Itab = new SmartDeviceProject1.Packaging.Zsdi504[1];
                Packaging.Zsdi525Response response = GlobalState.GetWebServiceSD().Zsdi525(io);
                message = response.Itab;
                if (message.Length > 0)
                {
                    for (int k = 0; k < message.Length; k++)
                    {
                        DataRow dr = DataGrid.NewRow();
                        dr["行项目"] = message[k].Posnr;
                        dr["描述"] = message[k].Maktx;
                        dr["托盘/盖板存位"] = message[k].Zlgort;
                        dr["数量"] = message[k].Menge;
                        DataGrid.Rows.Add(dr);
                    }
                    this.tabControl1.SelectedIndex = 3;
                }
                else
                {
                    this.tabControl1.SelectedIndex = 4;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public DataTable DataGrid
        {
            get;
            set;
        }
        private void CreateGrid()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("行项目", "行项目");
            dic.Add("描述", "描述");
            dic.Add("托盘/盖板存位", "托盘/盖板存位");
            dic.Add("数量", "数量");
            DataGrid = CreateRow(dic);

            this.dataGrid2.DataSource = DataGrid;
        }
        /// <summary>
        ///  初始化 DT列
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public DataTable CreateRow(Dictionary<string, string> dic)
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
        //盖板返回
        private void button12_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 2;
        }
        //交货单提交
        private void button7_Click(object sender, EventArgs e)
        {
            listl504.CopyTo(l504, 0);
            string name = textBox11.Text.Trim();
            string carnum = textBox10.Text.Trim();
            string phone = textBox9.Text.Trim();
            string id = textBox8.Text.Trim();
            string remark = textBox7.Text.Trim();
            string isgz = "";
            if (cboxY.Checked)
            {
                isgz = "X";
            }
            else
            {
                isgz = "";

            }
            // 交货单信息至SAP

            // 输入参数
            try
            {
                Packaging.Zsdi526 io = new SmartDeviceProject1.Packaging.Zsdi526();
                io.Ebeln = cboxOrder.Text.Trim();
                io.Vstel = factory;
                //过账日期
                io.Datum = (DateTime.Parse(txtDate.Text)).ToString("yyyyMMdd"); ;
                io.Ztext2 = textBox11.Text.Trim();
                io.Ztext3 = textBox10.Text.Trim();
                io.Ztext4 = textBox9.Text.Trim();
                io.Ztext5 = textBox8.Text.Trim();
                io.Ztext6 = textBox7.Text.Trim();
                io.Zmove = isgz;
                io.Ernam = user;
                //输入itab表
                io.Itab = itab;

                // 输入l_zsdi504表
                io.LZsdi504 = l504;

                Packaging.Zsdi526Response response = GlobalState.GetWebServiceSD().Zsdi526(io);
                string error = response.Return.Message;
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(error);
                }
                ClearCache();
                this.tabControl1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zsdi526");
            }
        }
        //
        public void ClearCache()
        {
            DeleteXML();
            CreateGrid();
            listl504.Clear();
            //txtStoreCroy.Text = "";
            //txtStorePro.Text = "";
            textBox11.Text = "";
            textBox10.Text = "";
            textBox9.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
        }
        //交货单返回
        private void button6_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 3;
        }
        private int index = 0;
        //托盘修改
        private void button17_Click(object sender, EventArgs e)
        {
            if (!validation.IsNumber(txtQty.Text.Trim()))
            {
                MessageBox.Show("数量必须为整数！");
                return;
            }
            CreateGrid();

            for (int i = 0; i < message.Length; i++)
            {
                if (index == i)
                {
                    DataRow dr = DataGrid.NewRow();
                    dr["行项目"] = txtItem.Text.Trim();
                    dr["描述"] = txtDes.Text.Trim();
                    dr["托盘/盖板存位"] = txtStore.Text.Trim();
                    dr["数量"] = txtQty.Text.Trim();
                    DataGrid.Rows.Add(dr);
                    message[i].Zlgort = txtStore.Text.Trim();
                    message[i].Menge = int.Parse(txtQty.Text.Trim());
                }
                else
                {
                    DataRow dr = DataGrid.NewRow();
                    dr["行项目"] = message[i].Posnr;
                    dr["描述"] = message[i].Maktx;
                    dr["托盘/盖板存位"] = message[i].Zlgort;
                    dr["数量"] = message[i].Menge;
                    DataGrid.Rows.Add(dr);
                }
            }
        }
        //托盘确认
        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)dataGrid2.DataSource;
                foreach (DataRow dr in dt.Rows)
                {
                    Packaging.Zsdi504 item = new SmartDeviceProject1.Packaging.Zsdi504();
                    item.Maktx = dr[1].ToString();
                    item.Vbeln = cboxOrder.Text.Trim();
                    item.Zlgort = dr[2].ToString();
                    item.Posnr = dr[0].ToString();
                    item.Menge = decimal.Parse(dr[3].ToString());
                    listl504.Add(item);
                }
                this.tabControl1.SelectedIndex = 4;
            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zsdi504");
            }
        }
        //托盘返回
        private void button16_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 2;
        }

        private void cboxY_Click(object sender, EventArgs e)
        {
            if (cboxY.Checked)
            {
                cboxN.Checked = false;
            }
        }

        private void cboxN_Click(object sender, EventArgs e)
        {
            if (cboxN.Checked)
            {
                cboxY.Checked = false;
            }
        }
    }
}