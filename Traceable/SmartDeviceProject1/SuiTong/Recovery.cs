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

namespace SmartDeviceProject1.SuiTong
{
    public partial class Recovery : Form
    {

        private string fac;
        public string userName = string.Empty;
        public Recovery()
        {
            InitializeComponent();
        }
        public int k1 = 0;
        public Recovery(string UN)
        {
            userName = UN;
            InitializeComponent();
        }
        //预留号
        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                {
                    if (string.IsNullOrEmpty(txtNum.Text.Trim()))
                    {
                        MessageBox.Show("请输入预留号！");
                        return;
                    }
                    if (!validation.IsNumber(txtNum.Text.Trim()) || txtNum.Text.Trim().Length > 10)
                    {
                        MessageBox.Show("预留号为不大于10位的整数！");
                        return;
                    }
                    //io = new SmartDeviceProject1.Webservice1.Zppi514();
                    SmartDeviceProject1.Webservice1.Zppi513 io1 = new SmartDeviceProject1.Webservice1.Zppi513();
                    string Num = txtNum.Text.Trim();
                    io1.Rsnum = Num;
                    Webservice1.Zsdmsg01[] outmess = new SmartDeviceProject1.Webservice1.Zsdmsg01[10];
                    io1.Return = outmess;

                    SmartDeviceProject1.Webservice1.Zppi513Response response = GlobalState.GetWebService().Zppi513(io1);

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


                    }

                    if (response != null)
                    {
                        if (!response.Matnr.StartsWith("00000000008"))
                        {
                            MessageBox.Show("物料编号不正确!");

                            return;
                        }
                        txtSHKC.Text = response.Lgort;
                        txtMaterialCode.Text = response.Matnr.Substring(10, 8);
                        textBox1.Text = response.Znmng.ToString();
                        if (txtMaterialCode.Text.Trim() != "")
                        {
                            k = 1;
                        }
                        txtSHKC.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("预留号输入错误！");
            }
        }
        //返回
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                //CommonAPI.GoBack(this);
            }
            catch { }
        }
        //开始扫描
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (k == 0)
                {
                    MessageBox.Show("请扫描预留号码");
                    return;
                }
                if (txtSHKC.Text.Trim() == "")
                {
                    MessageBox.Show("请输入收货库存地");
                    return;
                }
                if (!validation.IsNumber(txtSHKC.Text.Trim()) || txtSHKC.Text.Trim().Length != 4)
                {
                    MessageBox.Show("输入有误，请输入4位整数！");
                    return;
                }
                this.tabControl1.SelectedIndex = 1;
                txtBarCode.Focus();
            }
            catch { }
        }

        public SmartDeviceProject1.Webservice1.Zppi514 io = new SmartDeviceProject1.Webservice1.Zppi514();

        public string pickUserName;
        public string pickUserPwd;
        //确认回收
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (k1 == 0)
                {
                    MessageBox.Show("请扫描条码!");
                    return;
                }
                Copper cp = new Copper();
                cp.Owner = this;
                cp.ShowDialog();
                if (textBox1.Text.Trim() != "")
                {
                    decimal count = decimal.Parse(textBox1.Text.Trim());
                    if (AllCount > count)
                    {
                        MessageBox.Show("本次回收数量超过预计数量!");
                        return;
                    }
                }
                io.Werks = fac;
                io.Rsnum = txtNum.Text.Trim();
                //io.Matnr = "0000000000" + matnr[0].ToString();
                io.Matnr = "0000000000" + matnr.ToString();
                io.Menge = Math.Round(AllCount, 2);
                // io.Meins = meins[0].ToString();
                io.Meins = meins.ToString();
                // io.Zpnum = zpnum[0].ToString();
                io.Zpnum = zpnum.ToString();
                io.Lgort = txtSHKC.Text.Trim();
                io.Zusrd = userName;
                io.Ztjid = pickUserName;
                io.Zpasw = pickUserPwd;


                Webservice1.Zsdmsg01[] outmess = new SmartDeviceProject1.Webservice1.Zsdmsg01[10];
                io.Return = outmess;

                SmartDeviceProject1.Webservice1.Zppi514Response response = GlobalState.GetWebService().Zppi514(io);
                SmartDeviceProject1.Webservice1.Zsdmsg01[] message = response.Return;
                for (int i = 0; i < message.Length; i++)
                {
                    if (response.Return[i].Ztype == "E")
                    {
                        MessageBox.Show(response.Return[i].Message);
                        return;
                    }
                    else if (response.Return[i].Ztype == "S")
                    {
                        if (response.Return[i].Message == "同意提交ID或密码正确")
                        {
                            //
                        }
                        else
                        {
                            praseProduct = new MaterialInfo();
                            dataGrid1.DataSource = null;
                            PageInit();
                            AllCount = 0;
                            lblCount.Text = "0";
                            tabControl1.SelectedIndex = 0;
                            txtNum.Text = "";
                            txtSHKC.Text = "";
                            txtMaterialCode.Text = "";
                            textBox1.Text = "";
                            k = 0;
                            k1 = 0;
                            GridDT.Rows.Clear();
                            txtNum.Focus();

                            matnr = "";
                            menge = "";
                            meins = "";
                            zpnum = "";
                            Charg = "";
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                CommonAPI.Error(ex, "Zppi514");
            }
        }
        //取消回收
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                dataGrid1.DataSource = null;
                PageInit();
                AllCount = 0;
                txtBarCode.Focus();
                lblCount.Text = "0";
            }
            catch { }
        }
        //清空
        private void button5_Click(object sender, EventArgs e)
        {
            //praseProduct = new MaterialInfo();
            //AllCount = 0;
            //lblCount.Text = "0";
            txtBarCode.Text = "";
            txtBarCode.Focus();
            //k1 = 0;
        }
        //返回
        private void button6_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0;
        }

        private void Recovery_Load(object sender, EventArgs e)
        {
            fac = GlobalState.GetConfigInfo("工厂");

            PageInit();
        }

        //// 
        //public DataTable GridDT
        //{
        //    get;
        //    set;
        //}
        public DataTable GridDT = new DataTable();
        private void PageInit()
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("操作ID", "操作ID");
                dic.Add("物料号", "物料号");
                dic.Add("单位", "单位");
                dic.Add("数量", "数量");
                dic.Add("收货库位", "收货库位");
                dic.Add("包号", "包号");
                dic.Add("批次", "批次");
                dic.Add("预留号", "预留号");
                dic.Add("工厂", "工厂");
                GridDT = CreateInitDT(dic);
                this.dataGrid1.DataSource = GridDT;
                //GridDT = CreateInitDT(dic);
                // InitDGVColumn(GridDT);
                //GridDT.Columns.Add("操作ID");
                //GridDT.Columns.Add("物料号");
                //GridDT.Columns.Add("单位");
                //GridDT.Columns.Add("数量");
                //GridDT.Columns.Add("收货库位");
                //GridDT.Columns.Add("包号");
                //GridDT.Columns.Add("预留号");
                //GridDT.Columns.Add("工厂");
                //this.dataGrid1.DataSource = GridDT;
            }
            catch { }
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

        /// <summary>
        /// 扫描总数量
        /// </summary>
        public decimal AllCount = 0;

        string matnr = "";
        string menge = "";
        string meins = "";
        string zpnum = "";
        string Charg = "";
        public int k = 0;
        public MaterialInfo praseProduct = new MaterialInfo();
        //二维码扫描
        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                {
                    if (txtBarCode.Text.Trim() == "") return;
                    string BarCode = txtBarCode.Text.Trim();
                    if (BarCode == "")
                    {
                        MessageBox.Show("请扫描条码！");
                        return;
                    }
                    praseProduct = new MaterialInfo();
                    //BarCode = BarCode.Replace("||", "^");
                    praseProduct = CommonAPI.PraseMaterial(BarCode);
                    if (GridDT.Rows.Count > 0)
                    {
                        DataRow[] dr = GridDT.Select("物料号='" + praseProduct.Matnr + "' and 批次='" + praseProduct.Charg + "' and 包号='" + praseProduct.Zpnum + "'");
                        if (dr.Count() > 0)
                        {
                            MessageBox.Show("有物料、批次、包号重复的数据！");
                            txtBarCode.Text = "";
                            txtBarCode.Focus();
                            return;
                        }



                    }

                    //string[] values = BarCode.Split('^');
                    // string[] charg = values[2].ToString().Split(':');
                    zpnum = praseProduct.Zpnum;
                    matnr = praseProduct.Matnr;
                    menge = praseProduct.Menge.ToString();
                    meins = praseProduct.Meins;
                    Charg = praseProduct.Charg;
                    if (praseProduct != null)
                    {
                        if (textBox1.Text.Trim() != "")
                        {
                            decimal count = decimal.Parse(textBox1.Text.Trim());
                            if (AllCount > count)
                            {
                                MessageBox.Show("本次回收数量超过预计数量!");
                                return;
                            }
                        }
                        if (!praseProduct.Matnr.StartsWith("1"))
                        {
                            MessageBox.Show("扫描到不是铜线！");
                            return;
                        }
                        else
                        {
                            AllCount += decimal.Parse(menge);
                            lblCount.Text = AllCount.ToString();
                            DataRow row = GridDT.NewRow();
                            row["操作ID"] = userName;
                            row["物料号"] = matnr;
                            row["单位"] = meins;
                            row["数量"] = menge;
                            row["收货库位"] = txtSHKC.Text.Trim();
                            row["包号"] = zpnum;
                            row["批次"] = Charg;
                            row["预留号"] = txtNum.Text.Trim();
                            row["工厂"] = fac;
                            GridDT.Rows.Add(row);
                            k1 = 1;
                            //GridDT.Rows.Add(userName, matnr[0].ToString(), meins[0].ToString(), menge[0].ToString(), txtSHKC.Text.Trim(), zpnum[0].ToString(), txtNum.Text.Trim(),fac);
                            //dataGrid1.DataSource = GridDT;
                        }

                    }
                    else
                    {
                        MessageBox.Show("条码不符");
                    }
                    txtBarCode.Text = "";
                    txtBarCode.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("条形码格式不正确！");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.tabControl1.SelectedIndex == 1)
                {
                    //if (k == 0)
                    //{
                    //    MessageBox.Show("请扫描预留号码");
                    //    this.tabControl1.SelectedIndex = 0;
                    //    return;
                    //}
                    if (txtSHKC.Text.Trim() == "")
                    {
                        MessageBox.Show("请输入收货库存地");
                        this.tabControl1.SelectedIndex = 0;
                        return;
                    }

                }
            }
            catch { }
        }

        private void dataGrid1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int RowIndex = this.dataGrid1.CurrentRowIndex;
                DataTable row = this.dataGrid1.DataSource as DataTable;
                AllCount = AllCount - decimal.Parse(row.Rows[RowIndex]["数量"].ToString());
                lblCount.Text = AllCount.ToString();
                row.Rows.RemoveAt(RowIndex);
                this.dataGrid1.DataSource = row;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void lblCount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!validation.IsNumber(lblCount.Text.Trim()))
                {
                    MessageBox.Show("扫描总数量不是数字！");
                    lblCount.Text = AllCount.ToString();
                    return;
                }
                //decimal A = decimal.Parse(lblCount.Text.Trim());
                //if (textBox1.Text.Trim() != "")
                //{
                //    decimal count = decimal.Parse(textBox1.Text.Trim());
                //    if (A > count)
                //    {
                //        lblCount.Text = AllCount.ToString();
                //        MessageBox.Show("本次回收数量超过预计数量!");
                //        return;
                //    }
                //}
                AllCount = decimal.Parse(lblCount.Text.Trim());
            }
            catch { }
        }
    }
}