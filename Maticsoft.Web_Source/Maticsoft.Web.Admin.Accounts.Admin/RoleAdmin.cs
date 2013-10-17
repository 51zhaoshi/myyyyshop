namespace Maticsoft.Web.Admin.Accounts.Admin
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class RoleAdmin : PageBaseAdmin
    {
        protected int Act_AddData = 0x20;
        protected int Act_ShowReservedRole = 13;
        protected Button btnSave;
        protected HtmlGenericControl divAdd;
        protected Label lblToolTip;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        private string ReservedRoleIDs = ConfigSystem.GetValueByCache("ReservedRoleIDs").Trim();
        protected DataList RoleList;
        private DataSet roles;
        protected TextBox txtRoleName;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtRoleName.Text.Trim().Length <= 0)
            {
                MessageBox.ShowFailTip(this, SysManage.ErrorRoleIsNull);
            }
            else
            {
                try
                {
                    Role role = new Role();
                    if (!role.RoleExists(this.txtRoleName.Text.Trim()))
                    {
                        role.Description = this.txtRoleName.Text;
                        role.Create();
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("添加角色：【{0}】", this.txtRoleName.Text), this);
                        MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK);
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, Site.TooltipDataExist);
                    }
                }
                catch (Exception exception)
                {
                    this.lblToolTip.Text = exception.Message;
                    return;
                }
                this.txtRoleName.Text = "";
                this.dataBind();
            }
        }

        private void dataBind()
        {
            this.roles = AccountsTool.GetRoleList();
            DataView defaultView = this.roles.Tables["Roles"].DefaultView;
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_ShowReservedRole)))
            {
                defaultView.RowFilter = "RoleID not in (" + this.ReservedRoleIDs + ")";
            }
            this.RoleList.DataSource = defaultView;
            this.RoleList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.divAdd.Visible = false;
                }
                this.dataBind();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1f;
            }
        }
    }
}

