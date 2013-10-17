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

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.Ms.EntryForm bll = new Maticsoft.BLL.Ms.EntryForm();
        protected Button btnCancle;
        protected Button btnSave;
        protected DropDownList dropAge;
        protected Region dropProvince;
        protected DropDownList dropSex;
        protected DropDownList dropState;
        protected Label lblId;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal12;
        protected Literal Literal13;
        protected Literal Literal14;
        protected Literal Literal15;
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

        public void btnSave_Click(object sender, EventArgs e)
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
                int id = int.Parse(this.lblId.Text);
                Maticsoft.Model.Ms.EntryForm model = this.bll.GetModel(id);
                model.UserName = str;
                model.Age = new int?(int.Parse(this.dropAge.SelectedValue));
                model.Email = this.txtEmail.Text;
                model.TelPhone = this.txtTelPhone.Text;
                model.Phone = this.txtPhone.Text;
                model.QQ = this.txtQQ.Text;
                model.MSN = this.txtMSN.Text;
                model.HouseAddress = this.txtHouseAddress.Text;
                model.CompanyAddress = this.txtCompanyAddress.Text;
                model.RegionId = new int?(Convert.ToInt32(this.dropProvince.Area_iID));
                model.Sex = this.dropSex.SelectedValue;
                model.Description = this.txtDescription.Text;
                model.Remark = str2;
                model.State = new int?(int.Parse(this.dropState.SelectedValue));
                if (this.bll.Update(model))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "list.aspx");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string str = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    this.ShowInfo(int.Parse(str));
                }
            }
        }

        private void ShowInfo(int Id)
        {
            Maticsoft.Model.Ms.EntryForm model = this.bll.GetModel(Id);
            if (model != null)
            {
                this.lblId.Text = model.Id.ToString();
                this.txtUserName.Text = model.UserName;
                this.BindData();
                if (model.Age.HasValue)
                {
                    this.dropAge.SelectedValue = model.Age.ToString();
                }
                this.txtEmail.Text = model.Email;
                this.txtTelPhone.Text = model.TelPhone;
                this.txtPhone.Text = model.Phone;
                this.txtQQ.Text = model.QQ;
                this.txtMSN.Text = model.MSN;
                this.txtHouseAddress.Text = model.HouseAddress;
                this.txtCompanyAddress.Text = model.CompanyAddress;
                if (model.RegionId.HasValue)
                {
                    this.dropProvince.Area_iID = model.RegionId.Value;
                }
                this.dropSex.SelectedValue = model.Sex.Trim();
                this.txtDescription.Text = model.Description;
                this.txtRemark.Text = model.Remark;
                if (model.State.HasValue)
                {
                    this.dropState.SelectedValue = model.State.ToString();
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x115;
            }
        }
    }
}

