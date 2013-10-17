namespace Maticsoft.Common
{
    using System;

    public class FormulaDeal
    {
        private double CalculateExExpress(string strExpression, EnumFormula ExpressType)
        {
            double num = 0.0;
            switch (ExpressType)
            {
                case EnumFormula.Sin:
                    num = Math.Sin(Convert.ToDouble(strExpression));
                    break;

                case EnumFormula.Cos:
                    num = Math.Cos(Convert.ToDouble(strExpression));
                    break;

                case EnumFormula.Tan:
                    num = Math.Tan(Convert.ToDouble(strExpression));
                    break;

                case EnumFormula.ATan:
                    num = Math.Atan(Convert.ToDouble(strExpression));
                    break;

                case EnumFormula.Sqrt:
                    num = Math.Sqrt(Convert.ToDouble(strExpression));
                    break;

                case EnumFormula.Pow:
                    num = Math.Pow(Convert.ToDouble(strExpression), 2.0);
                    break;
            }
            if (num == 0.0)
            {
                return Convert.ToDouble(strExpression);
            }
            return num;
        }

        private double CalculateExpress(string strExpression)
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            double num = 0.0;
            while (((strExpression.IndexOf("+") != -1) || (strExpression.IndexOf("-") != -1)) || ((strExpression.IndexOf("*") != -1) || (strExpression.IndexOf("/") != -1)))
            {
                if (strExpression.IndexOf("*") != -1)
                {
                    str = strExpression.Substring(strExpression.IndexOf("*") + 1, (strExpression.Length - strExpression.IndexOf("*")) - 1);
                    str2 = strExpression.Substring(0, strExpression.IndexOf("*"));
                    str3 = str2.Substring(this.GetPrivorPos(str2) + 1, (str2.Length - this.GetPrivorPos(str2)) - 1);
                    str4 = str.Substring(0, this.GetNextPos(str));
                    num = Convert.ToDouble(this.GetExpType(str3)) * Convert.ToDouble(this.GetExpType(str4));
                    strExpression = strExpression.Replace(str3 + "*" + str4, num.ToString());
                }
                else
                {
                    if (strExpression.IndexOf("/") != -1)
                    {
                        str = strExpression.Substring(strExpression.IndexOf("/") + 1, (strExpression.Length - strExpression.IndexOf("/")) - 1);
                        str2 = strExpression.Substring(0, strExpression.IndexOf("/"));
                        str3 = str2.Substring(this.GetPrivorPos(str2) + 1, (str2.Length - this.GetPrivorPos(str2)) - 1);
                        str4 = str.Substring(0, this.GetNextPos(str));
                        num = Convert.ToDouble(this.GetExpType(str3)) / Convert.ToDouble(this.GetExpType(str4));
                        strExpression = strExpression.Replace(str3 + "/" + str4, num.ToString());
                        continue;
                    }
                    if (strExpression.IndexOf("+") != -1)
                    {
                        str = strExpression.Substring(strExpression.IndexOf("+") + 1, (strExpression.Length - strExpression.IndexOf("+")) - 1);
                        str2 = strExpression.Substring(0, strExpression.IndexOf("+"));
                        str3 = str2.Substring(this.GetPrivorPos(str2) + 1, (str2.Length - this.GetPrivorPos(str2)) - 1);
                        str4 = str.Substring(0, this.GetNextPos(str));
                        num = Convert.ToDouble(this.GetExpType(str3)) + Convert.ToDouble(this.GetExpType(str4));
                        strExpression = strExpression.Replace(str3 + "+" + str4, num.ToString());
                        continue;
                    }
                    if (strExpression.IndexOf("-") != -1)
                    {
                        str = strExpression.Substring(strExpression.IndexOf("-") + 1, (strExpression.Length - strExpression.IndexOf("-")) - 1);
                        str2 = strExpression.Substring(0, strExpression.IndexOf("-"));
                        str3 = str2.Substring(this.GetPrivorPos(str2) + 1, (str2.Length - this.GetPrivorPos(str2)) - 1);
                        str4 = str.Substring(0, this.GetNextPos(str));
                        strExpression = strExpression.Replace(str3 + "-" + str4, (Convert.ToDouble(this.GetExpType(str3)) - Convert.ToDouble(this.GetExpType(str4))).ToString());
                    }
                }
            }
            return Convert.ToDouble(strExpression);
        }

        private string GetExpType(string strExpression)
        {
            strExpression = strExpression.ToUpper();
            if (strExpression.IndexOf("SIN") != -1)
            {
                return this.CalculateExExpress(strExpression.Substring(strExpression.IndexOf("N") + 1, (strExpression.Length - 1) - strExpression.IndexOf("N")), EnumFormula.Sin).ToString();
            }
            if (strExpression.IndexOf("COS") != -1)
            {
                return this.CalculateExExpress(strExpression.Substring(strExpression.IndexOf("S") + 1, (strExpression.Length - 1) - strExpression.IndexOf("S")), EnumFormula.Cos).ToString();
            }
            if (strExpression.IndexOf("TAN") != -1)
            {
                return this.CalculateExExpress(strExpression.Substring(strExpression.IndexOf("N") + 1, (strExpression.Length - 1) - strExpression.IndexOf("N")), EnumFormula.Tan).ToString();
            }
            if (strExpression.IndexOf("ATAN") != -1)
            {
                return this.CalculateExExpress(strExpression.Substring(strExpression.IndexOf("N") + 1, (strExpression.Length - 1) - strExpression.IndexOf("N")), EnumFormula.ATan).ToString();
            }
            if (strExpression.IndexOf("SQRT") != -1)
            {
                return this.CalculateExExpress(strExpression.Substring(strExpression.IndexOf("T") + 1, (strExpression.Length - 1) - strExpression.IndexOf("T")), EnumFormula.Sqrt).ToString();
            }
            if (strExpression.IndexOf("POW") != -1)
            {
                return this.CalculateExExpress(strExpression.Substring(strExpression.IndexOf("W") + 1, (strExpression.Length - 1) - strExpression.IndexOf("W")), EnumFormula.Pow).ToString();
            }
            return strExpression;
        }

        private int GetNextPos(string strExpression)
        {
            int[] numArray = new int[] { strExpression.IndexOf("+"), strExpression.IndexOf("-"), strExpression.IndexOf("*"), strExpression.IndexOf("/") };
            int length = strExpression.Length;
            for (int i = 1; i <= numArray.Length; i++)
            {
                if ((length > numArray[i - 1]) && (numArray[i - 1] != -1))
                {
                    length = numArray[i - 1];
                }
            }
            return length;
        }

        private int GetPrivorPos(string strExpression)
        {
            int[] numArray = new int[] { strExpression.LastIndexOf("+"), strExpression.LastIndexOf("-"), strExpression.LastIndexOf("*"), strExpression.LastIndexOf("/") };
            int num = -1;
            for (int i = 1; i <= numArray.Length; i++)
            {
                if ((num < numArray[i - 1]) && (numArray[i - 1] != -1))
                {
                    num = numArray[i - 1];
                }
            }
            return num;
        }

        public string SpiltExpression(string strExpression)
        {
            string str = "";
            string str2 = "";
            while (strExpression.IndexOf("(") != -1)
            {
                str = strExpression.Substring(strExpression.LastIndexOf("(") + 1, (strExpression.Length - strExpression.LastIndexOf("(")) - 1);
                str2 = str.Substring(0, str.IndexOf(")"));
                strExpression = strExpression.Replace("(" + str2 + ")", this.CalculateExpress(str2).ToString());
            }
            if (((strExpression.IndexOf("+") != -1) || (strExpression.IndexOf("-") != -1)) || ((strExpression.IndexOf("*") != -1) || (strExpression.IndexOf("/") != -1)))
            {
                strExpression = this.CalculateExpress(strExpression).ToString();
            }
            return strExpression;
        }
    }
}

