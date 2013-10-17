namespace Maticsoft.Web.Admin.Accounts.Admin
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.WebControls;

    public class UserRoleAssignment : PageBaseAdmin
    {
        protected Button btnBack;
        protected Button btnSave;
        protected CheckBoxList cblRole;
        protected string DefaultText = SysManage.lstDefaultUser;
        protected Label lblName;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected ListBox ltbUser;
        private List<string> ReservedRoleIDs = StringPlus.GetStrArray(ConfigSystem.GetValueByCache("ReservedRoleIDs"), ',', true);

        public void btnBack_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Admin/Ms/Enterprise/EnterpriseRoleAssignment.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            int selectedUserId = this.SelectedUserId;
            User user = new User(selectedUserId);
            foreach (ListItem item in this.cblRole.Items)
            {
                int roleID = Globals.SafeInt(item.Value, -1);
                if (item.Selected)
                {
                    user.AddAssignRole(selectedUserId, roleID);
                }
                else
                {
                    user.DeleteAssignRole(selectedUserId, roleID);
                }
            }
        }

        protected void ltbUser_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectionRoles();
        }

        public void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string departmentId = this.DepartmentId;
                string userType = this.UserType;
                if (!string.IsNullOrWhiteSpace(departmentId) && !string.IsNullOrWhiteSpace(this.UserType))
                {
                    int enterpriseID = Globals.SafeInt(departmentId, -1);
                    if (enterpriseID >= 1)
                    {
                        Maticsoft.Model.Ms.Enterprise model = new Maticsoft.BLL.Ms.Enterprise().GetModel(enterpriseID);
                        this.lblName.Text = model.Name;
                        this.ltbUser.DataSource = new User().GetUserList(userType, departmentId, string.Empty);
                        this.ltbUser.DataTextField = "UserName";
                        this.ltbUser.DataValueField = "UserID";
                        this.ltbUser.DataBind();
                        this.ltbUser.SelectedIndex = 0;
                        for (int i = 0; i < this.ltbUser.Items.Count; i++)
                        {
                            if (this.ltbUser.Items[i].Text == model.UserName)
                            {
                                ListItem item1 = this.ltbUser.Items[i];
                                item1.Text = item1.Text + this.DefaultText;
                            }
                        }
                        DataSet roleList = AccountsTool.GetRoleList();
                        this.cblRole.DataSource = roleList.Tables[0].DefaultView;
                        this.cblRole.DataTextField = "Description";
                        this.cblRole.DataValueField = "RoleID";
                        this.cblRole.DataBind();
                        for (int j = 0; j < this.cblRole.Items.Count; j++)
                        {
                            if (this.ReservedRoleIDs.Contains(this.cblRole.Items[j].Value))
                            {
                                this.cblRole.Items.Remove(this.cblRole.Items[j]);
                            }
                        }
                        this.SelectionRoles();
                    }
                }
            }
        }

        private void SelectionRoles()
        {
            this.cblRole.ClearSelection();
            DataSet assignRolesByUser = new User(this.SelectedUserId).GetAssignRolesByUser(this.SelectedUserId);
            if ((assignRolesByUser != null) && (assignRolesByUser.Tables[0].Rows.Count >= 1))
            {
                for (int i = 0; i < assignRolesByUser.Tables[0].Rows.Count; i++)
                {
                    foreach (ListItem item in this.cblRole.Items)
                    {
                        if (item.Text == assignRolesByUser.Tables[0].Rows[i]["Description"].ToString())
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xcb;
            }
        }

        public string DepartmentId
        {
            get
            {
                return base.Request.Params["DepartmentId"];
            }
        }

        protected int SelectedUserId
        {
            get
            {
                return Globals.SafeInt(this.ltbUser.SelectedValue, -1);
            }
        }

        public string UserType
        {
            get
            {
                return base.Request.Params["UserType"];
            }
        }
    }
}

