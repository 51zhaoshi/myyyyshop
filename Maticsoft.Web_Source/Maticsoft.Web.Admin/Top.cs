namespace Maticsoft.Web.Admin
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Web;
    using System;
    using System.Data;
    using System.Text;

    public class Top : PageBaseAdmin
    {
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
            this.username = base.CurrentUser.TrueName;
            if (!base.IsPostBack)
            {
                DataSet treeList = new SysTree().GetTreeList("ParentID=0");
                this.LoadTree(treeList.Tables[0]);
            }
        }
    }
}

