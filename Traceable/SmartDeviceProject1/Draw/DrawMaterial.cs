using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartDeviceProject1.Input;
using SmartDeviceProject1.Common;
using SmartDeviceProject1.Webservice1;
using Datalogic_Preprocessor;

namespace SmartDeviceProject1.Draw
{
    public partial class DrawMaterial : Form
    {
        public string ReservedNumber { get; set; }

        public string IssueLocation { get; set; }

        public string ReceivingLocation { get; set; }

        public DrawStatus CurrentState { get; set; }
        public string fac { get; set; }

        public string user { get; set; }
        public List<MaterialModel> ConfirmedMatetials = new List<MaterialModel>();

        public List<MaterialModel> UploadedMatetials = new List<MaterialModel>();

        public DrawMaterial()
        {
            InitializeComponent();
        }
        public DrawMaterial(string username)
        {
            this.user = username;
            InitializeComponent();
        }
        public void CloseForm()
        {
            this.Close();
        }

        private void btnReturn2_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void btnReturn1_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void SetDrawStatus(DrawStatus status)
        {
            if (status == DrawStatus.Init)
            {
                pnlResearvedNumber.Enabled = true;
                pnlLocation.Enabled = false;

                ReservedNumber = string.Empty;
                IssueLocation = string.Empty;
                ReceivingLocation = string.Empty;

                CurrentState = DrawStatus.Init;
            }

            if (status == DrawStatus.ReservedConfirmed)
            {
                pnlResearvedNumber.Enabled = true;
                pnlLocation.Enabled = true;

                CurrentState = DrawStatus.ReservedConfirmed;
            }
        }

        private void InputMaterial_Load(object sender, EventArgs e)
        {
            PageInit();

            fac = GlobalState.GetConfigInfo("工厂");
            SetDrawStatus(DrawStatus.Init);
        }
        public DataTable GridDT
        {
            get;
            set;
        }
        private void PageInit()
        {
            GridDT = new DataTable(GetType().Name);
            GridDT.Columns.Add("工厂", typeof(string));
            GridDT.Columns.Add("预留号", typeof(string));
            GridDT.Columns.Add("物料号", typeof(string));
            GridDT.Columns.Add("批次", typeof(string));
            GridDT.Columns.Add("数量", typeof(decimal));
            GridDT.Columns.Add("单位", typeof(string));
            GridDT.Columns.Add("包号", typeof(string));
            GridDT.Columns.Add("发货库位", typeof(string));
            GridDT.Columns.Add("接收库位", typeof(string));
            GridDT.Columns.Add("操作ID", typeof(string));
            GridDT.Columns.Add("是否半成品", typeof(string));
        }
        private void btnConform_Click(object sender, EventArgs e)
        {
            string number = cboxOrder.Text.Trim();
            if (number == string.Empty)
            {
                MessageBox.Show("请输入预留号！");
                return;
            }
            if (!validation.IsNumber(number) || number.Length > 10)
            {
                MessageBox.Show("预留号为不大于10位的整数！");
                return;
            }
            try
            {
                string issueLocation = string.Empty;
                string receivingLocation = string.Empty;

                bool isSuccess = DrawLogic.ConfirmReservedNumber(number, out receivingLocation, out issueLocation);
                if (isSuccess)
                {
                    cboxOrder.Items.Add(number);
                    CommonAPI.Load(GridDT, GetType().Name);
                    //判断缓存是否符合条件
                    if (GridDT.Rows.Count > 0)
                    {
                        DataRow[] dr = GridDT.Select("预留号='" + cboxOrder.Text.Trim() + "' and 操作ID='" + GlobalState.UserID + "'");
                        if (dr.Count() > 0)
                        {
                            if (MessageBox.Show("是否加载已保存的数据！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                txtIssueLocation.Enabled = true;
                                txtReceivingLocation.Enabled = false;
                                txtIssueLocation.Text = GridDT.Rows[0]["发货库位"].ToString();
                                txtReceivingLocation.Text = GridDT.Rows[0]["接收库位"].ToString();
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
                    else
                    {
                        txtIssueLocation.Enabled = true;
                        txtReceivingLocation.Enabled = false;
                        ReservedNumber = number;
                        txtReceivingLocation.Text = receivingLocation;
                        txtIssueLocation.Text = issueLocation;

                        IssueLocation = issueLocation;
                        ReceivingLocation = receivingLocation;

                        SetDrawStatus(DrawStatus.ReservedConfirmed);
                    }
                }
                else
                {
                    pnlLocation.Enabled = false;
                }
            }
            catch (BusinessException businessException)
            {
                MessageBox.Show(businessException.Message);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void DeleteXML()
        {
            GridDT.Rows.Clear();
            dataGrid1.DataSource = null;
            CommonAPI.Remove(this.GetType().Name);
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcMain.SelectedIndex == 1)
            {
                if (CurrentState != DrawStatus.ReservedConfirmed)
                {
                    MessageBox.Show("请确认预留号。");
                    tcMain.SelectedIndex = 0;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelMaterial();
        }

        private void CancelMaterial()
        {
            txtBarcode.Text = string.Empty;
            txtBatchNumber.Text = string.Empty;
            txtPackNumber.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtUnit.Text = string.Empty;
            txtMetrialNumber.Text = string.Empty;

            txtBarcode.Focus();
        }

        private void CancelMaterial1()
        {
            txtBarcode.Text = string.Empty;
            txtBatchNumber.Text = string.Empty;
            txtPackNumber.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtUnit.Text = string.Empty;
            txtMetrialNumber.Text = string.Empty;

            txtBarcode.Focus();
        }

        private string ExtractBarcode(string barcode)
        {
            string str = "";
            try
            {
                string[] info = GlobalState.ExtractBarcode(barcode);
                if (info.Length == 19)
                {
                    txtMetrialNumber.Text = info[2];
                    txtBatchNumber.Text = info[10];
                    txtPackNumber.Text = info[6];
                    txtQty.Text = info[7];
                    txtUnit.Text = info[8];
                    str = "";
                }
                else
                {
                    txtMetrialNumber.Text = info[11];
                    txtBatchNumber.Text = info[2].Split(':')[1];
                    txtPackNumber.Text = info[3].Split(':')[1];
                    txtQty.Text = info[4].Split(':')[1];
                    txtUnit.Text = info[5];
                    str = "X";
                }
                return str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public Zsrohzctemp[] temp;
        private void btnConfirmDraw_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMetrialNumber.Text.Trim()))
            {
                MessageBox.Show("请扫描物料条码！");
                return;
            }
            if (GridDT.Rows.Count > 0)
            {
                DataRow[] dr = GridDT.Select("物料号='" + txtMetrialNumber.Text.Trim() + "' and 批次='" + txtBatchNumber.Text.Trim() + "' and 包号='" + txtPackNumber.Text.Trim() + "'");
                if (dr.Count() > 0)
                {
                    MessageBox.Show("包号重复扫描！");
                    CancelMaterial();
                    return;
                }
            }
            if (txtUnit.Text.Trim() == "吨" || txtUnit.Text.Trim() == "T" || txtUnit.Text.Trim() == "t" || txtUnit.Text.Trim() == "千克" || txtUnit.Text.Trim() == "KG" || txtUnit.Text.Trim() == "kg")
            {
                //
            }
            else
            {
                if (txtQty.Text.Trim().Contains('.'))
                {
                    MessageBox.Show("数量有误，请输入整数！");
                    return;
                }
            }
            decimal mal = decimal.Parse(txtQty.Text.Trim());
            MaterialModel model = new MaterialModel();
            model.Aufnr = ReservedNumber;
            model.Charg = txtBatchNumber.Text.Trim();
            model.Matnr = txtMetrialNumber.Text.Trim();
            model.Meins = txtUnit.Text.Trim();
            model.Menge = Math.Round(mal, 2);
            model.Werks = fac;
            model.Zgrop = user;
            model.Zmengd = decimal.Parse(txtQty.Text.Trim());
            model.Zplin = IssueLocation;
            model.Zpnum = txtPackNumber.Text.Trim();
            model.Zissl = IssueLocation;
            model.Zgetl = ReceivingLocation;
            model.Zusrd = user;
            model.Rsnum = ReservedNumber;
            model.Zlgte = DateTime.Parse(txtDate.Text).ToString("yyyyMMdd");

            bool isSuccess = DrawLogic.ConfirmDrawMaterial(model, sto);
            if (!isSuccess)
            {
                CancelMaterial();
            }
            else
            {
                GridDT.Rows.Add(fac, cboxOrder.Text.Trim(), model.Matnr, model.Charg, model.Zmengd, model.Meins, model.Zpnum, txtIssueLocation.Text.Trim(), txtReceivingLocation.Text.Trim(), GlobalState.UserID, sto);
                CommonAPI.Save(GridDT, GetType().Name);
                dataGrid1.DataSource = null;
                dataGrid1.DataSource = GridDT;
                ConfirmedMatetials.Add(model);
                CancelMaterial();
            }
        }

        private void txtIssueLocation_LostFocus(object sender, EventArgs e)
        {
            IssueLocation = txtIssueLocation.Text.Trim();
        }

        private void txtReceivingLocation_LostFocus(object sender, EventArgs e)
        {
            ReceivingLocation = txtReceivingLocation.Text.Trim();
        }

        private void txtBarcode1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ExtractBarcode(txtBarcode.Text.Trim());
                btnConfirmDraw.Focus();
            }
        }
        public string sto;
        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                sto = ExtractBarcode(txtBarcode.Text.Trim());

                MaterialModel model = new MaterialModel();
                model.Aufnr = ReservedNumber;
                model.Charg = txtBatchNumber.Text.Trim();
                model.Matnr = txtMetrialNumber.Text.Trim();
                model.Meins = txtUnit.Text.Trim();
                model.Menge = decimal.Parse(txtQty.Text.Trim());
                model.Werks = fac;
                model.Zgrop = user;
                model.Zmengd = decimal.Parse(txtQty.Text.Trim());
                model.Zplin = IssueLocation;
                model.Zpnum = txtPackNumber.Text.Trim();
                model.Zissl = IssueLocation;
                model.Zgetl = ReceivingLocation;
                model.Zusrd = user;
                model.Rsnum = ReservedNumber;

                //bool isFound = FindMaterial(model);
                //if (!isFound)
                //{
                //    MessageBox.Show("该包号物料未确认领料。");
                //    CancelMaterial1();
                //    return;
                //}

                UploadedMatetials.Add(model);
                // CancelMaterial1();
            }

        }

        private void btnCancelDraw_Click(object sender, EventArgs e)
        {
            try
            {
                Zppi516 para = new Zppi516();
                para.LtZrohzctemp = temp;
                Zsdmsg01[] outmess = new Zsdmsg01[10];
                para.Return = outmess;
                Zppi516Response response = GlobalState.GetWebService().Zppi516(para);
                Zsdmsg01[] mess = response.Return;
                for (int i = 0; i < mess.Length; i++)
                {
                    string str = mess[i].Message;
                    string val = mess[i].Ztype;
                    if (val != "S")
                    {
                        MessageBox.Show(str);
                        return;
                    }
                    else
                    {
                        MessageBox.Show(str);
                        cboxOrder.Text = "";
                        txtIssueLocation.Text = "";
                        txtReceivingLocation.Text = "";
                        this.tcMain.SelectedIndex = 0;
                        CancelMaterial1();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string pickUserName;
        public string pickUserPwd;
        //提交领料
        private void btnUploadDraw_Click(object sender, EventArgs e)
        {
            try
            {
                Purview login = new Purview();
                login.Owner = this;
                login.ShowDialog();
                Zppi517 para = new Zppi517();
                para.LtZrohzctemp = temp;
                para.Zusrd = GlobalState.UserID;
                Zsdmsg01[] outmess = new Zsdmsg01[10];
                para.Return = outmess;
                para.Werks = fac;
                para.Ztjid = pickUserName;
                para.Zpasw = pickUserPwd;
                Zppi517Response response = GlobalState.GetWebService().Zppi517(para);
                Zsdmsg01[] mess = response.Return;
                for (int i = 0; i < mess.Length; i++)
                {
                    string val = mess[i].Ztype;
                    string text = mess[i].Message;
                    if (val != "S")
                    {
                        MessageBox.Show(text);
                        return;
                    }
                    else if (text == "同意提交ID或密码正确")
                    {
                        // MessageBox.Show(text);
                        DeleteXML();
                        cboxOrder.Text = "";
                        txtIssueLocation.Text = "";
                        txtReceivingLocation.Text = "";
                        this.tcMain.SelectedIndex = 0;
                        CancelMaterial1();
                    }
                    else
                    {
                        MessageBox.Show(text);
                        DeleteXML();
                        cboxOrder.Text = "";
                        txtIssueLocation.Text = "";
                        txtReceivingLocation.Text = "";
                        this.tcMain.SelectedIndex = 0;
                        CancelMaterial1();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool FindMaterial(MaterialModel model)
        {
            foreach (MaterialModel material in ConfirmedMatetials)
            {
                if (material.Zpnum == model.Zpnum && material.Matnr == model.Matnr)
                    return true;
            }

            return false;
        }
        //确认接收发送库位
        private void button2_Click(object sender, EventArgs e)
        {
            string s1 = txtIssueLocation.Text.Trim();
            string s2 = txtReceivingLocation.Text.Trim();
            if (string.IsNullOrEmpty(s1))
            {
                MessageBox.Show("发货库位不能为空！");
                return;
            }
            if (!validation.IsNumber(s1) || s1.Length != 4)
            {
                MessageBox.Show("输入有误，请输入4位整数！");
                return;
            }
            if (string.IsNullOrEmpty(s2))
            {
                MessageBox.Show("接收库位不能为空！");
                return;
            }
            this.tcMain.SelectedIndex = 1;
            txtBarcode.Focus();
        }
        //扫描完成
        private void button3_Click(object sender, EventArgs e)
        {
            if (GridDT.Rows.Count < 1)
            {
                MessageBox.Show("至少扫描一个物料条码！");
                return;
            }
            temp = (from DataRow row in GridDT.Rows
                    select new Zsrohzctemp
                    {
                        Matnr = "0000000000" + row["物料号"].ToString(),
                        Charg = row["批次"].ToString(),
                        Zpnum = row["包号"].ToString(),
                        Zsto = row["是否半成品"].ToString(),
                        Zusez = GlobalState.UserID,
                        Rsnum = "0000" + cboxOrder.Text.Trim(),
                        Werks = fac
                    }).ToArray();
            this.tcMain.SelectedIndex = 2;
        }
        //
        private void txtDate_ValueChanged(object sender, EventArgs e)
        {
            this.txtDate.Enabled = false;
        }
    }
}