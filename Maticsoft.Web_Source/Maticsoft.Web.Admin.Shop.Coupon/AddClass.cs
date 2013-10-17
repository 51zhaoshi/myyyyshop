namespace Maticsoft.Web.Admin.Shop.Coupon
{
    using Maticsoft.BLL.Shop.Coupon;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Coupon;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class AddClass : PageBaseAdmin
    {
        protected Button btnSave;
        protected CheckBox chkStatus;
        private Maticsoft.BLL.Shop.Coupon.CouponClass classBll = new Maticsoft.BLL.Shop.Coupon.CouponClass();
        protected Label lblMsg;
        protected Literal Literal1;
        protected Literal Literal3;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected TextBox tName;
        protected TextBox txtSequence;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Coupon.CouponClass model = new Maticsoft.Model.Shop.Coupon.CouponClass();
            string text = this.tName.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.ShowFailTip(this, "请输入分类名称");
            }
            else
            {
                model.Name = text;
                model.Status = this.chkStatus.Checked ? 1 : 0;
                model.Sequence = Globals.SafeInt(this.txtSequence.Text, 0);
                if (this.classBll.Add(model) > 0)
                {
                    MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, "操作失败！");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtSequence.Text = (this.classBll.GetSequence() + 1).ToString();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 410;
            }
        }
    }
}

