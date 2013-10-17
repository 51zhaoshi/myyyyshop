namespace Maticsoft.Web.Supplier
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI.HtmlControls;

    public class Top : PageBaseSupplier
    {
        protected HtmlHead Head1;
        public string strMenu = "";
        public string username = "";

        public void LoadTree(DataTable dt)
        {
            int num = 1;
            StringBuilder builder = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                string str = row["NodeID"].ToString();
                string str2 = row["TreeText"].ToString();
                row["ParentID"].ToString();
                row["Location"].ToString();
                if ((row["Url"] != null) && (row["Url"].ToString().Length > 0))
                {
                    row["Url"].ToString();
                }
                row["ImageUrl"].ToString();
                int permissionid = int.Parse(row["PermissionID"].ToString().Trim());
                if ((permissionid == -1) || base.UserPrincipal.HasPermissionID(permissionid))
                {
                    builder.Append("<li id=\"Tab" + num.ToString() + "\">");
                    builder.Append("<a href=\"left.aspx?id=" + str + "\" target=\"leftFrame\" onclick=\"javascript:switchTab('TabPage1','Tab" + num.ToString() + "');\">" + str2);
                    builder.Append("</a></li>");
                }
                num++;
            }
            this.strMenu = builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack && (this.TreeType <= -1))
            {
                this.username = base.CurrentUser.UserName;
                DataSet set = new SysTree().GetEnabledTreeByParentId(0, 4, true);
                this.LoadTree(set.Tables[0]);
            }
        }

        public int TreeType
        {
            get
            {
                return Globals.SafeInt(base.Request.Params["TreeType"], -1);
            }
        }
    }
}

