namespace Maticsoft.Web.Admin.Shop.Products
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class SelectAccessorie : PageBaseAdmin
    {
        protected Button btnSearch;
        protected Label Count;
        protected DropDownList drpProductCategory;
        protected GridView gridView;
        protected HiddenField hfCurrentAllData;
        protected HiddenField hfCurrentDataCount;
        private SKUInfo manage = new SKUInfo();
        protected TextBox txtProductName;
        protected TextBox txtSKU;

        private void BindCategories()
        {
            DataSet list = new CategoryInfo().GetList("  Depth = 1 ");
            if (!DataSetTools.DataSetIsNull(list))
            {
                this.drpProductCategory.DataSource = list;
                this.drpProductCategory.DataTextField = "Name";
                this.drpProductCategory.DataValueField = "CategoryId";
                this.drpProductCategory.DataBind();
            }
            this.drpProductCategory.Items.Insert(0, new ListItem("请选择", string.Empty));
        }

        public void BindData()
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView view = sender as GridView;
            int newPageIndex = 0;
            if (-2 == e.NewPageIndex)
            {
                TextBox box = null;
                GridViewRow bottomPagerRow = view.BottomPagerRow;
                if (bottomPagerRow != null)
                {
                    box = bottomPagerRow.FindControl("txtNewPageIndex") as TextBox;
                }
                if (box != null)
                {
                    newPageIndex = int.Parse(box.Text) - 1;
                }
            }
            else
            {
                newPageIndex = e.NewPageIndex;
            }
            newPageIndex = (newPageIndex < 0) ? 0 : newPageIndex;
            newPageIndex = (newPageIndex >= view.PageCount) ? (view.PageCount - 1) : newPageIndex;
            view.PageIndex = newPageIndex;
            this.BindData();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("style", "background:#FFF;cursor: pointer;");
                e.Row.Attributes.Add("onclick", "$(this).find('[type=checkbox]').prop('checked',!$(this).find('[type=checkbox]').prop('checked'));");
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
                if ((this.Session["Style"] != null) && (this.Session["Style"].ToString() != ""))
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
                this.BindCategories();
                this.BindData();
            }
        }
    }
}

