namespace Maticsoft.Web.Admin.Members.UsersApprove
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Image ImageFrontView;
        protected Image ImageRearView;
        protected Label lblApproveDate;
        protected Label lblApproveID;
        protected Label lblApproveUserID;
        protected Label lblCreatedDate;
        protected Label lblDueDate;
        protected Label lblIDCardNum;
        protected Label lblStatus;
        protected Label lblTrueName;
        protected Label lblUserID;
        protected Label lblUserType;
        public string strid = "";

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected string GetUserName(int userId)
        {
            if (userId > 0)
            {
                Maticsoft.Model.Members.Users model = new Maticsoft.BLL.Members.Users().GetModel(userId);
                if (model != null)
                {
                    return model.UserName;
                }
            }
            return "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].Trim() != ""))
            {
                this.strid = base.Request.Params["id"];
                int approveID = Convert.ToInt32(this.strid);
                this.ShowInfo(approveID);
            }
        }

        private void ShowInfo(int ApproveID)
        {
            Maticsoft.Model.Members.UsersApprove model = new Maticsoft.BLL.Members.UsersApprove().GetModel(ApproveID);
            this.lblApproveID.Text = model.ApproveID.ToString();
            this.lblUserID.Text = this.GetUserName(model.UserID);
            this.lblTrueName.Text = model.TrueName;
            this.lblIDCardNum.Text = model.IDCardNum;
            this.ImageFrontView.ImageUrl = model.FrontView;
            this.ImageRearView.ImageUrl = model.RearView;
            this.lblDueDate.Text = model.DueDate.ToString();
            this.lblStatus.Text = model.Status.ToString();
            this.lblApproveUserID.Text = this.GetUserName(model.ApproveUserID);
            this.lblUserType.Text = model.UserType.ToString();
            this.lblCreatedDate.Text = model.CreatedDate.ToString();
            this.lblApproveDate.Text = model.ApproveDate.ToString();
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x132;
            }
        }
    }
}

