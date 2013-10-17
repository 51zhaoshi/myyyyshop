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

    public class UserRoleAdmin : PageBaseAdmin
    {
        protected Button btnSearch;
        protected DropDownList DropUserType;
        protected GridViewEx gridView;
        protected Label Label1;
        protected HtmlTableCell liAdd;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox txtKeyword;
        private UserType userTypeManage = new UserType();

        public void BindData()
        {
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
                object obj2 = DataBinder.Eval(e.Row.DataItem, "EmployeeID");
                if ((obj2 != null) && (obj2.ToString() != ""))
                {
                    string str = obj2.ToString().Trim();
                    if (str == "-1")
                    {
                        e.Row.Cells[4].Text = "";
                    }
                    else
                    {
                        e.Row.Cells[4].Text = str;
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
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)))
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
                this.DropUserType.Items.Remove(this.DropUserType.Items.FindByValue("UU"));
                this.DropUserType.Items.Remove(this.DropUserType.Items.FindByValue("AA"));
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xca;
            }
        }
    }
}

