﻿namespace SmartDeviceProject1.Draw
{
    partial class DrawMaterial
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnConform = new System.Windows.Forms.Button();
            this.btnReturn1 = new System.Windows.Forms.Button();
            this.pnlResearvedNumber = new System.Windows.Forms.Panel();
            this.cboxOrder = new System.Windows.Forms.ComboBox();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabConfirm = new System.Windows.Forms.TabPage();
            this.pnlLocation = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtReceivingLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIssueLocation = new System.Windows.Forms.TextBox();
            this.tabScan = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMetrialNumber = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtPackNumber = new System.Windows.Forms.TextBox();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.txtBatchNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.btnConfirmDraw = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.btnUploadDraw = new System.Windows.Forms.Button();
            this.btnCancelDraw = new System.Windows.Forms.Button();
            this.pnlResearvedNumber.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tabConfirm.SuspendLayout();
            this.pnlLocation.SuspendLayout();
            this.tabScan.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 25);
            this.label1.Text = "预留号";
            // 
            // btnConform
            // 
            this.btnConform.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.btnConform.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnConform.ForeColor = System.Drawing.Color.White;
            this.btnConform.Location = new System.Drawing.Point(168, 46);
            this.btnConform.Name = "btnConform";
            this.btnConform.Size = new System.Drawing.Size(65, 25);
            this.btnConform.TabIndex = 2;
            this.btnConform.Text = "确定";
            this.btnConform.Click += new System.EventHandler(this.btnConform_Click);
            // 
            // btnReturn1
            // 
            this.btnReturn1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.btnReturn1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnReturn1.ForeColor = System.Drawing.Color.White;
            this.btnReturn1.Location = new System.Drawing.Point(168, 213);
            this.btnReturn1.Name = "btnReturn1";
            this.btnReturn1.Size = new System.Drawing.Size(65, 25);
            this.btnReturn1.TabIndex = 3;
            this.btnReturn1.Text = "返回";
            this.btnReturn1.Click += new System.EventHandler(this.btnReturn1_Click);
            // 
            // pnlResearvedNumber
            // 
            this.pnlResearvedNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnlResearvedNumber.Controls.Add(this.cboxOrder);
            this.pnlResearvedNumber.Controls.Add(this.label1);
            this.pnlResearvedNumber.Controls.Add(this.btnConform);
            this.pnlResearvedNumber.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResearvedNumber.Location = new System.Drawing.Point(0, 0);
            this.pnlResearvedNumber.Name = "pnlResearvedNumber";
            this.pnlResearvedNumber.Size = new System.Drawing.Size(240, 79);
            // 
            // cboxOrder
            // 
            this.cboxOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboxOrder.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.cboxOrder.Location = new System.Drawing.Point(100, 13);
            this.cboxOrder.Name = "cboxOrder";
            this.cboxOrder.Size = new System.Drawing.Size(133, 27);
            this.cboxOrder.TabIndex = 3;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabConfirm);
            this.tcMain.Controls.Add(this.tabScan);
            this.tcMain.Controls.Add(this.tabPage1);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.None;
            this.tcMain.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(240, 268);
            this.tcMain.TabIndex = 5;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // tabConfirm
            // 
            this.tabConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabConfirm.Controls.Add(this.pnlLocation);
            this.tabConfirm.Controls.Add(this.btnReturn1);
            this.tabConfirm.Controls.Add(this.pnlResearvedNumber);
            this.tabConfirm.Location = new System.Drawing.Point(0, 0);
            this.tabConfirm.Name = "tabConfirm";
            this.tabConfirm.Size = new System.Drawing.Size(240, 236);
            this.tabConfirm.Text = "预留号确认";
            // 
            // pnlLocation
            // 
            this.pnlLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnlLocation.Controls.Add(this.button2);
            this.pnlLocation.Controls.Add(this.label10);
            this.pnlLocation.Controls.Add(this.txtReceivingLocation);
            this.pnlLocation.Controls.Add(this.label2);
            this.pnlLocation.Controls.Add(this.txtIssueLocation);
            this.pnlLocation.Location = new System.Drawing.Point(0, 86);
            this.pnlLocation.Name = "pnlLocation";
            this.pnlLocation.Size = new System.Drawing.Size(240, 121);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.button2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(168, 84);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 25);
            this.button2.TabIndex = 5;
            this.button2.Text = "确定";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(3, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 27);
            this.label10.Text = "接收库位";
            // 
            // txtReceivingLocation
            // 
            this.txtReceivingLocation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtReceivingLocation.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtReceivingLocation.Location = new System.Drawing.Point(100, 52);
            this.txtReceivingLocation.Name = "txtReceivingLocation";
            this.txtReceivingLocation.Size = new System.Drawing.Size(133, 26);
            this.txtReceivingLocation.TabIndex = 3;
            this.txtReceivingLocation.LostFocus += new System.EventHandler(this.txtReceivingLocation_LostFocus);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(3, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 27);
            this.label2.Text = "发货库位";
            // 
            // txtIssueLocation
            // 
            this.txtIssueLocation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtIssueLocation.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtIssueLocation.Location = new System.Drawing.Point(100, 13);
            this.txtIssueLocation.Name = "txtIssueLocation";
            this.txtIssueLocation.Size = new System.Drawing.Size(133, 26);
            this.txtIssueLocation.TabIndex = 1;
            this.txtIssueLocation.LostFocus += new System.EventHandler(this.txtIssueLocation_LostFocus);
            // 
            // tabScan
            // 
            this.tabScan.Controls.Add(this.panel3);
            this.tabScan.Location = new System.Drawing.Point(0, 0);
            this.tabScan.Name = "tabScan";
            this.tabScan.Size = new System.Drawing.Size(240, 236);
            this.tabScan.Text = "领料扫描";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel3.Controls.Add(this.txtDate);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txtMetrialNumber);
            this.panel3.Controls.Add(this.txtQty);
            this.panel3.Controls.Add(this.txtPackNumber);
            this.panel3.Controls.Add(this.txtUnit);
            this.panel3.Controls.Add(this.txtBatchNumber);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtBarcode);
            this.panel3.Controls.Add(this.btnConfirmDraw);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(240, 236);
            // 
            // txtDate
            // 
            this.txtDate.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtDate.Location = new System.Drawing.Point(94, 7);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(139, 24);
            this.txtDate.TabIndex = 48;
            this.txtDate.ValueChanged += new System.EventHandler(this.txtDate_ValueChanged);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(8, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 31);
            this.label6.Text = "日      期";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.button3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(162, 203);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 30);
            this.button3.TabIndex = 40;
            this.button3.Text = "扫描完成";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(6, 203);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 33;
            this.button1.Text = "确定领料";
            this.button1.Click += new System.EventHandler(this.btnConfirmDraw_Click);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(8, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 22);
            this.label9.Text = "物 料 号";
            // 
            // txtMetrialNumber
            // 
            this.txtMetrialNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtMetrialNumber.Enabled = false;
            this.txtMetrialNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtMetrialNumber.Location = new System.Drawing.Point(94, 67);
            this.txtMetrialNumber.Name = "txtMetrialNumber";
            this.txtMetrialNumber.Size = new System.Drawing.Size(139, 23);
            this.txtMetrialNumber.TabIndex = 27;
            // 
            // txtQty
            // 
            this.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtQty.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtQty.Location = new System.Drawing.Point(94, 178);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(139, 23);
            this.txtQty.TabIndex = 20;
            // 
            // txtPackNumber
            // 
            this.txtPackNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtPackNumber.Enabled = false;
            this.txtPackNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtPackNumber.Location = new System.Drawing.Point(94, 151);
            this.txtPackNumber.Name = "txtPackNumber";
            this.txtPackNumber.Size = new System.Drawing.Size(139, 23);
            this.txtPackNumber.TabIndex = 17;
            // 
            // txtUnit
            // 
            this.txtUnit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtUnit.Enabled = false;
            this.txtUnit.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtUnit.Location = new System.Drawing.Point(94, 124);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(139, 23);
            this.txtUnit.TabIndex = 11;
            // 
            // txtBatchNumber
            // 
            this.txtBatchNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtBatchNumber.Enabled = false;
            this.txtBatchNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtBatchNumber.Location = new System.Drawing.Point(94, 97);
            this.txtBatchNumber.Name = "txtBatchNumber";
            this.txtBatchNumber.Size = new System.Drawing.Size(139, 23);
            this.txtBatchNumber.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(8, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 22);
            this.label3.Text = "物料条码";
            // 
            // txtBarcode
            // 
            this.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtBarcode.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtBarcode.Location = new System.Drawing.Point(94, 37);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(139, 23);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_KeyPress);
            // 
            // btnConfirmDraw
            // 
            this.btnConfirmDraw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.btnConfirmDraw.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnConfirmDraw.ForeColor = System.Drawing.Color.White;
            this.btnConfirmDraw.Location = new System.Drawing.Point(84, 203);
            this.btnConfirmDraw.Name = "btnConfirmDraw";
            this.btnConfirmDraw.Size = new System.Drawing.Size(75, 30);
            this.btnConfirmDraw.TabIndex = 2;
            this.btnConfirmDraw.Text = "撤销领料";
            this.btnConfirmDraw.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(7, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 22);
            this.label8.Text = "数      量";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(8, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 22);
            this.label7.Text = "包      号";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(7, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 22);
            this.label5.Text = "单      位";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(7, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 22);
            this.label4.Text = "批 次 号";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(232, 233);
            this.tabPage1.Text = "提交领料";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.dataGrid1);
            this.panel1.Controls.Add(this.btnUploadDraw);
            this.panel1.Controls.Add(this.btnCancelDraw);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 241);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(240, 198);
            this.dataGrid1.TabIndex = 35;
            // 
            // btnUploadDraw
            // 
            this.btnUploadDraw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.btnUploadDraw.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnUploadDraw.ForeColor = System.Drawing.Color.White;
            this.btnUploadDraw.Location = new System.Drawing.Point(137, 204);
            this.btnUploadDraw.Name = "btnUploadDraw";
            this.btnUploadDraw.Size = new System.Drawing.Size(79, 30);
            this.btnUploadDraw.TabIndex = 34;
            this.btnUploadDraw.Text = "提交领料";
            this.btnUploadDraw.Click += new System.EventHandler(this.btnUploadDraw_Click);
            // 
            // btnCancelDraw
            // 
            this.btnCancelDraw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.btnCancelDraw.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancelDraw.ForeColor = System.Drawing.Color.White;
            this.btnCancelDraw.Location = new System.Drawing.Point(24, 204);
            this.btnCancelDraw.Name = "btnCancelDraw";
            this.btnCancelDraw.Size = new System.Drawing.Size(79, 30);
            this.btnCancelDraw.TabIndex = 4;
            this.btnCancelDraw.Text = "取消领料";
            this.btnCancelDraw.Click += new System.EventHandler(this.btnCancelDraw_Click);
            // 
            // DrawMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.tcMain);
            this.Menu = this.mainMenu1;
            this.Name = "DrawMaterial";
            this.Text = "车间领料";
            this.Load += new System.EventHandler(this.InputMaterial_Load);
            this.pnlResearvedNumber.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tabConfirm.ResumeLayout(false);
            this.pnlLocation.ResumeLayout(false);
            this.tabScan.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConform;
        private System.Windows.Forms.Button btnReturn1;
        private System.Windows.Forms.Panel pnlResearvedNumber;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tabConfirm;
        private System.Windows.Forms.TabPage tabScan;
        private System.Windows.Forms.Panel pnlLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIssueLocation;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Button btnConfirmDraw;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtReceivingLocation;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnUploadDraw;
        private System.Windows.Forms.Button btnCancelDraw;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMetrialNumber;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtPackNumber;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.TextBox txtBatchNumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker txtDate;
        private System.Windows.Forms.ComboBox cboxOrder;
    }
}