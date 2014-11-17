using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace Print
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public PrintService.zmmi507 service;
        public PrintInfo info;
        List<PrintInfo> list = new List<PrintInfo>();

        /// <summary>
        /// 页面加载test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            System.Net.NetworkCredential credential = new System.Net.NetworkCredential("sd02", "123456");
            service = new Print.PrintService.zmmi507();
            service.Url = "http://114.251.177.94:8000/sap/bc/srt/rfc/sap/zmmi507/500/zmmi507/bindin" +
                "g";
            service.Credentials = credential;
            serialPort1.Open();
            serialPort1.Encoding = System.Text.Encoding.Default;
            serialPort1.WriteTimeout = 60000;
        }
        /// <summary>
        /// 确认订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string order = txtOrder.Text.Trim();
                if (string.IsNullOrEmpty(order))
                {
                    MessageBox.Show("请输入订单号！");
                    return;
                }
                PrintService.Zmmi507 para = new Print.PrintService.Zmmi507();
                para.Ebeln = order;
                para.Itab = new Print.PrintService.Zrohgrbprt[1];
                PrintService.Zmmi507Response response = service.Zmmi507(para);
                if (response.Mtype == "S")
                {
                    PrintService.Zrohgrbprt[] temp = response.Itab;
                    for (int i = 0; i < temp.Length; i++)
                    {
                        info = new PrintInfo
                        {
                            ProName = temp[i].Maktx,
                            ProNum = temp[i].Matnr,
                            Batch = temp[i].Charg,
                            PackNum = temp[i].Zpnum,
                            Quantity = temp[i].Bmenge.ToString(),
                            ProDate = temp[i].Zpdat.ToString("yyyyMMdd"),
                            SupplyBatch = temp[i].Zvlot,
                            Factory = temp[i].Werks,
                            HardNess = temp[i].Ztdxl + "/" + temp[i].Ztydz,
                            IronNum = temp[i].Zmatn + "/" + temp[i].Zltcj,
                            IronWeight = temp[i].Szmeng.ToString(),
                            ResourceNum = temp[i].Ztres,
                            OrderNum = temp[i].Ebeln,
                            OrderItem = temp[i].Ebelp,
                            Layout = temp[i].Zbanm,
                            Unit = temp[i].Meins,
                            IronBatch = temp[i].Zwlot,
                            IronUnit = temp[i].Szmeni
                        };
                        list.Add(info);
                    }
                    foreach (PrintInfo printinfo in list)
                    {
                        ListViewItem item = new ListViewItem();
                        item.SubItems[0].Text = printinfo.ProName;
                        item.SubItems.Add(printinfo.ProNum);
                        item.SubItems.Add(printinfo.Batch);
                        item.SubItems.Add(printinfo.PackNum);
                        item.SubItems.Add(printinfo.Quantity);
                        item.SubItems.Add(printinfo.ProDate);
                        item.SubItems.Add(printinfo.SupplyBatch);
                        item.SubItems.Add(printinfo.Factory);
                        item.SubItems.Add(printinfo.HardNess);
                        item.SubItems.Add(printinfo.IronNum);
                        item.SubItems.Add(printinfo.IronWeight);
                        item.SubItems.Add(printinfo.ResourceNum);
                        item.SubItems.Add(printinfo.OrderNum);
                        item.SubItems.Add(printinfo.OrderItem);
                        item.SubItems.Add(printinfo.Layout);
                        item.SubItems.Add(printinfo.Unit);
                        item.SubItems.Add(printinfo.IronBatch);
                        item.SubItems.Add(printinfo.IronUnit);
                        lvData.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("订单有误，请重新输入！");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //退出
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //产品名称
        public string name;
        //产品编码
        public string pcode;
        //批次
        public string tch;
        //包/桶号
        public string pNum;
        //数量
        public string qty;
        //生产日期
        public string pdate;
        //供应商批次
        public string sBatch;
        //工厂
        public string fac;
        //铁镀锡量/铁硬度
        public string hNess;
        //素铁编号/厂家
        public string iNum;
        //素铁重量
        public string iWeight;
        //素铁资源号
        public string rNum;
        //采购订单号
        public string ordernum { get; set; }
        //采购订单行项目
        public string orderitem { get; set; }
        //版面
        public string layout { get; set; }
        //计量单位
        public string unit { get; set; }
        //素铁批次
        public string ironbatch { get; set; }
        //素铁重量单位
        public string ironunit { get; set; }

        public string Label;
        public string Code;
        //打印
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvData.Items)
            {
                if (item.Checked)
                {
                    try
                    {
                        string d = item.SubItems[5].Text;
                        DateTime dt = DateTime.ParseExact(d, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        name = item.SubItems[0].Text;
                        pcode = item.SubItems[1].Text;
                        tch = item.SubItems[2].Text;
                        pNum = item.SubItems[3].Text;
                        qty = item.SubItems[4].Text;
                        pdate = dt.ToString("yyyy.MM.dd");
                        sBatch = item.SubItems[6].Text;
                        fac = item.SubItems[7].Text;
                        hNess = item.SubItems[8].Text;
                        iNum = item.SubItems[9].Text;
                        iWeight = item.SubItems[10].Text;
                        rNum = item.SubItems[11].Text;
                        ordernum = item.SubItems[12].Text;
                        orderitem = item.SubItems[13].Text;
                        layout = item.SubItems[14].Text;
                        unit = item.SubItems[15].Text;
                        ironbatch = item.SubItems[16].Text;
                        ironunit = item.SubItems[17].Text;
                        Code = @"" + ordernum + "+||+" + orderitem + "+||+" + pcode + "+||+" + name + "+||" + layout + "+||+" + sBatch + "+||+" + pNum + "+||+" + qty +
                            "+||+" + unit + "+||+" + pdate + "+||+" + tch + "+||+" + iNum.Split('/')[0] + "+||+" + hNess.Split('/')[0]
                            + "+||+" + hNess.Split('/')[1] + "+||+" + iNum.Split('/')[1] + "+||+" + iWeight + "+||+" + ironunit + "+||+" + rNum + "+||+" + ironbatch + "";
                        Label = "PP235,327:PL182,2\r\n" +
                                "PP129,394:AN7\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT \"产品名称:\"\r\n" +
                                "PP238,279:AN1\r\n" +
                                "PL182,2\r\n" +
                                "PP238,231:PL182,2\r\n" +
                                "PP238,191:PL182,2\r\n" +
                                "PP238,143:PL182,2\r\n" +
                                "PP238,103:PL182,2\r\n" +
                                "PP238,55:PL182,2\r\n" +
                                "PP238,15:PL182,2\r\n" +
                                "PP541,15:PL182,2\r\n" +
                                "PP542,55:PL182,2\r\n" +
                                "PP581,103:PL139,2\r\n" +
                                "PP597,143:PL123,2\r\n" +
                                "PP129,322:AN7\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT \"产品编码:\"\r\n" +
                                "PP129,274\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT \"批次:\"\r\n" +
                                "PP129,234\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT \"包/桶号:\"\r\n" +
                                "PP129,186\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT \"数量:\"\r\n" +
                                "PP129,138\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT \"生产日期:\"\r\n" +
                                "PP129,98\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT \"供应商批次:\"\r\n" +
                                "PP129,50\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT \"工厂:\"\r\n" +
                                "PP433,186\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT \"铁镀锡量/铁硬度:\"\r\n" +
                                "PP433,138\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT \"素铁编号/厂家:\"\r\n" +
                                "PP433,98\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT \"素铁重量:\"\r\n" +
                                "PP433,50\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT \"素铁资源号:\"\r\n" +
                                "PP241,394\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT " + name + "\r\n" +
                                "PP265,314\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT " + pcode + "\r\n" +
                                "PP289,226\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT " + pNum + "\r\n" +
                                "PP281,186\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT " + qty + "\r\n" +
                                "PP273,138\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT " + pdate + "\r\n" +
                                "PP265,98\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT " + sBatch + "\r\n" +
                                "PP600,99\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT " + iWeight + "\r\n" +
                                "PP584,131\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",7\r\n" +
                                "PT " + iNum + "\r\n" +
                                "PP617,178\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT \"Q23/344\"\r\n" +
                                "PP481,272:BARSET \"DATAMATRIX\",1,1,3" +
                                "PB " + Code + "\r\n" +
                                "PP257,266\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT " + rNum + "\r\n" +
                                "PP233,50\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT " + fac + "\r\n" +
                                "PP600,50\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT " + rNum + "\r\n" +
                                "PP281,362\r\n" +
                                "NASCD \"c:GB2312.NCD\"\r\n" +
                                "FONTD \"SimHei\",8\r\n" +
                                "PT " + tch + "\r\n" +
                                "PF\r\n";
                        serialPort1.WriteLine(Label);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }

            }
            MessageBox.Show("打印成功！");
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //int count = 0;
            //count = serialPort1.BytesToRead;
            //byte[] dates = new byte[count];
            //string ss = "";
            //for (int i = 0; i < count; i++)
            //{
            //    dates[i] = (byte)serialPort1.ReadByte();
            //    ss += dates[i];
            //}
            //MessageBox.Show(ss);
        }
    }
}