﻿namespace SmartDeviceProject1.Input
{
    partial class InputMaterial
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
            this.btnConformOrder = new System.Windows.Forms.Button();
            this.btnReturn1 = new System.Windows.Forms.Button();
            this.pnlProductionOrder = new System.Windows.Forms.Panel();
            this.cboxOrder = new System.Windows.Forms.ComboBox();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabConfirm = new System.Windows.Forms.TabPage();
            this.tabScan = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMetrialNumber = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPackNumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPackQty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBatchNumber = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.btnConfirmInput = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labOrder = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.pnlProductionOrder.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tabConfirm.SuspendLayout();
            this.tabScan.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(21, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 32);
            this.label1.Text = "生产订单号";
            // 
            // btnConformOrder
            // 
            this.btnConformOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.btnConformOrder.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnConformOrder.ForeColor = System.Drawing.Color.White;
            this.btnConformOrder.Location = new System.Drawing.Point(147, 139);
            this.btnConformOrder.Name = "btnConformOrder";
            this.btnConformOrder.Size = new System.Drawing.Size(75, 30);
            this.btnConformOrder.TabIndex = 2;
            this.btnConformOrder.Text = "确定";
            this.btnConformOrder.Click += new System.EventHandler(this.btnConformOrder_Click);
            // 
            // btnReturn1
            // 
            this.btnReturn1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.btnReturn1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnReturn1.ForeColor = System.Drawing.Color.White;
            this.btnReturn1.Location = new System.Drawing.Point(21, 139);
            this.btnReturn1.Name = "btnReturn1";
            this.btnReturn1.Size = new System.Drawing.Size(75, 30);
            this.btnReturn1.TabIndex = 3;
            this.btnReturn1.Text = "返回";
            this.btnReturn1.Click += new System.EventHandler(this.btnReturn1_Click);
            // 
            // pnlProductionOrder
            // 
            this.pnlProductionOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnlProductionOrder.Controls.Add(this.cboxOrder);
            this.pnlProductionOrder.Controls.Add(this.btnReturn1);
            this.pnlProductionOrder.Controls.Add(this.label1);
            this.pnlProductionOrder.Controls.Add(this.btnConformOrder);
            this.pnlProductionOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProductionOrder.Location = new System.Drawing.Point(0, 0);
            this.pnlProductionOrder.Name = "pnlProductionOrder";
            this.pnlProductionOrder.Size = new System.Drawing.Size(240, 238);
            // 
            // cboxOrder
            // 
            this.cboxOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboxOrder.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.cboxOrder.Location = new System.Drawing.Point(113, 66);
            this.cboxOrder.Name = "cboxOrder";
            this.cboxOrder.Size = new System.Drawing.Size(109, 27);
            this.cboxOrder.TabIndex = 5;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabConfirm);
            this.tcMain.Controls.Add(this.tabScan);
            this.tcMain.Controls.Add(this.tabPage1);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.tabConfirm.Controls.Add(this.pnlProductionOrder);
            this.tabConfirm.Location = new System.Drawing.Point(0, 0);
            this.tabConfirm.Name = "tabConfirm";
            this.tabConfirm.Size = new System.Drawing.Size(240, 236);
            this.tabConfirm.Text = "投料单确认";
            // 
            // tabScan
            // 
            this.tabScan.Controls.Add(this.panel3);
            this.tabScan.Location = new System.Drawing.Point(0, 0);
            this.tabScan.Name = "tabScan";
            this.tabScan.Size = new System.Drawing.Size(240, 236);
            this.tabScan.Text = "投料扫描";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txtMetrialNumber);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.txtQty);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.txtPackNumber);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txtPackQty);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtUnit);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtBatchNumber);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtBarcode);
            this.panel3.Controls.Add(this.btnConfirmInput);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(240, 236);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(7, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 20);
            this.label9.Text = "物 料 号";
            // 
            // txtMetrialNumber
            // 
            this.txtMetrialNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtMetrialNumber.Enabled = false;
            this.txtMetrialNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtMetrialNumber.Location = new System.Drawing.Point(81, 35);
            this.txtMetrialNumber.Name = "txtMetrialNumber";
            this.txtMetrialNumber.Size = new System.Drawing.Size(152, 23);
            this.txtMetrialNumber.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(7, 173);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 20);
            this.label8.Text = "数      量";
            // 
            // txtQty
            // 
            this.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtQty.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtQty.Location = new System.Drawing.Point(81, 173);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(152, 23);
            this.txtQty.TabIndex = 20;
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(7, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 20);
            this.label7.Text = "包      号";
            // 
            // txtPackNumber
            // 
            this.txtPackNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtPackNumber.Enabled = false;
            this.txtPackNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtPackNumber.Location = new System.Drawing.Point(81, 146);
            this.txtPackNumber.Name = "txtPackNumber";
            this.txtPackNumber.Size = new System.Drawing.Size(152, 23);
            this.txtPackNumber.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(7, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 20);
            this.label6.Text = "本包数量";
            // 
            // txtPackQty
            // 
            this.txtPackQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtPackQty.Enabled = false;
            this.txtPackQty.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtPackQty.Location = new System.Drawing.Point(81, 119);
            this.txtPackQty.Name = "txtPackQty";
            this.txtPackQty.Size = new System.Drawing.Size(152, 23);
            this.txtPackQty.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(7, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 20);
            this.label5.Text = "单      位";
            // 
            // txtUnit
            // 
            this.txtUnit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtUnit.Enabled = false;
            this.txtUnit.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtUnit.Location = new System.Drawing.Point(81, 92);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(152, 23);
            this.txtUnit.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(7, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 20);
            this.label4.Text = "批 次 号";
            // 
            // txtBatchNumber
            // 
            this.txtBatchNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtBatchNumber.Enabled = false;
            this.txtBatchNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtBatchNumber.Location = new System.Drawing.Point(81, 65);
            this.txtBatchNumber.Name = "txtBatchNumber";
            this.txtBatchNumber.Size = new System.Drawing.Size(152, 23);
            this.txtBatchNumber.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(19, 202);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消投料";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(7, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.Text = "物料条码";
            // 
            // txtBarcode
            // 
            this.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtBarcode.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtBarcode.Location = new System.Drawing.Point(81, 6);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(152, 23);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_KeyPress);
            // 
            // btnConfirmInput
            // 
            this.btnConfirmInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.btnConfirmInput.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnConfirmInput.ForeColor = System.Drawing.Color.White;
            this.btnConfirmInput.Location = new System.Drawing.Point(140, 202);
            this.btnConfirmInput.Name = "btnConfirmInput";
            this.btnConfirmInput.Size = new System.Drawing.Size(80, 30);
            this.btnConfirmInput.TabIndex = 2;
            this.btnConfirmInput.Text = "确定投料";
            this.btnConfirmInput.Click += new System.EventHandler(this.btnConfirmInput_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabPage1.Controls.Add(this.labOrder);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.dataGrid1);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(232, 233);
            this.tabPage1.Text = "投料列表";
            // 
            // labOrder
            // 
            this.labOrder.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.labOrder.Location = new System.Drawing.Point(120, 12);
            this.labOrder.Name = "labOrder";
            this.labOrder.Size = new System.Drawing.Size(112, 20);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(7, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 20);
            this.label10.Text = "生产订单号:";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGrid1.Location = new System.Drawing.Point(0, 33);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(232, 200);
            this.dataGrid1.TabIndex = 0;
            // 
            // InputMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.tcMain);
            this.Menu = this.mainMenu1;
            this.Name = "InputMaterial";
            this.Text = "生产投料";
            this.Load += new System.EventHandler(this.InputMaterial_Load);
            this.pnlProductionOrder.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tabConfirm.ResumeLayout(false);
            this.tabScan.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConformOrder;
        private System.Windows.Forms.Button btnReturn1;
        private System.Windows.Forms.Panel pnlProductionOrder;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tabConfirm;
        private System.Windows.Forms.TabPage tabScan;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Button btnConfirmInput;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPackNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPackQty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBatchNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMetrialNumber;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Label labOrder;
        private System.Windows.Forms.ComboBox cboxOrder;
    }
}