namespace Maticsoft.Web.Admin.Members.Points
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using System;
    using System.Drawing;
    using System.Web.UI.WebControls;

    public class AddRule : PageBaseAdmin
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
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected RequiredFieldValidator RequiredFieldValidator2;
        private Maticsoft.BLL.Members.PointsRule RuleBll = new Maticsoft.BLL.Members.PointsRule();
        protected TextBox tDesc;
        protected TextBox tName;
        protected TextBox tScore;
        protected TextBox tType;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("PointsRule.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (this.RuleBll.Exists(this.tType.Text.Trim()))
            {
                MessageBox.ShowSuccessTip(this, "已存在该规则码，请重新填写");
            }
            else
            {
                Maticsoft.Model.Members.PointsRule model = new Maticsoft.Model.Members.PointsRule {
                    RuleAction = this.tType.Text.Trim(),
                    Name = this.tName.Text.Trim(),
                    PointsLimitID = Convert.ToInt32(this.DropLimitName.SelectedValue),
                    Description = this.tDesc.Text.Trim()
                };
                if (PageValidate.IsNumber(this.tScore.Text.Trim()))
                {
                    model.Score = Globals.SafeInt(this.tScore.Text.Trim(), 0);
                }
                if (this.RuleBll.Add(model))
                {
                    base.Response.Redirect("PointsRule.aspx");
                }
                else
                {
                    this.lblMsg.ForeColor = Color.Red;
                    this.lblMsg.Text = "添加规则出错！";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.DropLimitName.DataSource = this.LimitBll.GetAllList();
                this.DropLimitName.DataTextField = "Name";
                this.DropLimitName.DataValueField = "PointsLimitID";
                this.DropLimitName.DataBind();
                this.DropLimitName.Items.Insert(0, new ListItem("无限制", "-1"));
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 290;
            }
        }
    }
}

