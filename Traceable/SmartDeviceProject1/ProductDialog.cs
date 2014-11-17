using System;
using System.Windows.Forms;
using SmartDeviceProject1.Common;

namespace SmartDeviceProject1
{
    public partial class ProductDialog : Form
    {
        private readonly BaseInfo _praseProduct;
        public BeforeSave BeforeSave;
        public ProductDialog(BaseInfo praseProduct)
        {
            _praseProduct = praseProduct;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string text = txtQty.Text;
            _praseProduct.Menge = Convert.ToInt32(text);
            if (BeforeSave != null)
            {
                if (!BeforeSave(_praseProduct))
                {
                    return;
                }
            }
            DialogResult = DialogResult.OK;
        }

        private void ProductDialog_Load(object sender, EventArgs e)
        {
            txtMetrialNumber.Text = _praseProduct.Matnr;
            txtBatchNumber.Text = _praseProduct.Charg;
            txtUnit.Text = _praseProduct.Meins;
            txtPackNumber.Text = _praseProduct.Zpnum;
            txtQty.Text = _praseProduct.Menge.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }

    public delegate bool BeforeSave(BaseInfo baseInfo);


}