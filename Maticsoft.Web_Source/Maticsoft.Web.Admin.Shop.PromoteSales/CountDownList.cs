namespace Maticsoft.Web.Admin.Shop.PromoteSales
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.Shop.PromoteSales;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class CountDownList : PageBaseAdmin
    {
        protected Button btnDelete;
        protected Button btnSearch;
        protected Button Button1;
        protected Button Button2;
        protected DropDownList ddlStatus;
        private CountDown downBll = new CountDown();
        protected GridViewEx gridView;
        private ProductInfo infoBll = new ProductInfo();
        protected HtmlGenericControl liAdd;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal5;
        protected TextBox txtDate;
        protected TextBox txtKeyword;

        public void BindData()
        {
            StringBuilder builder = new StringBuilder();
            int num = Globals.SafeInt(this.ddlStatus.SelectedValue, -1);
            string text = this.txtDate.Text;
            if (num != -1)
            {
                builder.AppendFormat(" Status ={0}", num);
            }
            if (!string.IsNullOrWhiteSpace(text))
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat(" EndDate <='{0}'", text);
            }
            string str2 = this.txtKeyword.Text;
            if (!string.IsNullOrWhiteSpace(str2))
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat(" Description Like '%{0}%'", str2);
            }
            DataSet set = this.downBll.GetList(-1, builder.ToString(), " Sequence desc  ");
            if (set != null)
            {
                this.gridView.DataSetSource = set;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.downBll.DeleteList(selIDlist))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipDelError);
                }
                this.gridView.OnBind();
            }
        }

        protected void btnOff_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.downBll.UpdateStatus(selIDlist, 0))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "操作失败");
                }
                this.gridView.OnBind();
            }
        }

        protected void btnOn_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.downBll.UpdateStatus(selIDlist, 1))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "操作失败");
                }
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        public string GetProductName(object target)
        {
            string productName = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                long productId = Globals.SafeLong(target, (long) 0L);
                productName = this.infoBll.GetProductName(productId);
            }
            return productName;
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
                        str = str + "'" + this.gridView.DataKeys[i].Value.ToString() + "',";
                    }
                }
            }
            if (flag)
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
        }

        public string GetStatusName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target, 0))
                {
                    case 0:
                        return "下架";

                    case 1:
                        return "上架";
                }
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
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}

