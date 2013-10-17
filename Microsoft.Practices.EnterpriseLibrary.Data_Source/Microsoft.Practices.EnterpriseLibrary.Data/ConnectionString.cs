namespace Microsoft.Practices.EnterpriseLibrary.Data
{
    using Microsoft.Practices.EnterpriseLibrary.Data.Properties;
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ConnectionString
    {
        private string connectionString;
        private string connectionStringWithoutCredentials;
        private const char CONNSTRING_DELIM = ';';
        private string passwordTokens;
        private string userIdTokens;

        public ConnectionString(string connectionString, string userIdTokens, string passwordTokens)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, "connectionString");
            }
            if (string.IsNullOrEmpty(userIdTokens))
            {
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, "userIdTokens");
            }
            if (string.IsNullOrEmpty(passwordTokens))
            {
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, "passwordTokens");
            }
            this.connectionString = connectionString;
            this.userIdTokens = userIdTokens;
            this.passwordTokens = passwordTokens;
            this.connectionStringWithoutCredentials = null;
        }

        public ConnectionString CreateNewConnectionString(string connectionStringToFormat)
        {
            return new ConnectionString(connectionStringToFormat, this.userIdTokens, this.passwordTokens);
        }

        private void GetTokenPositions(string tokenString, out int tokenPos, out int tokenMPos)
        {
            string[] strArray = tokenString.Split(new char[] { ',' });
            int index = -1;
            int num2 = -1;
            string str = this.connectionString.ToLower(CultureInfo.CurrentCulture);
            tokenPos = -1;
            tokenMPos = -1;
            foreach (string str2 in strArray)
            {
                index = str.IndexOf(str2);
                if (index > num2)
                {
                    tokenPos = index;
                    tokenMPos = index + str2.Length;
                    num2 = index;
                }
            }
        }

        private string RemoveCredentials(string connectionStringToModify)
        {
            StringBuilder builder = new StringBuilder();
            string[] strArray = connectionStringToModify.ToLower(CultureInfo.CurrentCulture).Split(new char[] { ';' });
            string[] strArray2 = (this.userIdTokens + "," + this.passwordTokens).ToLower(CultureInfo.CurrentCulture).Split(new char[] { ',' });
            foreach (string str2 in strArray)
            {
                bool flag = false;
                string str3 = str2.Trim();
                if (str3.Length != 0)
                {
                    foreach (string str4 in strArray2)
                    {
                        if (str3.StartsWith(str4))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        builder.Append(str3 + ';');
                    }
                }
            }
            return builder.ToString();
        }

        public override string ToString()
        {
            return this.connectionString;
        }

        public string ToStringNoCredentials()
        {
            if (this.connectionStringWithoutCredentials == null)
            {
                this.connectionStringWithoutCredentials = this.RemoveCredentials(this.connectionString);
            }
            return this.connectionStringWithoutCredentials;
        }

        public string Password
        {
            get
            {
                int num;
                int num2;
                string str = this.connectionString.ToLower(CultureInfo.CurrentCulture);
                this.GetTokenPositions(this.passwordTokens, out num, out num2);
                if (0 <= num)
                {
                    int index = str.IndexOf(';', num2);
                    return this.connectionString.Substring(num2, index - num2);
                }
                return string.Empty;
            }
            set
            {
                int num;
                int num2;
                string str = this.connectionString.ToLower(CultureInfo.CurrentCulture);
                this.GetTokenPositions(this.passwordTokens, out num, out num2);
                if (0 <= num)
                {
                    int index = str.IndexOf(';', num2);
                    this.connectionString = this.connectionString.Substring(0, num2) + value + this.connectionString.Substring(index);
                }
                else
                {
                    string[] strArray = this.passwordTokens.Split(new char[] { ',' });
                    object connectionString = this.connectionString;
                    this.connectionString = string.Concat(new object[] { connectionString, strArray[0], value, ';' });
                }
            }
        }

        public string UserName
        {
            get
            {
                int num;
                int num2;
                string str = this.connectionString.ToLower(CultureInfo.CurrentCulture);
                this.GetTokenPositions(this.userIdTokens, out num, out num2);
                if (0 <= num)
                {
                    int index = str.IndexOf(';', num2);
                    return this.connectionString.Substring(num2, index - num2);
                }
                return string.Empty;
            }
            set
            {
                int num;
                int num2;
                string str = this.connectionString.ToLower(CultureInfo.CurrentCulture);
                this.GetTokenPositions(this.userIdTokens, out num, out num2);
                if (0 <= num)
                {
                    int index = str.IndexOf(';', num2);
                    this.connectionString = this.connectionString.Substring(0, num2) + value + this.connectionString.Substring(index);
                }
                else
                {
                    string[] strArray = this.userIdTokens.Split(new char[] { ',' });
                    object connectionString = this.connectionString;
                    this.connectionString = string.Concat(new object[] { connectionString, strArray[0], value, ';' });
                }
            }
        }
    }
}

