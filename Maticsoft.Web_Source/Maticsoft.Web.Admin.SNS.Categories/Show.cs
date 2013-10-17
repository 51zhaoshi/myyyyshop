namespace Maticsoft.Web.Admin.SNS.Categories
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Label lblCategoryId;
        protected Label lblCreatedDate;
        protected Label lblCreatedUserID;
        protected Label lblDepth;
        protected Label lblDescription;
        protected Label lblHasChildren;
        protected Label lblIsMenu;
        protected Label lblMenuIsShow;
        protected Label lblMenuSequence;
        protected Label lblName;
        protected Label lblParentID;
        protected Label lblPath;
        protected Label lblSequence;
        protected Label lblStatus;
        protected Label lblType;
        public string strid = "";

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].Trim() != ""))
            {
                this.strid = base.Request.Params["id"];
                int categoryId = Convert.ToInt32(this.strid);
                this.ShowInfo(categoryId);
            }
        }

        private void ShowInfo(int CategoryId)
        {
            Maticsoft.Model.SNS.Categories model = new Maticsoft.BLL.SNS.Categories().GetModel(CategoryId);
            this.lblCategoryId.Text = model.CategoryId.ToString();
            this.lblName.Text = model.Name;
            this.lblDescription.Text = model.Description;
            this.lblParentID.Text = model.ParentID.ToString();
            this.lblPath.Text = model.Path;
            this.lblDepth.Text = model.Depth.ToString();
            this.lblSequence.Text = model.Sequence.ToString();
            this.lblHasChildren.Text = model.HasChildren ? "是" : "否";
            this.lblIsMenu.Text = model.IsMenu ? "是" : "否";
            this.lblType.Text = model.Type.ToString();
            this.lblMenuIsShow.Text = model.MenuIsShow ? "是" : "否";
            this.lblMenuSequence.Text = model.MenuSequence.ToString();
            this.lblCreatedUserID.Text = model.CreatedUserID.ToString();
            this.lblCreatedDate.Text = model.CreatedDate.ToString();
            this.lblStatus.Text = model.Status.ToString();
        }

        protected override int Act_PageLoad
        {
            get
            {
                switch (this.Type)
                {
                    case 0:
                        return 0x236;

                    case 1:
                        return 0x233;
                }
                return 0x236;
            }
        }

        public int Type
        {
            get
            {
                int num = 0;
                if (this.Session["CategoryType"] != null)
                {
                    num = Globals.SafeInt(this.Session["CategoryType"].ToString(), 0);
                }
                return num;
            }
        }
    }
}

