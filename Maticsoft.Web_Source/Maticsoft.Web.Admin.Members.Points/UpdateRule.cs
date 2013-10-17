namespace Maticsoft.Web.Admin.Members.Points
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using System;
    using System.Drawing;
    using System.Web.UI.WebControls;

    public class UpdateRule : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected DropDownList DropLimitName;
        protected Label lblMsg;
        private Maticsoft.BLL.Members.PointsLimit LimitBll = new Maticsoft.BLL.Members.PointsLimit();
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected RangeValidator RangeValidator1;
        protected RequiredFieldValidator RequiredFieldValidator2;
        private Maticsoft.BLL.Members.PointsRule RuleBll = new Maticsoft.BLL.Members.PointsRule();
        protected TextBox tDesc;
        protected TextBox tName;
        protected TextBox tScore;
        protected Literal tType;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("PointsRule.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Members.PointsRule model = this.RuleBll.GetModel(this.tType.Text);
            model.Name = this.tName.Text.Trim();
            model.PointsLimitID = Convert.ToInt32(this.DropLimitName.SelectedValue);
            model.Description = this.tDesc.Text.Trim();
            if (PageValidate.IsNumber(this.tScore.Text.Trim()))
            {
                model.Score = Globals.SafeInt(this.tScore.Text.Trim(), 0);
            }
            if (this.RuleBll.Update(model))
            {
                base.Response.Redirect("PointsRule.aspx");
            }
            else
            {
                this.lblMsg.ForeColor = Color.Red;
                this.lblMsg.Text = "修改规则出错！";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["Action"] != null)) && (base.Request.Params["Action"].ToString() != ""))
            {
                string ruleAction = base.Request.Params["Action"];
                Maticsoft.Model.Members.PointsRule model = this.RuleBll.GetModel(ruleAction);
                if (model == null)
                {
                    base.Response.Redirect("PointsRule.aspx");
                }
                this.DropLimitName.DataSource = this.LimitBll.GetAllList();
                this.DropLimitName.DataTextField = "Name";
                this.DropLimitName.DataValueField = "PointsLimitID";
                this.DropLimitName.DataBind();
                this.DropLimitName.Items.Insert(0, new ListItem("无限制", "-1"));
                this.tType.Text = model.RuleAction;
                this.tName.Text = model.Name;
                this.tScore.Text = model.Score.ToString();
                this.tDesc.Text = model.Description;
                this.DropLimitName.SelectedValue = model.PointsLimitID.ToString();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x123;
            }
        }
    }
}

