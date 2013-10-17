namespace Maticsoft.Web.Admin
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Components;
    using Maticsoft.Web;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Top2 : PageBaseAdmin
    {
        protected HtmlHead Head1;
        protected HiddenField hfCurrentID;
        protected Label lblTotal;
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
                int permissionid = -1;
                row["ImageUrl"].ToString();
                if (row["PermissionID"] != null)
                {
                    permissionid = int.Parse(row["PermissionID"].ToString().Trim());
                }
                if ((permissionid == -1) || base.UserPrincipal.HasPermissionID(permissionid))
                {
                    builder.Append("<li id=\"Tab" + num.ToString() + "\">");
                    builder.Append("<a href=\"left2.aspx?id=" + str + "\" target=\"leftFrame\" onclick=\"javascript:switchTab('TabPage1','Tab" + num.ToString() + "');\">" + str2);
                    builder.Append("</a></li>");
                }
                num++;
            }
            builder.AppendFormat("<script language=\"JavaScript\" type=\"text/javascript\">window.top.document.title='动软系统框架-系统管理{0}';</script>", !Maticsoft.Components.MvcApplication.IsAuthorize ? " Powered by Maticsoft" : "");
            this.strMenu = builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.username = string.IsNullOrWhiteSpace(base.CurrentUser.NickName) ? base.CurrentUser.UserName : base.CurrentUser.NickName;
            if (!base.IsPostBack)
            {
                DataSet set = new SysTree().GetEnabledTreeByParentId(0, 0, true);
                this.LoadTree(set.Tables[0]);
            }
            this.hfCurrentID.Value = base.CurrentUser.UserID.ToString();
        }
    }
}

