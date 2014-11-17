using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartDeviceProject1.Common;
using System.IO;
using System.Collections;
using SmartDeviceProject1.Input;
using Datalogic_Preprocessor;

namespace SmartDeviceProject1.BaoGong
{
    public partial class Submit : Form
    {
        private string username;
        private string fac;
        private int status = 0;
        public Submit()
        {
            InitializeComponent();
        }

        public Submit(string user)
        {
            this.username = user;
            InitializeComponent();
        }

        //订单确认
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboxOrder.Text.Trim()))
            {
                MessageBox.Show("请输入生产订单号！");
                return;
            }
            if (!validation.IsNumber(cboxOrder.Text) || cboxOrder.Text.Length > 12)
            {
                MessageBox.Show("订单号为不能大于12位整数！");
                return;
            }
            try
            {
                Webservice1.Zppi501 io = new SmartDeviceProject1.Webservice1.Zppi501();
                int j = int.Parse(cboxOrder.Text.Trim());
                string OrderNum = j.ToString("000000000000");
                io.Aufnr = OrderNum;
                Webservice1.Zsdmsg01[] outmess = new SmartDeviceProject1.Webservice1.Zsdmsg01[10];
                io.Return = outmess;
                Webservice1.Zppi501Response response = GlobalState.GetWebService().Zppi501(io);
                if (fac != response.Werks)
                {
                    MessageBox.Show("工厂不一致！");
                    cboxOrder.Text = "";
                    return;
                }
                status = 1;
                Webservice1.Zsdmsg01[] message = response.Return;
                string str = "";
                for (int i = 0; i < message.Length; i++)
                {
                    str += message[i].Message;
                }
                if (!string.IsNullOrEmpty(str))
                {
                    if (str == "生产订单可以使用")
                    {
                        status = 1;
                        cboxOrder.Items.Add(cboxOrder.Text);
                        this.tabControl1.SelectedIndex = 1;
                        txtBarCode.Focus();
                        labOrder.Text = cboxOrder.Text.Trim();
                    }
                    else
                    {
                        MessageBox.Show(str);
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //清空
        private void button2_Click(object sender, EventArgs e)
        {
            cboxOrder.Text = "";
        }

        public DateTime NowDay = DateTime.Now;
        public int material;

        //报工
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int t = int.Parse(cboxOrder.Text.Trim());
                material = int.Parse(matnr.ToString());
                string m = material.ToString("000000000000000000");
                NowDay = dateTimePicker1.Value;
                SmartDeviceProject1.Webservice1.Zppi502 io = new SmartDeviceProject1.Webservice1.Zppi502();
                io.Werks = fac;
                io.Zpdte = zpdte[1].ToString();
                io.Zbgte = DateTime.Parse(dateTimePicker1.Text).ToString("yyyyMMdd");
                io.Aufnr = t.ToString("000000000000");
                io.Charg = charg[1].ToString();
                io.Zpnum = zpnum[1].ToString();
                io.Matnr = m;
                io.Menge = decimal.Parse(menge);
                io.Zbanm = zbanm[1].ToString();
                io.Zusrd = GlobalState.UserID;

                Webservice1.Zsdmsg01[] outmess = new SmartDeviceProject1.Webservice1.Zsdmsg01[10];
                io.Return = outmess;

                SmartDeviceProject1.Webservice1.Zppi502Response response = GlobalState.GetWebService().Zppi502(io);
                SmartDeviceProject1.Webservice1.Zsdmsg01[] message = response.Return;
                string str = "";
                for (int i = 0; i < message.Length; i++)
                {
                    str += message[i].Message.ToString() + "\r";
                }
                if (!string.IsNullOrEmpty(str))
                {
                    MessageBox.Show(str);
                    txtBarCode.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //清空
        private void button6_Click(object sender, EventArgs e)
        {
            txtBarCode.Text = "";
            txtBarCode.Focus();
        }

        //page_load
        private void Submit_Load(object sender, EventArgs e)
        {
            fac = GlobalState.GetConfigInfo("工厂");

            PageInit();
        }
        // 
        public DataTable GridDT
        {
            get;
            set;
        }
        private void PageInit()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("操作ID", "操作ID");
            dic.Add("物料号", "物料号");
            dic.Add("版面", "版面");
            dic.Add("数量", "数量");
            dic.Add("批次", "批次");
            dic.Add("包号", "包号");
            dic.Add("报工日期", "报工日期");
            dic.Add("工厂", "工厂");
            dic.Add("生产订单号", "生产订单号");
            GridDT = CreateInitDT(dic);
            // InitDGVColumn(GridDT);
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


        //报工返回
        private void button1_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0;
        }

        private string[] charg;
        private string[] zpnum;
        private string matnr;
        private string menge;
        private string[] zbanm;
        private string[] zpdte;

        //扫描条码
        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Return)
            {
                string BarCode = txtBarCode.Text.Trim();
                if (string.IsNullOrEmpty(BarCode))
                {
                    MessageBox.Show("请扫描条码！");
                    return;
                }
                NowDay = dateTimePicker1.Value;
                BarCode = BarCode.Replace("||", "^");
                string[] values = BarCode.Split('^');
                if (values.Length > 0)
                {
                    string[] str = values[4].ToString().Split(':');
                    charg = values[2].ToString().Split(':');
                    zpnum = values[3].ToString().Split(':');
                    matnr = values[11].ToString();
                    menge = str[1].ToString();
                    zbanm = values[1].ToString().Split(':');
                    zpdte = values[7].ToString().Split(':');
                }

                DataRow row = GridDT.NewRow();
                row["操作ID"] = GlobalState.UserID;
                row["物料号"] = matnr;
                row["版面"] = zbanm[1].ToString();
                row["数量"] = menge;
                row["批次"] = charg[1].ToString();
                row["包号"] = zpnum[1].ToString();
                row["报工日期"] = dateTimePicker1.Text.ToString();
                row["工厂"] = fac;
                row["生产订单号"] = cboxOrder.Text.Trim();
                GridDT.Rows.Add(row);

            }
        }
        //th
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 1)
            {
                if (status == 0)
                {
                    MessageBox.Show("请先确认订单号");
                    this.tabControl1.SelectedIndex = 0;
                }
            }
        }

    }
}