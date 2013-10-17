namespace Maticsoft.Common
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.InteropServices;

    public class FormulaCalculator
    {
        private NameValueCollection _data;
        private string _expression;
        private string _leftValue;
        private string _opt;
        private static object[][] Level = new object[][] { 
            new object[] { ",", 0 }, new object[] { "=", 1 }, new object[] { ">=", 1 }, new object[] { "<=", 1 }, new object[] { "<>", 1 }, new object[] { ">", 1 }, new object[] { "<", 1 }, new object[] { "+", 2 }, new object[] { "-", 2 }, new object[] { "*", 3 }, new object[] { "/", 3 }, new object[] { "%", 3 }, new object[] { "NEG", 4 }, new object[] { "^", 5 }, new object[] { "(", 0x63 }, new object[] { "ROUND(", 0x63 }, 
            new object[] { "TRUNC(", 0x63 }, new object[] { "MAX(", 0x63 }, new object[] { "MIN(", 0x63 }, new object[] { "ABS(", 0x63 }, new object[] { "SUM(", 0x63 }, new object[] { "AVERAGE(", 0x63 }, new object[] { "SQRT(", 0x63 }, new object[] { "EXP(", 0x63 }, new object[] { "LOG(", 0x63 }, new object[] { "LOG10(", 0x63 }, new object[] { "SIN(", 0x63 }, new object[] { "COS(", 0x63 }, new object[] { "TAN(", 0x63 }, new object[] { "IF(", 0x63 }, new object[] { "NOT(", 0x63 }, new object[] { "AND(", 0x63 }, 
            new object[] { "OR(", 0x63 }
         };
        protected const int MAX_LEVEL = 0x63;

        public FormulaCalculator(string expression, NameValueCollection dataProvider)
        {
            this._data = dataProvider;
            this._expression = expression.ToUpper();
            if (this.GetIndex(this._expression) != -1)
            {
                throw new Exception("缺少\"(\"");
            }
            this.Initialize();
        }

        private decimal[] Calc(string opt, string expression)
        {
            decimal[] numArray = new FormulaCalculator(expression, this._data).Calculate();
            decimal d = numArray[numArray.Length - 1];
            decimal num2 = 0M;
            switch (this._opt)
            {
                case "(":
                    num2 = d;
                    break;

                case "ROUND(":
                    if (numArray.Length > 2)
                    {
                        throw new Exception("Round函数需要一个或两个参数!");
                    }
                    if (numArray.Length == 1)
                    {
                        num2 = decimal.Round(d, 0);
                    }
                    else
                    {
                        num2 = decimal.Round(numArray[0], (int) numArray[1]);
                    }
                    break;

                case "TRUNC(":
                    if (numArray.Length > 1)
                    {
                        throw new Exception("Trunc函数只需要一个参数!");
                    }
                    num2 = decimal.Truncate(d);
                    break;

                case "MAX(":
                    if (numArray.Length < 2)
                    {
                        throw new Exception("Max函数至少需要两个参数!");
                    }
                    num2 = numArray[0];
                    for (int i = 1; i < numArray.Length; i++)
                    {
                        if (numArray[i] > num2)
                        {
                            num2 = numArray[i];
                        }
                    }
                    break;

                case "MIN(":
                    if (numArray.Length < 2)
                    {
                        throw new Exception("Min函数至少需要两个参数!");
                    }
                    num2 = numArray[0];
                    for (int j = 1; j < numArray.Length; j++)
                    {
                        if (numArray[j] < num2)
                        {
                            num2 = numArray[j];
                        }
                    }
                    break;

                case "ABS":
                    if (numArray.Length > 1)
                    {
                        throw new Exception("Abs函数只需要一个参数!");
                    }
                    num2 = Math.Abs(d);
                    break;

                case "SUM(":
                    foreach (decimal num5 in numArray)
                    {
                        num2 += num5;
                    }
                    break;

                case "AVERAGE(":
                    foreach (decimal num6 in numArray)
                    {
                        num2 += num6;
                    }
                    num2 /= numArray.Length;
                    break;

                case "IF(":
                    if (numArray.Length != 3)
                    {
                        throw new Exception("IF函数需要三个参数!");
                    }
                    if (this.GetBoolean(numArray[0]))
                    {
                        num2 = numArray[1];
                    }
                    else
                    {
                        num2 = numArray[2];
                    }
                    break;

                case "NOT(":
                    if (numArray.Length != 1)
                    {
                        throw new Exception("NOT函数需要一个参数!");
                    }
                    if (this.GetBoolean(numArray[0]))
                    {
                        num2 = 0M;
                    }
                    else
                    {
                        num2 = 1M;
                    }
                    break;

                case "OR(":
                    if (numArray.Length < 1)
                    {
                        throw new Exception("OR函数至少需要两个参数!");
                    }
                    foreach (decimal num7 in numArray)
                    {
                        if (this.GetBoolean(num7))
                        {
                            return new decimal[] { 1M };
                        }
                    }
                    break;

                case "AND(":
                    if (numArray.Length < 1)
                    {
                        throw new Exception("AND函数至少需要两个参数!");
                    }
                    foreach (decimal num8 in numArray)
                    {
                        if (!this.GetBoolean(num8))
                        {
                            return new decimal[1];
                        }
                    }
                    num2 = 1M;
                    break;

                case "SQRT(":
                    if (numArray.Length != 1)
                    {
                        throw new Exception("SQRT函数需要一个参数!");
                    }
                    num2 = (decimal) Math.Sqrt((double) d);
                    break;

                case "SIN(":
                    if (numArray.Length != 1)
                    {
                        throw new Exception("Sin函数需要一个参数!");
                    }
                    num2 = (decimal) Math.Sin((double) d);
                    break;

                case "COS(":
                    if (numArray.Length != 1)
                    {
                        throw new Exception("Cos函数需要一个参数!");
                    }
                    num2 = (decimal) Math.Cos((double) d);
                    break;

                case "TAN(":
                    if (numArray.Length != 1)
                    {
                        throw new Exception("Tan函数需要一个参数!");
                    }
                    num2 = (decimal) Math.Tan((double) d);
                    break;

                case "EXP(":
                    if (numArray.Length != 1)
                    {
                        throw new Exception("Exp函数需要一个参数!");
                    }
                    num2 = (decimal) Math.Exp((double) d);
                    break;

                case "LOG(":
                    if (numArray.Length > 2)
                    {
                        throw new Exception("Log函数需要一个或两个参数!");
                    }
                    if (numArray.Length == 1)
                    {
                        num2 = (decimal) Math.Log((double) d);
                    }
                    else
                    {
                        num2 = (decimal) Math.Log((double) numArray[0], (double) numArray[1]);
                    }
                    break;

                case "LOG10(":
                    if (numArray.Length != 1)
                    {
                        throw new Exception("Log10函数需要一个参数!");
                    }
                    num2 = (decimal) Math.Log10((double) d);
                    break;
            }
            return new decimal[] { num2 };
        }

        private decimal[] Calc(string opt, string leftEx, string rightEx)
        {
            decimal num = 0M;
            decimal num2 = 0M;
            decimal d = 0M;
            try
            {
                num2 = decimal.Parse(leftEx);
            }
            catch
            {
                if (opt != "NEG")
                {
                    try
                    {
                        num2 = decimal.Parse(this._data[leftEx]);
                    }
                    catch
                    {
                        throw new Exception("错误的格式:" + leftEx);
                    }
                }
            }
            try
            {
                d = decimal.Parse(rightEx);
            }
            catch
            {
                try
                {
                    d = decimal.Parse(this._data[rightEx]);
                }
                catch
                {
                    throw new Exception("错误的格式:" + leftEx);
                }
            }
            switch (this._opt)
            {
                case "NEG":
                    num = decimal.Negate(d);
                    break;

                case "+":
                    num = num2 + d;
                    break;

                case "-":
                    num = num2 - d;
                    break;

                case "*":
                    num = num2 * d;
                    break;

                case "/":
                    num = num2 / d;
                    break;

                case "%":
                    num = decimal.Remainder(num2, d);
                    break;

                case "^":
                    num = (decimal) Math.Pow((double) num2, (double) d);
                    break;

                case ",":
                    return new decimal[] { num2, d };

                case "=":
                    num = (num2 == d) ? 1 : 0;
                    break;

                case "<>":
                    num = (num2 != d) ? 1 : 0;
                    break;

                case "<":
                    num = (num2 < d) ? 1 : 0;
                    break;

                case ">":
                    num = (num2 > d) ? 1 : 0;
                    break;

                case ">=":
                    num = (num2 >= d) ? 1 : 0;
                    break;

                case "<=":
                    num = (num2 <= d) ? 1 : 0;
                    break;
            }
            return new decimal[] { num };
        }

        public decimal[] Calculate()
        {
            if (this._opt == null)
            {
                decimal num = 0M;
                try
                {
                    num = decimal.Parse(this._leftValue);
                }
                catch
                {
                    try
                    {
                        num = decimal.Parse(this._data[this._leftValue]);
                    }
                    catch
                    {
                        throw new Exception("错误的格式:" + this._leftValue);
                    }
                }
                return new decimal[] { num };
            }
            if (GetOperatorLevel(this._opt) != 0x63)
            {
                if ((this._opt != "-") && (this._leftValue == string.Empty))
                {
                    throw new Exception("\"" + this._opt + "\"运算符的左边需要值或表达式");
                }
                if ((this._opt == "-") && (this._leftValue == string.Empty))
                {
                    this._opt = "NEG";
                }
                if (this._expression == string.Empty)
                {
                    throw new Exception("\"" + this._opt + "\"运算符的右边需要值或表达式");
                }
                return this.CalculateTwoParms();
            }
            if (this._leftValue != string.Empty)
            {
                throw new Exception("\"" + this._opt + "\"运算符的左边不需要值或表达式");
            }
            return this.CalculateFunction();
        }

        public static decimal[] CalculateExpression(string expression, NameValueCollection dataProvider)
        {
            FormulaCalculator calculator = new FormulaCalculator(expression, dataProvider);
            return calculator.Calculate();
        }

        private decimal[] CalculateFunction()
        {
            string str2;
            string str3;
            string str4;
            int index = this.GetIndex(this._expression);
            if (index == -1)
            {
                throw new Exception("缺少\")\"");
            }
            string expression = this._expression.Substring(0, index);
            if (index == (this._expression.Length - 1))
            {
                return this.Calc(this._opt, expression);
            }
            this._expression = this._expression.Substring(index + 1, (this._expression.Length - index) - 1);
            this.GetNext(this._expression, out str2, out str4, out str3);
            decimal[] numArray = this.Calc(this._opt, expression);
            this._leftValue = numArray[numArray.Length - 1].ToString();
            if (str4 == null)
            {
                throw new Exception("\")\"运算符的右边需要运算符");
            }
            this._opt = str4;
            this._expression = str3;
            return this.Calculate();
        }

        private decimal[] CalculateTwoParms()
        {
            string str;
            string str2;
            string str3;
            decimal[] numArray2;
            this.GetNext(this._expression, out str, out str3, out str2);
            if ((str3 == null) || (GetOperatorLevel(this._opt) >= GetOperatorLevel(str3)))
            {
                numArray2 = this.Calc(this._opt, this._leftValue, str);
            }
            else
            {
                string expression = str;
                while ((GetOperatorLevel(this._opt) < GetOperatorLevel(str3)) && (str2 != string.Empty))
                {
                    expression = expression + str3;
                    if (GetOperatorLevel(str3) == 0x63)
                    {
                        int index = this.GetIndex(str2);
                        expression = expression + str2.Substring(0, index + 1);
                        str2 = str2.Substring(index + 1);
                    }
                    this.GetNext(str2, out str, out str3, out str2);
                    expression = expression + str;
                }
                decimal[] numArray3 = new FormulaCalculator(expression, this._data).Calculate();
                numArray2 = this.Calc(this._opt, this._leftValue, numArray3[numArray3.Length - 1].ToString());
            }
            this._leftValue = numArray2[numArray2.Length - 1].ToString();
            this._opt = str3;
            this._expression = str2;
            decimal[] numArray4 = this.Calculate();
            decimal[] numArray = new decimal[(numArray2.Length - 1) + numArray4.Length];
            for (int i = 0; i < (numArray2.Length - 1); i++)
            {
                numArray[i] = numArray2[i];
            }
            for (int j = 0; j < numArray4.Length; j++)
            {
                numArray[(numArray2.Length - 1) + j] = numArray4[j];
            }
            return numArray;
        }

        private bool GetBoolean(decimal d)
        {
            return (((int) d) == 1);
        }

        private int GetIndex(string expression)
        {
            int num = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == ')')
                {
                    if (num == 0)
                    {
                        return i;
                    }
                    num--;
                }
                if (expression[i] == '(')
                {
                    num++;
                }
            }
            return -1;
        }

        private void GetNext(string expression, out string left, out string opt, out string right)
        {
            right = expression;
            left = string.Empty;
            opt = null;
            while (right != string.Empty)
            {
                opt = GetOperator(right);
                if (opt != null)
                {
                    right = right.Substring(opt.Length, right.Length - opt.Length);
                    break;
                }
                left = left + right[0];
                right = right.Substring(1, right.Length - 1);
            }
            right = right.Trim();
        }

        private static string GetOperator(string v)
        {
            for (int i = 0; i < Level.Length; i++)
            {
                if (v.StartsWith((string) Level[i][0]))
                {
                    return (string) Level[i][0];
                }
            }
            return null;
        }

        private static int GetOperatorLevel(string o)
        {
            for (int i = 0; i < Level.Length; i++)
            {
                if (((string) Level[i][0]) == o)
                {
                    return (int) Level[i][1];
                }
            }
            return -1;
        }

        private void Initialize()
        {
            string str;
            this.GetNext(this._expression, out this._leftValue, out this._opt, out str);
            this._expression = str;
        }
    }
}

