namespace Maticsoft.Web.Ms.Regions
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_DelData = 0x149;
        protected int Act_UpdateData = 0x148;
        protected Button btnSearch;
        protected GridView gridView;
        protected Literal Literal2;
        private Regions Regbll = new Regions();
        protected Maticsoft.Web.Controls.Region Regions1;
        protected ScriptManager ScriptManager1;
        protected TextBox txtKeyword;
        protected UpdatePanel UpdatePanel1;

        public void BindData()
        {
            this.gridView.DataSource = this.Regbll.GetList(this.Regions1.Province_iID);
            this.gridView.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.DataBind();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.BindData();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RegionRec")
            {
                int regionId = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                RegionRec rec = new RegionRec();
                if (rec.AddEx(regionId, 0) > 0)
                {
                    MessageBox.ShowSuccessTip(this, "推荐成功！");
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, "推荐失败！");
                }
            }
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlGenericControl control = (HtmlGenericControl) e.Row.FindControl("linkModify");
                LinkButton button = (LinkButton) e.Row.FindControl("LinkButton1");
                LinkButton button2 = (LinkButton) e.Row.FindControl("LinkRegionRec");
                button.Attributes.Add("onclick", "return confirm(\"删除该数据会影响整站与地区相关的数据，确定要删除吗？\")");
                int num = (int) DataBinder.Eval(e.Row.DataItem, "Depth");
                string str = DataBinder.Eval(e.Row.DataItem, "RegionName").ToString();
                e.Row.Cells[0].CssClass = "productcag" + num.ToString();
                if (1 != num)
                {
                    HtmlGenericControl control2 = e.Row.FindControl("spShowImage") as HtmlGenericControl;
                    control2.Visible = false;
                }
                if (num == 2)
                {
                    button2.Visible = true;
                }
                if (num == 3)
                {
                    button.Visible = true;
                }
                Label label = e.Row.FindControl("lblRegionName") as Label;
                label.Text = str;
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    button.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    control.Visible = false;
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int regionId = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.Regbll.Delete(regionId);
            this.BindData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
                this.Regions1.ProvinceVisible = true;
                this.Regions1.CityVisible = false;
                this.Regions1.AreaVisible = false;
                this.Regions1.VisibleAll = true;
            }
            this.BindData();
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x145;
            }
        }
    }
}

