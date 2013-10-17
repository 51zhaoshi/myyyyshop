namespace Maticsoft.Web.Admin.Shop.Gift
{
    using Maticsoft.BLL.Shop.Gift;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class CategoryList : PageBaseAdmin
    {
        protected int Act_AddData = 0x1ad;
        protected int Act_DelData = 0x1af;
        protected int Act_UpdateData = 430;
        private GiftsCategory bll = new GiftsCategory();
        protected Button btnSearch;
        protected GridView gridView;
        protected HtmlGenericControl liAdd;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox txtKeyword;

        public void BindData()
        {
            DataSet categoryList = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.txtKeyword.Text.Trim() != "")
            {
                builder.AppendFormat("Name like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            categoryList = this.bll.GetCategoryList(builder.ToString());
            this.gridView.DataSource = categoryList;
            this.gridView.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.DataBind();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.DataBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow) ((Control) e.CommandSource).NamingContainer).RowIndex;
            int categoryId = (int) this.gridView.DataKeys[rowIndex].Value;
            if (e.CommandName == "Fall")
            {
                this.bll.SwapSequence(categoryId, SwapSequenceIndex.Up);
            }
            if (e.CommandName == "Rise")
            {
                this.bll.SwapSequence(categoryId, SwapSequenceIndex.Down);
            }
            this.BindData();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    HtmlGenericControl control = (HtmlGenericControl) e.Row.FindControl("lbtnModify");
                    control.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    LinkButton button = (LinkButton) e.Row.FindControl("linkDel");
                    button.Visible = false;
                }
                int num = (int) DataBinder.Eval(e.Row.DataItem, "Depth");
                string str = DataBinder.Eval(e.Row.DataItem, "Name").ToString();
                e.Row.Cells[0].CssClass = "productcag" + num.ToString();
                if (num != 1)
                {
                    HtmlGenericControl control2 = e.Row.FindControl("spShowImage") as HtmlGenericControl;
                    control2.Visible = false;
                }
                Label label = e.Row.FindControl("lblName") as Label;
                label.Text = str;
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int categoryId = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.DeleteCategory(categoryId);
            this.BindData();
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
                this.gridView.DataBind();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1ac;
            }
        }
    }
}

