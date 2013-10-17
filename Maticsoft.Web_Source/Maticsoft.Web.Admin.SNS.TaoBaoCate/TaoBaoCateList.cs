namespace Maticsoft.Web.Admin.SNS.TaoBaoCate
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class TaoBaoCateList : PageBaseAdmin
    {
        private Maticsoft.BLL.SNS.CategorySource bll = new Maticsoft.BLL.SNS.CategorySource();
        protected Button btnBatch;
        protected Button btnTaoBaoShow;
        protected Button btnUpdateTaoBaoCate;
        protected GridViewEx gridView;
        protected Literal Literal1;
        protected Literal Literal11;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList radlState;
        protected Maticsoft.Web.Controls.SNSCategoryDropList SNSCategoryDropList;
        protected TaoBaoCategoryDropList TaoBaoCate;
        protected TextBox txtKeyword;

        public void BindData()
        {
            DataSet categoryList = new DataSet();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" ParentID=" + Globals.SafeInt(this.TaoBaoCate.SelectedValue, 0), new object[0]);
            if (this.txtKeyword.Text.Trim() != "")
            {
                builder.AppendFormat(" and Name like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            categoryList = this.bll.GetCategoryList(builder.ToString());
            this.gridView.DataSource = categoryList;
            this.gridView.DataBind();
        }

        protected void btnBatch_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                int sNSCateId = Globals.SafeInt(this.SNSCategoryDropList.SelectedValue, 0);
                bool isLoop = Globals.SafeBool(this.radlState.SelectedValue, false);
                if (this.bll.UpdateSNSCateList(selIDlist, sNSCateId, isLoop))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量对应淘宝分类(id=" + sNSCateId + ")成功成功", this);
                    MessageBox.ShowSuccessTip(this, "分类批量对应成功");
                    base.Response.Redirect("TaoBaoCateList.aspx");
                }
                else
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量对应淘宝分类(id=" + sNSCateId + ")失败成功", this);
                    MessageBox.ShowFailTip(this, "分类批量对应失败！");
                }
            }
        }

        protected void btnTaoBaoShow_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void btnUpdateTaoBaoCate_Click(object sender, EventArgs e)
        {
            int addCount = 0;
            int updateCount = 0;
            this.bll.ResetCategory(out addCount, out updateCount);
            LogHelp.AddUserLog(base.CurrentUser.UserName, "更新淘宝分类", string.Concat(new object[] { "更新淘宝分类成功，新增了【", addCount, "】条数据，更新了【", updateCount, "】条数据" }), this);
            MessageBox.ShowSuccessTip(this, string.Concat(new object[] { "更新淘宝分类成功，新增了【", addCount, "】条数据，更新了【", updateCount, "】条数据" }), "TaoBaoCateList.aspx");
        }

        private void DoCallback()
        {
            string str3;
            string str = base.Request.Form["Action"];
            base.Response.Clear();
            base.Response.ContentType = "application/json";
            string s = string.Empty;
            if (((str3 = str) != null) && (str3 == "GetTaoBaoCateList"))
            {
                this.GetTaoBaoCateList();
            }
            base.Response.Write(s);
            base.Response.End();
        }

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.gridView.Rows[i].FindControl(this.gridView.CheckBoxID);
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    if (this.gridView.DataKeys[i].Value != null)
                    {
                        str = str + this.gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (flag)
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
        }

        protected string GetSNSCateName(object target)
        {
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            Maticsoft.BLL.SNS.Categories categories = new Maticsoft.BLL.SNS.Categories();
            int categoryId = Globals.SafeInt(target.ToString(), 0);
            Maticsoft.Model.SNS.Categories model = categories.GetModel(categoryId);
            if (model != null)
            {
                return model.Name;
            }
            return "";
        }

        protected void GetTaoBaoCateList()
        {
            JsonObject obj2 = new JsonObject();
            int addCount = 0;
            int updateCount = 0;
            this.bll.ResetCategory(out addCount, out updateCount);
            obj2.Put("DATA", addCount + "|" + updateCount);
            obj2.Put("STATUS", "SUCCESS");
            base.Response.Write(obj2.ToString());
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.DataBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow) ((Control) e.CommandSource).NamingContainer).RowIndex;
            int num1 = (int) this.gridView.DataKeys[rowIndex].Value;
            this.BindData();
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
                LinkButton button1 = (LinkButton) e.Row.FindControl("LinkButton1");
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int categoryId = (int) this.gridView.DataKeys[e.RowIndex].Value;
            if (this.bll.DeleteCategory(categoryId))
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除淘宝分类(id=" + categoryId + ")成功", this);
            }
            else
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除淘宝分类(id=" + categoryId + ")失败", this);
            }
            this.BindData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(base.Request.Form["Callback"]) && (base.Request.Form["Callback"] == "true"))
                {
                    this.Controls.Clear();
                    this.DoCallback();
                }
                if ((this.Session["Style"] != null) && (this.Session["Style"].ToString() != ""))
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 120;
            }
        }
    }
}

