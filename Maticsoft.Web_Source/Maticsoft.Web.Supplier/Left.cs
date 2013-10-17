namespace Maticsoft.Web.Supplier
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;
    using System.Web.UI.HtmlControls;

    public class Left : PageBaseSupplier
    {
        private MultiLanguage bllML = new MultiLanguage();
        protected HtmlForm form1;
        private bool MenuExpanded = Globals.SafeBool(ConfigSystem.GetValueByCache("MenuExpanded"), false);
        public string NodeName = "";
        public string strMenuTree = "";
        private Hashtable TreeListofLang;

        public void LoadMenu(DataTable dt, int NodeId)
        {
            bool flag = false;
            DataRow[] rowArray = dt.Select("ParentID= " + NodeId);
            if (rowArray.Length > 0)
            {
                string str = rowArray[0]["NodeID"].ToString();
                if (dt.Select("ParentID= " + str).Length > 0)
                {
                    flag = true;
                }
            }
            StringBuilder builder = new StringBuilder();
            if (!flag)
            {
                builder.Append("<ul class=\"open\">");
                builder.AppendFormat("<span class=\"span_open\">{0}</span>", this.NodeName);
                foreach (DataRow row in rowArray)
                {
                    string str2 = row["NodeID"].ToString();
                    string str3 = row["TreeText"].ToString();
                    if (this.TreeListofLang[str2] != null)
                    {
                        str3 = this.TreeListofLang[str2].ToString();
                    }
                    row["ParentID"].ToString();
                    row["Location"].ToString();
                    string str4 = row["Url"].ToString();
                    row["ImageUrl"].ToString();
                    int permissionid = int.Parse(row["PermissionID"].ToString().Trim());
                    if ((permissionid == -1) || base.UserPrincipal.HasPermissionID(permissionid))
                    {
                        builder.AppendFormat("<li><a href=\"{0}\" target=\"mainFrame\">{1}</a></li>", str4, str3);
                    }
                }
                builder.Append("</ul>");
            }
            else
            {
                foreach (DataRow row2 in rowArray)
                {
                    string str5 = row2["NodeID"].ToString();
                    string str6 = row2["TreeText"].ToString();
                    if (this.TreeListofLang[str5] != null)
                    {
                        str6 = this.TreeListofLang[str5].ToString();
                    }
                    row2["ParentID"].ToString();
                    row2["Location"].ToString();
                    row2["Url"].ToString();
                    row2["ImageUrl"].ToString();
                    int num2 = int.Parse(row2["PermissionID"].ToString().Trim());
                    if ((num2 == -1) || base.UserPrincipal.HasPermissionID(num2))
                    {
                        builder.Append("<ul class=\"open\">");
                        builder.AppendFormat("<span class=\"span_open\">{0}</span>", str6);
                        DataRow[] rowArray3 = dt.Select("ParentID= " + str5);
                        builder.Append(this.LoadMenu3(rowArray3));
                        builder.Append("</ul>");
                    }
                }
            }
            this.strMenuTree = builder.ToString();
        }

        public string LoadMenu3(DataRow[] dr3)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DataRow row in dr3)
            {
                string str = row["NodeID"].ToString();
                string str2 = row["TreeText"].ToString();
                if (this.TreeListofLang[str] != null)
                {
                    str2 = this.TreeListofLang[str].ToString();
                }
                row["ParentID"].ToString();
                row["Location"].ToString();
                string str3 = row["Url"].ToString();
                row["ImageUrl"].ToString();
                int permissionid = int.Parse(row["PermissionID"].ToString().Trim());
                if ((permissionid == -1) || base.UserPrincipal.HasPermissionID(permissionid))
                {
                    builder.Append("<li><a href=\"" + str3 + "\" target=\"mainFrame\">" + str2 + "</a></li>");
                }
            }
            return builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].ToString() != ""))
            {
                string str = "zh-CN";
                if (this.Session["language"] != null)
                {
                    str = this.Session["language"].ToString();
                }
                this.TreeListofLang = this.bllML.GetHashValueListByLangCache("TreeText", str);
                string str2 = base.Request.Params["id"];
                SysTree tree = new SysTree();
                SysNode modelByCache = tree.GetModelByCache(Convert.ToInt32(str2));
                this.NodeName = modelByCache.TreeText;
                this.Page.Title = this.NodeName;
                DataSet allEnabledTreeByType = tree.GetAllEnabledTreeByType(4);
                this.LoadMenu(allEnabledTreeByType.Tables[0], modelByCache.NodeID);
            }
        }
    }
}

