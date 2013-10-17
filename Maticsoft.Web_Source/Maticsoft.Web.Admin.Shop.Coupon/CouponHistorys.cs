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
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class CouponHistorys : PageBaseAdmin
    {
        protected int Act_DelData = 0x1a5;
        protected Button btnDelete;
        protected Button Button2;
        protected CheckBox chkCategory;
        protected CheckBox chkRule;
        protected CheckBox chkSupplier;
        protected CheckBox chkUser;
        private Maticsoft.BLL.Shop.Coupon.CouponClass classBll = new Maticsoft.BLL.Shop.Coupon.CouponClass();
        private Maticsoft.BLL.Shop.Coupon.CouponHistory couponBll = new Maticsoft.BLL.Shop.Coupon.CouponHistory();
        protected DropDownList ddlClass;
        protected DropDownList ddlRule;
        protected DropDownList ddlStatus;
        protected GridViewEx gridView;
        protected Label Label1;
        protected Literal Literal1;
        protected Literal Literal3;
        protected Literal Literal4;
        private Maticsoft.BLL.Shop.Coupon.CouponRule ruleBll = new Maticsoft.BLL.Shop.Coupon.CouponRule();
        protected TextBox txtEndDate;
        protected TextBox txtKeyword;
        protected TextBox txtStartDate;

        public void BindData()
        {
            StringBuilder builder = new StringBuilder();
            int num = Globals.SafeInt(this.ddlClass.SelectedValue, 0);
            int num2 = Globals.SafeInt(this.ddlRule.SelectedValue, 0);
            int num3 = Globals.SafeInt(this.ddlStatus.SelectedValue, -1);
            string text = this.txtStartDate.Text;
            string str2 = this.txtEndDate.Text;
            if (num3 != -1)
            {
                builder.AppendFormat(" Status ={0}", num3);
            }
            if (num > 0)
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat(" ClassId ={0}", num);
            }
            if (num2 > 0)
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat(" RuleId ={0}", num2);
            }
            if (!string.IsNullOrWhiteSpace(str2))
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat(" EndDate <='{0}'", str2);
            }
            if (!string.IsNullOrWhiteSpace(text))
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat(" StartDate >='{0}'", text);
            }
            string str3 = this.txtKeyword.Text;
            if (!string.IsNullOrWhiteSpace(str3))
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat(" CouponCode Like '%{0}%'", str3);
            }
            DataSet list = this.couponBll.GetList(builder.ToString());
            if (list != null)
            {
                this.gridView.DataSetSource = list;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.couponBll.DeleteList(selIDlist))
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.Columns[2].Visible = this.chkRule.Checked;
            this.gridView.Columns[8].Visible = this.chkCategory.Checked;
            this.gridView.Columns[4].Visible = this.chkSupplier.Checked;
            this.gridView.Columns[9].Visible = this.chkUser.Checked;
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
                        return "未分配";

                    case 1:
                        return "未使用";

                    case 2:
                        return "已使用";
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
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)) && (base.GetPermidByActID(base.Act_AddData) != -1))
                {
                    this.btnDelete.Visible = false;
                }
                this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
                this.ddlClass.DataSource = this.classBll.GetList("Status=1");
                this.ddlClass.DataTextField = "Name";
                this.ddlClass.DataValueField = "ClassId";
                this.ddlClass.DataBind();
                this.ddlClass.Items.Insert(0, new ListItem("请选择", "0"));
                this.ddlRule.DataSource = this.ruleBll.GetList("Status=1");
                this.ddlRule.DataTextField = "Name";
                this.ddlRule.DataValueField = "RuleId";
                this.ddlRule.DataBind();
                this.ddlRule.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 420;
            }
        }
    }
}

