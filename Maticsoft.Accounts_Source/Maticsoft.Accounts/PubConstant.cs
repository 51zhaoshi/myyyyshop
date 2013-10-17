namespace Maticsoft.Accounts
{
    using System;

    public class PubConstant
    {
        public static bool IsSQLServer = (ConfigHelper.GetConfigString("DAL") == "Maticsoft.SQLServerDAL");
        private const string SQLSERVERDAL = "Maticsoft.SQLServerDAL";

        public static string ConnectionString
        {
            get
            {
                string configString = ConfigHelper.GetConfigString("ConnectionString");
                if (ConfigHelper.GetConfigString("ConStringEncrypt") == "true")
                {
                    configString = DESEncrypt.Decrypt(configString);
                }
                if (string.IsNullOrWhiteSpace(configString))
                {
                    throw new ArgumentNullException("Illegal [ConnectionString] in the configuration file.");
                }
                return configString;
            }
        }
    }
}

