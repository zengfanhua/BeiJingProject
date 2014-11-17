namespace SmartDeviceProject1.STOInbund
{
    partial class STOinbund
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabConfirm = new System.Windows.Forms.TabPage();
            this.pnlResearvedNumber = new System.Windows.Forms.Panel();
            this.cboxOrder = new System.Windows.Forms.ComboBox();
            this.btnGoBack = new System.Windows.Forms.Button();
            this.dtpEdatu = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.btnConform = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtMetrialNumber = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtPackNumber = new System.Windows.Forms.TextBox();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.txtBatchNumber = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.btnUploadDraw = new System.Windows.Forms.Button();
            this.btnCancelDraw = new System.Windows.Forms.Button();
            this.tcMain.SuspendLayout();
            this.tabConfirm.SuspendLayout();
            this.pnlResearvedNumber.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabConfirm);
            this.tcMain.Controls.Add(this.tabPage2);
            this.tcMain.Controls.Add(this.tabPage1);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(240, 268);
            this.tcMain.TabIndex = 6;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // tabConfirm
            // 
            this.tabConfirm.Controls.Add(this.pnlResearvedNumber);
            this.tabConfirm.Location = new System.Drawing.Point(0, 0);
            this.tabConfirm.Name = "tabConfirm";
            this.tabConfirm.Size = new System.Drawing.Size(240, 236);
            this.tabConfirm.Text = "交货单号确认";
            // 
            // pnlResearvedNumber
            // 
            this.pnlResearvedNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnlResearvedNumber.Controls.Add(this.cboxOrder);
            this.pnlResearvedNumber.Controls.Add(this.btnGoBack);
            this.pnlResearvedNumber.Controls.Add(this.dtpEdatu);
            this.pnlResearvedNumber.Controls.Add(this.label1);
            this.pnlResearvedNumber.Controls.Add(this.label2);
            this.pnlResearvedNumber.Controls.Add(this.button3);
            this.pnlResearvedNumber.Controls.Add(this.btnConform);
            this.pnlResearvedNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResearvedNumber.Location = new System.Drawing.Point(0, 0);
            this.pnlResearvedNumber.Name = "pnlResearvedNumber";
            this.pnlResearvedNumber.Size = new System.Drawing.Size(240, 236);
            // 
            // cboxOrder
            // 
            this.cboxOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboxOrder.Location = new System.Drawing.Point(91, 31);
            this.cboxOrder.Name = "cboxOrder";
            this.cboxOrder.Size = new System.Drawing.Size(132, 22);
            this.cboxOrder.TabIndex = 8;
            // 
            // btnGoBack
            // 
            this.btnGoBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.btnGoBack.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnGoBack.ForeColor = System.Drawing.Color.White;
            this.btnGoBack.Location = new System.Drawing.Point(3, 158);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(75, 30);
            this.btnGoBack.TabIndex = 3;
            this.btnGoBack.Text = "返回";
            this.btnGoBack.Click += new System.EventHandler(this.btnGoBack_Click);
            // 
            // dtpEdatu
            // 
            this.dtpEdatu.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.dtpEdatu.Location = new System.Drawing.Point(91, 72);
            this.dtpEdatu.Name = "dtpEdatu";
            this.dtpEdatu.Size = new System.Drawing.Size(132, 24);
            this.dtpEdatu.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.Text = "交货单号";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(13, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.Text = "过账日期";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.button3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(81, 158);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 30);
            this.button3.TabIndex = 2;
            this.button3.Text = "清空";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnConform
            // 
            this.btnConform.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.btnConform.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnConform.ForeColor = System.Drawing.Color.White;
            this.btnConform.Location = new System.Drawing.Point(158, 158);
            this.btnConform.Name = "btnConform";
            this.btnConform.Size = new System.Drawing.Size(75, 30);
            this.btnConform.TabIndex = 2;
            this.btnConform.Text = "确定";
            this.btnConform.Click += new System.EventHandler(this.btnConform_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabPage2.Controls.Add(this.txtMetrialNumber);
            this.tabPage2.Controls.Add(this.txtQty);
            this.tabPage2.Controls.Add(this.txtPackNumber);
            this.tabPage2.Controls.Add(this.txtUnit);
            this.tabPage2.Controls.Add(this.txtBatchNumber);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.txtBarcode);
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(240, 236);
            this.tabPage2.Text = "条码详细";
            // 
            // txtMetrialNumber
            // 
            this.txtMetrialNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtMetrialNumber.Enabled = false;
            this.txtMetrialNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtMetrialNumber.Location = new System.Drawing.Point(91, 36);
            this.txtMetrialNumber.Name = "txtMetrialNumber";
            this.txtMetrialNumber.ReadOnly = true;
            this.txtMetrialNumber.Size = new System.Drawing.Size(134, 23);
            this.txtMetrialNumber.TabIndex = 64;
            // 
            // txtQty
            // 
            this.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtQty.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtQty.Location = new System.Drawing.Point(91, 147);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(134, 23);
            this.txtQty.TabIndex = 63;
            this.txtQty.Text = "0";
            this.txtQty.TextChanged += new System.EventHandler(this.txtQty_TextChanged);
            // 
            // txtPackNumber
            // 
            this.txtPackNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtPackNumber.Enabled = false;
            this.txtPackNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtPackNumber.Location = new System.Drawing.Point(91, 120);
            this.txtPackNumber.Name = "txtPackNumber";
            this.txtPackNumber.ReadOnly = true;
            this.txtPackNumber.Size = new System.Drawing.Size(134, 23);
            this.txtPackNumber.TabIndex = 62;
            // 
            // txtUnit
            // 
            this.txtUnit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtUnit.Enabled = false;
            this.txtUnit.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtUnit.Location = new System.Drawing.Point(91, 93);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.ReadOnly = true;
            this.txtUnit.Size = new System.Drawing.Size(134, 23);
            this.txtUnit.TabIndex = 61;
            // 
            // txtBatchNumber
            // 
            this.txtBatchNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtBatchNumber.Enabled = false;
            this.txtBatchNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtBatchNumber.Location = new System.Drawing.Point(91, 66);
            this.txtBatchNumber.Name = "txtBatchNumber";
            this.txtBatchNumber.ReadOnly = true;
            this.txtBatchNumber.Size = new System.Drawing.Size(134, 23);
            this.txtBatchNumber.TabIndex = 60;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(7, 147);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 20);
            this.label8.Text = "数     量";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(7, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 20);
            this.label7.Text = "包     号";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(7, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 20);
            this.label5.Text = "单     位";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(7, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.Text = "批 次 号";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(7, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 20);
            this.label9.Text = "物 料 号";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.button2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(24, 189);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 30);
            this.button2.TabIndex = 58;
            this.button2.Text = "清空";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(126, 189);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 30);
            this.button1.TabIndex = 59;
            this.button1.Text = "确定";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(7, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 29);
            this.label3.Text = "物料条码";
            // 
            // txtBarcode
            // 
            this.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtBarcode.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtBarcode.Location = new System.Drawing.Point(91, 7);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(134, 23);
            this.txtBarcode.TabIndex = 41;
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_KeyPress);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(240, 236);
            this.tabPage1.Text = "提交收货";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.dataGrid1);
            this.panel1.Controls.Add(this.btnUploadDraw);
            this.panel1.Controls.Add(this.btnCancelDraw);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 236);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(240, 186);
            this.dataGrid1.TabIndex = 35;
            // 
            // btnUploadDraw
            // 
            this.btnUploadDraw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.btnUploadDraw.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnUploadDraw.ForeColor = System.Drawing.Color.White;
            this.btnUploadDraw.Location = new System.Drawing.Point(129, 192);
            this.btnUploadDraw.Name = "btnUploadDraw";
            this.btnUploadDraw.Size = new System.Drawing.Size(93, 41);
            this.btnUploadDraw.TabIndex = 34;
            this.btnUploadDraw.Text = "确认收货";
            this.btnUploadDraw.Click += new System.EventHandler(this.btnUploadDraw_Click);
            // 
            // btnCancelDraw
            // 
            this.btnCancelDraw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.btnCancelDraw.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancelDraw.ForeColor = System.Drawing.Color.White;
            this.btnCancelDraw.Location = new System.Drawing.Point(16, 192);
            this.btnCancelDraw.Name = "btnCancelDraw";
            this.btnCancelDraw.Size = new System.Drawing.Size(91, 41);
            this.btnCancelDraw.TabIndex = 4;
            this.btnCancelDraw.Text = "取消收货";
            this.btnCancelDraw.Click += new System.EventHandler(this.btnCancelDraw_Click);
            // 
            // STOinbund
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.tcMain);
            this.Menu = this.mainMenu1;
            this.Name = "STOinbund";
            this.Text = "收-STO调拨单";
            this.Load += new System.EventHandler(this.STOinbund_Load);
            this.tcMain.ResumeLayout(false);
            this.tabConfirm.ResumeLayout(false);
            this.pnlResearvedNumber.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tabConfirm;
        private System.Windows.Forms.Panel pnlResearvedNumber;
        private System.Windows.Forms.Button btnGoBack;
        private System.Windows.Forms.DateTimePicker dtpEdatu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConform;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnUploadDraw;
        private System.Windows.Forms.Button btnCancelDraw;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.TextBox txtMetrialNumber;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtPackNumber;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.TextBox txtBatchNumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox cboxOrder;
    }
}