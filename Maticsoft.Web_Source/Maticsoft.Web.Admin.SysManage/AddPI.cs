namespace Maticsoft.Web.Admin.SysManage
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class AddPI : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnDown;
        protected Button btnSave;
        protected Button btnUP;
        protected ListBox listboxSysManage;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected ScriptManager ScriptManager1;
        private Maticsoft.BLL.SysManage.TreeFavorite sm = new Maticsoft.BLL.SysManage.TreeFavorite();
        protected UpdatePanel UpdatePanel1;
        protected UpdateProgress UpdateProgress1;

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("TreeFavorite.aspx?TreeType=" + this.TreeType);
        }

        protected void btnDown_Click(object sender, EventArgs e)
        {
            if (this.listboxSysManage.SelectedItem != null)
            {
                int selectedIndex = this.listboxSysManage.SelectedIndex;
                if (selectedIndex < (this.listboxSysManage.Items.Count - 1))
                {
                    string text = this.listboxSysManage.Items[selectedIndex + 1].Text;
                    string str2 = this.listboxSysManage.Items[selectedIndex + 1].Value;
                    this.listboxSysManage.Items[selectedIndex + 1].Text = this.listboxSysManage.Items[selectedIndex].Text;
                    this.listboxSysManage.Items[selectedIndex + 1].Value = this.listboxSysManage.Items[selectedIndex].Value;
                    this.listboxSysManage.Items[selectedIndex].Text = text;
                    this.listboxSysManage.Items[selectedIndex].Value = str2;
                    this.listboxSysManage.SelectedIndex = selectedIndex + 1;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (base.CurrentUser != null)
            {
                User currentUser = base.CurrentUser;
                int count = this.listboxSysManage.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    int nodeID = Convert.ToInt32(this.listboxSysManage.Items[i].Value);
                    int orderID = i + 1;
                    this.sm.UpDate(orderID, currentUser.UserID, nodeID);
                }
                MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK);
            }
        }

        protected void btnUP_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.listboxSysManage.SelectedIndex;
            if (selectedIndex > 0)
            {
                string text = this.listboxSysManage.Items[selectedIndex - 1].Text;
                string str2 = this.listboxSysManage.Items[selectedIndex - 1].Value;
                this.listboxSysManage.Items[selectedIndex - 1].Text = this.listboxSysManage.Items[selectedIndex].Text;
                this.listboxSysManage.Items[selectedIndex - 1].Value = this.listboxSysManage.Items[selectedIndex].Value;
                this.listboxSysManage.Items[selectedIndex].Text = text;
                this.listboxSysManage.Items[selectedIndex].Value = str2;
                this.listboxSysManage.SelectedIndex = selectedIndex - 1;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (((!this.Page.IsPostBack && this.Context.User.Identity.IsAuthenticated) && (this.TreeType >= 0)) && (base.CurrentUser != null))
            {
                User currentUser = base.CurrentUser;
                DataSet menuListByUser = this.sm.GetMenuListByUser(currentUser.UserID);
                this.listboxSysManage.DataSource = menuListByUser;
                this.listboxSysManage.DataTextField = "TreeText";
                this.listboxSysManage.DataValueField = "NodeID";
                this.listboxSysManage.DataBind();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xd4;
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

