namespace Maticsoft.Web.FriendlyLink.FLinks
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Model.Settings;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class ShowNew : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Image imgImgUrl;
        protected Label lblContactPerson;
        protected Label lblEmail;
        protected Label lblID;
        protected Label lblImgHeight;
        protected Label lblImgWidth;
        protected Label lblIsDisplay;
        protected Label lblLinkDesc;
        protected Label lblLinkUrl;
        protected Label lblName;
        protected Label lblOrderID;
        protected Label lblTelPhone;
        protected Label lblTypeID;
        protected Literal Litera13;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal12;
        protected Literal Literal13;
        protected Literal Literal14;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        public string strid = "";

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("listnew.aspx");
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
            Maticsoft.Model.Settings.FriendlyLink model = new Maticsoft.BLL.Settings.FriendlyLink().GetModel(ID);
            this.lblID.Text = model.ID.ToString();
            this.lblName.Text = model.Name;
            this.imgImgUrl.ImageUrl = model.ImgUrl;
            this.lblImgWidth.Text = model.ImgWidth.ToString();
            this.lblImgHeight.Text = model.ImgWidth.ToString();
            this.lblLinkUrl.Text = model.LinkUrl;
            this.lblLinkDesc.Text = model.LinkDesc;
            this.lblIsDisplay.Text = null;
            if (model.IsDisplay)
            {
                this.lblIsDisplay.Text = SiteSetting.lblYes;
            }
            else
            {
                this.lblIsDisplay.Text = SiteSetting.lblNo;
            }
            this.lblOrderID.Text = model.OrderID.ToString();
            this.lblContactPerson.Text = model.ContactPerson;
            this.lblEmail.Text = model.Email;
            this.lblTelPhone.Text = model.TelPhone;
            this.lblTypeID.Text = null;
            switch (Convert.ToInt32(model.TypeID))
            {
                case 0:
                    this.lblTypeID.Text = SiteSetting.lblImgLink;
                    return;

                case 1:
                    this.lblTypeID.Text = SiteSetting.lblTextLink;
                    return;
            }
            this.lblTypeID.Text = Site.Unknown;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x17e;
            }
        }
    }
}

