namespace Maticsoft.Accounts.Bus
{
    using Maticsoft.Accounts;
    using Maticsoft.Accounts.Data;
    using Maticsoft.Accounts.IData;
    using Maticsoft.Accounts.MySqlData;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Security.Principal;
    using System.Text;
    using System.Web;

    public class AccountsPrincipal : IPrincipal
    {
        private IUser dataUser;
        private const string DOMAIN_RULES = "||com.cn|net.cn|org.cn|gov.cn|com.hk|公司|中国|网络|com|net|org|int|edu|gov|mil|arpa|Asia|biz|info|name|pro|coop|aero|museum|ac|ad|ae|af|ag|ai|al|am|an|ao|aq|ar|as|at|au|aw|az|ba|bb|bd|be|bf|bg|bh|bi|bj|bm|bn|bo|br|bs|bt|bv|bw|by|bz|ca|cc|cf|cg|ch|ci|ck|cl|cm|cn|co|cq|cr|cu|cv|cx|cy|cz|de|dj|dk|dm|do|dz|ec|ee|eg|eh|es|et|ev|fi|fj|fk|fm|fo|fr|ga|gb|gd|ge|gf|gh|gi|gl|gm|gn|gp|gr|gt|gu|gw|gy|hk|hm|hn|hr|ht|hu|id|ie|il|in|io|iq|ir|is|it|jm|jo|jp|ke|kg|kh|ki|km|kn|kp|kr|kw|ky|kz|la|lb|lc|li|lk|lr|ls|lt|lu|lv|ly|ma|mc|md|me|mg|mh|ml|mm|mn|mo|mp|mq|mr|ms|mt|mv|mw|mx|my|mz|na|nc|ne|nf|ng|ni|nl|no|np|nr|nt|nu|nz|om|pa|pe|pf|pg|ph|pk|pl|pm|pn|pr|pt|pw|py|qa|re|ro|ru|rw|sa|sb|sc|sd|se|sg|sh|si|sj|sk|sl|sm|sn|so|sr|st|su|sy|sz|tc|td|tf|tg|th|tj|tk|tm|tn|to|tp|tr|tt|tv|tw|tz|ua|ug|uk|us|uy|va|vc|ve|vg|vn|vu|wf|ws|ye|yu|za|zm|zr|zw|";
        protected IIdentity identity;
        protected List<int> permissionListid;
        protected DataSet permissionLists;
        protected List<string> permissionsDesc;
        protected Dictionary<int, string> rolesKeyValue;

        public AccountsPrincipal(int userID)
        {
            this.dataUser = PubConstant.IsSQLServer ? ((IUser) new Maticsoft.Accounts.Data.User()) : ((IUser) new Maticsoft.Accounts.MySqlData.User());
            this.permissionsDesc = new List<string>();
            this.permissionListid = new List<int>();
            this.identity = new SiteIdentity(userID);
            this.permissionLists = this.dataUser.GetEffectivePermissionLists(userID);
            if (this.permissionLists.Tables.Count > 0)
            {
                foreach (DataRow row in this.permissionLists.Tables[0].Rows)
                {
                    this.permissionListid.Add(Convert.ToInt32(row["PermissionID"]));
                    this.permissionsDesc.Add(row["Description"].ToString());
                }
            }
            this.rolesKeyValue = this.dataUser.GetUserRoles4KeyValues(userID);
        }

        public AccountsPrincipal(string userName)
        {
            SiteIdentity identity;
            this.dataUser = PubConstant.IsSQLServer ? ((IUser) new Maticsoft.Accounts.Data.User()) : ((IUser) new Maticsoft.Accounts.MySqlData.User());
            this.permissionsDesc = new List<string>();
            this.permissionListid = new List<int>();
            this.identity = identity = new SiteIdentity(userName);
            this.permissionLists = this.dataUser.GetEffectivePermissionLists(identity.UserID);
            if (this.permissionLists.Tables.Count > 0)
            {
                foreach (DataRow row in this.permissionLists.Tables[0].Rows)
                {
                    this.permissionListid.Add(Convert.ToInt32(row["PermissionID"]));
                    this.permissionsDesc.Add(row["Description"].ToString());
                }
            }
            this.rolesKeyValue = this.dataUser.GetUserRoles4KeyValues(identity.UserID);
        }

        public static bool CheckAuthorize(string authorizeCode, string productInfo = "")
        {
            if (!string.IsNullOrWhiteSpace(authorizeCode) && (authorizeCode.Length >= 10))
            {
                byte[] buffer = new byte[1];
                bool flag = authorizeCode.Contains("&");
                bool flag2 = productInfo == "Maticsoft Shop";
                if (flag2)
                {
                    authorizeCode = authorizeCode.Substring(6);
                }
                if (!flag)
                {
                    if (flag2)
                    {
                        buffer = EncryptPassword(TopLevelDomain + "_MS_" + productInfo);
                    }
                    else
                    {
                        buffer = EncryptPassword(TopLevelDomain + "_MS");
                    }
                }
                else
                {
                    buffer = EncryptPassword(Domain + "_MS_SUB");
                }
                string str = BitConverter.ToString(buffer).Replace("-", "");
                if (!authorizeCode.Contains("|"))
                {
                    if (flag)
                    {
                        return (str == authorizeCode.Split(new char[] { '&' })[0]);
                    }
                    return (str == authorizeCode);
                }
                foreach (string str2 in authorizeCode.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!string.IsNullOrWhiteSpace(str2))
                    {
                        if (flag && (str == authorizeCode.Split(new char[] { '&' })[0]))
                        {
                            return true;
                        }
                        if (str == str2)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static byte[] EncryptPassword(string password)
        {
            byte[] bytes = new UnicodeEncoding().GetBytes(password);
            SHA1 sha = new SHA1CryptoServiceProvider();
            return sha.ComputeHash(bytes);
        }

        internal static string GetDomain(string domain)
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                return string.Empty;
            }
            if (domain.IndexOf(".") < 1)
            {
                domain = domain.Split(new char[] { ':' })[0];
                return domain;
            }
            string[] strArray = domain.Split(new char[] { ':' })[0].Split(new char[] { '.' });
            if (IsNumeric(strArray[strArray.Length - 1]))
            {
                return domain.Split(new char[] { ':' })[0];
            }
            return domain;
        }

        internal static string GetTopLevelDomain(string domain)
        {
            string str;
            if (string.IsNullOrWhiteSpace(domain))
            {
                return string.Empty;
            }
            if (domain.IndexOf(".") < 1)
            {
                domain = domain.Split(new char[] { ':' })[0];
                return domain;
            }
            string[] strArray = domain.Split(new char[] { ':' })[0].Split(new char[] { '.' });
            if (IsNumeric(strArray[strArray.Length - 1]))
            {
                return domain.Split(new char[] { ':' })[0];
            }
            if (strArray.Length >= 4)
            {
                str = strArray[strArray.Length - 3] + "." + strArray[strArray.Length - 2] + "." + strArray[strArray.Length - 1];
                if ("||com.cn|net.cn|org.cn|gov.cn|com.hk|公司|中国|网络|com|net|org|int|edu|gov|mil|arpa|Asia|biz|info|name|pro|coop|aero|museum|ac|ad|ae|af|ag|ai|al|am|an|ao|aq|ar|as|at|au|aw|az|ba|bb|bd|be|bf|bg|bh|bi|bj|bm|bn|bo|br|bs|bt|bv|bw|by|bz|ca|cc|cf|cg|ch|ci|ck|cl|cm|cn|co|cq|cr|cu|cv|cx|cy|cz|de|dj|dk|dm|do|dz|ec|ee|eg|eh|es|et|ev|fi|fj|fk|fm|fo|fr|ga|gb|gd|ge|gf|gh|gi|gl|gm|gn|gp|gr|gt|gu|gw|gy|hk|hm|hn|hr|ht|hu|id|ie|il|in|io|iq|ir|is|it|jm|jo|jp|ke|kg|kh|ki|km|kn|kp|kr|kw|ky|kz|la|lb|lc|li|lk|lr|ls|lt|lu|lv|ly|ma|mc|md|me|mg|mh|ml|mm|mn|mo|mp|mq|mr|ms|mt|mv|mw|mx|my|mz|na|nc|ne|nf|ng|ni|nl|no|np|nr|nt|nu|nz|om|pa|pe|pf|pg|ph|pk|pl|pm|pn|pr|pt|pw|py|qa|re|ro|ru|rw|sa|sb|sc|sd|se|sg|sh|si|sj|sk|sl|sm|sn|so|sr|st|su|sy|sz|tc|td|tf|tg|th|tj|tk|tm|tn|to|tp|tr|tt|tv|tw|tz|ua|ug|uk|us|uy|va|vc|ve|vg|vn|vu|wf|ws|ye|yu|za|zm|zr|zw|".IndexOf("|" + str + "|") > 0)
                {
                    return (strArray[strArray.Length - 4] + "." + str);
                }
            }
            if (strArray.Length >= 3)
            {
                str = strArray[strArray.Length - 2] + "." + strArray[strArray.Length - 1];
                if ("||com.cn|net.cn|org.cn|gov.cn|com.hk|公司|中国|网络|com|net|org|int|edu|gov|mil|arpa|Asia|biz|info|name|pro|coop|aero|museum|ac|ad|ae|af|ag|ai|al|am|an|ao|aq|ar|as|at|au|aw|az|ba|bb|bd|be|bf|bg|bh|bi|bj|bm|bn|bo|br|bs|bt|bv|bw|by|bz|ca|cc|cf|cg|ch|ci|ck|cl|cm|cn|co|cq|cr|cu|cv|cx|cy|cz|de|dj|dk|dm|do|dz|ec|ee|eg|eh|es|et|ev|fi|fj|fk|fm|fo|fr|ga|gb|gd|ge|gf|gh|gi|gl|gm|gn|gp|gr|gt|gu|gw|gy|hk|hm|hn|hr|ht|hu|id|ie|il|in|io|iq|ir|is|it|jm|jo|jp|ke|kg|kh|ki|km|kn|kp|kr|kw|ky|kz|la|lb|lc|li|lk|lr|ls|lt|lu|lv|ly|ma|mc|md|me|mg|mh|ml|mm|mn|mo|mp|mq|mr|ms|mt|mv|mw|mx|my|mz|na|nc|ne|nf|ng|ni|nl|no|np|nr|nt|nu|nz|om|pa|pe|pf|pg|ph|pk|pl|pm|pn|pr|pt|pw|py|qa|re|ro|ru|rw|sa|sb|sc|sd|se|sg|sh|si|sj|sk|sl|sm|sn|so|sr|st|su|sy|sz|tc|td|tf|tg|th|tj|tk|tm|tn|to|tp|tr|tt|tv|tw|tz|ua|ug|uk|us|uy|va|vc|ve|vg|vn|vu|wf|ws|ye|yu|za|zm|zr|zw|".IndexOf("|" + str + "|") > 0)
                {
                    return (strArray[strArray.Length - 3] + "." + str);
                }
            }
            if (strArray.Length >= 2)
            {
                str = strArray[strArray.Length - 1];
                if ("||com.cn|net.cn|org.cn|gov.cn|com.hk|公司|中国|网络|com|net|org|int|edu|gov|mil|arpa|Asia|biz|info|name|pro|coop|aero|museum|ac|ad|ae|af|ag|ai|al|am|an|ao|aq|ar|as|at|au|aw|az|ba|bb|bd|be|bf|bg|bh|bi|bj|bm|bn|bo|br|bs|bt|bv|bw|by|bz|ca|cc|cf|cg|ch|ci|ck|cl|cm|cn|co|cq|cr|cu|cv|cx|cy|cz|de|dj|dk|dm|do|dz|ec|ee|eg|eh|es|et|ev|fi|fj|fk|fm|fo|fr|ga|gb|gd|ge|gf|gh|gi|gl|gm|gn|gp|gr|gt|gu|gw|gy|hk|hm|hn|hr|ht|hu|id|ie|il|in|io|iq|ir|is|it|jm|jo|jp|ke|kg|kh|ki|km|kn|kp|kr|kw|ky|kz|la|lb|lc|li|lk|lr|ls|lt|lu|lv|ly|ma|mc|md|me|mg|mh|ml|mm|mn|mo|mp|mq|mr|ms|mt|mv|mw|mx|my|mz|na|nc|ne|nf|ng|ni|nl|no|np|nr|nt|nu|nz|om|pa|pe|pf|pg|ph|pk|pl|pm|pn|pr|pt|pw|py|qa|re|ro|ru|rw|sa|sb|sc|sd|se|sg|sh|si|sj|sk|sl|sm|sn|so|sr|st|su|sy|sz|tc|td|tf|tg|th|tj|tk|tm|tn|to|tp|tr|tt|tv|tw|tz|ua|ug|uk|us|uy|va|vc|ve|vg|vn|vu|wf|ws|ye|yu|za|zm|zr|zw|".IndexOf("|" + str + "|") > 0)
                {
                    return (strArray[strArray.Length - 2] + "." + str);
                }
            }
            return domain;
        }

        public bool HasPermission(string permission)
        {
            return this.permissionsDesc.Contains(permission);
        }

        public bool HasPermissionID(int permissionid)
        {
            return this.permissionListid.Contains(permissionid);
        }

        public bool HasRole(int roleId)
        {
            return ((this.rolesKeyValue != null) && this.rolesKeyValue.ContainsKey(roleId));
        }

        public bool IsInRole(string role)
        {
            return ((this.rolesKeyValue != null) && this.rolesKeyValue.ContainsValue(role));
        }

        private static bool IsNumeric(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            int length = value.Length;
            if ((('-' != value[0]) && ('+' != value[0])) && !char.IsNumber(value[0]))
            {
                return false;
            }
            for (int i = 1; i < length; i++)
            {
                if (!char.IsNumber(value[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static AccountsPrincipal ValidateLogin(string userName, string password)
        {
            byte[] encPassword = EncryptPassword(password);
            IUser user = PubConstant.IsSQLServer ? ((IUser) new Maticsoft.Accounts.Data.User()) : ((IUser) new Maticsoft.Accounts.MySqlData.User());
            int userID = user.ValidateLogin(userName, encPassword);
            if (userID > 0)
            {
                return new AccountsPrincipal(userID);
            }
            return null;
        }

        public static AccountsPrincipal ValidateLogin4Email(string email, string password)
        {
            byte[] encPassword = EncryptPassword(password);
            IUser user = PubConstant.IsSQLServer ? ((IUser) new Maticsoft.Accounts.Data.User()) : ((IUser) new Maticsoft.Accounts.MySqlData.User());
            int userID = user.ValidateLogin4Email(email, encPassword);
            if (userID > 0)
            {
                return new AccountsPrincipal(userID);
            }
            return null;
        }

        internal static string Domain
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                return GetDomain(HOST);
            }
        }

        private static string HOST
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToLower();
            }
        }

        public IIdentity Identity
        {
            get
            {
                return this.identity;
            }
            set
            {
                this.identity = value;
            }
        }

        public DataSet PermissionLists
        {
            get
            {
                return this.permissionLists;
            }
        }

        public List<string> PermissionsDesc
        {
            get
            {
                return this.permissionsDesc;
            }
        }

        public List<int> PermissionsID
        {
            get
            {
                return this.permissionListid;
            }
        }

        public ArrayList Roles
        {
            get
            {
                if (this.rolesKeyValue != null)
                {
                    return new ArrayList(this.rolesKeyValue.Values);
                }
                return null;
            }
        }

        internal static string TopLevelDomain
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                return GetTopLevelDomain(HOST);
            }
        }
    }
}

