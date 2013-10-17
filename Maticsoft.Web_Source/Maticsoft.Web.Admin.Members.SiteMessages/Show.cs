namespace Maticsoft.Web.Admin.Members.SiteMessages
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        private Maticsoft.BLL.Members.SiteMessage bll = new Maticsoft.BLL.Members.SiteMessage();
        protected Button btnCancle;
        protected Label lblContent;
        protected Label lblID;
        protected Label lblReceiverID;
        protected Label lblSenderID;
        protected Literal Literal1;
        public string strid = "";
        private Maticsoft.Accounts.Bus.UserType UserType = new Maticsoft.Accounts.Bus.UserType();

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        public string GetUser(object obj, object ReceiverID)
        {
            if (obj != null)
            {
                string description = this.UserType.GetDescription(obj.ToString());
                if (description != null)
                {
                    return description;
                }
            }
            else if (PageValidate.IsNumber(ReceiverID.ToString()))
            {
                Maticsoft.Model.Members.Users model = new Maticsoft.BLL.Members.Users().GetModel(Convert.ToInt32(ReceiverID));
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
                int iD = Convert.ToInt32(this.strid);
                this.ShowInfo(iD);
            }
        }

        private void ShowInfo(int ID)
        {
            Maticsoft.Model.Members.SiteMessage model = new Maticsoft.BLL.Members.SiteMessage().GetModel(ID);
            this.lblID.Text = model.ID.ToString();
            this.lblReceiverID.Text = this.GetUser(model.MsgType, model.ReceiverID);
            this.lblContent.Text = model.Content;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x12f;
            }
        }
    }
}

