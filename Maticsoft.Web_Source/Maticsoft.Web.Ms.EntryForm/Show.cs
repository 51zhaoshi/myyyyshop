namespace Maticsoft.Web.Ms.EntryForm
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnEdit;
        protected Label lblAge;
        protected Label lblCompanyAddress;
        protected Label lblDescription;
        protected Label lblEmail;
        protected Label lblHouseAddress;
        protected Label lblId;
        protected Label lblMSN;
        protected Label lblPhone;
        protected Label lblQQ;
        protected Label lblRegionId;
        protected Label lblremark;
        protected Label lblSex;
        protected Label lblState;
        protected Label lblTelPhone;
        protected Label lblUserName;
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
        public string strid = "";

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("Modify.aspx?id=" + this.Id);
        }

        public string GetRegionNameByRID(int RID)
        {
            Maticsoft.BLL.Ms.Regions regions = new Maticsoft.BLL.Ms.Regions();
            return regions.GetRegionNameByRID(RID);
        }

        public string GetSex(object target)
        {
            string str2;
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString().Trim()) == null))
            {
                return str;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    return str;
                }
            }
            else
            {
                return Site.SexMale;
            }
            return Site.SexWoman;
        }

        public string GetState(object target)
        {
            string str2;
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString()) == null))
            {
                return str;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    return str;
                }
            }
            else
            {
                return Site.Untreated;
            }
            return Site.Processed;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.Ms.EntryForm model = new Maticsoft.BLL.Ms.EntryForm().GetModel(this.Id);
            if (model != null)
            {
                this.lblId.Text = model.Id.ToString();
                this.lblUserName.Text = model.UserName;
                if (model.Age.HasValue)
                {
                    this.lblAge.Text = model.Age.ToString();
                }
                this.lblEmail.Text = model.Email;
                this.lblTelPhone.Text = model.TelPhone;
                this.lblPhone.Text = model.Phone;
                this.lblQQ.Text = model.QQ;
                this.lblMSN.Text = model.MSN;
                this.lblHouseAddress.Text = model.HouseAddress;
                this.lblCompanyAddress.Text = model.CompanyAddress;
                if (model.RegionId.HasValue)
                {
                    this.lblRegionId.Text = this.GetRegionNameByRID(model.RegionId.Value);
                }
                this.lblSex.Text = this.GetSex(model.Sex);
                this.lblDescription.Text = model.Description;
                this.lblremark.Text = model.Remark;
                if (model.State.HasValue)
                {
                    this.lblState.Text = this.GetState(model.State);
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x116;
            }
        }

        public int Id
        {
            get
            {
                int num = 0;
                this.strid = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(this.strid) && PageValidate.IsNumber(this.strid))
                {
                    num = int.Parse(this.strid);
                }
                return num;
            }
        }
    }
}

