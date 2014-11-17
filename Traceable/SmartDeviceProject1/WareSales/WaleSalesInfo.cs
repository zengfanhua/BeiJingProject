using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SmartDeviceProject1.Common;
using Datalogic_Preprocessor;

namespace SmartDeviceProject1.WareSales
{
    public partial class WaleSalesInfo : Form
    {
        public WaleSalesInfo()
        {
            InitializeComponent();
        }

        private string user = "";
        //选择日期
        private string selectdate = "";
        private string IsCheck;
        public WaleSalesInfo(string userName)
        {
            this.user = userName;
            InitializeComponent();
        }
        // 确认订单
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboxOrder.Text.Trim()))
            {
                MessageBox.Show("销售订单不能为空!");
                return;
            }
            if (!validation.IsNumber(cboxOrder.Text) || cboxOrder.Text.Length != 10)
            {
                MessageBox.Show("销售订单有误，请输入10位整数！");
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                MessageBox.Show("装运地点不能为空!");
                return;
            }
            string order = cboxOrder.Text.Trim();
            string address = textBox2.Text.Trim();
            string date = dateTimePicker1.Text.Trim();
            //验证订单
            try
            {
                Packaging.Zsdi507 io = new SmartDeviceProject1.Packaging.Zsdi507();
                io.Vbeln = order;
                io.Vkorg = org;
                io.Vstel = address;
                io.Edatu = DateTime.Parse(date).ToString("yyyyMMdd");
                Packaging.Zsdmsg01[] outmess = new Packaging.Zsdmsg01[10];
                io.Return = outmess;
                Packaging.Zsdi507Response response = GlobalState.GetWebServiceSD().Zsdi507(io);
                Packaging.Zsdmsg01[] message = response.Return;
                string str = "";
                string val = "";
                for (int i = 0; i < message.Length; i++)
                {
                    str += message[i].Message.ToString() + "\r";
                    val += message[i].Ztype;
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
                            DataRow[] dr = GridDT.Select("订单='" + cboxOrder.Text.Trim() + "' and 操作ID='" + user + "'");
                            if (dr.Count() > 0)
                            {
                                if (MessageBox.Show("是否加载已保存数据！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    textBox4.Text = GridDT.Rows[0]["产品存位"].ToString();
                                    textBox5.Text = GridDT.Rows[0]["托盘存位"].ToString();
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
                        selectdate = response.LDate;
                        cboxOrder.Enabled = false;
                        dateTimePicker1.Enabled = false;
                        panel2.Visible = true;
                    }
                }
                else
                {
                    CommonAPI.Load(GridDT, GetType().Name);

                    //判断缓存是否符合条件
                    if (GridDT.Rows.Count > 0)
                    {
                        DataRow[] dr = GridDT.Select("订单='" + cboxOrder.Text.Trim() + "' and 操作ID='" + user + "'");
                        if (dr.Count() > 0)
                        {
                            if (MessageBox.Show("是否加载已保存数据！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                textBox4.Text = GridDT.Rows[0]["产品存位"].ToString();
                                textBox5.Text = GridDT.Rows[0]["托盘存位"].ToString();
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
                    selectdate = response.LDate;
                    cboxOrder.Enabled = false;
                    dateTimePicker1.Enabled = false;
                    panel2.Visible = true;
                }
            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zsdi507");
            }
        }

        public void DeleteXML()
        {
            GridDT.Rows.Clear();
            dataGrid1.DataSource = GridDT;
            CommonAPI.Remove(this.GetType().Name);
        }

        // 开始扫描
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text.Trim()))
            {
                MessageBox.Show("产品存位不能为空！");
                return;
            }
            if (!validation.IsNumber(textBox4.Text.Trim()) || textBox4.Text.Trim().Length != 4)
            {
                MessageBox.Show("产品存位有误，请输入4位整数！");
                return;
            }
            if (string.IsNullOrEmpty(textBox5.Text.Trim()))
            {
                MessageBox.Show("托盘/盖板存位不能为空！");
                return;
            }
            if (!validation.IsNumber(textBox5.Text.Trim()) || textBox5.Text.Trim().Length != 4)
            {
                MessageBox.Show("托盘盖板有误，请输入4位整数！");
                return;
            }
            this.tabControl1.SelectedIndex = 1;
            textBox3.Focus();
        }

        // 订单撤销
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                itab = (from DataRow row in GridDT.Rows
                        select new Packaging.Zsdi503
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
                            Werks = factory
                        }).ToArray();
                Packaging.Zsdi527 io = new SmartDeviceProject1.Packaging.Zsdi527();
                io.Itab = itab;
                io.Ernam = user;
                io.Itac = new SmartDeviceProject1.Packaging.Zsdi518[1];
                Packaging.Zsdi527Response response = GlobalState.GetWebServiceSD().Zsdi527(io);
                ClearCache();
                cboxOrder.Text = "";
                textBox5.Text = "";
                textBox4.Text = "";
                cboxOrder.Enabled = true;
                dateTimePicker1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //扫描返回
        private void button4_Click_1(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0;
        }

        // 交货单返回
        private void button6_Click_1(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 3;
        }
        public DataTable GridDT
        {
            get;
            set;
        }
        private void PageInit()
        {
            GridDT = new DataTable(GetType().Name);
            GridDT.Columns.Add("订单", typeof(string));
            GridDT.Columns.Add("物料", typeof(string));
            GridDT.Columns.Add("版面", typeof(string));
            GridDT.Columns.Add("批次", typeof(string));
            GridDT.Columns.Add("包", typeof(string));
            GridDT.Columns.Add("每包数量", typeof(string));
            GridDT.Columns.Add("包发货数量", typeof(string));
            GridDT.Columns.Add("交货工厂", typeof(string));
            GridDT.Columns.Add("实际发货日期", typeof(string));
            GridDT.Columns.Add("产品存位", typeof(string));
            GridDT.Columns.Add("托盘存位", typeof(string));
            GridDT.Columns.Add("操作ID", typeof(string));
            GridDT.Columns.Add("销售订单", typeof(string));
            GridDT.Columns.Add("销售订单行项目", typeof(string));
            GridDT.Columns.Add("扫码工厂", typeof(string));
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
        // 二维码扫描
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Return)
            {
                // 二维码
                string barcode = textBox3.Text.Trim();

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
                        //工 厂
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

                    Packaging.Zsdi502 io = new SmartDeviceProject1.Packaging.Zsdi502();
                    io.Vbeln = vbeln;
                    //销售订单项目号
                    io.Posnr = posnr;
                    io.Vstel = textBox2.Text.Trim();
                    io.Matnr = material;
                    io.Zfsla = textBox4.Text.Trim();
                    io.Charg = batch;
                    io.Zpnum = bnum;
                    io.Zdate = date;
                    //选择日期
                    io.Zdatum = selectdate;
                    io.Edatu = DateTime.Parse(dateTimePicker1.Text).ToString("yyyyMMdd");
                    io.Zbanm = layout;
                    //  io.Zmeng = decimal.Parse(amount);
                    io.Zfslb = textBox5.Text.Trim();
                    io.Zmein = unit;
                    //生产线
                    io.Zline = proline;
                    io.Maktx = remark;
                    Packaging.Zsdi5021 io2 = new SmartDeviceProject1.Packaging.Zsdi5021();
                    io2.Zbarcode = io;
                    io2.Zcheck = IsCheck;
                    io2.Vbeln = cboxOrder.Text.Trim();
                    Packaging.Zsdi502Response response = GlobalState.GetWebServiceSD().Zsdi502(io2);

                    Packaging.Zsdmsg01 mess = response.Return;
                    string str = mess.Message;
                    string val = mess.Ztype;
                    if (!string.IsNullOrEmpty(val) && val == "E")
                    {
                        MessageBox.Show(str);
                        textBox3.Text = "";
                        return;
                    }
                    if (!string.IsNullOrEmpty(val) && val == "W")
                    {
                        MessageBox.Show(str);
                        textBox3.Text = "";
                        textBox12.Text = material;
                        textBox13.Text = layout;
                        textBox14.Text = batch;
                        textBox16.Text = amount;
                        textBox15.Text = "1";
                        totalcount = (decimal.Parse(textBox15.Text.Trim()) * decimal.Parse(amount)).ToString();//发货数量
                        textBox17.Text = totalcount;
                        textBox18.Text = bnum;
                        button5.Focus();
                    }
                    else
                    {
                        textBox3.Text = "";
                        textBox12.Text = material;
                        textBox13.Text = layout;
                        textBox14.Text = batch;
                        textBox16.Text = amount;
                        textBox15.Text = "1";
                        totalcount = (decimal.Parse(textBox15.Text.Trim()) * decimal.Parse(amount)).ToString();//发货数量
                        textBox17.Text = totalcount;
                        textBox18.Text = bnum;
                        this.button11.Focus();
                    }

                    #endregion
                }
            }
        }
        public string jhdh;
        //确认提交 
        private void button7_Click_1(object sender, EventArgs e)
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
                Packaging.Zsdi510 io = new SmartDeviceProject1.Packaging.Zsdi510();
                io.Vbeln = cboxOrder.Text.Trim();
                io.Vstel = textBox2.Text.Trim();
                io.Ztext2 = textBox11.Text.Trim();
                io.Ztext3 = textBox10.Text.Trim();
                io.Ztext4 = textBox9.Text.Trim();
                io.Ztext5 = textBox8.Text.Trim();
                io.Ztext6 = textBox7.Text.Trim();
                //过账日期
                io.Datum = (DateTime.Parse(dateTimePicker1.Text)).ToString("yyyyMMdd");
                io.Zmove = isgz;
                io.Ernam = user;
                //输入itab表
                io.Itab = itab;

                // 输入l_zsdi504表
                io.LZsdi504 = l504;

                Packaging.Zsdi510Response response = GlobalState.GetWebServiceSD().Zsdi510(io);
                string error = response.Return.Message;
                jhdh = "";
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(error);
                }
                ClearCache();
                this.tabControl1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zsdi510");
            }
        }
        private string factory;
        private string org;

        private void WaleSalesInfo_Load(object sender, EventArgs e)
        {
            PageInit();
            CreateGrid();
            this.factory = GlobalState.GetConfigInfo("工厂");
            this.org = GlobalState.GetConfigInfo("销售组织");
            this.IsCheck = GlobalState.GetConfigInfo("是否检查批次");

            textBox2.Text = factory;
        }
        //订单返回
        private void button8_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Packaging.Zsdi527 io = new SmartDeviceProject1.Packaging.Zsdi527();
            //    io.Itab = itab;
            //    io.Ernam = user;
            //    Packaging.Zsdi518[] item = new SmartDeviceProject1.Packaging.Zsdi518[20];
            //    io.Itac = item;
            //    GlobalState.GetWebServiceSD().Zsdi527(io);
            //    pack.Clear();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            this.Close();
        }
        //扫描返回
        private void button10_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0;
        }
        //清空
        private void button9_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            textBox18.Text = "";
            textBox3.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            textBox3.Focus();
        }
        //扫描确认
        private void button11_Click(object sender, EventArgs e)
        {
            SaveScanInfo();
        }
        public string IsClear;
        //
        public void SaveScanInfo()
        {
            try
            {
                //检查包号唯一性
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
                Packaging.Zsdi503 para = new SmartDeviceProject1.Packaging.Zsdi503();
                Packaging.Zsdi503 tem = new SmartDeviceProject1.Packaging.Zsdi503();
                tem.Werks = codefac;
                tem.Posnr = posnr;
                tem.Vbeln = cboxOrder.Text.Trim();
                tem.Matnr = material;
                tem.Lgort = textBox4.Text.Trim();
                tem.Charg = batch;
                tem.Zpnum = bnum;
                tem.Zmeng1 = decimal.Parse(amount);
                //实际每包发货数量
                tem.Zmeng2 = decimal.Parse(totalcount);
                tem.Zbanm = layout;
                //生产线
                tem.Zline = proline;
                para.Vbeln = vbeln;
                para.Werks = factory;
                para.Posnr = posnr;
                if (vbeln != "")
                {
                    para.Vbeln = vbeln;
                }
                para.Matnr = material;
                para.Lgort = textBox4.Text.Trim();
                para.Charg = batch;
                para.Zpnum = bnum;
                para.Zmeng1 = decimal.Parse(amount);
                //实际每包发货数量
                para.Zmeng2 = decimal.Parse(totalcount);
                para.Zbanm = layout;
                //生产线
                para.Zline = proline;
                Packaging.Zsdi5081 para2 = new SmartDeviceProject1.Packaging.Zsdi5081();
                para2.Zbarcode = para;
                para2.Ernam = user;
                // para2.Ztest = IsCheck;
                para2.Vbeln = cboxOrder.Text.Trim();
                para2.Datum = DateTime.Parse(dateTimePicker1.Text).ToString("yyyyMMdd");
                Packaging.Zsdi508Response ret = GlobalState.GetWebServiceSD().Zsdi508(para2);
                Packaging.Zsdmsg01 message = ret.Return;
                Packaging.Zsdi508 info = ret.Zmtont;
                if (info != null && info.Menge > 0)
                {
                    string s1 = info.Vbeln;
                    string s2 = info.Posnr;
                    string s3 = info.Zban1;
                    string s4 = info.Vgbel;
                    string s5 = info.Vgpos;
                    string s6 = info.Zban2;
                    string s7 = info.Menge.ToString();
                    Zmtont zmtont = new Zmtont(s1, s2, s3, s4, s5, s6, s7, "WaleSalesInfo");
                    zmtont.Owner = this;
                    zmtont.ShowDialog();
                    if (IsClear == "是")
                    {

                        pack.Add(tem);
                        Packaging.Zsdi527 io = new SmartDeviceProject1.Packaging.Zsdi527();
                        io.Itab = pack.ToArray();
                        io.Ernam = user;
                        io.Itac = new SmartDeviceProject1.Packaging.Zsdi518[1];
                        GlobalState.GetWebServiceSD().Zsdi527(io);
                        Clear();
                        return;
                    }
                }
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
                    totalcount = textBox17.Text = str.Trim();
                    //para.Zmeng2 = decimal.Parse(totalcount);
                    //GridDT.Rows.Add(cboxOrder.Text.Trim(), material.Substring(10, 8), layout, batch, bnum, amount, totalcount, textBox2.Text.Trim(),
                    //    DateTime.Parse(dateTimePicker1.Text).ToString("yyyyMMdd"), textBox4.Text.Trim(), textBox5.Text.Trim(),
                    //    user, vbeln, posnr, codefac);
                    //Packaging.Zsdi5081 item = new SmartDeviceProject1.Packaging.Zsdi5081();
                    //para.Zmeng2 = decimal.Parse(str);
                    //item.Zbarcode = para;
                    //item.Datum = DateTime.Parse(dateTimePicker1.Text).ToString("yyyyMMdd");
                    //item.Ernam = user;
                    //item.Ztest = IsCheck;
                    //item.Vbeln = cboxOrder.Text.Trim();
                    //GlobalState.GetWebServiceSD().Zsdi508(item);
                    //CommonAPI.Save(GridDT, GetType().Name);
                    //// dataGrid1.DataSource = null;
                    //dataGrid1.DataSource = GridDT;
                    //pack.Add(tem);
                    //Clear();
                }
                else
                {
                    GridDT.Rows.Add(cboxOrder.Text.Trim(), material.Substring(10, 8), layout, batch, bnum, amount, totalcount, textBox2.Text.Trim(),
                        DateTime.Parse(dateTimePicker1.Text).ToString("yyyyMMdd"), textBox4.Text.Trim(), textBox5.Text.Trim(),
                        user, vbeln, posnr, codefac);
                    CommonAPI.Save(GridDT, GetType().Name);
                    // dataGrid1.DataSource = null;
                    dataGrid1.DataSource = GridDT;
                    pack.Add(tem);
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //扫描明细返回
        private void button4_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 1;
        }

        List<Packaging.Zsdi503> listpack = new List<SmartDeviceProject1.Packaging.Zsdi503>();
        List<Packaging.Zsdi503> pack = new List<SmartDeviceProject1.Packaging.Zsdi503>();
        public Packaging.Zsdi503[] itab = new SmartDeviceProject1.Packaging.Zsdi503[25];
        public Packaging.Zsdi503[] tab = new SmartDeviceProject1.Packaging.Zsdi503[25];
        public Packaging.Zsdi504[] l504 = new SmartDeviceProject1.Packaging.Zsdi504[25];
        Packaging.Zsdi504[] message;
        //扫描完成
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                pack.CopyTo(tab, 0);
                itab = (from DataRow row in GridDT.Rows
                        select new Packaging.Zsdi503
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
                Packaging.Zsdi509 io = new SmartDeviceProject1.Packaging.Zsdi509();
                io.Itac = itab;
                io.Vbeln = cboxOrder.Text.Trim();
                io.Lgort = textBox5.Text.Trim();
                io.Ernam = user;
                Packaging.Zsdi504[] outmess = new SmartDeviceProject1.Packaging.Zsdi504[20];
                io.Itab = outmess;
                Packaging.Zsdi509Response response = GlobalState.GetWebServiceSD().Zsdi509(io);
                // Packaging.Zsdi504[] message = response.Itab;
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
        //3
        private void button12_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 2;

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

        List<Packaging.Zsdi504> listl504 = new List<SmartDeviceProject1.Packaging.Zsdi504>();

        //4
        private void button16_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 2;
        }
        //修改包
        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (textBox15.Text != "")
                {
                    string bao = textBox15.Text.Trim();
                    string sl = textBox16.Text.Trim();
                    totalcount = textBox17.Text = (decimal.Parse(bao) * decimal.Parse(sl)).ToString();
                    //
                    SaveScanInfo();
                }
            }
        }
        //修改包发货数量
        private void textBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                totalcount = textBox17.Text.Trim();
                int up = int.Parse(textBox17.Text.Trim());
                int ol = int.Parse(textBox16.Text.Trim());
                if (up > ol)
                {
                    MessageBox.Show("实际发货数量不能大于每包数量！");
                    return;
                }
                //验证发货数量
                SaveScanInfo();
            }
        }
        //发货明细撤销
        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                itab = (from DataRow row in GridDT.Rows
                        select new Packaging.Zsdi503
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
                            Werks = factory
                        }).ToArray();
                pack.CopyTo(tab, 0);
                Packaging.Zsdi527 io = new SmartDeviceProject1.Packaging.Zsdi527();
                io.Itab = tab;
                io.Ernam = user;
                io.Itac = new SmartDeviceProject1.Packaging.Zsdi518[1];
                Packaging.Zsdi527Response response = GlobalState.GetWebServiceSD().Zsdi527(io);
                pack.Clear();
                ClearCache();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //
        public void ClearCache()
        {
            CreateGrid();
            pack.Clear();
            listl504.Clear();
            textBox11.Text = "";
            textBox10.Text = "";
            textBox9.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
            txtDes.Text = "";
            txtItem.Text = "";
            txtQty.Text = "";
            txtStore.Text = "";
            DeleteXML();
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
        private int index = 0;
        //修改托盘盖板
        private void dataGrid2_DoubleClick(object sender, EventArgs e)
        {
            index = dataGrid2.CurrentRowIndex;
            txtItem.Text = dataGrid2[dataGrid2.CurrentRowIndex, 0].ToString();
            txtDes.Text = dataGrid2[dataGrid2.CurrentRowIndex, 1].ToString();
            txtStore.Text = dataGrid2[dataGrid2.CurrentRowIndex, 2].ToString();
            txtQty.Text = dataGrid2[dataGrid2.CurrentRowIndex, 3].ToString();
        }
        //修改
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
        //确认托盘盖板
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

    }
}