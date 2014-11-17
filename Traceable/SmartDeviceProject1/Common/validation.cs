using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datalogic_Preprocessor
{
    /// <summary>
    /// 表单验证类
    /// </summary>
    public class validation
    {
        #region Check(object obj,string reg)验证基函数
        public static bool Check(object obj, string reg)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(obj.ToString(), reg, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
        #endregion

        #region IsNotEmpty验证是否为空
        ///

        /// IsNotEmpty验证是否为空
        ///

        public static bool IsNotEmpty(object obj)
        {
            return Check(obj, @".?[^\s　]+");
        }
        #endregion

        #region 验证是不是正常字符 字母，数字，下划线的组合
        /// <summary>
        /// 验证是不是正常字符 字母，数字，下划线的组合
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>

        public static bool IsNormalChar(object obj)
        {
            return Check(obj, @"[\w\d_]+");
        }
        #endregion

        #region IsEnglish验证是否为英文字符及下划线
        /// <summary>
        /// IsEnglish验证是否为英文字符及下划线
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsEnglish(object obj)
        {
            return Check(obj, @"^[a-zA-Z0-9_\-]+$");
        }
        #endregion

        #region IsChinese验证是否为中文字符
        /// <summary>
        /// IsChinese验证是否为中文字符
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsChinese(object obj)
        {
            return Check(obj, @"^[\u0391-\uFFE5]+$");
        }
        #endregion

        #region IsDate是否为有效的日期格式
        /// <summary>
        /// IsDate是否为有效的日期格式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsDate(object obj)
        {
            try
            {
                DateTime time = Convert.ToDateTime(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region IsEmail是否为有效的邮箱格式
        /// <summary>
        ///  IsEmail是否为有效的邮箱格式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsEmail(object obj)
        {
            return Check(obj, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
        }
        #endregion

        #region IsUrl是否为有效的超链接格式
        /// <summary>
        /// IsUrl是否为有效的超链接格式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsUrl(object obj)
        {
            return Check(obj, @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&%_\./-~-]*)?$");
        }
        #endregion

        #region IsPhone是否为有效的电话号码
        /// <summary>
        /// IsPhone是否为有效的电话号码
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsPhone(object obj)
        {
            return Check(obj, @"^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$");
        }
        #endregion

        #region IsMobile是否为有效的手机号码
        /// <summary>
        /// IsMobile是否为有效的手机号码
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsMobile(object obj)
        {
            return Check(obj, @"^((\(\d{2,3}\))|(\d{3}\-))?((13\d{9})|(159\d{8}))$");
        }
        #endregion

        #region IsIP是否为有效的IP地址
        /// <summary>
        /// IsIP是否为有效的IP地址
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsIP(object obj)
        {
            return Check(obj, @"^(0|[1-9]\d?|[0-1]\d{2}|2[0-4]\d|25[0-5]).(0|[1-9]\d?|[0-1]\d{2}|2[0-4]\d|25[0-5]).(0|[1-9]\d?|[0-1]\d{2}|2[0-4]\d|25[0-5]).(0|[1-9]\d?|[0-1]\d{2}|2[0-4]\d|25[0-5])$");
        }
        #endregion

        #region IsZipCode是否为有效的邮政编码
        /// <summary>
        /// IsZipCode是否为有效的邮政编码
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsZipCode(object obj)
        {
            return Check(obj, @"^[1-9]\d{5}$");
        }
        #endregion

        #region IsIdCard是否为有效的身份证号码
        /// <summary>
        /// IsIdCard是否为有效的身份证号码
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsIdCard(object obj)
        {
            return Check(obj, @"(^\d{15}$)|(^\d{17}[0-9Xx]$)");
        }
        #endregion

        #region IsQQ是否为有效的QQ号码
        /// <summary>
        /// IsQQ是否为有效的QQ号码
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsQQ(object obj)
        {
            return Check(obj, @"^[1-9]\d{4,10}$");
        }
        #endregion

        #region IsMSN是否为有效的MSN帐户
        /// <summary>
        /// IsMSN是否为有效的MSN帐户
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsMSN(object obj)
        {
            return Check(obj, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }
        #endregion

        #region IsNumber验证是不是数字
        /// <summary>
        /// IsNumber验证是不是数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNumber(object obj)
        {
            return Check(obj, @"^[-\+]?\d+(\.\d+)?$");
        }
        #endregion

        #region IsInteger验证是不是整数
        /// <summary>
        /// IsInteger验证是不是整数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsInteger(object obj)
        {
            return Check(obj, @"^-?\d+$");
        }
        #endregion

        #region IsUnsignedInteger验证是不是正整数
        /// <summary>
        /// IsUnsignedInteger验证是不是正整数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsUnsignedInteger(object obj)
        {
            return Check(obj, @"^[0-9]*[1-9][0-9]*$");
        }
        #endregion

        #region IsSignedInteger验证是不是负整数
        /// <summary>
        /// IsSignedInteger验证是不是负整数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsSignedInteger(object obj)
        {
            return Check(obj, @"^-[0-9]*[1-9][0-9]*$");
        }
        #endregion
    }
}
