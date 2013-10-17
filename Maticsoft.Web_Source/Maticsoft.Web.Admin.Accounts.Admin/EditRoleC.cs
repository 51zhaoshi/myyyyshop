namespace Maticsoft.Web.Admin.Accounts.Admin
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class EditRoleC : PageBaseAdmin
    {
        protected int Act_DelData = 0x22;
        protected int Act_DelUserData = 0x23;
        protected int Act_ShowReservedPerm = 14;
        protected int Act_UpdateData = 0x21;
        protected int Act_UpdateUserData = 0x24;
        private User bllUser = new User();
        protected Button btnCancle;
        protected Button btnRemove;
        protected Button btnSave;
        protected Button BtnUpName;
        protected Button Button1;
        protected ListBox CategoryDownList;
        protected CheckBox chkAll;
        protected CheckBoxList chkPermissions;
        private Role currentRole;
        protected GridViewEx gridView;
        protected Label lblRoleID;
        protected Label lblTiptool;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal6;
        protected Button RemoveRoleButton;
        private string ReservedPermIDs = ConfigSystem.GetValueByCache("ReservedPermIDs");
        private List<string> ReservedRoleIDs = StringPlus.GetStrArray(ConfigSystem.GetValueByCache("ReservedRoleIDs"), ',', true);
        public string RoleID = "";
        protected Label RoleLabel;
        private List<int> rolePermissionlist;
        protected TextBox TxtNewname;

        public void BindData()
        {
            this.gridView.DataSetSource = this.bllUser.GetUsersByRole(int.Parse(this.lblRoleID.Text));
        }

        public void btnBach_ServerClick(object sender, EventArgs e)
        {
            base.Response.Redirect("RoleAdmin.aspx");
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.gridView.Rows[i].FindControl(this.gridView.CheckBoxID);
                if (((box != null) && box.Checked) && (this.gridView.DataKeys[i].Value != null))
                {
                    int userID = Convert.ToInt32(this.gridView.DataKeys[i].Value);
                    this.bllUser.RemoveRole(userID, int.Parse(this.lblRoleID.Text));
                    User user = new User(userID);
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("从角色中移除用户：【{0}】", user.UserName), this);
                }
            }
            MessageBox.ShowSuccessTip(this, "移除成功！");
            this.gridView.OnBind();
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            Role role = new Role {
                RoleID = Convert.ToInt32(this.lblRoleID.Text)
            };
            foreach (ListItem item in this.chkPermissions.Items)
            {
                if (item.Selected)
                {
                    role.AddPermission(Convert.ToInt32(item.Value));
                }
                else
                {
                    role.RemovePermission(Convert.ToInt32(item.Value));
                }
            }
            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "编辑角色权限", this);
            MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK);
        }

        public void BtnUpName_Click(object sender, EventArgs e)
        {
            string str = this.TxtNewname.Text.Trim();
            this.currentRole = new Role(Convert.ToInt32(this.lblRoleID.Text));
            this.currentRole.Description = str;
            this.currentRole.Update();
            this.DoInitialDataBind();
            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("编辑角色：【{0}】", str), this);
            this.lblTiptool.Text = Site.TooltipUpdateOK;
        }

        public void CategoryDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chkAll.Checked = false;
            if (((this.CategoryDownList.SelectedItem != null) && (this.CategoryDownList.SelectedValue.Length > 0)) && PageValidate.IsNumber(this.CategoryDownList.SelectedValue))
            {
                this.FillCategoryList(Convert.ToInt32(this.CategoryDownList.SelectedItem.Value));
            }
        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAll.Checked)
            {
                foreach (ListItem item in this.chkPermissions.Items)
                {
                    item.Selected = true;
                }
            }
            else
            {
                foreach (ListItem item2 in this.chkPermissions.Items)
                {
                    item2.Selected = false;
                }
            }
        }

        public void chkPermissions_DataBinding(object sender, EventArgs e)
        {
            foreach (ListItem item in ((CheckBoxList) sender).Items)
            {
                if (this.rolePermissionlist.Contains(Convert.ToInt32(item.Value)))
                {
                    item.Selected = true;
                }
            }
        }

        private void DoInitialDataBind()
        {
            this.currentRole = new Role(Convert.ToInt32(this.lblRoleID.Text));
            this.RoleLabel.Text = this.currentRole.Description;
            this.TxtNewname.Text = this.currentRole.Description;
            DataSet allCategories = AccountsTool.GetAllCategories();
            if (!DataSetTools.DataSetIsNull(allCategories))
            {
                this.CategoryDownList.DataSource = allCategories.Tables[0];
                this.CategoryDownList.DataTextField = "Description";
                this.CategoryDownList.DataValueField = "CategoryID";
                this.CategoryDownList.DataBind();
            }
        }

        public void FillCategoryList(int categoryId)
        {
            this.currentRole = new Role(Convert.ToInt32(this.lblRoleID.Text));
            this.GetRolePermissionlist();
            DataSet permissionsByCategory = AccountsTool.GetPermissionsByCategory(categoryId);
            if (!DataSetTools.DataSetIsNull(permissionsByCategory))
            {
                DataView defaultView = permissionsByCategory.Tables[0].DefaultView;
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_ShowReservedPerm)))
                {
                    defaultView.RowFilter = "PermissionID not in (" + this.ReservedPermIDs + ")";
                }
                this.chkPermissions.DataSource = defaultView;
                this.chkPermissions.DataValueField = "PermissionID";
                this.chkPermissions.DataTextField = "Description";
                this.chkPermissions.DataBind();
            }
        }

        private void GetRolePermissionlist()
        {
            DataSet permissions = this.currentRole.Permissions;
            if (!DataSetTools.DataSetIsNull(permissions))
            {
                this.rolePermissionlist = new List<int>();
                DataTable dt = permissions.Tables["Permissions"];
                if (!DataTableTools.DataTableIsNull(dt))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        this.rolePermissionlist.Add(Convert.ToInt32(row["PermissionID"]));
                    }
                }
            }
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowIndex % 2) == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
                object obj2 = DataBinder.Eval(e.Row.DataItem, "UserID");
                if ((obj2 != null) && StringPlus.GetStrArray(ConfigSystem.GetValueByCache("AdminUserID"), ',', true).Contains(obj2.ToString()))
                {
                    CheckBox box = (CheckBox) e.Row.FindControl(this.gridView.CheckBoxID);
                    if ((box != null) && box.Checked)
                    {
                        box.Visible = false;
                    }
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string item = this.gridView.DataKeys[e.RowIndex].Value.ToString();
            if (StringPlus.GetStrArray(ConfigSystem.GetValueByCache("AdminUserID"), ',', true).Contains(item))
            {
                MessageBox.ShowSuccessTip(this, Site.ErrorCannotDeleteID);
            }
            else
            {
                try
                {
                    User user = new User(int.Parse(item));
                    user.Delete();
                    new UsersExp().DeleteUsersExp(user.UserID);
                    this.gridView.OnBind();
                }
                catch (SqlException exception)
                {
                    if (exception.Number == 0x223)
                    {
                        MessageBox.ShowSuccessTip(this, Site.ErrorCannotDeleteUser);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.lblRoleID.Text = base.Request["RoleID"];
                if (!this.ReservedRoleIDs.Contains(this.lblRoleID.Text))
                {
                    this.RemoveRoleButton.Attributes.Add("onclick", "return confirm(\"" + Site.TooltipDelConfirm + "\")");
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    this.BtnUpName.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.RemoveRoleButton.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelUserData)) && (base.GetPermidByActID(this.Act_DelUserData) != -1))
                {
                    this.btnRemove.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateUserData)) && (base.GetPermidByActID(this.Act_UpdateUserData) != -1))
                {
                    this.btnSave.Visible = false;
                }
                if ((this.Session["Style"] != null) && (this.Session["Style"].ToString() != ""))
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
                this.gridView.OnBind();
                this.DoInitialDataBind();
                this.CategoryDownList_SelectedIndexChanged(sender, e);
            }
            this.RoleID = this.lblRoleID.Text;
        }

        public void RemoveRoleButton_Click(object sender, EventArgs e)
        {
            if (this.ReservedRoleIDs.Contains(this.lblRoleID.Text))
            {
                MessageBox.ShowSuccessTip(this, Site.ErrorCannotDeleteRole);
            }
            else
            {
                new Role(Convert.ToInt32(this.lblRoleID.Text)).Delete();
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("删除角色：【{0}】", this.TxtNewname.Text.Trim()), this);
                base.Server.Transfer("RoleAdmin.aspx");
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
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

