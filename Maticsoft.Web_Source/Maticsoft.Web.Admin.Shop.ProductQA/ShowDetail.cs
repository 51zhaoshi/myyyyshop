namespace Maticsoft.Web.Admin.Shop.ProductQA
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class ShowDetail : PageBaseAdmin
    {
        protected Button btnSave;
        protected Label lbCreatedDate;
        protected Label lbQuestion;
        protected Label lbReply;
        protected Label lbReplyDate;
        protected Label lbReplyName;
        protected Label lbState;
        protected Label lbUserName;
        protected Literal Literal2;
        private Maticsoft.BLL.Shop.Products.ProductQA QAbll = new Maticsoft.BLL.Shop.Products.ProductQA();

        public void btnReturn_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("ProductQAList.aspx");
        }

        public string GetState(int state)
        {
            switch (state)
            {
                case 0:
                    return "未审核";

                case 1:
                    return "审核通过";

                case 2:
                    return "未审核通过";
            }
            return "";
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
                this.lbQuestion.Text = Globals.HtmlDecode(model.Question);
                this.lbUserName.Text = model.UserName;
                this.lbCreatedDate.Text = model.CreatedDate.ToString();
                this.lbReply.Text = Globals.HtmlDecode(model.ReplyContent);
                this.lbReplyDate.Text = model.ReplyDate.ToString();
                this.lbReplyName.Text = model.ReplyUserName;
                this.lbState.Text = this.GetState(model.State);
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1cf;
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

