using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SmartDeviceProject1.Common;
using SmartDeviceProject1.Webservice1;
using System.IO;
using System.Collections;
using System.Windows.Forms;

namespace SmartDeviceProject1.Input
{
    public class InputLogic
    {
        public static string ConfirmOrder(string productionOrder, string factory)
        {
            string proLine = "";
            Zppi501 item = new Zppi501();
            item.Aufnr = productionOrder;

            //  item.Return = new Zsdmsg01[10];

            Zppi501Response response = GlobalState.GetWebService().Zppi501(item);
            for (int i = 0; i < response.LtZplinemaster.Length; i++)
            {
                proLine += response.LtZplinemaster[i].Zpline + ",";
            }
            return proLine;
        }

        public static bool ConformLine(string productionLine)
        {
            return true;
        }

        public static bool InputMaterial(MaterialModel model, string sto)
        {
            Zppi511 item = new Zppi511();
            item.Aufnr = model.Aufnr;
            item.Werks = model.Werks;
            item.Zplin = model.Zplin;
            item.Charg = model.Charg;
            item.Zpnum = model.Zpnum;
            item.Matnr = model.Matnr;
            item.Meins = model.Meins;
            item.Menge = model.Menge;
            item.Zmengd = model.Zmengd;
            item.Zgrop = model.Zgrop;
            item.Zsto = sto;
            Zsdmsg01[] outmess = new Zsdmsg01[10];
            item.Return = outmess;
            Zppi511Response response = GlobalState.GetWebService().Zppi511(item);
            Zsdmsg01[] mess = response.Return;
            string str = "";
            string val = "";
            for (int i = 0; i < mess.Length; i++)
            {
                str += mess[i].Message + "\r";
                val += mess[i].Ztype;
            }
            if (val.Contains("W"))
            {
                MessageBox.Show(str);
                return true;
            }
            else if (val == "E")
            {
                MessageBox.Show(str);
                return false;
            }
            return true;

            //try
            //{
            //    if (response.Return[0].Message == "S")
            //        return true;
            //}
            //catch (Exception exception)
            //{
            //    throw new BusinessException("投料失败。", exception);
            //}

            //  return false;
        }

        public static bool InputMaterial2(MaterialModel model, string sto)
        {
            Zppi512 item = new Zppi512();
            item.Aufnr = model.Aufnr;
            item.Werks = model.Werks;
            item.Zplin = model.Zplin;
            item.Charg = model.Charg;
            item.Zpnum = model.Zpnum;
            item.Matnr = model.Matnr;
            item.Meins = model.Meins;
            item.Menge = model.Menge;
            item.Zmengd = model.Zmengd;
            item.Zgrop = model.Zgrop;
            item.Zsto = sto;
            Zsdmsg01[] outmess = new Zsdmsg01[10];
            item.Return = outmess;
            Zppi512Response response = GlobalState.GetWebService().Zppi512(item);
            Zsdmsg01[] mess = response.Return;
            string str = "";
            string val = "";
            for (int i = 0; i < mess.Length; i++)
            {
                str += mess[i].Message + "\r";
                val += mess[i].Ztype;
            }
            if (val.Contains("W"))
            {
                MessageBox.Show(str);
                return true;
            }
            else if (val == "E")
            {
                MessageBox.Show(str);
                return false;
            }
            return true;

            //try
            //{
            //    if (response.Return[0].Message == "S")
            //        return true;
            //}
            //catch (Exception exception)
            //{
            //    throw new BusinessException("投料失败。", exception);
            //}

            //  return false;
        }

        public static bool ReturnMaterial(MaterialModel model)
        {
            Zppi512 item = new Zppi512();
            item.Aufnr = model.Aufnr;
            item.Werks = model.Werks;
            item.Zplin = model.Zplin;
            item.Charg = model.Charg;
            item.Zpnum = model.Zpnum;
            item.Matnr = model.Matnr;
            item.Meins = model.Meins;
            item.Menge = model.Menge;
            item.Zmengd = model.Zmengd;
            item.Zgrop = model.Zgrop;

            Zppi512Response response = GlobalState.GetWebService().Zppi512(item);
            try
            {
                if (response.Return[0].Message == "Y")
                    return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException("退料失败。", exception);
            }

            return false;
        }
    }
}
