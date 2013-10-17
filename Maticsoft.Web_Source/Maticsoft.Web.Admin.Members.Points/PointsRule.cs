namespace Maticsoft.Web.Admin.Members.Points
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class PointsRule : PageBaseAdmin
    {
        protected int Act_AddData = 0x124;
        protected int Act_DelData = 0x126;
        protected int Act_UpdateData = 0x125;
        protected Button Button1;
        protected GridViewEx gridView;
        protected Label Label1;
        protected HtmlGenericControl liadd;
        private Maticsoft.BLL.Members.PointsLimit limitBll = new Maticsoft.BLL.Members.PointsLimit();
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        private Maticsoft.BLL.Members.PointsRule RuleBll = new Maticsoft.BLL.Members.PointsRule();
        protected TextBox txtKeyword;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[5].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[6].Visible = false;
            }
            string text = this.txtKeyword.Text;
            DataSet listByKeyWord = this.RuleBll.GetListByKeyWord(text);
            if (listByKeyWord != null)
            {
                this.gridView.DataSetSource = listByKeyWord;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        public string GetLimitName(int limitId)
        {
            if (limitId != -1)
            {
                Maticsoft.Model.Members.PointsLimit model = this.limitBll.GetModel(limitId);
                if (model != null)
                {
                    return model.Name;
                }
            }
            return "无限制";
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowIndex % 2) == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ruleAction = this.gridView.DataKeys[e.RowIndex].Value.ToString();
            this.RuleBll.Delete(ruleAction);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.liadd.Visible = false;
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x121;
            }
        }
    }
}

