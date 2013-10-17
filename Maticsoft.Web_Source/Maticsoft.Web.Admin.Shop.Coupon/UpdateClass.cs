namespace Maticsoft.Web.Admin.Shop.Coupon
{
    using Maticsoft.BLL.Shop.Coupon;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Coupon;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class UpdateClass : PageBaseAdmin
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
            Maticsoft.Model.Shop.Coupon.CouponClass model = this.classBll.GetModel(this.ClassId);
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
                if (this.classBll.Update(model))
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
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.Shop.Coupon.CouponClass model = this.classBll.GetModel(this.ClassId);
            this.tName.Text = model.Name;
            this.chkStatus.Checked = model.Status == 1;
            this.txtSequence.Text = model.Sequence.ToString();
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x19b;
            }
        }

        public int ClassId
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["id"]))
                {
                    num = Globals.SafeInt(base.Request.Params["id"], 0);
                }
                return num;
            }
        }
    }
}

