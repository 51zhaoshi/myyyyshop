namespace Maticsoft.Web.Admin.SNS.Report
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        private Maticsoft.BLL.SNS.Report bll = new Maticsoft.BLL.SNS.Report();
        protected Button btnReportFalse;
        protected Button btnReportTrue;
        protected Button btnReportUnKnow;
        protected Label lblDesc;
        protected Image lblImage;
        protected Label lblName;
        protected Label lblType;
        protected Literal Literal2;
        protected Literal Literal3;
        private Maticsoft.BLL.SNS.Photos photoBll = new Maticsoft.BLL.SNS.Photos();
        private Maticsoft.BLL.SNS.Posts postBll = new Maticsoft.BLL.SNS.Posts();
        private Maticsoft.BLL.SNS.Products productBll = new Maticsoft.BLL.SNS.Products();
        private Maticsoft.BLL.SNS.ReportType typeBll = new Maticsoft.BLL.SNS.ReportType();

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("Report.aspx");
        }

        protected void btnReportFalse_Click(object sender, EventArgs e)
        {
            this.postBll.DeleteListByNormalPost(this.Id.ToString(), true, base.CurrentUser.UserID);
            this.bll.UpdateReportStatus(2, this.Id);
            MessageBox.ResponseScript(this, "parent.location.href='Report.aspx'");
        }

        protected void btnReportTrue_Click(object sender, EventArgs e)
        {
            this.postBll.DeleteListByNormalPost(this.Id.ToString(), true, base.CurrentUser.UserID);
            this.bll.UpdateReportStatus(1, this.Id);
            MessageBox.ResponseScript(this, "parent.location.href='Report.aspx'");
        }

        protected void btnReportUnKnow_Click(object sender, EventArgs e)
        {
            this.postBll.DeleteListByNormalPost(this.Id.ToString(), true, base.CurrentUser.UserID);
            this.bll.UpdateReportStatus(3, this.Id);
            MessageBox.ResponseScript(this, "parent.location.href='Report.aspx'");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && (this.Id > 0))
            {
                this.ShowInfo(this.Id);
            }
        }

        private void ShowInfo(int Id)
        {
            Maticsoft.Model.SNS.Report model = this.bll.GetModel(Id);
            if (model != null)
            {
                Maticsoft.Model.SNS.ReportType type = this.typeBll.GetModel(model.ReportTypeID);
                this.lblType.Text = type.TypeName;
                this.lblDesc.Text = model.Description;
                if (model.TargetType == 0)
                {
                    Maticsoft.Model.SNS.Posts posts = this.postBll.GetModel(model.TargetID);
                    this.lblName.Text = "此信息已不存在";
                    if (posts != null)
                    {
                        this.lblName.Text = posts.Description;
                        if (!string.IsNullOrWhiteSpace(posts.ImageUrl))
                        {
                            this.lblImage.ImageUrl = posts.ImageUrl;
                            this.lblImage.Visible = true;
                        }
                    }
                }
                if (model.TargetType == 1)
                {
                    Maticsoft.Model.SNS.Photos photos = this.photoBll.GetModel(model.TargetID);
                    this.lblName.Text = "此信息已不存在";
                    if (photos != null)
                    {
                        this.lblName.Text = string.Concat(new object[] { "<a href='/Photo/Detail/", photos.PhotoID, "' target='_blank'>", photos.PhotoName, "</a>" });
                        if (!string.IsNullOrWhiteSpace(photos.ThumbImageUrl))
                        {
                            this.lblImage.ImageUrl = photos.ThumbImageUrl;
                            this.lblImage.Visible = true;
                        }
                    }
                }
                if (model.TargetType == 2)
                {
                    Maticsoft.Model.SNS.Products products = this.productBll.GetModel((long) model.TargetID);
                    this.lblName.Text = "此信息已不存在";
                    if (products != null)
                    {
                        this.lblName.Text = string.Concat(new object[] { "<a href='/Product/Detail/", products.ProductID, "' target='_blank'>", products.ProductName, "</a>" });
                        if (!string.IsNullOrWhiteSpace(products.ThumbImageUrl))
                        {
                            this.lblImage.ImageUrl = products.ThumbImageUrl;
                            this.lblImage.Visible = true;
                        }
                    }
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 610;
            }
        }

        public int Id
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

