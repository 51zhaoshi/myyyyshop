namespace Maticsoft.Web.Admin
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class MLShow : PageBaseAdmin
    {
        private MultiLanguage bllML = new MultiLanguage();
        protected Button btnAddMValue;
        protected DropDownList dropLanguage;
        protected HtmlForm form1;
        protected GridViewEx gridView;
        protected Label lblF;
        protected Label lblK;
        protected Label lblML;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected TextBox txtMValue;

        public void BindData()
        {
            this.gridView.DataSetSource = this.bllML.GetLangListByValue(this.lblF.Text, Globals.SafeInt(this.lblK.Text, 0));
        }

        public void BindLanguage()
        {
            this.dropLanguage.DataSource = this.bllML.GetLanguageListByCache();
            this.dropLanguage.DataTextField = "Language_cName";
            this.dropLanguage.DataValueField = "Language_cCode";
            this.dropLanguage.DataBind();
        }

        public void btnAddMValue_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtMValue.Text.Length > 0)
                {
                    if (this.bllML.Exists(this.lblF.Text, Globals.SafeInt(this.lblK.Text, 0), this.dropLanguage.SelectedValue))
                    {
                        this.lblML.Text = Site.TooltipDataExist;
                    }
                    else
                    {
                        this.bllML.Add(this.lblF.Text, Globals.SafeInt(this.lblK.Text, 0), this.dropLanguage.SelectedValue, this.txtMValue.Text);
                        this.txtMValue.Text = "";
                        this.gridView.OnBind();
                    }
                }
            }
            catch
            {
                this.lblML.Text = Site.TooltipSaveError;
            }
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
                object obj2 = DataBinder.Eval(e.Row.DataItem, "MultiLang_cLang");
                if ((obj2 != null) && (obj2.ToString() != ""))
                {
                    e.Row.Cells[0].Text = this.bllML.GetLanguageNameByCache(obj2.ToString());
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.bllML.Delete((int) this.gridView.DataKeys[e.RowIndex].Value);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if ((this.Session["Style"] != null) && (this.Session["Style"].ToString() != ""))
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
                this.BindLanguage();
                string str2 = base.Request.Params["f"];
                string str3 = base.Request.Params["k"];
                if (!string.IsNullOrWhiteSpace(str2))
                {
                    this.lblF.Text = base.Request.Params["f"];
                    if (!string.IsNullOrWhiteSpace(str3))
                    {
                        this.lblK.Text = str3;
                        this.gridView.OnBind();
                    }
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}

