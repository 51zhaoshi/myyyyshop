namespace Maticsoft.Web.Admin.SNS.StarType
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Label lblCheckRule;
        protected Label lblRemark;
        protected Label lblStatus;
        protected Label lblTypeID;
        protected Label lblTypeName;
        public string strid = "";

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("StarTypeList.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].Trim() != ""))
            {
                this.strid = base.Request.Params["id"];
                int typeID = Convert.ToInt32(this.strid);
                this.ShowInfo(typeID);
            }
        }

        private void ShowInfo(int TypeID)
        {
            Maticsoft.Model.SNS.StarType model = new Maticsoft.BLL.SNS.StarType().GetModel(TypeID);
            this.lblTypeID.Text = model.TypeID.ToString();
            this.lblTypeName.Text = model.TypeName;
            this.lblCheckRule.Text = model.CheckRule;
            this.lblRemark.Text = model.Remark;
            this.lblStatus.Text = model.Status.ToString();
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x268;
            }
        }
    }
}

