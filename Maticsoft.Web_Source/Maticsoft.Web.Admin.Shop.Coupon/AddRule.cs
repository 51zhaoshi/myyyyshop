namespace Maticsoft.Web.Admin.Shop.Coupon
{
    using Maticsoft.BLL.Shop.Coupon;
    using Maticsoft.BLL.Shop.Supplier;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Coupon;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI.WebControls;

    public class AddRule : PageBaseAdmin
    {
        protected Button btnSave;
        protected CheckBox chkIsPwd;
        protected CheckBox chkIsReuse;
        protected CheckBox chkStatus;
        private Maticsoft.BLL.Shop.Coupon.CouponClass classBll = new Maticsoft.BLL.Shop.Coupon.CouponClass();
        protected CategoriesDropList ddlCateList;
        protected DropDownList ddlClass;
        protected DropDownList ddlLength;
        protected DropDownList ddlPwd;
        protected DropDownList ddlSupplier;
        protected HiddenField hfCategory;
        protected HiddenField hfSupplier;
        protected Label lblMsg;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal12;
        protected Literal Literal13;
        protected Literal Literal14;
        protected Literal Literal15;
        protected Literal Literal17;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal9;
        private Maticsoft.BLL.Shop.Coupon.CouponRule ruleBll = new Maticsoft.BLL.Shop.Coupon.CouponRule();
        private SupplierInfo supplierBll = new SupplierInfo();
        protected TextBox txtEndDate;
        protected TextBox txtLimitPrice;
        protected TextBox txtName;
        protected TextBox txtPoint;
        protected TextBox txtPreName;
        protected TextBox txtPrice;
        protected TextBox txtSendCount;
        protected TextBox txtStartDate;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Coupon.CouponRule model = new Maticsoft.Model.Shop.Coupon.CouponRule();
            string text = this.txtName.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.ShowFailTip(this, "请填写优惠券名称");
            }
            else
            {
                decimal num = Globals.SafeDecimal(this.txtLimitPrice.Text, (decimal) -1M);
                if (num == -1M)
                {
                    MessageBox.ShowFailTip(this, "请填写最低消费金额");
                }
                else
                {
                    decimal num2 = Globals.SafeDecimal(this.txtPrice.Text, (decimal) -1M);
                    if (num2 == -1M)
                    {
                        MessageBox.ShowFailTip(this, "请填写消费券面值");
                    }
                    else if (string.IsNullOrWhiteSpace(this.txtStartDate.Text) || string.IsNullOrWhiteSpace(this.txtEndDate.Text))
                    {
                        MessageBox.ShowFailTip(this, "请选择优惠券使用时间");
                    }
                    else
                    {
                        int num3 = Globals.SafeInt(this.txtSendCount.Text, 0);
                        if (num3 == 0)
                        {
                            MessageBox.ShowFailTip(this, "请填写生成数量");
                        }
                        else
                        {
                            model.Name = text;
                            model.NeedPoint = Globals.SafeInt(this.txtPoint.Text, 0);
                            model.IsPwd = this.chkIsPwd.Checked ? 1 : 0;
                            model.IsReuse = this.chkIsReuse.Checked ? 1 : 0;
                            model.LimitPrice = num;
                            model.PreName = this.txtPreName.Text;
                            model.SendCount = num3;
                            model.StartDate = Globals.SafeDateTime(this.txtStartDate.Text, DateTime.Now);
                            model.Status = this.chkStatus.Checked ? 1 : 0;
                            model.SupplierId = Globals.SafeInt(this.ddlSupplier.SelectedValue, 0);
                            model.CategoryId = Globals.SafeInt(this.ddlCateList.SelectedValue, 0);
                            model.ClassId = Globals.SafeInt(this.ddlClass.SelectedValue, 0);
                            model.CouponPrice = num2;
                            model.CreateDate = DateTime.Now;
                            model.CreateUserId = base.CurrentUser.UserID;
                            model.EndDate = Globals.SafeDateTime(this.txtEndDate.Text, DateTime.MaxValue);
                            int cpLength = Globals.SafeInt(this.ddlLength.SelectedValue, 0);
                            int pwdLegth = Globals.SafeInt(this.ddlPwd.SelectedValue, 0);
                            if (this.ruleBll.AddEx(model, cpLength, pwdLegth))
                            {
                                MessageBox.ShowSuccessTip(this, "生成优惠券成功", "CouponList.aspx");
                            }
                            else
                            {
                                MessageBox.ShowFailTip(this, "生成优惠券失败");
                            }
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.hfCategory.Value = ConfigSystem.GetValueByCache("Shop_CouponRule_IsCategory");
                this.hfSupplier.Value = ConfigSystem.GetValueByCache("Shop_CouponRule_IsSupplier");
                this.ddlClass.DataSource = this.classBll.GetList(" Status=1");
                this.ddlClass.DataTextField = "Name";
                this.ddlClass.DataValueField = "ClassId";
                this.ddlClass.DataBind();
                this.ddlClass.Items.Insert(0, new ListItem("请选择", "0"));
                this.ddlSupplier.DataSource = this.supplierBll.GetList("Status=1");
                this.ddlSupplier.DataTextField = "Name";
                this.ddlSupplier.DataValueField = "SupplierId";
                this.ddlSupplier.DataBind();
                this.ddlSupplier.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1a0;
            }
        }
    }
}

