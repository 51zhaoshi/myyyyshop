namespace Maticsoft.Web.Admin.Members.FeedBack
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class ShowDetail : PageBaseAdmin
    {
        private Maticsoft.BLL.Members.Feedback bll = new Maticsoft.BLL.Members.Feedback();
        protected Button btnSolve;
        protected Label lblCompany;
        protected Label lblCreatedDate;
        protected Label lblEmail;
        protected Label lblIsSolved;
        protected Label lblPhone;
        protected Label lblSex;
        protected Label lblStatus;
        protected TextBox lbltxtContent;
        protected Label lbltypeName;
        protected Label lblUserIP;
        protected Label lblUserName;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal12;
        protected Literal Literal13;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected TextBox txtResult;
        private Maticsoft.BLL.Members.FeedbackType typeBll = new Maticsoft.BLL.Members.FeedbackType();

        protected void btnSolve_Click(object sender, EventArgs e)
        {
            string text = this.txtResult.Text;
            Maticsoft.Model.Members.Feedback model = this.bll.GetModel(this.FeedbackID);
            model.IsSolved = true;
            model.Result = text;
            if (this.bll.Update(model))
            {
                new EmailTemplet().SendFeedbackEmail(model);
                MessageBox.ShowSuccessTip(this, "反馈已解决", "FeedbackList.aspx");
            }
        }

        protected string GetTypeName(int typeId)
        {
            string typeName = string.Empty;
            Maticsoft.Model.Members.FeedbackType model = this.typeBll.GetModel(typeId);
            if (model != null)
            {
                typeName = model.TypeName;
            }
            return typeName;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ShowInfo(this.FeedbackID);
            }
        }

        private void ShowInfo(int FeedbackID)
        {
            Maticsoft.Model.Members.Feedback model = this.bll.GetModel(FeedbackID);
            this.lblUserName.Text = model.UserName;
            this.lblPhone.Text = model.Phone;
            this.lblCompany.Text = model.UserCompany;
            this.lblEmail.Text = model.UserEmail;
            this.lbltxtContent.Text = model.Description;
            this.lblIsSolved.Text = model.IsSolved ? Site.lblTrue : Site.lblFalse;
            this.lblUserIP.Text = model.UserIP;
            this.lblCreatedDate.Text = model.CreatedDate.ToString();
            this.txtResult.Text = model.Result;
            this.lblSex.Text = model.UserSex;
            this.lblStatus.Text = (model.Status == 0) ? Site.lblFalse : Site.lblTrue;
            this.lbltypeName.Text = this.GetTypeName(model.TypeId);
            if (model.IsSolved)
            {
                this.btnSolve.Visible = false;
                this.txtResult.ReadOnly = true;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x11d;
            }
        }

        public int FeedbackID
        {
            get
            {
                if ((base.Request.Params["id"] != null) && PageValidate.IsNumber(base.Request.Params["id"]))
                {
                    return int.Parse(base.Request.Params["id"]);
                }
                base.Response.Redirect("FeedbackList.aspx");
                return 0;
            }
        }
    }
}

