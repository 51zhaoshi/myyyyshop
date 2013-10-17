namespace Maticsoft.Web.Admin.Shop.ProductReview
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Button btnCancle;
        protected HtmlInputHidden hidImagesNames;
        protected HtmlInputHidden hidImagesPath;
        protected Label lblCreatedDate;
        protected Label lblParentId;
        protected Label lblProductId;
        protected Label lblReviewId;
        protected Label lblReviewText;
        protected Label lblScore;
        protected Label lblState;
        protected Label lblUserEmail;
        protected Label lblUserId;
        protected Label lblUserName;
        protected Literal Literal2;
        public string strid = "";

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && (this.ReviewId > 0))
            {
                this.ShowInfo(this.ReviewId);
            }
        }

        private void ShowInfo(int ReviewId)
        {
            Maticsoft.Model.Shop.Products.ProductReviews model = new Maticsoft.BLL.Shop.Products.ProductReviews().GetModel(ReviewId);
            if (model != null)
            {
                this.lblReviewId.Text = model.ReviewId.ToString();
                this.lblProductId.Text = new Maticsoft.BLL.Shop.Products.ProductInfo().GetProductName(model.ProductId);
                this.lblUserId.Text = model.UserId.ToString();
                this.lblReviewText.Text = Globals.HtmlDecode(model.ReviewText);
                this.lblUserName.Text = model.UserName;
                this.lblUserEmail.Text = model.UserEmail;
                this.lblCreatedDate.Text = model.CreatedDate.ToString();
                this.lblParentId.Text = model.ParentId.ToString();
                this.hidImagesNames.Value = model.ImagesNames;
                this.hidImagesPath.Value = model.ImagesPath;
                if (model.Status == 0)
                {
                    this.lblState.Text = "未审核";
                }
                else if (model.Status == 1)
                {
                    this.lblState.Text = "已审核";
                }
                else
                {
                    this.lblState.Text = "审核失败";
                }
                this.lblScore.Text = new Maticsoft.BLL.Shop.Products.ScoreDetails().GetScore(new int?(ReviewId)).ToString();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1d4;
            }
        }

        public int ReviewId
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

