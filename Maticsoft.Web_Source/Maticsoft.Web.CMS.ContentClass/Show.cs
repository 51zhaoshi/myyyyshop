namespace Maticsoft.Web.CMS.ContentClass
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.ClassType bllClassType = new Maticsoft.BLL.CMS.ClassType();
        private Maticsoft.BLL.CMS.ContentClass bllContentClass = new Maticsoft.BLL.CMS.ContentClass();
        protected Button btnCancle;
        protected CheckBox chkAllowAddContent;
        protected CheckBox chkAllowSubclass;
        protected Image imgUrl;
        protected Label lblClassID;
        protected Label lblClassIndex;
        protected Label lblClassModel;
        protected Label lblClassName;
        protected Label lblClassTypeID;
        protected Label lblCreatedDate;
        protected Label lblCreatedUserID;
        protected Label lblDescription;
        protected Label lblKeywords;
        protected Label lblOrders;
        protected Label lblPageModelName;
        protected Label lblParentId;
        protected Label lblRemark;
        protected Label lblState;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal12;
        protected Literal Literal13;
        protected Literal Literal14;
        protected Literal Literal15;
        protected Literal Literal16;
        protected Literal Literal17;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.ClassID > 0)
                {
                    this.ShowInfo();
                }
                else
                {
                    MessageBox.ShowServerBusyTip(this, CMS.ContentErrorNoContent, "List.aspx");
                }
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.CMS.ContentClass model = this.bllContentClass.GetModel(this.ClassID);
            if (model != null)
            {
                this.lblClassID.Text = model.ClassID.ToString();
                this.lblClassName.Text = model.ClassName;
                this.lblClassIndex.Text = model.ClassIndex;
                this.lblOrders.Text = model.Sequence.ToString();
                if (model.ParentId.HasValue)
                {
                    this.lblParentId.Text = model.ParentId.ToString();
                }
                this.lblState.Text = model.State.ToString();
                this.chkAllowSubclass.Checked = model.AllowSubclass;
                this.chkAllowAddContent.Checked = model.AllowAddContent;
                this.imgUrl.ImageUrl = model.ImageUrl;
                this.lblDescription.Text = model.Description;
                this.lblKeywords.Text = model.Keywords;
                this.lblClassModel.Text = model.ClassModel.ToString();
                this.lblPageModelName.Text = model.PageModelName;
                this.lblCreatedDate.Text = model.CreatedDate.ToString();
                this.lblCreatedUserID.Text = model.CreatedUserID.ToString();
                this.lblRemark.Text = model.Remark;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xdf;
            }
        }

        public int ClassID
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(str) && PageValidate.IsNumber(str))
                {
                    num = int.Parse(str);
                }
                return num;
            }
        }
    }
}

