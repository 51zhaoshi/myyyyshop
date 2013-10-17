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
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class UserAdmin : PageBaseAdmin
    {
        protected int Act_AddData = 0x10;
        protected int Act_DelData = 30;
        protected int Act_SetPerData = 0x1b;
        protected int Act_UpdateData = 0x1d;
        protected Button btnSearch;
        protected DropDownList DropUserType;
        protected GridViewEx gridView;
        protected Label Label1;
        protected HtmlGenericControl liAdd;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected TextBox txtKeyword;
        private UserType userTypeManage = new UserType();

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_SetPerData)) && (base.GetPermidByActID(this.Act_SetPerData) != -1))
            {
                this.gridView.Columns[1].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[7].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[8].Visible = false;
            }
            DataSet usersByType = new DataSet();
            string selectedValue = this.DropUserType.SelectedValue;
            string key = "";
            if (this.txtKeyword.Text.Trim() != "")
            {
                key = this.txtKeyword.Text.Trim();
            }
            User user = new User();
            if (selectedValue != "")
            {
                usersByType = user.GetUsersByType(selectedValue, key);
            }
            else
            {
                usersByType = user.GetUserList(key);
            }
            this.gridView.DataSetSource = usersByType;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.gridView.Rows[i].FindControl(this.gridView.CheckBoxID);
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    if (this.gridView.DataKeys[i].Value != null)
                    {
                        str = str + this.gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (flag)
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
        }

        public string GetTypeName(object userType)
        {
            if ((userType != null) && !string.IsNullOrEmpty(userType.ToString()))
            {
                return this.userTypeManage.GetDescriptionByCache(userType.ToString());
            }
            return "";
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
                    LinkButton button = (LinkButton) e.Row.FindControl("LinkButton1");
                    button.Visible = false;
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string item = this.gridView.DataKeys[e.RowIndex].Value.ToString();
            if (StringPlus.GetStrArray(ConfigSystem.GetValueByCache("AdminUserID"), ',', true).Contains(item))
            {
                MessageBox.ShowFailTip(this, Site.ErrorCannotDeleteID);
            }
            else
            {
                try
                {
                    User user = new User(int.Parse(item));
                    new Users().DeleteEx(int.Parse(item));
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("删除用户：【{0}】", user.UserName), this);
                    MessageBox.ShowSuccessTip(this, "删除成功！");
                    this.gridView.OnBind();
                }
                catch (SqlException exception)
                {
                    if (exception.Number == 0x223)
                    {
                        MessageBox.ShowFailTip(this, Site.ErrorCannotDeleteUser);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.liAdd.Visible = false;
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
                this.DropUserType.DataSource = this.userTypeManage.GetAllList();
                this.DropUserType.DataTextField = "Description";
                this.DropUserType.DataValueField = "UserType";
                this.DropUserType.DataBind();
                this.DropUserType.Items.Insert(0, new ListItem(SysManage.ListItemAll, ""));
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1c;
            }
        }
    }
}

