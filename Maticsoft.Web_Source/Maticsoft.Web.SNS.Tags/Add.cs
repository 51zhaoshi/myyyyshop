namespace Maticsoft.Web.SNS.Tags
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected DropDownList dropTypeId;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtTagName.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "标签的名称不能为空");
            }
            else
            {
                Maticsoft.Model.SNS.Tags model = new Maticsoft.Model.SNS.Tags {
                    TagName = str,
                    TypeId = Globals.SafeInt(this.dropTypeId.SelectedValue, 0),
                    IsRecommand = int.Parse(this.radlIsRecommand.SelectedValue),
                    Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0)
                };
                Maticsoft.BLL.SNS.Tags tags2 = new Maticsoft.BLL.SNS.Tags();
                if (tags2.Add(model) > 0)
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "增加商品标签成功", this);
                    MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "List.aspx");
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipSaveError);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindData();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x257;
            }
        }
    }
}

