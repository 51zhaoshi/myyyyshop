namespace Maticsoft.Web.Admin.SysManage
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class MultiLanguage : PageBaseAdmin
    {
        private Maticsoft.BLL.SysManage.MultiLanguage bll = new Maticsoft.BLL.SysManage.MultiLanguage();
        protected Button btnSave;
        protected Button btnSearch;
        protected DropDownList dropLanguage;
        protected GridViewEx gridView;
        protected Label lbltip1;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal6;
        protected Literal Literal7;
        protected RangeValidator RangeValidator1;
        protected TextBox txtKeyword;
        protected TextBox txtMultiLang_cField;
        protected TextBox txtMultiLang_cValue;
        protected TextBox txtMultiLang_iPKValue;

        public void BindData()
        {
            string strWhere = "";
            if (this.txtKeyword.Text.Trim() != "")
            {
                strWhere = "MultiLang_cField like '%" + this.txtKeyword.Text.Trim() + "%' or MultiLang_cValue like '%" + this.txtKeyword.Text.Trim() + "%'";
            }
            DataSet list = this.bll.GetList(strWhere);
            this.gridView.DataSetSource = list;
        }

        public void BindLanguage()
        {
            DataSet languageList = this.bll.GetLanguageList();
            this.dropLanguage.DataSource = languageList;
            this.dropLanguage.DataTextField = "Language_cName";
            this.dropLanguage.DataValueField = "Language_cCode";
            this.dropLanguage.DataBind();
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            this.lbltip1.Text = "";
            if (((this.txtMultiLang_cField.Text.Length > 0) && (this.txtMultiLang_cValue.Text.Length > 0)) && (this.txtMultiLang_iPKValue.Text.Length > 0))
            {
                string str = this.txtMultiLang_cField.Text.Trim();
                int num = Convert.ToInt32(this.txtMultiLang_iPKValue.Text.Trim());
                string str2 = this.txtMultiLang_cValue.Text.Trim();
                string selectedValue = this.dropLanguage.SelectedValue;
                if (!this.bll.Exists(str, num, selectedValue))
                {
                    this.bll.Add(str, num, selectedValue, str2);
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("添加多语言数据：【{0}】", this.txtMultiLang_cField.Text.Trim()), this);
                    this.txtMultiLang_cField.Text = "";
                    this.txtMultiLang_iPKValue.Text = "";
                    this.txtMultiLang_cValue.Text = "";
                    this.gridView.OnBind();
                }
                else
                {
                    this.lbltip1.Text = Site.TooltipDataExist;
                }
            }
            else
            {
                this.lbltip1.Text = Site.TooltipNoNull;
            }
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        public void gridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gridView.EditIndex = -1;
            this.gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            DataControlRowType rowType = e.Row.RowType;
        }

        public void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.gridView.DataKeys[e.RowIndex].Value != null)
            {
                int num = Convert.ToInt32(this.gridView.DataKeys[e.RowIndex].Value);
                this.bll.Delete(num);
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除多语言数据", this);
                this.gridView.OnBind();
            }
        }

        public void gridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gridView.EditIndex = e.NewEditIndex;
            this.gridView.OnBind();
        }

        public void gridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string s = this.gridView.DataKeys[e.RowIndex].Values[0].ToString();
            string text = ((TextBox) this.gridView.Rows[e.RowIndex].FindControl("TBDescription")).Text;
            if (text == "")
            {
                MessageBox.ShowSuccessTip(this, Site.TooltipNoNull);
            }
            else
            {
                this.bll.Update(int.Parse(s), text);
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "编辑多语言数据", this);
                this.gridView.EditIndex = -1;
                this.gridView.OnBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindLanguage();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x3f;
            }
        }
    }
}

