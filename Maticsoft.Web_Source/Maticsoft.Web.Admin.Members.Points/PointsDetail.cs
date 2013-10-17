namespace Maticsoft.Web.Admin.Members.Points
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class PointsDetail : PageBaseAdmin
    {
        protected Button btnReturn;
        protected Button btnSearch;
        private User currentUser;
        protected DropDownList DropPointsType;
        protected GridViewEx gridView;
        protected Literal Literal2;
        protected Literal txtUserName;

        public void BindData()
        {
            if (((base.Request.Params["userid"] != null) && (base.Request.Params["userid"].ToString() != "")) && PageValidate.IsNumber(base.Request.Params["userid"]))
            {
                int num = int.Parse(base.Request.Params["userid"]);
                int num2 = Convert.ToInt32(this.DropPointsType.SelectedValue);
                DataSet list = new DataSet();
                Maticsoft.BLL.Members.PointsDetail detail = new Maticsoft.BLL.Members.PointsDetail();
                if (num2 != -1)
                {
                    list = detail.GetList(string.Concat(new object[] { " UserId=", num, " and type=", num2 }));
                }
                else
                {
                    list = detail.GetList(" UserId=" + num);
                }
                if (list != null)
                {
                    this.gridView.DataSetSource = list;
                }
            }
        }

        public void btnReturn_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("/Admin/Accounts/Admin/UserAdmin.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        public string GetActionName(string action)
        {
            if (action == "Use")
            {
                return "积分消费";
            }
            if (action == "Buy")
            {
                return "购买商品";
            }
            Maticsoft.Model.Members.PointsRule model = new Maticsoft.BLL.Members.PointsRule().GetModel(action);
            if (model != null)
            {
                return model.Name;
            }
            return "";
        }

        public string GetTypeName(int type)
        {
            if (type != 0)
            {
                return "积分消费";
            }
            return "积分获取";
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
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["userid"] != null)) && ((base.Request.Params["userid"].ToString() != "") && PageValidate.IsNumber(base.Request.Params["userid"])))
            {
                int existingUserID = int.Parse(base.Request.Params["userid"]);
                this.currentUser = new User(existingUserID);
                if (this.currentUser == null)
                {
                    base.Response.Write("<script language=javascript>window.alert('" + Site.TooltipUserExist + @"\');history.back();</script>");
                }
                else
                {
                    this.txtUserName.Text = this.currentUser.NickName + "的积分明细";
                    this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                    this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
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
                return 0x12d;
            }
        }
    }
}

