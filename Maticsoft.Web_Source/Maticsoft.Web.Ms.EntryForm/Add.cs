namespace Maticsoft.Web.Ms.EntryForm
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected DropDownList dropAge;
        protected Region dropProvince;
        protected DropDownList dropSex;
        protected DropDownList dropState;
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
        protected TextBox txtCompanyAddress;
        protected TextBox txtDescription;
        protected TextBox txtEmail;
        protected TextBox txtHouseAddress;
        protected TextBox txtMSN;
        protected TextBox txtPhone;
        protected TextBox txtQQ;
        protected TextBox txtRemark;
        protected TextBox txtTelPhone;
        protected TextBox txtUserName;

        protected void BindData()
        {
            for (int i = 0; i <= 0x77; i++)
            {
                this.dropAge.Items.Add(i.ToString());
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtUserName.Text.Trim();
            string str2 = this.txtRemark.Text.Trim();
            if (string.IsNullOrWhiteSpace(str))
            {
                MessageBox.ShowFailTip(this, MsEntryForm.ErrorNameNotNull);
            }
            else if (str2.Length > 300)
            {
                MessageBox.ShowFailTip(this, MsEntryForm.ErrorRemarkoverlength);
            }
            else
            {
                Maticsoft.Model.Ms.EntryForm model = new Maticsoft.Model.Ms.EntryForm {
                    UserName = this.txtUserName.Text.Trim(),
                    Age = new int?(int.Parse(this.dropAge.SelectedValue)),
                    Email = this.txtEmail.Text.Trim(),
                    TelPhone = this.txtTelPhone.Text.Trim(),
                    Phone = this.txtPhone.Text.Trim(),
                    QQ = this.txtQQ.Text.Trim(),
                    MSN = this.txtMSN.Text.Trim(),
                    HouseAddress = this.txtHouseAddress.Text.Trim(),
                    CompanyAddress = this.txtCompanyAddress.Text.Trim(),
                    RegionId = new int?(Convert.ToInt32(this.dropProvince.Area_iID)),
                    Sex = this.dropSex.SelectedValue,
                    Description = this.txtDescription.Text.Trim(),
                    Remark = str2,
                    State = new int?(int.Parse(this.dropState.SelectedValue))
                };
                Maticsoft.BLL.Ms.EntryForm form2 = new Maticsoft.BLL.Ms.EntryForm();
                if (form2.Add(model) > 0)
                {
                    this.btnCancle.Enabled = false;
                    this.btnSave.Enabled = false;
                    MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "list.aspx");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x114;
            }
        }
    }
}

