namespace Maticsoft.Web.Admin.Shop.ProductQA
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Reply : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected Label lbCreatedDate;
        protected Label lbQuestion;
        protected Label lbUserName;
        protected Literal Literal2;
        protected Literal Literal3;
        private Maticsoft.BLL.Shop.Products.ProductQA QAbll = new Maticsoft.BLL.Shop.Products.ProductQA();
        protected RadioButton raFalse;
        protected RadioButton raTrue;
        protected RequiredFieldValidator RequiredFieldValidator2;
        protected TextBox tReply;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("ProductQAList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Products.ProductQA model = this.QAbll.GetModel(this.QAId);
            model.ReplyContent = this.tReply.Text;
            model.ReplyDate = new DateTime?(DateTime.Now);
            model.ReplyUserId = new int?(base.CurrentUser.UserID);
            model.ReplyUserName = base.CurrentUser.UserName;
            if (this.raTrue.Checked)
            {
                model.State = 1;
            }
            if (this.raFalse.Checked)
            {
                model.State = 2;
            }
            if (this.QAbll.Update(model))
            {
                this.btnSave.Enabled = false;
                this.btnCancle.Enabled = false;
                MessageBox.ShowSuccessTip(this, "回复成功,正在跳转...", "ProductQAList.aspx");
            }
            else
            {
                this.btnSave.Enabled = false;
                this.btnCancle.Enabled = false;
                MessageBox.ShowFailTip(this, "添加失败！");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Maticsoft.Model.Shop.Products.ProductQA model = this.QAbll.GetModel(this.QAId);
                if (model == null)
                {
                    base.Response.Redirect("ProductQAList.aspx");
                }
                this.lbQuestion.Text = model.Question;
                this.lbUserName.Text = model.UserName;
                this.lbCreatedDate.Text = model.CreatedDate.ToString();
                this.tReply.Text = model.ReplyContent;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1ce;
            }
        }

        public int QAId
        {
            get
            {
                if ((base.Request.Params["qaid"] != null) && PageValidate.IsNumber(base.Request.Params["qaid"]))
                {
                    return int.Parse(base.Request.Params["qaid"]);
                }
                base.Response.Redirect("ProductQAList.aspx");
                return 0;
            }
        }
    }
}

