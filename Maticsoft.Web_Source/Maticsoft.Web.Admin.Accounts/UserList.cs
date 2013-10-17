namespace Maticsoft.Web.Admin.Accounts
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
    using System.Web.UI.WebControls;

    public class UserList : PageBaseAdmin
    {
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected Label Label1;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected TextBox txtKeyword;
        private UserType userTypeManage = new UserType();

        public void BindData()
        {
            DataSet listEX = new DataSet();
            string keyWord = "";
            if (this.txtKeyword.Text.Trim() != "")
            {
                keyWord = this.txtKeyword.Text.Trim();
            }
            listEX = new Users().GetListEX(keyWord);
            this.gridView.DataSetSource = listEX;
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
                    user.Delete();
                    new UsersExp().DeleteUsersExp(user.UserID);
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
            if ((!this.Page.IsPostBack && (this.Session["Style"] != null)) && (this.Session["Style"].ToString() != ""))
            {
                string str = this.Session["Style"] + "xtable_bordercolorlight";
                if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                {
                    this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xd0;
            }
        }
    }
}

