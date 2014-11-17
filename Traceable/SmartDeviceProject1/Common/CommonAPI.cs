using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using SmartDeviceProject1.STOInbund;

namespace SmartDeviceProject1.Common
{
    /// <summary>
    /// </summary>
    public class CommonAPI
    {
        /// <summary>
        ///     解析产品条码
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static ProductInfo PraseProduct(string barcode)
        {
            try
            {


                //txtBarcode.Text =
                //  "采购订单号||采购订单行项目||物料编码||物料描述||版面||供应商批次||包号/桶号||数量||计量单位||生产日期||批次||供应商名称||素铁物料编号||铁镀锡量||铁硬度||来铁厂家||素铁重量||素铁重量单位||素铁资源号||素铁批次";

                string[] strings = barcode
                    .Replace("||", "$").Split(new[] { '$' });
                var productModel = new ProductInfo
                {
                    Werks = strings[0],
                    // Maktx = strings[3].Split(':')[1],
                    //Zbanm = strings[4].Split(':')[1],
                    // Charg = strings[2].Split(':')[1],
                    //Zpnum = strings[3].Split(':')[1],
                    //  Menge = Convert.ToInt32(strings[4].Split(':')[1]),
                    Meins = strings[2],
                    // Zline = strings[6].Split(':')[1],
                    //  Zdate = strings[7].Split(':')[1],
                    // 生产时间 = strings[8].Substring(5),
                    //  检验结果 = strings[9].Split(':')[1],
                    //  检验员 = strings[10].Split(':')[1],
                    Matnr = strings[1]
                    // Werks = strings[12]
                };
                return productModel;
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        ///     原材料条码
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static ProductInfo RawMaterials(string barcode)
        {
            try
            {

                //txtBarcode.Text =
                //  "采购订单号||采购订单行项目||物料编码||物料描述||版面||供应商批次||包号/桶号||数量||计量单位||生产日期||批次||供应商名称||素铁物料编号||铁镀锡量||铁硬度||来铁厂家||素铁重量||素铁重量单位||素铁资源号||素铁批次";
                //产品名称:红牛/691/普通/饮料罐||版面:精装1段||批次:101490554T||包号:0002||数量:7980||罐||生产线:制罐八线||生产日期:20141020||生产时间:19:00||检验结果:合格||检验员:张三||30000000||2040

                string[] strings = barcode
                    .Replace("||", "$").Split(new[] { '$' });
                var productModel = new ProductInfo
                {
                    //Maktx = strings[3].Split(':')[1],
                    //版面
                    Zbanm = strings[4].Split(':')[1],
                    //批次
                    Charg = strings[2].Split(':')[1],
                    //包号
                    Zpnum = strings[3].Split(':')[1],
                    //数量
                    Menge = decimal.Parse(strings[4].Split(':')[1]),
                    Zpdte = strings[7].Split(':')[1],
                    //单位
                    Meins = strings[5],
                    //Zline = strings[6].Split(':')[1],
                    //Zdate = strings[7].Split(':')[1],
                    //生产时间 = strings[8].Substring(5),
                    // 检验结果 = strings[9].Split(':')[1],
                    // 检验员 = strings[10].Split(':')[1],
                    //物料
                    Matnr = strings[11],
                    //工厂
                    Werks = GlobalState.GetConfigInfo("工厂")
                };
                return productModel;
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// 4500004683||10||20000134||红牛/691/彩印铁/普通/0.20*858*1035||FF||123113A311||88||15||张||20141017||FF||FF||FF||FF||FF||FF||FF||FF||FF
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static MaterialInfo PraseMaterial(string barcode)
        {
            //采购订单号||采购订单行项目||物料编码||物料描述||版面||供应商批次||包号/桶号||数量||计量单位||生产日期||批次||素铁物料编号||铁镀锡量||铁硬度||来铁厂家||素铁重量||素铁重量单位||素铁资源号||素铁批次
            //"4500006648||20||20000134||红牛/691/彩印铁/普通/0.20*858*1035||FF||123113A311||13||120||张||20141017||123113A311||FF||FF||FF||FF||FF||FF||FF||FF"      

            //4500004683||10||20000134||红牛/691/彩印铁/普通/0.20*858*1035||FF||123113A311||89||20||张||20141017||FF||FF||FF||FF||FF||FF||FF||FF||FF
            decimal d;
            try
            {
                string[] info = barcode
             .Replace("||", "$").Split(new[] { '$' });
                if (info[15] != "FF")
                {
                    d = decimal.Parse(info[15]);
                }
                else
                {
                    d = decimal.Parse("0");
                }
                var productModel = new MaterialInfo
                {
                    //采购凭证号
                    Ebeln = info[0],
                    //采购项目编号
                    Ebelp = info[1],
                    //物料编码
                    Matnr = info[2],
                    //版面
                    Zbanm = info[4],
                    //供应商批次
                    Zvlot = info[5],
                    //包号/桶号
                    Zpnum = info[6],
                    //数量
                    Menge = decimal.Parse(info[7]),
                    //计量单位
                    Meins = info[8],
                    //生产日期
                    Zpdte = info[9],
                    //批次
                    Charg = info[10],
                    //素铁物料编号
                    Zmatn = info[11],
                    //铁镀锡量
                    Ztdxl = info[12],
                    //铁硬度
                    Ztydz = info[13],
                    //来铁厂家
                    Zltcj = info[14],
                    //素铁重量
                    Szmeng = Math.Round(d, 2),
                    //素铁单位
                    Szment = info[16],
                    //素铁资源号
                    Ztres = info[17]

                };

                return productModel;
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public static void GoBack(Form form)
        {
            string filePath = Assembly.GetExecutingAssembly().GetName().CodeBase;
            var s = form.GetType().Name;
            if (filePath.Contains(@"\"))
            {
                filePath = filePath.Substring(0, filePath.LastIndexOf(@"\")) + @"\" + s + @".xml";
            }
            else
            {
                filePath = filePath.Substring(0, filePath.LastIndexOf(@"/")) + @"/" + s + @".xml";
                filePath = filePath.Replace(@"file:///", "").Replace(@"/", @"\");
            }

            if (File.Exists(filePath))
            {
                if (
                    MessageBox.Show("还有未提交的修改，是否保留数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    File.Delete(filePath);
                }

            }

            form.Close();
        }

        public static void Error(Exception exception, string m)
        {
            if (!string.IsNullOrEmpty(m))
            {
                MessageBox.Show(m + ":" + exception.Message);
            }
            else
                MessageBox.Show(exception.Message);
        }

        public static void Message(string message)
        {

            MessageBox.Show(message);
        }

        /// <summary>
        ///  组织
        /// </summary>
        public static string Ekorg
        {
            get
            {
                // TODO:
                return "2040";
            }
        }

        public static string Werks
        {
            get
            {
                // TODO:
                return "2040";
            }
        }

        public static string Zusrd
        {
            get
            {
                // TODO:
                return "1004";
            }
        }

        /// <summary>
        /// 默认装运地点
        /// </summary>
        public static string Vstel
        {
            get
            {
                // TODO:默认装运地点
                return "2040";
            }

        }


        public static T GetValue<T>(string s) where T : class
        {
            string filePath = Assembly.GetExecutingAssembly().GetName().CodeBase;

            filePath = filePath.Substring(0, filePath.LastIndexOf(@"\")) + @"\" + s + @".xml";
            if (!File.Exists(filePath))
                return default(T);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StreamReader writer = new StreamReader(filePath, Encoding.UTF8))
            {
                return xmlSerializer.Deserialize(writer) as T;
            }
        }

        public static void SetValue<T>(T value, string s)
        {
            string filePath = Assembly.GetExecutingAssembly().GetName().CodeBase;

            filePath = filePath.Substring(0, filePath.LastIndexOf(@"\")) + @"\" + s + @".xml";

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                xmlSerializer.Serialize(writer, value);
                writer.Flush();
            }
        }

        public static void Save(DataTable dt, string s)
        {
            string filePath = Assembly.GetExecutingAssembly().GetName().CodeBase;

            if (filePath.Contains(@"\"))
            {
                filePath = filePath.Substring(0, filePath.LastIndexOf(@"\")) + @"\" + s + @".xml";
            }
            else
            {
                filePath = filePath.Substring(0, filePath.LastIndexOf(@"/")) + @"/" + s + @".xml";
                filePath = filePath.Replace(@"file:///", "").Replace(@"/", @"\");
            }
            dt.TableName = s
                ; dt.WriteXml(filePath);
        }

        public static void Save(DataSet dt, string s)
        {
            string filePath = Assembly.GetExecutingAssembly().GetName().CodeBase;

            if (filePath.Contains(@"\"))
            {
                filePath = filePath.Substring(0, filePath.LastIndexOf(@"\")) + @"\" + s + @".xml";
            }
            else
            {
                filePath = filePath.Substring(0, filePath.LastIndexOf(@"/")) + @"/" + s + @".xml";
                filePath = filePath.Replace(@"file:///", "").Replace(@"/", @"\");
            }
            dt.WriteXml(filePath);
        }


        public static bool Load(DataTable dt, string s)
        {
            string filePath = Assembly.GetExecutingAssembly().GetName().CodeBase;

            if (filePath.Contains(@"\"))
            {
                filePath = filePath.Substring(0, filePath.LastIndexOf(@"\")) + @"\" + s + @".xml";
            }
            else
            {
                filePath = filePath.Substring(0, filePath.LastIndexOf(@"/")) + @"/" + s + @".xml";
                filePath = filePath.Replace(@"file:///", "").Replace(@"/", @"\");
            }

            if (!File.Exists(filePath))
            {
                return false;
            }

            ; dt.ReadXml(filePath);
            return true;
        }

        public static bool Load(DataSet dt, string s)
        {
            string filePath = Assembly.GetExecutingAssembly().GetName().CodeBase;

            if (filePath.Contains(@"\"))
            {
                filePath = filePath.Substring(0, filePath.LastIndexOf(@"\")) + @"\" + s + @".xml";
            }
            else
            {
                filePath = filePath.Substring(0, filePath.LastIndexOf(@"/")) + @"/" + s + @".xml";
                filePath = filePath.Replace(@"file:///", "").Replace(@"/", @"\");
            }

            if (!File.Exists(filePath))
            {
                return false;
            }

            ; dt.ReadXml(filePath);
            return true;
        }

        public static bool Remove(string s)
        {
            string filePath = Assembly.GetExecutingAssembly().GetName().CodeBase;

            if (filePath.Contains(@"\"))
            {
                filePath = filePath.Substring(0, filePath.LastIndexOf(@"\")) + @"\" + s + @".xml";
            }
            else
            {
                filePath = filePath.Substring(0, filePath.LastIndexOf(@"/")) + @"/" + s + @".xml";
                filePath = filePath.Replace(@"file:///", "").Replace(@"/", @"\");
            }

            if (!File.Exists(filePath)) return false;
            File.Delete(filePath);
            return true;
        }
        public static void Sequence(params Control[] controls)
        {
            Sequence(null, controls);
        }
        public static void Sequence(EventHandler eve, params Control[] controls)
        {
            for (int index = 0; index < controls.Length - 1; index++)
            {
                Control control = controls[index];
                Control nextControl = controls[index + 1];
                control.KeyPress += (sender, e) =>
                {
                    if (e.KeyChar == (char)Keys.Return)
                    {
                        nextControl.Focus();
                        if (nextControl is Button)
                        {
                            try
                            {
                                eve.Invoke(nextControl, new SequenceEventArgs { SequenceType = typeof(TextBox).Name });
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                //   throw;
                            }

                        }
                    }
                };
            }

            Control control1 = controls[controls.Length - 1];
            if (control1 is Button)
            {
                control1.Click += (sender1, e1) =>
                {
                    try
                    {
                        eve.Invoke(control1, new SequenceEventArgs { SequenceType = typeof(Button).Name });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        //throw;
                    }

                };
            }
        }

        public static void Fill(DataGrid dataGrid, DataTable table)
        {


        }

        public static DateTime ParseTime(string toString)
        {
            return
                DateTime.Parse(toString.Substring(0, 4) + "-" + toString.Substring(4, 2) + "-" + toString.Substring(6));
        }
    }

    public delegate void CallFunciton();




    public class SequenceEventArgs : EventArgs
    {
        public string SequenceType { get; set; }
    }
}