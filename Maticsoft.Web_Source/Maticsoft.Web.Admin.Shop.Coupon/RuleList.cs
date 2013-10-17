namespace Maticsoft.Web.Admin.Shop.Coupon
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Shop.Coupon;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.Shop.Supplier;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Coupon;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.Shop.Supplier;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class RuleList : PageBaseAdmin
    {
        protected int Act_AddData = 0x1a1;
        protected Button Button1;
        private Maticsoft.BLL.Shop.Coupon.CouponClass classBll = new Maticsoft.BLL.Shop.Coupon.CouponClass();
        protected GridViewEx gridView;
        private Maticsoft.BLL.Shop.Coupon.CouponInfo infoBll = new Maticsoft.BLL.Shop.Coupon.CouponInfo();
        protected Label Label1;
        protected HtmlGenericControl liAdd;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        private Maticsoft.BLL.Shop.Coupon.CouponRule ruleBll = new Maticsoft.BLL.Shop.Coupon.CouponRule();
        protected TextBox txtKeyword;

        public void BindData()
        {
            StringBuilder builder = new StringBuilder();
            string text = this.txtKeyword.Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                builder.AppendFormat(" Name Like '%{0}%'", text);
            }
            DataSet list = this.ruleBll.GetList(builder.ToString());
            if (list != null)
            {
                this.gridView.DataSetSource = list;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        public string GeStatusName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target, 0))
                {
                    case 0:
                        return "不启用";

                    case 1:
                        return "启用";
                }
            }
            return str;
        }

        public string GetCategoryName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int categoryId = Globals.SafeInt(target, 0);
                Maticsoft.Model.Shop.Products.CategoryInfo model = new Maticsoft.BLL.Shop.Products.CategoryInfo().GetModel(categoryId);
                str = (model == null) ? "" : model.Name;
            }
            return str;
        }

        public string GetClassName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int classId = Globals.SafeInt(target, 0);
                Maticsoft.Model.Shop.Coupon.CouponClass model = this.classBll.GetModel(classId);
                str = (model == null) ? "" : model.Name;
            }
            return str;
        }

        public string GetSupplierName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int supplierId = Globals.SafeInt(target, 0);
                Maticsoft.Model.Shop.Supplier.SupplierInfo model = new Maticsoft.BLL.Shop.Supplier.SupplierInfo().GetModel(supplierId);
                str = (model == null) ? "" : model.Name;
            }
            return str;
        }

        public string GetUserName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                User user = new User(Globals.SafeInt(target, 0));
                str = (user == null) ? "" : user.NickName;
            }
            return str;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if ((e.CommandName == "DeleteCoupon") && (e.CommandArgument != null))
            {
                int ruleId = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                if (this.infoBll.DeleteEx(ruleId))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                    this.gridView.OnBind();
                }
            }
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
            int couponId = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.ruleBll.Delete(couponId);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.liAdd.Visible = false;
                }
                this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x19f;
            }
        }
    }
}

