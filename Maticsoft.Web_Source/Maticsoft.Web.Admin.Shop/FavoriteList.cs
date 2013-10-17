namespace Maticsoft.Web.Admin.Shop
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Shop;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class FavoriteList : PageBaseAdmin
    {
        protected Button btnSearch;
        private User currentUser;
        private Favorite favorBll = new Favorite();
        protected GridViewEx gridView;
        protected Literal Literal2;
        private ProductInfo productBll = new ProductInfo();
        protected TextBox txtKeyword;
        protected Literal txtTitle;

        public void BindData()
        {
            string text = this.txtKeyword.Text;
            DataSet listEX = this.favorBll.GetListEX(this.UserId, text);
            if (listEX != null)
            {
                this.gridView.DataSetSource = listEX;
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

        public string GetTargetName(long ProductId, short typeid)
        {
            if (typeid == 1)
            {
                return this.productBll.GetProductName(ProductId);
            }
            return "";
        }

        public string GetUserName(int userid)
        {
            User user = new User(userid);
            if (user != null)
            {
                return user.UserName;
            }
            return "--";
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
            if (!this.Page.IsPostBack)
            {
                if (this.UserId != 0)
                {
                    this.currentUser = new User(this.UserId);
                    if (this.currentUser == null)
                    {
                        base.Response.Write("<script language=javascript>window.alert('" + Site.TooltipUserExist + @"\');history.back();</script>");
                        return;
                    }
                    this.txtTitle.Text = this.currentUser.UserName + "的礼品兑换明细";
                }
                else
                {
                    this.txtTitle.Text = "会员收藏列表";
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
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 560;
            }
        }

        public int UserId
        {
            get
            {
                int num = 0;
                if ((base.Request.Params["userid"] != null) && PageValidate.IsNumber(base.Request.Params["userid"]))
                {
                    num = int.Parse(base.Request.Params["userid"]);
                }
                return num;
            }
        }
    }
}

