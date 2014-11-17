using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartDeviceProject1.SingleSales;
using SmartDeviceProject1.WareSales;
using SmartDeviceProject1.SuiTong;
using SmartDeviceProject1.BaoGong;
using SmartDeviceProject1.Input;
using SmartDeviceProject1.AllocatioinSale;
using SmartDeviceProject1.Return;
using SmartDeviceProject1.STOInbund;
using SmartDeviceProject1.Purchase;
using SmartDeviceProject1.Parts.OrderOutbund;
using SmartDeviceProject1.parts;
using SmartDeviceProject1.move;
using SmartDeviceProject1.Draw;

namespace SmartDeviceProject1
{
    public partial class Main : Form
    {
        private string func = "";
        private string str = "";
        private string[] main;
        private string userName = "";
        private string orgnazition;
        private string fac;

        public Main()
        {
            InitializeComponent();
        }
        public Main(string menu, string user, string org, string factory)
        {
            this.fac = factory;
            this.orgnazition = org;
            this.userName = user;
            this.func = menu;
            int count = func.Length;
            str = func.Remove(count - 1, 1);
            main = str.Split(',');
            InitializeComponent();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            FontStyle style = FontStyle.Bold;
            int x = 34;
            int y = 3;
            for (int i = 0; i < main.Length; i++)
            {
                Button btn = new Button();
                btn.Text = main[i].ToString();
                btn.Location = new Point(x, y);
                btn.Size = new System.Drawing.Size(150, 27);
                btn.Font = new Font("微软雅黑", 10, style);
                btn.ForeColor = Color.White;
                btn.BackColor = Color.FromArgb(64, 64, 128);
                this.Controls.Add(btn);
                y += 33;
            }
            Button button = new Button();
            button.Text = "返 回";
            button.Location = new Point(x, y);
            button.Size = new System.Drawing.Size(150, 27);
            button.Font = new Font("微软雅黑", 10, style);
            button.ForeColor = Color.White;
            button.BackColor = Color.FromArgb(64, 64, 128);
            this.Controls.Add(button);
            button.Click += new System.EventHandler(this.button20_Click);

            foreach (Control c in this.Controls)
            {
                if (c is Button && c.Text == "按库发货-销售订单")
                {
                    c.Click += new System.EventHandler(this.btnWareSale_Click);
                }
                if (c is Button && c.Text == "按库发货-无单据")
                {
                    c.Click += new System.EventHandler(this.btnWareSaleNoForm_Click);
                }
                if (c is Button && c.Text == "按单发货-销售订单")
                {
                    c.Click += new System.EventHandler(this.btnSingleSale_Click);
                }
                if (c is Button && c.Text == "按单发货-无单据")
                {
                    c.Click += new System.EventHandler(this.btnSingleSaleNoForm_Click);
                }
                if (c is Button && c.Text == "调拨发货-采购订单")
                {
                    c.Click += new System.EventHandler(this.btnAllocaSaleInfo_Click);
                }
                if (c is Button && c.Text == "调拨发货-无单据")
                {
                    c.Click += new System.EventHandler(this.btnAllocaSaleNoForm_Click);
                }
                if (c is Button && c.Text == "退货收货-采购订单")
                {
                    c.Click += new System.EventHandler(this.btnPurcharse_Click);
                }
                if (c is Button && c.Text == "退货收货-销售订单")
                {
                    c.Click += new System.EventHandler(this.btnSale_Click);
                }
                if (c is Button && c.Text == "退货收货-无单据")
                {
                    c.Click += new System.EventHandler(this.btnNoForm_Click);
                }
                if (c is Button && c.Text == "转-铜回收到原材料")
                {
                    c.Click += new System.EventHandler(this.btnSuiTong_Click);
                }
                if (c is Button && c.Text == "车间投料退料")
                {
                    c.Click += new System.EventHandler(this.btnLineRma_Click);
                }
                if (c is Button && c.Text == "车间退料")
                {
                    c.Click += new System.EventHandler(this.btnReturn_Click);
                }
                if (c is Button && c.Text == "车间领料")
                {
                    c.Click += new System.EventHandler(this.btnDraw_Click);
                }

                if (c is Button && c.Text == "车间投料")
                {
                    c.Click += new System.EventHandler(this.btnInput_Click);
                }
                if (c is Button && c.Text == "下线报工")
                {
                    c.Click += new System.EventHandler(this.btnSubmit_Click);
                }
                if (c is Button && c.Text == "取消报工")
                {
                    c.Click += new System.EventHandler(this.btnCancel_Click);
                }
                if (c is Button && c.Text == "收-STO调拨单")
                {
                    c.Click += new System.EventHandler(this.btnSTOinbund_Click);
                }
                if (c is Button && c.Text == "收-标准和寄售")
                {
                    c.Click += new System.EventHandler(this.btnPurchaseInbund_Click);
                }
                if (c is Button && c.Text == "收-委外")
                {
                    c.Click += new System.EventHandler(this.btnPurchaseInbund_Click);
                }
                if (c is Button && c.Text == "转-内部订单领料")
                {
                    c.Click += new System.EventHandler(this.btnOrderoutbound_Click);
                }
                if (c is Button && c.Text == "备件收货")
                {
                    c.Click += new System.EventHandler(this.btnPartIn_Click);
                }
                if (c is Button && c.Text == "备件发放")
                {
                    c.Click += new System.EventHandler(this.btnPartout_Click);
                }
                if (c is Button && c.Text == "转-线边仓到成品库")
                {
                    c.Click += new System.EventHandler(this.btnLinetoWH_Click);
                }
                if (c is Button && c.Text == "转-成品库到线边仓")
                {
                    c.Click += new System.EventHandler(this.btnWHTOLine_Click);
                }
                if (c is Button && c.Text == "转-库房<->库房")
                {
                    c.Click += new System.EventHandler(this.btnWHTOside_Click);
                }

            }
        }
        private string funame;

        private void button20_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //转-转-成品<->外库
        private void btnWHTOside_Click(object sender, EventArgs e)
        {
            WHTOside info = new WHTOside(userName);
            info.ShowDialog();
        }
        //转-成品库到线边仓
        private void btnWHTOLine_Click(object sender, EventArgs e)
        {
            WHTOLine info = new WHTOLine(userName);
            info.ShowDialog();
        }
        //转-线边仓到成品库
        private void btnLinetoWH_Click(object sender, EventArgs e)
        {
            LinetoWH info = new LinetoWH(userName);
            info.ShowDialog();
        }
        //备件发货
        private void btnPartout_Click(object sender, EventArgs e)
        {
            Partout info = new Partout(userName);
            info.ShowDialog();
        }
        //备件收货
        private void btnPartIn_Click(object sender, EventArgs e)
        {
            PartIn info = new PartIn(userName);
            info.ShowDialog();
        }
        //转-内部订单领料
        private void btnOrderoutbound_Click(object sender, EventArgs e)
        {
            Orderoutbound info = new Orderoutbound(userName);
            info.ShowDialog();
        }
        //收-标准和寄售
        private void btnPurchaseInbund_Click(object sender, EventArgs e)
        {
            PurchaseInbund info = new PurchaseInbund(userName);
            info.ShowDialog();
        }
        //收-STO调拨单
        private void btnSTOinbund_Click(object sender, EventArgs e)
        {
            STOinbund info = new STOinbund(userName);
            info.ShowDialog();
        }
        //按库发货-销售订单
        private void btnWareSale_Click(object sender, EventArgs e)
        {
            SaleInfo info = new SaleInfo(userName);
            info.ShowDialog();
        }
        //按库发货-无单据
        private void btnWareSaleNoForm_Click(object sender, EventArgs e)
        {
            funame = "4002";
            Warn info = new Warn(funame, userName);
            info.ShowDialog();
        }
        //按单发货-销售订单
        private void btnSingleSale_Click(object sender, EventArgs e)
        {
            WaleSalesInfo info = new WaleSalesInfo(userName);
            info.ShowDialog();
        }
        //按单发货-无单据
        private void btnSingleSaleNoForm_Click(object sender, EventArgs e)
        {
            funame = "4004";
            Warn info = new Warn(funame, userName);
            info.ShowDialog();
        }
        //调拨发货-采购订单
        private void btnAllocaSaleInfo_Click(object sender, EventArgs e)
        {
            AllocationSaleInfo info = new AllocationSaleInfo(userName);
            info.ShowDialog();
        }
        //调拨发货-无单据
        private void btnAllocaSaleNoForm_Click(object sender, EventArgs e)
        {
            funame = "4006";
            Warn info = new Warn(funame, userName);
            info.ShowDialog();
        }
        //退货收货-采购订单
        private void btnPurcharse_Click(object sender, EventArgs e)
        {
            PurcharseOrder info = new PurcharseOrder(userName);
            info.ShowDialog();
        }
        //退货收货-销售订单
        private void btnSale_Click(object sender, EventArgs e)
        {
            SalesOrder info = new SalesOrder(userName);
            info.ShowDialog();
        }
        //退货收货-无单据
        private void btnNoForm_Click(object sender, EventArgs e)
        {
            funame = "4009";
            Warn info = new Warn(funame, userName);
            info.ShowDialog();
        }
        // 转-铜回收到原材料
        private void btnSuiTong_Click(object sender, EventArgs e)
        {
            Recovery info = new Recovery(userName);
            info.ShowDialog();
        }
        //车间领料退料
        private void btnReturn_Click(object sender, EventArgs e)
        {
            ReturnDrawMaterial info = new ReturnDrawMaterial();
            info.ShowDialog();
        }
        //车间退料
        private void btnLineRma_Click(object sender, EventArgs e)
        {
            ReturnMaterial info = new ReturnMaterial(userName);
            info.ShowDialog();
        }
        //车间领料
        private void btnDraw_Click(object sender, EventArgs e)
        {
            DrawMaterial info = new DrawMaterial();
            info.ShowDialog();
        }
        //车间投料
        private void btnInput_Click(object sender, EventArgs e)
        {
            InputMaterial info = new InputMaterial(userName);
            info.ShowDialog();
        }
        //下线报工
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Submit info = new Submit(userName);
            info.ShowDialog();
        }
        //取消报工
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel info = new Cancel(userName);
            info.ShowDialog();
        }
    }
}