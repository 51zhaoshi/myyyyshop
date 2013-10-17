namespace Maticsoft.Common
{
    using System;
    using System.Collections;
    using System.IO;

    public class FileManage
    {
        public static bool DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteFolder(string path)
        {
            try
            {
                Directory.Delete(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsCanEdit(string strExtension)
        {
            strExtension = strExtension.ToLower();
            if (strExtension.LastIndexOf(".") >= 0)
            {
                strExtension = strExtension.Substring(strExtension.LastIndexOf("."));
            }
            else
            {
                strExtension = ".txt";
            }
            string[] strArray = new string[] { ".htm", ".html", ".txt", ".js", ".css", ".xml", ".sitemap" };
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strExtension.Equals(strArray[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsSafeName(string strExtension)
        {
            strExtension = strExtension.ToLower();
            if (strExtension.LastIndexOf(".") >= 0)
            {
                strExtension = strExtension.Substring(strExtension.LastIndexOf("."));
            }
            else
            {
                strExtension = ".txt";
            }
            string[] strArray = new string[] { ".htm", ".html", ".txt", ".js", ".css", ".xml", ".sitemap", ".jpg", ".gif", ".png", ".rar", ".zip" };
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strExtension.Equals(strArray[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsUnsafeName(string strExtension)
        {
            strExtension = strExtension.ToLower();
            if (strExtension.LastIndexOf(".") >= 0)
            {
                strExtension = strExtension.Substring(strExtension.LastIndexOf("."));
            }
            else
            {
                strExtension = ".txt";
            }
            string[] strArray = new string[] { 
                ".", ".asp", ".aspx", ".cs", ".net", ".dll", ".config", ".ascx", ".master", ".asmx", ".asax", ".cd", ".browser", ".rpt", ".ashx", ".xsd", 
                ".mdf", ".resx", ".xsd"
             };
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strExtension.Equals(strArray[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static void MoveFile(string oldPath, string newPath, ArrayList fileNameList)
        {
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            for (int i = 0; i < fileNameList.Count; i++)
            {
                File.Move(oldPath + fileNameList[i], newPath + fileNameList[i]);
            }
        }

        public static void MoveFile(string oldPath, string newPath, string fileName)
        {
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            File.Move(oldPath + fileName, newPath + fileName);
        }

        public static bool MoveFolder(string oldPath, string newPath)
        {
            try
            {
                Directory.Move(oldPath, newPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

