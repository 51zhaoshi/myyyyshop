namespace Maticsoft.Web.Admin.Accounts.Admin
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Web;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.WebControls;

    public class roleassignment : PageBaseAdmin
    {
        protected int Act_ShowReservedRole = 13;
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBoxList CheckBoxList1;
        protected Label lblUserID;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        private List<string> ReservedRoleIDs = StringPlus.GetStrArray(ConfigSystem.GetValueByCache("ReservedRoleIDs"), ',', true);

        public void btnBack_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("UserAdmin.aspx?PageIndex=" + base.Request.Params["PageIndex"]);
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            User user = new User(Convert.ToInt32(this.lblUserID.Text));
            foreach (ListItem item in this.CheckBoxList1.Items)
            {
                if (item.Selected)
                {
                    user.AddToRole(Convert.ToInt32(item.Value));
                }
                else
                {
                    user.RemoveRole(Convert.ToInt32(item.Value));
                }
            }
            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("编辑用户【{0}】的权限", user.UserName), this);
            base.Response.Redirect("UserAdmin.aspx?PageIndex=" + base.Request.Params["PageIndex"]);
        }

        public void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["UserID"] != null)) && (base.Request.Params["UserID"] != ""))
            {
                string str = base.Request.Params["UserID"];
                this.lblUserID.Text = str;
                User user = new User(Convert.ToInt32(str));
                DataSet roleList = AccountsTool.GetRoleList();
                this.CheckBoxList1.DataSource = roleList.Tables[0].DefaultView;
                this.CheckBoxList1.DataTextField = "Description";
                this.CheckBoxList1.DataValueField = "RoleID";
                this.CheckBoxList1.DataBind();
                AccountsPrincipal principal = new AccountsPrincipal(user.UserName);
                if (principal.Roles.Count > 0)
                {
                    ArrayList roles = principal.Roles;
                    for (int i = 0; i < roles.Count; i++)
                    {
                        foreach (ListItem item in this.CheckBoxList1.Items)
                        {
                            if (item.Text == roles[i].ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_ShowReservedRole)))
                {
                    for (int j = 0; j < this.CheckBoxList1.Items.Count; j++)
                    {
                        if (this.ReservedRoleIDs.Contains(this.CheckBoxList1.Items[j].Value))
                        {
                            this.CheckBoxList1.Items.Remove(this.CheckBoxList1.Items[j]);
                        }
                    }
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xc6;
            }
        }
    }
}

