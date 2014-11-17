using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace SmartDeviceProject1.Common
{
    public class GlobalState
    {
        public static string UserID = string.Empty;

        public static System.Net.NetworkCredential GetNetworkCredential()
        {
            System.Net.NetworkCredential credential = new System.Net.NetworkCredential();
            credential.UserName = "sd02";
            credential.Password = "123456";

            return credential;
        }
        //zppi
        public static Webservice1.zdev02_webservice GetWebService()
        {
            //todo: get service address from app config file
            Webservice1.zdev02_webservice service = new SmartDeviceProject1.Webservice1.zdev02_webservice();
            service.Url = "http://114.251.177.94:8000/sap/bc/srt/rfc/sap/zdev02_webservice/500/zdev" +
                "02_webservice/binding";
            service.Credentials = GetNetworkCredential();
            return service;
        }
        //zsdi
        public static Packaging.ZDEV_WEBSERVICE GetWebServiceSD()
        {
            //todo: get service address from app config file
            Packaging.ZDEV_WEBSERVICE service = new SmartDeviceProject1.Packaging.ZDEV_WEBSERVICE();
            service.Url = "http://114.251.177.94:8000/sap/bc/srt/rfc/sap/zdev_webservice/500/zdev_w" +
                "ebservice/binding";
            service.Credentials = GetNetworkCredential();
            return service;
        }
        //zmmi
        public static WebReference.ZMMI_WEBSERVICE GetWebReference()
        {
            //todo: get service address from app config file
            WebReference.ZMMI_WEBSERVICE service = new SmartDeviceProject1.WebReference.ZMMI_WEBSERVICE();
            service.Url = "http://114.251.177.94:8000/sap/bc/srt/rfc/sap/zmmi_webservice/500/zmmi_w" +
                "ebservice/binding";
            service.Credentials = GetNetworkCredential();
            return service;
        }
        public static string[] ExtractBarcode(string barcode)
        {
            barcode = barcode.Replace("||", "^");
            return barcode.Split('^');
        }

        // 获取配置信息
        public static string GetConfigInfo(string type)
        {
            string info = "";
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            string filepath = path + @"\config.txt";
            StreamReader objReader = new StreamReader(filepath, System.Text.Encoding.Default);
            string sLine = "";
            ArrayList LineList = new ArrayList();
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                    LineList.Add(sLine);
            }
            objReader.Close();
            for (int i = 0; i < LineList.Count; i++)
            {
                string[] str = LineList[i].ToString().Split('：');
                if (str[0] == type)
                {
                    info = str[1].ToString();
                }
            }
            return info;

        }

        //  val ID
        public static bool IsExist(string sn)
        {
            string path = System.IO.Path.GetDirectoryName("/Windows/");
            string filepath = path + @"\intermac.dat";
            StreamReader objReader = new StreamReader(filepath, System.Text.Encoding.Default);
            string sLine = "";
            ArrayList LineList = new ArrayList();
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                    LineList.Add(sLine);
            }
            objReader.Close();
            string str = "";
            for (int i = 0; i < LineList.Count; i++)
            {
                str += LineList[i].ToString() + ",";
                if (str.Contains(sn))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;

        }
    }
}
