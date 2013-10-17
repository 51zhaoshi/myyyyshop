namespace Maticsoft.Web.SNS.TagType
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.SNS.TagType bll = new Maticsoft.BLL.SNS.TagType();
        protected Button btnCancle;
        protected Button btnSave;
        protected SNSCategoryDropList dropCid;
        protected Label lblID;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList radlStatus;
        protected TextBox txtRemark;
        protected TextBox txtTypeName;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("TagTypelist.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtTypeName.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "类型名称不能为空！");
            }
            else if (str.Length > 50)
            {
                MessageBox.ShowServerBusyTip(this, "类型名称不能大于50个字符！");
            }
            else
            {
                string str2 = this.txtRemark.Text.Trim();
                if (str2.Length > 100)
                {
                    MessageBox.ShowServerBusyTip(this, "备注不能大于100个字符！");
                }
                else
                {
                    Maticsoft.Model.SNS.TagType model = this.bll.GetModel(this.Id);
                    if (model != null)
                    {
                        model.TypeName = str;
                        model.Remark = str2;
                        model.Cid = new int?(Globals.SafeInt(this.dropCid.SelectedValue, 0));
                        model.Status = new int?(Globals.SafeInt(this.radlStatus.SelectedValue, 0));
                        if (this.bll.Update(model))
                        {
                            MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "List.aspx");
                        }
                        else
                        {
                            MessageBox.ShowFailTip(this, Site.TooltipUpdateError);
                        }
                    }
                }
            }
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
            Maticsoft.Model.SNS.TagType model = this.bll.GetModel(this.Id);
            if (model != null)
            {
                this.lblID.Text = model.ID.ToString();
                this.txtTypeName.Text = model.TypeName;
                this.txtRemark.Text = model.Remark;
                if (model.Cid.HasValue)
                {
                    this.dropCid.SelectedValue = model.Cid.ToString();
                }
                if (model.Status.HasValue)
                {
                    this.radlStatus.SelectedValue = model.Status.ToString();
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x256;
            }
        }

        protected int Id
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    num = Globals.SafeInt(str, 0);
                }
                return num;
            }
        }
    }
}

