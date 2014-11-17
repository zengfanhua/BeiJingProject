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

namespace SmartDeviceProject1.SingleSales
{
    public partial class SaleNoForm : Form
    {
        private string user = "";
        private string factory;
        private string org;
        private string function;
        private string rule;
        private int SerialNum = 1;
        public SaleNoForm()
        {
            InitializeComponent();
        }

        public SaleNoForm(string userName, string fun)
        {
            this.function = fun;
            this.rule = function.Substring(3, 1);
            this.user = userName;
            InitializeComponent();
        }

        //开始扫描
        private void button3_Click(object sender, EventArgs e)
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
        //存位撤销
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string scanDate;
        private string scanTime;
        List<Packaging.Zsdi521I> listpack = new List<SmartDeviceProject1.Packaging.Zsdi521I>();

        List<Packaging.Zsdi504> listl504 = new List<SmartDeviceProject1.Packaging.Zsdi504>();
        public Packaging.Zsdi521I[] itab = new SmartDeviceProject1.Packaging.Zsdi521I[25];

        public Packaging.Zsdi504[] l504 = new SmartDeviceProject1.Packaging.Zsdi504[25];

        //扫描确认
        private void button5_Click(object sender, EventArgs e)
        {
            saveScan();
        }
        //保存扫描iinfo
        public void saveScan()
        {
            try
            {
                //检查包号唯一性
                if (listpack.Count > 0)
                {
                    for (int i = 0; i < listpack.Count; i++)
                    {
                        string bh = listpack[i].Zpnum;
                        string pc = listpack[i].Charg;
                        string wl = listpack[i].Matnr;
                        if (bh == txtBnum.Text.Trim() && pc == txtBatch.Text.Trim() && wl == txtMaterial.Text.Trim())
                        {
                            MessageBox.Show("包号重复扫描！");
                            Clear();
                            return;
                        }
                    }
                }
                scanDate = DateTime.Now.ToString("yyyyMMdd");
                scanTime = DateTime.Now.ToString("HHmmss");
                Packaging.Zsdi521I para = new SmartDeviceProject1.Packaging.Zsdi521I();
                para.Znumb = "A" + rule + SerialNum.ToString("00000000");//流水码?
                para.Maktx = remark;
                para.Werks = factory;
                para.Matnr = material;
                para.Lgort = textBox4.Text.Trim();
                para.Charg = batch;
                para.Zpnum = bnum;
                para.Zmeng1 = decimal.Parse(amount);
                //实际每包发货数量
                para.Zmeng2 = decimal.Parse(totalcount);
                para.Zbanm = layout;
                para.Datum = date;//日期?
                para.Ernam = user;
                para.Zline = proline;
                para.Meins = unit;
                DataRow row = DeliveryTable.NewRow();
                row["物料"] = material.Substring(10, 8);
                row["版面"] = layout;
                row["批次"] = batch;
                row["包"] = bnum;
                row["每包数量"] = amount;
                row["包发货数量"] = totalcount;
                DeliveryTable.Rows.Add(row);

                listpack.Add(para);
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            txtPckage.Text = "";
            txtMaterial.Text = "";
            txtShipnum.Text = "";
            txtLayout.Text = "";
            txtBnum.Text = "";
            txtAmount.Text = "";
            txtBatch.Text = "";
            textBox3.Text = "";
            textBox3.Focus();
        }

        //扫描返回
        private void button4_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0;
        }
        //交货单返回
        private void button6_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 3;
        }
        private string customer;
        private string address;
        private string receUser;
        private string mobile;
        private string carUser;
        private string carNum;
        private string carPhone;
        private string carID;
        private string jhtt;

        public DataTable dt;
        //交货单提交
        private void button7_Click(object sender, EventArgs e)
        {

            listpack.CopyTo(itab, 0);
            //listl504.CopyTo(l504, 0);

            customer = textBox6.Text.Trim();//客户名称
            address = textBox12.Text.Trim();//送货地址
            mobile = textBox14.Text.Trim();//收货人电话
            receUser = textBox13.Text.Trim();//收货人姓名
            carUser = textBox11.Text.Trim();//运输司机名称
            carNum = textBox10.Text.Trim();//运输车号
            carPhone = textBox9.Text.Trim();//运输司机电话
            carID = textBox8.Text.Trim();//运输司机身份证
            jhtt = textBox7.Text.Trim();//交货抬头备注

            Packaging.Zsdi521 para = new SmartDeviceProject1.Packaging.Zsdi521();
            para.Znumb = "A" + rule + SerialNum.ToString("00000000");
            para.Ernam = user;
            para.Zbame = function;
            para.Werks = factory;
            para.Vkorg = org;
            para.Vstel = factory;
            para.Name1 = customer;
            para.Street = address;
            para.Zname1 = receUser;
            para.TelNumber = mobile;
            para.Zname2 = carUser;
            para.Znume = carNum;
            para.Ztelph = carPhone;
            para.Zicad = carID;
            para.Ztext = jhtt;
            para.Datum = scanDate;
            para.Ztime = scanTime;
            para.LItab = itab;
            try
            {
                Packaging.Zsdi521Response response = GlobalState.GetWebServiceSD().Zsdi521(para);
                string order = response.Zznum;
                if (string.IsNullOrEmpty(order))
                {
                    MessageBox.Show("提交失败！");
                    return;
                }
                MessageBox.Show("提交成功！流水码：" + order);
                SerialNum += 1;
                dt.Rows.Add(SerialNum);
                CommonAPI.Save(dt, "rule" + rule);
                listpack.Clear();
                PageInit();
                textBox1.Text = "";
                textBox2.Text = "";
                this.tabControl1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string material = "";//物料编号
        private string batch = "";//批次
        private string bnum = "";//包号
        private string amount = "";//包数量
        private string layout = "";//版面
        private string remark = "";//物料名称
        private string unit = "";//单位 
        private string date = "";//生产日期
        private string proline = "";//生产线
        private string package = "";//包
        private string shipquantity = "";//包发货数量


        private string packaging;//包
        private string delivercount;//发货数量
        private string totalcount;
        private string posnr;
        private string vbeln;

        // 扫描二维码
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                // 二维码
                string barcode = textBox3.Text.Trim();

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
                }
                #endregion

                txtMaterial.Text = material;
                txtBatch.Text = batch;
                txtBnum.Text = bnum;
                txtLayout.Text = layout;
                txtAmount.Text = amount;
                txtPckage.Text = "1";
                totalcount = (decimal.Parse(txtPckage.Text.Trim()) * decimal.Parse(amount)).ToString();//发货数量
                txtShipnum.Text = totalcount;
                button5.Focus();
            }

        }
        //page_load
        private void SaleNoForm_Load(object sender, EventArgs e)
        {
            dt = new DataTable("rule" + rule);
            dt.Columns.Add("规则", typeof(string));
            if (function == "4002")
            {
                this.Text = "按库发货-无单据";
            }
            if (function == "4004")
            {

                this.Text = "按单发货-无单据";
            }
            if (function == "4006")
            {

                this.Text = "调拨发货-无单据";
            }
            if (function == "4009")
            {

                this.Text = "退货收货-无单据";
            }
            if (CommonAPI.Load(dt, "rule" + rule))
            {
                SerialNum = int.Parse(dt.Rows[0]["规则"].ToString());
            }
            dt.Rows.Clear();
            CommonAPI.Remove("rule" + SerialNum);
            factory = GlobalState.GetConfigInfo("工厂");
            org = GlobalState.GetConfigInfo("销售组织");
            PageInit();
        }

        public DataTable GridDT
        {
            get;
            set;
        }
        public DataTable GridTable
        {
            get;
            set;
        }
        public DataTable DeliveryTable
        {
            get;
            set;
        }
        private void PageInit()
        {
            Dictionary<string, string> delivery = new Dictionary<string, string>();
            delivery.Add("物料", "物料");
            delivery.Add("版面", "版面");
            delivery.Add("批次", "批次");
            delivery.Add("包", "包");
            delivery.Add("每包数量", "每包数量");
            delivery.Add("包发货数量", "包发货数量");
            DeliveryTable = CreateInitDT(delivery);
            this.dataGrid3.DataSource = DeliveryTable;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("托盘/盖板", "托盘/盖板");
            dic.Add("描述", "描述");
            dic.Add("数量", "数量");
            GridDT = CreateInitDT(dic);
            GridTable = CreateInitDT(dic);
            this.dataGrid1.DataSource = GridDT;
            this.dataGrid2.DataSource = GridTable;
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
        //新增托盘
        private void button1_Click(object sender, EventArgs e)
        {
            Packaging.Zsdi504 item = new SmartDeviceProject1.Packaging.Zsdi504();
            string remark = textBox1.Text.Trim();
            string count = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(remark))
            {
                MessageBox.Show("请输入描述！");
                return;
            }
            if (string.IsNullOrEmpty(count))
            {
                MessageBox.Show("请输入数量！");
                return;
            }
            if (!validation.IsNumber(textBox2.Text.Trim()))
            {
                MessageBox.Show("数量必须为整数！");
                return;
            }
            DataRow row = GridDT.NewRow();

            row["托盘/盖板"] = "托盘";
            row["描述"] = remark;
            row["数量"] = count;
            GridDT.Rows.Add(row);
            item.Maktx = textBox1.Text.Trim();
            item.Menge = decimal.Parse(textBox2.Text.Trim());
            listl504.Add(item);
        }
        /// <summary>
        /// 新增盖板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            Packaging.Zsdi504 tem = new SmartDeviceProject1.Packaging.Zsdi504();
            string remark = textBox1.Text.Trim();
            string count = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(remark))
            {
                MessageBox.Show("请输入描述！");
                return;
            }
            if (string.IsNullOrEmpty(count))
            {
                MessageBox.Show("请输入数量！");
                return;
            }
            if (!validation.IsNumber(textBox2.Text.Trim()))
            {
                MessageBox.Show("数量必须为整数！");
                return;
            }
            DataRow row = GridTable.NewRow();

            row["托盘/盖板"] = "盖板";
            row["描述"] = remark;
            row["数量"] = count;
            GridTable.Rows.Add(row);
            tem.Maktx = textBox1.Text.Trim();
            tem.Menge = decimal.Parse(textBox2.Text.Trim());
            listl504.Add(tem);
        }

        //托盘/盖板返回
        private void button10_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 2;
        }
        //明细返回
        private void button15_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 1;
        }
        //扫描完成 
        private void button16_Click(object sender, EventArgs e)
        {
            listpack.CopyTo(itab, 0);
            this.tabControl1.SelectedIndex = 3;
        }
        //托盘盖板确认
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tray = (DataTable)dataGrid1.DataSource;
                foreach (DataRow dr in tray.Rows)
                {
                    Packaging.Zsdi521I item = new SmartDeviceProject1.Packaging.Zsdi521I();
                    item.Lgort = textBox5.Text.Trim();
                    item.Werks = factory;
                    item.Maktx = dr[1].ToString();
                    item.Matnr = "000000000000000001";
                    item.Zmeng2 = decimal.Parse(dr[2].ToString());
                    item.Znumb = "A" + rule + SerialNum.ToString("00000000");
                    item.Meins = "个";
                    listpack.Add(item);
                }
                DataTable cover = (DataTable)dataGrid2.DataSource;
                foreach (DataRow dr in cover.Rows)
                {
                    Packaging.Zsdi521I item = new SmartDeviceProject1.Packaging.Zsdi521I();
                    item.Lgort = textBox5.Text.Trim();
                    item.Werks = factory;
                    item.Maktx = dr[1].ToString();
                    item.Matnr = "000000000000000002";
                    item.Zmeng2 = decimal.Parse(dr[2].ToString());
                    item.Znumb = "A" + rule + SerialNum.ToString("00000000");
                    item.Meins = "个";
                    listpack.Add(item);
                }
                this.tabControl1.SelectedIndex = 4;
            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zsdi521I");
            }
        }

        //修改包
        private void txtPckage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtPckage.Text != "")
                {
                    string bao = txtPckage.Text.Trim();
                    string sl = txtAmount.Text.Trim();
                    totalcount = txtShipnum.Text = (decimal.Parse(bao) * decimal.Parse(sl)).ToString();
                    saveScan();
                }
            }
        }
        //修改包发货数量
        private void txtShipnum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                totalcount = txtShipnum.Text.Trim();
                int up = int.Parse(totalcount);
                int ol = int.Parse(txtAmount.Text.Trim());
                if (up > ol)
                {
                    MessageBox.Show("实际发货数量不能大于每包数量！");
                    return;
                }
                saveScan();
            }
        }
        //存位撤销
        private void button11_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox5.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            listpack.Clear();
            PageInit();
        }
        //明细撤销
        private void button12_Click(object sender, EventArgs e)
        {
            listpack.Clear();
            PageInit();
            textBox1.Text = "";
            textBox2.Text = "";
        }

    }
}