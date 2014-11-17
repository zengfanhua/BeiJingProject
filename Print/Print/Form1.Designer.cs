namespace Print
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtOrder = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lvData = new System.Windows.Forms.ListView();
            this.proName = new System.Windows.Forms.ColumnHeader();
            this.proNum = new System.Windows.Forms.ColumnHeader();
            this.batch = new System.Windows.Forms.ColumnHeader();
            this.pack = new System.Windows.Forms.ColumnHeader();
            this.quantity = new System.Windows.Forms.ColumnHeader();
            this.date = new System.Windows.Forms.ColumnHeader();
            this.customerBatch = new System.Windows.Forms.ColumnHeader();
            this.factory = new System.Windows.Forms.ColumnHeader();
            this.hardNess = new System.Windows.Forms.ColumnHeader();
            this.ironNum = new System.Windows.Forms.ColumnHeader();
            this.ironWeight = new System.Windows.Forms.ColumnHeader();
            this.ironResource = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM6";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(0, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.Text = "订单：";
            // 
            // txtOrder
            // 
            this.txtOrder.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txtOrder.Location = new System.Drawing.Point(48, 13);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(126, 23);
            this.txtOrder.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(180, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "确定";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(144, 228);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 33);
            this.button2.TabIndex = 4;
            this.button2.Text = "打 印";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(27, 228);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(72, 33);
            this.button3.TabIndex = 5;
            this.button3.Text = "退  出";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lvData
            // 
            this.lvData.CheckBoxes = true;
            this.lvData.Columns.Add(this.proName);
            this.lvData.Columns.Add(this.proNum);
            this.lvData.Columns.Add(this.batch);
            this.lvData.Columns.Add(this.pack);
            this.lvData.Columns.Add(this.quantity);
            this.lvData.Columns.Add(this.date);
            this.lvData.Columns.Add(this.customerBatch);
            this.lvData.Columns.Add(this.factory);
            this.lvData.Columns.Add(this.hardNess);
            this.lvData.Columns.Add(this.ironNum);
            this.lvData.Columns.Add(this.ironWeight);
            this.lvData.Columns.Add(this.ironResource);
            this.lvData.Columns.Add(this.columnHeader1);
            this.lvData.Columns.Add(this.columnHeader2);
            this.lvData.Columns.Add(this.columnHeader3);
            this.lvData.Columns.Add(this.columnHeader4);
            this.lvData.Columns.Add(this.columnHeader5);
            this.lvData.Columns.Add(this.columnHeader6);
            this.lvData.Location = new System.Drawing.Point(3, 42);
            this.lvData.Name = "lvData";
            this.lvData.Size = new System.Drawing.Size(233, 180);
            this.lvData.TabIndex = 7;
            this.lvData.View = System.Windows.Forms.View.Details;
            // 
            // proName
            // 
            this.proName.Text = "产品名称";
            this.proName.Width = 60;
            // 
            // proNum
            // 
            this.proNum.Text = "产品编码";
            this.proNum.Width = 60;
            // 
            // batch
            // 
            this.batch.Text = "批次";
            this.batch.Width = 60;
            // 
            // pack
            // 
            this.pack.Text = "包/桶号";
            this.pack.Width = 60;
            // 
            // quantity
            // 
            this.quantity.Text = "数量";
            this.quantity.Width = 60;
            // 
            // date
            // 
            this.date.Text = "生产日期";
            this.date.Width = 60;
            // 
            // customerBatch
            // 
            this.customerBatch.Text = "供应商批次";
            this.customerBatch.Width = 60;
            // 
            // factory
            // 
            this.factory.Text = "工厂";
            this.factory.Width = 60;
            // 
            // hardNess
            // 
            this.hardNess.Text = "铁镀锡量/铁硬度";
            this.hardNess.Width = 60;
            // 
            // ironNum
            // 
            this.ironNum.Text = "素铁编号/厂家";
            this.ironNum.Width = 60;
            // 
            // ironWeight
            // 
            this.ironWeight.Text = "素铁重量";
            this.ironWeight.Width = 60;
            // 
            // ironResource
            // 
            this.ironResource.Text = "素铁资源号";
            this.ironResource.Width = 60;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "采购订单号";
            this.columnHeader1.Width = 60;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "采购订单行项目";
            this.columnHeader2.Width = 60;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "版面";
            this.columnHeader3.Width = 60;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "计量单位";
            this.columnHeader4.Width = 60;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "素铁批次";
            this.columnHeader5.Width = 60;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "素铁重量单位";
            this.columnHeader6.Width = 60;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lvData);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtOrder);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "打印";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOrder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListView lvData;
        private System.Windows.Forms.ColumnHeader proName;
        private System.Windows.Forms.ColumnHeader proNum;
        private System.Windows.Forms.ColumnHeader batch;
        private System.Windows.Forms.ColumnHeader pack;
        private System.Windows.Forms.ColumnHeader quantity;
        private System.Windows.Forms.ColumnHeader date;
        private System.Windows.Forms.ColumnHeader customerBatch;
        private System.Windows.Forms.ColumnHeader factory;
        private System.Windows.Forms.ColumnHeader hardNess;
        private System.Windows.Forms.ColumnHeader ironNum;
        private System.Windows.Forms.ColumnHeader ironWeight;
        private System.Windows.Forms.ColumnHeader ironResource;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
    }
}

