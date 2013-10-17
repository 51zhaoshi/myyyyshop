namespace Maticsoft.Web.Admin.Shop.Tags
{
    using Maticsoft.BLL.Shop.Tags;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Tags;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Tags.Tags bll = new Maticsoft.BLL.Shop.Tags.Tags();
        private Maticsoft.BLL.Shop.Tags.TagCategories bllCate = new Maticsoft.BLL.Shop.Tags.TagCategories();
        protected Button btnCancle;
        protected Button btnSave;
        protected HiddenField Hidden_SelectName;
        protected HiddenField Hidden_SelectValue;
        protected Label lblTagID;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList radlIsRecommand;
        protected RadioButtonList radlStatus;
        protected TextBox txtCategoryText;
        protected TextBox txtTagName;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtTagName.Text.Trim();
            if (string.IsNullOrWhiteSpace(str))
            {
                MessageBox.ShowServerBusyTip(this, "标签的名称不能为空");
            }
            else
            {
                Maticsoft.Model.Shop.Tags.Tags model = this.bll.GetModel(this.TagID);
                if (model != null)
                {
                    model.TagID = this.TagID;
                    model.TagName = str;
                    model.TagCategoryId = Globals.SafeInt(this.Hidden_SelectValue.Value, 0);
                    model.IsRecommand = bool.Parse(this.radlIsRecommand.SelectedValue);
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
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.Shop.Tags.Tags model = this.bll.GetModel(this.TagID);
            int iD = 0;
            if (model != null)
            {
                this.lblTagID.Text = model.TagID.ToString();
                this.txtTagName.Text = model.TagName;
                this.Hidden_SelectValue.Value = model.TagCategoryId.ToString();
                iD = Globals.SafeInt(model.TagCategoryId.ToString(), 0);
                if (iD > 0)
                {
                    this.txtCategoryText.Text = this.bllCate.GetModel(iD).CategoryName.ToString();
                }
                if (model.IsRecommand)
                {
                    this.radlIsRecommand.SelectedValue = "true";
                }
                else
                {
                    this.radlIsRecommand.SelectedValue = "false";
                }
                this.radlStatus.SelectedValue = model.Status.ToString();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 550;
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

