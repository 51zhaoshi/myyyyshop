namespace Maticsoft.Web.Admin.SNS.Members
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class UserTags : PageBaseAdmin
    {
        protected int Act_AddData = 0x243;
        protected int Act_DelData = 580;
        private Maticsoft.BLL.SNS.Tags bTags = new Maticsoft.BLL.SNS.Tags();
        private Maticsoft.BLL.SNS.TagType bTagType = new Maticsoft.BLL.SNS.TagType();
        protected Button btnAdd;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Repeater rptTags;
        protected TextBox txtTags;

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtTags.Text))
            {
                Maticsoft.Model.SNS.Tags model = new Maticsoft.Model.SNS.Tags {
                    TypeId = this.bTagType.GetTagsTypeId("用户标签"),
                    TagName = this.txtTags.Text
                };
                this.bTags.Add(model);
                base.Response.Redirect("UserTags.aspx");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Id > 0)
            {
                this.bTags.Delete(this.Id);
            }
            if (!base.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.btnAdd.Visible = false;
                }
                this.rptTags.DataSource = this.bTags.GetList("TypeId=" + this.bTagType.GetTagsTypeId("用户标签"));
                this.rptTags.DataBind();
            }
        }

        protected void rptTags_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                HtmlGenericControl control = (HtmlGenericControl) e.Item.FindControl("lbtnDel");
                if (control != null)
                {
                    control.Visible = false;
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x242;
            }
        }

        public int Id
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["id"]))
                {
                    num = Globals.SafeInt(base.Request.Params["id"], 0);
                }
                return num;
            }
        }
    }
}

