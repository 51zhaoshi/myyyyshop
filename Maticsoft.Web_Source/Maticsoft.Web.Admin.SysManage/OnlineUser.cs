namespace Maticsoft.Web.Admin.SysManage
{
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class OnlineUser : PageBaseAdmin
    {
        protected GridViewEx gridView1;
        protected Literal Literal1;
        protected Literal Literal2;

        public void BindData()
        {
            DataTable table = (DataTable) base.Application["OnlineUsers"];
            this.gridView1.DataSource = table;
            this.gridView1.DataBind();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView1.PageIndex = e.NewPageIndex;
            this.gridView1.DataBind();
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

        private void Page_Load(object sender, EventArgs e)
        {
            if ((!base.IsPostBack && (this.Session["Style"] != null)) && (this.Session["Style"].ToString() != ""))
            {
                string str = this.Session["Style"] + "xtable_bordercolorlight";
                if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                {
                    this.gridView1.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    this.gridView1.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    this.gridView1.OnBind();
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x4f;
            }
        }
    }
}

