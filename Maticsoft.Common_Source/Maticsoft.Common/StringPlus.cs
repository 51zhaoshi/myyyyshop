namespace Maticsoft.Common
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public class StringPlus
    {
        public static string DelLastChar(string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }

        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }

        public static string GetArrayStr(List<int> list)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == (list.Count - 1))
                {
                    builder.Append(list[i].ToString());
                }
                else
                {
                    builder.Append(list[i]);
                    builder.Append(",");
                }
            }
            return builder.ToString();
        }

        public static string GetArrayStr(List<string> list, string speater)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == (list.Count - 1))
                {
                    builder.Append(list[i]);
                }
                else
                {
                    builder.Append(list[i]);
                    builder.Append(speater);
                }
            }
            return builder.ToString();
        }

        public static string GetArrayValueStr(Dictionary<int, int> list)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<int, int> pair in list)
            {
                builder.Append(pair.Value + ",");
            }
            if (list.Count > 0)
            {
                return DelLastComma(builder.ToString());
            }
            return "";
        }

        public static string GetCleanStyle(string StrList, string SplitString)
        {
            if (StrList == null)
            {
                return "";
            }
            return StrList.Replace(SplitString, "");
        }

        public static string GetNewStyle(string StrList, string NewStyle, string SplitString, out string Error)
        {
            string str = "";
            if (StrList == null)
            {
                str = "";
                Error = "请输入需要划分格式的字符串";
                return str;
            }
            int length = StrList.Length;
            int num2 = GetCleanStyle(NewStyle, SplitString).Length;
            if (length != num2)
            {
                str = "";
                Error = "样式格式的长度与输入的字符长度不符，请重新输入";
                return str;
            }
            string str2 = "";
            for (int i = 0; i < NewStyle.Length; i++)
            {
                if (NewStyle.Substring(i, 1) == SplitString)
                {
                    str2 = str2 + "," + i;
                }
            }
            if (str2 != "")
            {
                str2 = str2.Substring(1);
            }
            foreach (string str3 in str2.Split(new char[] { ',' }))
            {
                StrList = StrList.Insert(int.Parse(str3), SplitString);
            }
            str = StrList;
            Error = "";
            return str;
        }

        public static string[] GetStrArray(string str)
        {
            return str.Split(new char[] { ',' });
        }

        public static List<string> GetStrArray(string str, char speater, bool toLower)
        {
            List<string> list = new List<string>();
            foreach (string str2 in str.Split(new char[] { speater }))
            {
                if (!string.IsNullOrEmpty(str2) && (str2 != speater.ToString()))
                {
                    string item = str2;
                    if (toLower)
                    {
                        item = str2.ToLower();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public static List<string> GetSubStringList(string o_str, char sepeater)
        {
            List<string> list = new List<string>();
            foreach (string str in o_str.Split(new char[] { sepeater }))
            {
                if (!string.IsNullOrEmpty(str) && (str != sepeater.ToString()))
                {
                    list.Add(str);
                }
            }
            return list;
        }

        public static bool IsNullOrEmpty(object target)
        {
            if ((target != null) && ("" != target.ToString()))
            {
                return (target.ToString().Trim().Length == 0);
            }
            return true;
        }

        public static string[] SplitMulti(string str, string splitstr)
        {
            string[] strArray = null;
            if ((str != null) && (str != ""))
            {
                strArray = new Regex(splitstr).Split(str);
            }
            return strArray;
        }

        public static Size SplitToSize(string str, char splitstr, int defWidth, int defHeight)
        {
            int width = defWidth;
            int height = defHeight;
            if (!string.IsNullOrWhiteSpace(str))
            {
                string[] strArray = str.Split(new char[] { splitstr }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length == 2)
                {
                    width = Globals.SafeInt(strArray[0], defWidth);
                    height = Globals.SafeInt(strArray[1], defHeight);
                }
            }
            return new Size(width, height);
        }

        public static string SqlSafeString(string String, bool IsDel)
        {
            if (IsDel)
            {
                String = String.Replace("'", "");
                String = String.Replace("\"", "");
                return String;
            }
            String = String.Replace("'", "&#39;");
            String = String.Replace("\"", "&#34;");
            return String;
        }

        public static string SubString(object target, int subLength, string sign = new string())
        {
            string str = string.Empty;
            if (IsNullOrEmpty(target))
            {
                return str;
            }
            str = target.ToString();
            if (str.Length <= subLength)
            {
                return str;
            }
            if (!string.IsNullOrWhiteSpace(sign))
            {
                return (str.Substring(0, subLength - (sign.Length / 2)) + sign);
            }
            return str.Substring(0, subLength);
        }

        [Obsolete]
        public static string SubString(object target, string sign, int subLength, bool isShow)
        {
            string str = string.Empty;
            if (!IsNullOrEmpty(target))
            {
                str = target.ToString();
                if (str.Length > subLength)
                {
                    str = str.Substring(0, subLength);
                    if (isShow)
                    {
                        str = str + sign;
                    }
                }
            }
            return str;
        }

        public static string ToDBC(string input)
        {
            char[] chArray = input.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                if (chArray[i] == '　')
                {
                    chArray[i] = ' ';
                }
                else if ((chArray[i] > 0xff00) && (chArray[i] < 0xff5f))
                {
                    chArray[i] = (char) (chArray[i] - 0xfee0);
                }
            }
            return new string(chArray);
        }

        public static string ToSBC(string input)
        {
            char[] chArray = input.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                if (chArray[i] == ' ')
                {
                    chArray[i] = '　';
                }
                else if (chArray[i] < '\x007f')
                {
                    chArray[i] = (char) (chArray[i] + 0xfee0);
                }
            }
            return new string(chArray);
        }

        public static string TrimEnd(string srcString, string trimString)
        {
            if (IsNullOrEmpty(trimString))
            {
                return srcString;
            }
            if (!srcString.EndsWith(trimString))
            {
                return srcString;
            }
            if (srcString.Equals(trimString))
            {
                return "";
            }
            return srcString.Substring(0, srcString.Length - trimString.Length);
        }

        public static string TrimStart(string srcString, string trimString)
        {
            if (srcString == null)
            {
                return null;
            }
            if (trimString == null)
            {
                return srcString;
            }
            if (IsNullOrEmpty(srcString))
            {
                return string.Empty;
            }
            if (!srcString.StartsWith(trimString))
            {
                return srcString;
            }
            return srcString.Substring(trimString.Length);
        }
    }
}

