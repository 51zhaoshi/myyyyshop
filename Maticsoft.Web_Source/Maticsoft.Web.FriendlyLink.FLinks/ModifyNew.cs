namespace Maticsoft.Web.FriendlyLink.FLinks
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Model.Settings;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class ModifyNew : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected DropDownList dropTypeID;
        protected Image imgImgUrl;
        protected Label lblID;
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
        protected RadioButton radioBtnNo;
        protected RadioButton radioBtnYes;
        protected RegularExpressionValidator RegularExpressionValidator2;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected RequiredFieldValidator RequiredFieldValidator3;
        protected RequiredFieldValidator RequiredFieldValidator5;
        protected TextBox txtContactPerson;
        protected TextBox txtEmail;
        protected TextBox txtImgHeight;
        protected TextBox txtImgUrl;
        protected TextBox txtImgWidth;
        protected TextBox txtLinkDesc;
        protected TextBox txtLinkUrl;
        protected TextBox txtName;
        protected TextBox txtOrderID;
        protected TextBox txtTelPhone;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("listnew.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            int num = int.Parse(this.dropTypeID.SelectedValue);
            if (num == 1)
            {
                this.txtImgUrl.Text = "";
                this.txtImgWidth.Text = "";
                this.txtImgHeight.Text = "";
            }
            int num2 = int.Parse(this.lblID.Text);
            string text = this.txtName.Text;
            string str2 = this.txtImgUrl.Text;
            int num3 = Globals.SafeInt(this.txtImgWidth.Text, 0);
            int num4 = Globals.SafeInt(this.txtImgHeight.Text, 0);
            string str3 = this.txtLinkUrl.Text;
            string str4 = this.txtLinkDesc.Text;
            bool flag = this.radioBtnYes.Checked;
            int num5 = int.Parse(this.txtOrderID.Text);
            string str5 = this.txtContactPerson.Text;
            string str6 = this.txtEmail.Text;
            string str7 = this.txtTelPhone.Text;
            Maticsoft.Model.Settings.FriendlyLink model = new Maticsoft.Model.Settings.FriendlyLink {
                ID = num2,
                Name = text,
                ImgUrl = str2,
                ImgWidth = new int?(num3),
                ImgHeight = new int?(num4),
                LinkUrl = str3,
                LinkDesc = str4,
                IsDisplay = flag,
                OrderID = num5,
                ContactPerson = str5,
                Email = str6,
                TelPhone = str7,
                TypeID = num
            };
            Maticsoft.BLL.Settings.FriendlyLink link2 = new Maticsoft.BLL.Settings.FriendlyLink();
            if (link2.Update(model))
            {
                MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "listNew.aspx");
            }
            else
            {
                MessageBox.ShowFailTip(this, Site.TooltipSaveError, "ListNew.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].Trim() != ""))
            {
                int iD = Convert.ToInt32(base.Request.Params["id"]);
                this.ShowInfo(iD);
            }
        }

        private void ShowInfo(int ID)
        {
            Maticsoft.Model.Settings.FriendlyLink model = new Maticsoft.BLL.Settings.FriendlyLink().GetModel(ID);
            this.lblID.Text = model.ID.ToString();
            this.txtName.Text = model.Name;
            this.txtImgUrl.Text = model.ImgUrl;
            this.txtImgWidth.Text = model.ImgWidth.ToString();
            this.txtImgHeight.Text = model.ImgHeight.ToString();
            this.imgImgUrl.ImageUrl = model.ImgUrl;
            this.txtLinkUrl.Text = model.LinkUrl;
            this.txtLinkDesc.Text = model.LinkDesc;
            this.radioBtnYes.Checked = model.IsDisplay;
            this.txtOrderID.Text = model.OrderID.ToString();
            this.txtContactPerson.Text = model.ContactPerson;
            this.txtEmail.Text = model.Email;
            this.txtTelPhone.Text = model.TelPhone;
            this.dropTypeID.SelectedValue = model.TypeID.ToString();
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x17d;
            }
        }
    }
}

