namespace Maticsoft.Web.Admin.Accounts.Admin
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Data;
    using System.Web.UI.WebControls;

    public class editrole : PageBaseAdmin
    {
        protected Button AddPermissionButton;
        protected Button BtnUpName;
        protected Button Button1;
        protected DropDownList CategoryDownList;
        protected ListBox CategoryList;
        private Role currentRole;
        protected Label lblRoleID;
        protected Label lblTiptool;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal6;
        protected Literal Literal7;
        protected ListBox PermissionList;
        protected Button RemovePermissionButton;
        protected Button RemoveRoleButton;
        public string RoleID = "";
        protected Label RoleLabel;
        protected TextBox TxtNewname;

        public void AddPermissionButton_Click(object sender, EventArgs e)
        {
            if (this.CategoryList.SelectedIndex > -1)
            {
                new Role(Convert.ToInt32(this.lblRoleID.Text)).AddPermission(Convert.ToInt32(this.CategoryList.SelectedValue));
                this.CategoryDownList_SelectedIndexChanged(sender, e);
            }
        }

        public void BtnUpName_Click(object sender, EventArgs e)
        {
            string str = this.TxtNewname.Text.Trim();
            this.currentRole = new Role(Convert.ToInt32(this.lblRoleID.Text));
            this.currentRole.Description = str;
            this.currentRole.Update();
            this.DoInitialDataBind();
            this.lblTiptool.Text = Site.TooltipUpdateOK;
        }

        public void Button2_ServerClick(object sender, EventArgs e)
        {
            base.Response.Redirect("RoleAdmin.aspx");
        }

        public void CategoryDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int categoryId = Convert.ToInt32(this.CategoryDownList.SelectedItem.Value);
            this.FillCategoryList(categoryId);
            this.SelectCategory(categoryId, false);
        }

        private void DoInitialDataBind()
        {
            this.currentRole = new Role(Convert.ToInt32(this.lblRoleID.Text));
            this.RoleLabel.Text = this.currentRole.Description;
            this.TxtNewname.Text = this.currentRole.Description;
            DataSet allCategories = AccountsTool.GetAllCategories();
            this.CategoryDownList.DataSource = allCategories.Tables[0];
            this.CategoryDownList.DataTextField = "Description";
            this.CategoryDownList.DataValueField = "CategoryID";
            this.CategoryDownList.DataBind();
        }

        public void FillCategoryList(int categoryId)
        {
            this.currentRole = new Role(Convert.ToInt32(this.lblRoleID.Text));
            DataTable table = this.currentRole.NoPermissions.Tables["Categories"];
            DataRow row = table.Rows.Find(categoryId);
            if (row != null)
            {
                DataRow[] childRows = row.GetChildRows("PermissionCategories");
                this.CategoryList.Items.Clear();
                foreach (DataRow row2 in childRows)
                {
                    this.CategoryList.Items.Add(new ListItem((string) row2["Description"], Convert.ToString(row2["PermissionID"])));
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.lblRoleID.Text = base.Request["RoleID"];
                this.DoInitialDataBind();
                this.CategoryDownList_SelectedIndexChanged(sender, e);
            }
            this.RoleID = this.lblRoleID.Text;
        }

        public void RemovePermissionButton_Click(object sender, EventArgs e)
        {
            if (this.PermissionList.SelectedIndex > -1)
            {
                new Role(Convert.ToInt32(this.lblRoleID.Text)).RemovePermission(Convert.ToInt32(this.PermissionList.SelectedValue));
                this.CategoryDownList_SelectedIndexChanged(sender, e);
            }
        }

        public void RemoveRoleButton_Click(object sender, EventArgs e)
        {
            new Role(Convert.ToInt32(this.lblRoleID.Text)).Delete();
            base.Server.Transfer("RoleAdmin.aspx");
        }

        public void SelectCategory(int categoryId, bool forceSelection)
        {
            this.currentRole = new Role(Convert.ToInt32(this.lblRoleID.Text));
            DataTable table = this.currentRole.Permissions.Tables["Categories"];
            DataRow row = table.Rows.Find(categoryId);
            if (row != null)
            {
                DataRow[] childRows = row.GetChildRows("PermissionCategories");
                this.PermissionList.Items.Clear();
                foreach (DataRow row2 in childRows)
                {
                    this.PermissionList.Items.Add(new ListItem((string) row2["Description"], Convert.ToString(row2["PermissionID"])));
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xc7;
            }
        }
    }
}

