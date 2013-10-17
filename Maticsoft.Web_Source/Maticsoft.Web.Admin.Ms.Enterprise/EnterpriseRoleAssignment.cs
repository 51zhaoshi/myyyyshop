namespace Maticsoft.Web.Admin.Ms.Enterprise
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class EnterpriseRoleAssignment : PageBaseAdmin
    {
        private Enterprise bll = new Enterprise();
        protected Button btnSearch;
        protected DropDownList dropState;
        protected GridViewEx gridView;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected TextBox txtKeyword;

        public void BindData()
        {
            StringBuilder builder = new StringBuilder();
            if (this.txtKeyword.Text.Trim() != "")
            {
                builder.AppendFormat("Name like '%{0}%' ", this.txtKeyword.Text.Trim());
            }
            string selectedValue = this.dropState.SelectedValue;
            if (PageValidate.IsNumber(selectedValue) && (selectedValue != "-1"))
            {
                if (builder.ToString().Length > 0)
                {
                    builder.Append(" and Status=" + this.dropState.SelectedValue);
                }
                else
                {
                    builder.Append(" Status=" + this.dropState.SelectedValue);
                }
            }
            this.gridView.DataSource = this.bll.GetList(builder.ToString());
            this.gridView.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        public string GetStatus(object target)
        {
            string str2;
            string parameterError = Site.ParameterError;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString()) == null))
            {
                return parameterError;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    if (str2 != "2")
                    {
                        return parameterError;
                    }
                    return Site.Freeze;
                }
            }
            else
            {
                return Site.Unaudited;
            }
            return Site.Normal;
        }

        protected void gridView_OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
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

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.BindData();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (this.Session["Style"] != null)) && (this.Session["Style"].ToString() != ""))
            {
                string str = this.Session["Style"] + "xtable_bordercolorlight";
                if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                {
                    this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x141;
            }
        }
    }
}

