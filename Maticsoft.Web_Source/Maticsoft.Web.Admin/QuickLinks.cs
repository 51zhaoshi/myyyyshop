namespace Maticsoft.Web.Admin
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Web;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI.HtmlControls;

    public class QuickLinks : PageBaseAdmin
    {
        public string FavoriteMenu = "";
        protected HtmlForm form2;
        protected HtmlHead Head1;
        public string ShortcutMenu = "";

        public void LoadMenu(DataTable dt)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DataRow row in dt.Select("", "OrderID"))
            {
                row["NodeID"].ToString();
                string str = row["TreeText"].ToString();
                string str2 = row["Url"].ToString();
                builder.AppendFormat("<li><a href=\"{0}\" target=\"mainFrame\">{1}</a></li>", str2, str);
            }
            this.FavoriteMenu = builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && this.Context.User.Identity.IsAuthenticated) && (base.CurrentUser != null))
            {
                DataSet menuListByUser = new TreeFavorite().GetMenuListByUser(base.CurrentUser.UserID);
                this.LoadMenu(menuListByUser.Tables[0]);
            }
        }
    }
}

