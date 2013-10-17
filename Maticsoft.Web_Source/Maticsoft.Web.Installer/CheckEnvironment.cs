namespace Maticsoft.Web.Installer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Sql;
    using System.IO;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Security.Principal;
    using System.Text;
    using System.Web;

    public class CheckEnvironment
    {
        public static bool CheckFileSystem()
        {
            return false;
        }

        public static bool CheckSqlServerVersion()
        {
            return GetSqlServer().Exists(delegate (string c) {
                if (!c.StartsWith("9.00") && !c.StartsWith("10"))
                {
                    return c.StartsWith("11");
                }
                return true;
            });
        }

        public static bool DoCheckFileSystem(string path)
        {
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                return false;
            }
            try
            {
                string str = HttpContext.Current.Server.MapPath(path + "/test.htm");
                File.WriteAllBytes(str, Encoding.ASCII.GetBytes("OK"));
                if (File.Exists(str))
                {
                    File.Delete(str);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<string> GetDirPermission(string path)
        {
            List<string> list = new List<string>();
            foreach (FileSystemRights rights in GetDirPermission2(path))
            {
                list.Add(rights.ToString());
            }
            return list;
        }

        public static List<FileSystemRights> GetDirPermission2(string path)
        {
            AuthorizationRuleCollection rules = Directory.GetAccessControl(HttpContext.Current.Server.MapPath(path), AccessControlSections.Access).GetAccessRules(true, true, typeof(SecurityIdentifier));
            List<FileSystemRights> source = new List<FileSystemRights>();
            for (int i = 0; i < rules.Count; i++)
            {
                WindowsIdentity current = WindowsIdentity.GetCurrent();
                FileSystemAccessRule rule = (FileSystemAccessRule) rules[i];
                if (current.Owner.Value == rule.IdentityReference.Value)
                {
                    source.Add(rule.FileSystemRights);
                }
            }
            return source.Distinct<FileSystemRights>().ToList<FileSystemRights>();
        }

        public static List<string> GetFilePermission(string path)
        {
            List<string> list = new List<string>();
            foreach (FileSystemRights rights in GetFilePermission2(path))
            {
                list.Add(rights.ToString());
            }
            return list;
        }

        public static List<FileSystemRights> GetFilePermission2(string path)
        {
            AuthorizationRuleCollection rules = File.GetAccessControl(HttpContext.Current.Server.MapPath(path), AccessControlSections.Access).GetAccessRules(true, true, typeof(SecurityIdentifier));
            List<FileSystemRights> source = new List<FileSystemRights>();
            for (int i = 0; i < rules.Count; i++)
            {
                WindowsIdentity current = WindowsIdentity.GetCurrent();
                FileSystemAccessRule rule = (FileSystemAccessRule) rules[i];
                if (current.Owner.Value == rule.IdentityReference.Value)
                {
                    source.Add(rule.FileSystemRights);
                }
            }
            return source.Distinct<FileSystemRights>().ToList<FileSystemRights>();
        }

        public static List<string> GetSqlServer()
        {
            DataTable dataSources = SqlDataSourceEnumerator.Instance.GetDataSources();
            List<string> list = new List<string>();
            foreach (DataRow row in dataSources.Rows)
            {
                if (row["ServerName"].ToString() == "SERVER")
                {
                    list.Add(row["Version"].ToString());
                }
            }
            return list;
        }
    }
}

