namespace Maticsoft.Web.Admin.Members.Points
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using System;
    using System.Drawing;
    using System.Web.UI.WebControls;

    public class AddLimit : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected DropDownList DropCycleUnit;
        protected Label lblMsg;
        private Maticsoft.BLL.Members.PointsLimit LimitBll = new Maticsoft.BLL.Members.PointsLimit();
        protected Literal Literal1;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected RangeValidator RangeValidator1;
        protected RangeValidator RangeValidator2;
        protected RequiredFieldValidator RequiredFieldValidator2;
        protected TextBox tCycle;
        protected TextBox tMaxTimes;
        protected TextBox tName;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("PointsLimit.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (this.LimitBll.Exists(this.tName.Text.Trim()))
            {
                MessageBox.ShowSuccessTip(this, "已存在该规则限制名称，请重新填写");
            }
            else
            {
                Maticsoft.Model.Members.PointsLimit model = new Maticsoft.Model.Members.PointsLimit {
                    Name = this.tName.Text.Trim()
                };
                if (PageValidate.IsNumber(this.tCycle.Text.Trim()))
                {
                    model.Cycle = Globals.SafeInt(this.tCycle.Text.Trim(), 0);
                }
                if (PageValidate.IsNumber(this.tMaxTimes.Text.Trim()))
                {
                    model.MaxTimes = Globals.SafeInt(this.tMaxTimes.Text.Trim(), 0);
                }
                model.CycleUnit = this.DropCycleUnit.SelectedValue;
                if (this.LimitBll.Add(model) > 0)
                {
                    base.Response.Redirect("PointsLimit.aspx");
                }
                else
                {
                    this.lblMsg.ForeColor = Color.Red;
                    this.lblMsg.Text = "添加条件限制出错！";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.DropCycleUnit.DataBind();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x128;
            }
        }
    }
}

