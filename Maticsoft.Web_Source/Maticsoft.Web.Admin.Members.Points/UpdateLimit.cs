namespace Maticsoft.Web.Admin.Members.Points
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using System;
    using System.Drawing;
    using System.Web.UI.WebControls;

    public class UpdateLimit : PageBaseAdmin
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if ((base.Request.Params["limitId"] != null) && (base.Request.Params["limitId"].ToString() != ""))
            {
                int pointsLimitID = Globals.SafeInt(base.Request.Params["limitId"], 0);
                Maticsoft.Model.Members.PointsLimit model = this.LimitBll.GetModel(pointsLimitID);
                if (this.LimitBll.Exists(this.tName.Text.Trim(), model.PointsLimitID))
                {
                    MessageBox.ShowSuccessTip(this, "已存在该规则限制名称，请重新填写");
                }
                else
                {
                    model.Name = this.tName.Text.Trim();
                    if (PageValidate.IsNumber(this.tCycle.Text.Trim()))
                    {
                        model.Cycle = Globals.SafeInt(this.tCycle.Text.Trim(), 0);
                    }
                    if (PageValidate.IsNumber(this.tMaxTimes.Text.Trim()))
                    {
                        model.MaxTimes = Globals.SafeInt(this.tMaxTimes.Text.Trim(), 0);
                    }
                    model.CycleUnit = this.DropCycleUnit.SelectedValue;
                    if (this.LimitBll.Update(model))
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["limitId"] != null)) && (base.Request.Params["limitId"].ToString() != ""))
            {
                int pointsLimitID = Globals.SafeInt(base.Request.Params["limitId"], 0);
                Maticsoft.Model.Members.PointsLimit model = this.LimitBll.GetModel(pointsLimitID);
                if (model == null)
                {
                    base.Response.Redirect("PointsLimit.aspx");
                }
                this.tName.Text = model.Name;
                this.tCycle.Text = model.Cycle.ToString();
                this.tMaxTimes.Text = model.MaxTimes.ToString();
                this.DropCycleUnit.SelectedValue = model.CycleUnit.ToString();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x129;
            }
        }
    }
}

