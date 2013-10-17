namespace Maticsoft.SQLServerDAL
{
    using Maticsoft.Common;
    using Maticsoft.Common.DEncrypt;
    using System;

    public class PubConstant
    {
        public static string GetConnectionString(string configName)
        {
            string configString = ConfigHelper.GetConfigString(configName);
            if (ConfigHelper.GetConfigString("ConStringEncrypt") == "true")
            {
                configString = DESEncrypt.Decrypt(configString);
            }
            return configString;
        }
    }
}

