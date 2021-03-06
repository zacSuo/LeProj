﻿using System;
using System.Windows.Forms;

namespace Core
{
    public class StringValidator
    {
        #region 字符串
        private static bool RegexCheck(string input, string errorMsg, Func<string,bool> checkMethod)
        {
            if (!checkMethod(input))
            {
                MessageBox.Show(errorMsg);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 实数校验
        /// </summary>
        /// <param name="strCheck"></param>
        /// <returns></returns>
        public static bool IsRealNumber(string strCheck)
        {
            return RegexCheck(strCheck, "仅可输入正实数", RegexHelper.IsValidNumber);
        }

        /// <summary>
        /// 整数校验
        /// </summary>
        /// <param name="strCheck"></param>
        /// <returns></returns>
        public static bool IsNumber(string strCheck)
        {
            return RegexCheck(strCheck, "仅可输入整数", RegexHelper.IsValidNumber);
        }

        /// <summary>
        /// 正整数校验
        /// </summary>
        /// <param name="strCheck"></param>
        /// <returns></returns>
        public static bool IsUnsignedInteger(string strCheck)
        {
            return RegexCheck(strCheck, "仅可输入正整数", RegexHelper.IsNumberUnsignedInteger);
        }

        /// <summary>
        /// 字符串是否为空
        /// </summary>
        /// <param name="strCheck"></param>
        /// <param name="tipTitle"></param>
        /// <returns></returns>
        public static bool HasContent(string strCheck, string tipTitle)
        {
            if (strCheck == null || strCheck.Equals(string.Empty))
            {
                MessageBox.Show(tipTitle + "不可为空");
                return false;
            }
            return true;
        }
        #endregion 

        #region Text控件

        /// <summary>
        /// 字符串是否为空
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="tipTitle"></param>
        /// <returns></returns>
        public static bool HasContent(Control ctrl, string tipTitle)
        {
            return HasContent(ctrl, tipTitle, " can NOT be empty");
        }

        /// <summary>
        /// 字符串是否为空
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="tipTitle"></param>
        /// <param name="tipContent"></param>
        /// <returns></returns>
        public static bool HasContent(Control ctrl, string tipTitle,string tipContent)
        {
            string strCheck = ctrl.Text.Trim();
            if (strCheck.Equals(string.Empty))
            {
                MessageBox.Show(tipTitle + tipContent);
                ctrl.Focus();
                return false;
            }
            return true;
        }


        private static bool RegexCheck(Control ctrl, string errorMsg, Func<string, bool> checkMethod)
        {
            string strCheck = ctrl.Text.Trim();
            if (!checkMethod(strCheck))
            {
                MessageBox.Show(errorMsg);
                ctrl.Focus();
                return false;
            }
            return true;
        }

        private static bool RegexEmptyOrCheck(Control ctrl, string errorMsg, Func<string, bool> checkMethod)
        {
            return ctrl.Text.Trim().Equals(string.Empty) || RegexCheck(ctrl, errorMsg, checkMethod);
        }

        /// <summary>
        /// 正实数校验
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public static bool IsUnsignedRealNumber(Control ctrl)
        {
            return IsUnsignedRealNumber(ctrl, "Only real number allow");
        }

        /// <summary>
        /// 正实数校验
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="tipContent"></param>
        /// <returns></returns>
        public static bool IsUnsignedRealNumber(Control ctrl, string tipContent)
        {
            return RegexCheck(ctrl, tipContent, RegexHelper.IsUnsignedRealNumber);
        }

        /// <summary>
        /// 空或正实数
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public static bool IsEmptyOrUnsignedRealNumber(Control ctrl)
        {
            return IsEmptyOrUnsignedRealNumber(ctrl, "仅可输入正实数");
        }

        /// <summary>
        /// 空或正实数
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public static bool IsEmptyOrUnsignedRealNumber(Control ctrl, string tipContent)
        {
            return RegexEmptyOrCheck(ctrl, tipContent, RegexHelper.IsUnsignedRealNumber);
        }

        /// <summary>
        /// 空或正整数
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public static bool IsEmptyOrUnsignedInteger(Control ctrl)
        {
            return IsEmptyOrUnsignedInteger(ctrl, "仅可输入正整数");
        }

        /// <summary>
        /// 空或正整数
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public static bool IsEmptyOrUnsignedInteger(Control ctrl, string tipContent)
        {
            return RegexEmptyOrCheck(ctrl, tipContent, RegexHelper.IsNumberUnsignedInteger);
        }

        /// <summary>
        /// 整数校验
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public static bool IsNumber(Control ctrl)
        {
            return RegexCheck(ctrl, "仅可输入整数", RegexHelper.IsValidNumber);
        }

        /// <summary>
        /// 正整数校验
        /// </summary>
        /// <param name="strCheck"></param>
        /// <returns></returns>
        public static bool IsUnsignedInteger(Control ctrl)
        {
            return RegexCheck(ctrl, "仅可输入正整数", RegexHelper.IsNumberUnsignedInteger);
        }
        #endregion
    }
}
