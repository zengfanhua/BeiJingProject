using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SmartDeviceProject1.Common;
using SmartDeviceProject1.Webservice1;
using System.Windows.Forms;

namespace SmartDeviceProject1.Draw
{
    public class DrawLogic
    {
        public static bool ConfirmReservedNumber(string number, out string receivingLocation, out string issueLocation)
        {
            issueLocation = string.Empty;
            receivingLocation = string.Empty;

            Zppi506 item = new Zppi506();
            item.Rsnum = number;
            item.Return = new Zsdmsg01[10];

            Zppi506Response response = GlobalState.GetWebService().Zppi506(item);
            try
            {
                if (response.Return[0].Ztype == "S")
                {
                    issueLocation = response.Fhkw;
                    receivingLocation = response.Jskw;

                    return true;
                }
                else
                {
                    MessageBox.Show(response.Return[0].Message);
                    return false;
                }

            }
            catch (Exception exception)
            {
                throw new BusinessException("调用确认预留号接口失败。");
            }

            return false;
        }

        public static bool ConfirmDrawMaterial(MaterialModel model, string oper)
        {
            try
            {
                int rs = int.Parse(model.Rsnum);
                int i = int.Parse(model.Matnr);
                Zppi515 item = new Zppi515();
                item.Werks = model.Werks;
                item.Charg = model.Charg;
                item.Zpnum = model.Zpnum;
                item.Matnr = i.ToString("000000000000000000");
                item.Meins = model.Meins;
                item.Menge = model.Menge;
                item.Rsnum = rs.ToString("0000000000");
                item.Zgetl = model.Zgetl;
                item.Zissl = model.Zissl;
                item.Zusrd = GlobalState.UserID;
                item.Zlgte = model.Zlgte;
                item.Zsto = oper;
                Zsdmsg01[] outmess = new Zsdmsg01[10];
                item.Return = outmess;
                Zppi515Response response = GlobalState.GetWebService().Zppi515(item);
                Zsdmsg01[] mess = response.Return;
                for (int k = 0; k < mess.Length; k++)
                {
                    if (mess[k].Ztype == "S")
                    {
                        return true;
                    }
                    MessageBox.Show(mess[k].Message);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool UploadDrawMaterial(MaterialModel model)
        {
            // Zrohzctemp temp = new Zrohzctemp();
            //temp.Werks = model.Werks;
            //temp.Charg = model.Charg;
            //temp.Zpnum = model.Zpnum;
            //temp.Matnr = model.Matnr;
            //temp.Meins = model.Meins;
            //temp.Menge = model.Menge;
            //temp.Rsnum = model.Rsnum;
            //temp.Zgetl = model.Zgetl;
            //temp.Zissl = model.Zissl;

            //Zppi517 item = new Zppi517();
            //item.Zusrd = model.Zusrd;
            ////item.LtZrohzctemp = temp;


            //Zppi517Response response = GlobalState.GetWebService().Zppi517(item);
            //try
            //{
            //    if (response.Return[0].Message == "Y")
            //        return true;
            //}
            //catch (Exception exception)
            //{
            //    throw new BusinessException("提交领料失败。", exception);
            //}

            return false;
        }

        public static bool DeleteDrawMaterial(MaterialModel model)
        {
            //Zppi516 item = new Zppi516();
            //item.Werks = model.Werks;
            //item.Charg = model.Charg;
            //item.Zpnum = model.Zpnum;
            //item.Matnr = model.Matnr;
            //item.Meins = model.Meins;
            //item.Menge = model.Menge;
            //item.Rsnum = model.Rsnum;
            //item.Zgetl = model.Zgetl;
            //item.Zissl = model.Zissl;
            //item.Zusrd = model.Zusrd;


            //  Zppi516Response response = GlobalState.GetWebService().Zppi516(item);
            //try
            //{
            //    if (response.Return[0].Message == "Y")
            //        return true;
            //}
            //catch (Exception exception)
            //{
            //    throw new BusinessException("取消领料失败。", exception);
            //}

            return false;
        }


    }
}
