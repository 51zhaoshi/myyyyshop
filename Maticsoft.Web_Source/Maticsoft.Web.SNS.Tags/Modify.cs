namespace Maticsoft.Web.SNS.Tags
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.SNS.Tags bll = new Maticsoft.BLL.SNS.Tags();
        protected Button btnCancle;
        protected Button btnSave;
        protected DropDownList dropTypeId;
        protected Label lblTagID;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList radlIsRecommand;
        protected RadioButtonList radlStatus;
        protected TextBox txtTagName;

        public void BindData()
        {
            this.dropTypeId.DataSource = new Maticsoft.BLL.SNS.TagType().GetAllListEX();
            this.dropTypeId.DataTextField = "Name";
            this.dropTypeId.DataValueField = "ID";
            this.dropTypeId.DataBind();
            this.dropTypeId.Items.Insert(0, new ListItem("--请选择--", ""));
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("Tagslist.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtTagName.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "标签的名称不能为空");
            }
            else
            {
                Maticsoft.Model.SNS.Tags model = this.bll.GetModel(this.TagID);
                if (model != null)
                {
                    model.TagID = this.TagID;
                    model.TagName = str;
                    model.TypeId = Globals.SafeInt(this.dropTypeId.SelectedValue, 0);
                    model.IsRecommand = int.Parse(this.radlIsRecommand.SelectedValue);
                    model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                    if (this.bll.Update(model))
                    {
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "修改商品标签(id=" + model.TagID + ")成功", this);
                        MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "List.aspx");
                    }
                    else
                    {
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "修改商品标签(id=" + model.TagID + ")失败", this);
                        MessageBox.ShowSuccessTip(this, Site.TooltipUpdateError);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindData();
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.SNS.Tags model = this.bll.GetModel(this.TagID);
            if (model != null)
            {
                this.lblTagID.Text = model.TagID.ToString();
                this.txtTagName.Text = model.TagName;
                this.dropTypeId.SelectedValue = model.TypeId.ToString();
                this.radlIsRecommand.SelectedValue = model.IsRecommand.ToString();
                this.radlStatus.SelectedValue = model.Status.ToString();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 600;
            }
        }

        protected int TagID
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

