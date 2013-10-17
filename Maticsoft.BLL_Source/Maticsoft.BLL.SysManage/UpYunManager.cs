namespace Maticsoft.BLL.SysManage
{
    using Maticsoft.Model.SysManage;
    using System;
    using System.Runtime.InteropServices;

    public class UpYunManager
    {
        public static bool DeleteImage(string path, ApplicationKeyType applicationKeyType)
        {
            string valueByCache = ConfigSystem.GetValueByCache(applicationKeyType + "_YouPaiYunOperaterName");
            string password = ConfigSystem.GetValueByCache(applicationKeyType + "_YouPaiOperaterPassword");
            string bucketname = ConfigSystem.GetValueByCache(applicationKeyType + "_YouPaiSpaceName");
            string oldValue = ConfigSystem.GetValueByCache(applicationKeyType + "_YouPaiPhotoDomain");
            UpYun yun = new UpYun(bucketname, valueByCache, password);
            if (path.Contains("http://"))
            {
                path = path.Replace(oldValue, "");
                path = path.StartsWith("/") ? path : ("/" + path);
            }
            return yun.deleteFile(path);
        }

        public static string UploadExecute(byte[] buffer, string FileName, ApplicationKeyType applicationKeyType)
        {
            string valueByCache = ConfigSystem.GetValueByCache(applicationKeyType + "_YouPaiYunOperaterName");
            string password = ConfigSystem.GetValueByCache(applicationKeyType + "_YouPaiOperaterPassword");
            string bucketname = ConfigSystem.GetValueByCache(applicationKeyType + "_YouPaiSpaceName");
            string str4 = ConfigSystem.GetValueByCache(applicationKeyType + "_YouPaiPhotoDomain");
            UpYun yun = new UpYun(bucketname, valueByCache, password);
            byte[] data = buffer;
            string path = "/" + DateTime.Now.ToString("yyyyMMdd") + "/" + FileName;
            if (yun.writeFile(path, data, true))
            {
                return (str4 + path);
            }
            return "";
        }

        public static bool UploadExecute(byte[] buffer, string FileName, ApplicationKeyType applicationKeyType, out string imageUrl)
        {
            imageUrl = UploadExecute(buffer, FileName, applicationKeyType);
            if (string.IsNullOrEmpty(imageUrl))
            {
                return false;
            }
            return true;
        }
    }
}

