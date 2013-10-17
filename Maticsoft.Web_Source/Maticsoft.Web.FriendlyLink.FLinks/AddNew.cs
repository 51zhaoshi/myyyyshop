namespace Maticsoft.Web.FriendlyLink.FLinks
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Model.Settings;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class AddNew : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected DropDownList dropTypeID;
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
        protected Literal Literal9;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string text = this.txtName.Text;
            string str2 = this.txtImgUrl.Text;
            int num = Globals.SafeInt(this.txtImgWidth.Text, 0);
            int num2 = Globals.SafeInt(this.txtImgHeight.Text, 0);
            string str3 = this.txtLinkUrl.Text;
            string str4 = this.txtLinkDesc.Text;
            int num3 = int.Parse(this.txtOrderID.Text);
            string str5 = this.txtContactPerson.Text;
            string str6 = this.txtEmail.Text;
            string str7 = this.txtTelPhone.Text;
            int num4 = int.Parse(this.dropTypeID.SelectedValue);
            bool flag = this.radioBtnYes.Checked;
            Maticsoft.Model.Settings.FriendlyLink model = new Maticsoft.Model.Settings.FriendlyLink {
                Name = text,
                ImgUrl = str2,
                ImgWidth = new int?(num),
                ImgHeight = new int?(num2),
                LinkUrl = str3,
                LinkDesc = str4,
                OrderID = num3,
                ContactPerson = str5,
                Email = str6,
                TelPhone = str7,
                TypeID = num4,
                IsDisplay = flag
            };
            Maticsoft.BLL.Settings.FriendlyLink link2 = new Maticsoft.BLL.Settings.FriendlyLink();
            if (link2.Add(model) > 0)
            {
                MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "ListNew.aspx");
            }
            else
            {
                MessageBox.ShowFailTip(this, Site.TooltipSaveError, "ListNew.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 380;
            }
        }
    }
}

