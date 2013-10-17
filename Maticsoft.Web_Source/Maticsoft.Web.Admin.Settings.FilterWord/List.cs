namespace Maticsoft.Web.Admin.Settings.FilterWord
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Model.Settings;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 0x178;
        protected int Act_DelData = 0x17a;
        protected int Act_UpdateData = 0x179;
        private Maticsoft.BLL.Settings.FilterWords bll = new Maticsoft.BLL.Settings.FilterWords();
        protected Button btnDelete;
        protected Button btnSave;
        protected Button btnSearch;
        protected Button btnSet;
        protected DropDownList ddlSelectType;
        protected DropDownList ddSelect;
        protected GridViewEx gridView;
        protected Label lblMsg;
        protected HtmlGenericControl liAdd;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected TextBox tName;
        protected TextBox txtAddReplace;
        protected TextBox txtKeyword;
        protected HiddenField txtLookupListId;
        protected TextBox txtReplace;

        public void BindData()
        {
            DataSet set = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.txtKeyword.Text.Trim() != "")
            {
                builder.AppendFormat("WordPattern like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            set = this.bll.GetList(-1, builder.ToString(), " FilterId desc");
            this.gridView.DataSetSource = set;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.bll.DeleteList(selIDlist);
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                this.gridView.OnBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Settings.FilterWords model = new Maticsoft.Model.Settings.FilterWords {
                WordPattern = this.tName.Text,
                ActionType = Globals.SafeInt(this.ddlSelectType.SelectedValue, 0),
                RepalceWord = this.txtAddReplace.Text
            };
            if (this.bll.Add(model) > 0)
            {
                this.gridView.OnBind();
            }
            else
            {
                MessageBox.ShowFailTip(this, "添加失败，请稍候再试！");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                int type = Globals.SafeInt(this.ddSelect.SelectedValue, 0);
                string text = this.txtReplace.Text;
                if (this.bll.UpdateActionType(selIDlist, type, text))
                {
                    MessageBox.ShowSuccessTip(this, "批量设置成功!");
                }
                this.gridView.OnBind();
            }
        }

        private void DoCallback()
        {
            string str3;
            string str = base.Request.Form["Action"];
            base.Response.Clear();
            base.Response.ContentType = "application/json";
            string s = string.Empty;
            if (((str3 = str) != null) && (str3 == "Update"))
            {
                s = this.Update();
            }
            base.Response.Write(s);
            base.Response.End();
        }

        public string GetActionText(object target)
        {
            switch (Globals.SafeInt(target.ToString(), 0))
            {
                case 0:
                    return "禁止关键词";

                case 1:
                    return "审核关键词";

                case 2:
                    return "替换关键词";
            }
            return "禁止关键词";
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    HtmlGenericControl control = (HtmlGenericControl) e.Row.FindControl("btnModify");
                    control.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    LinkButton button = (LinkButton) e.Row.FindControl("linkDel");
                    button.Visible = false;
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int filterId = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.Delete(filterId);
            this.bll.ClearCache();
            this.gridView.OnBind();
        }

        public void gridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gridView.EditIndex = e.NewEditIndex;
            string text = this.gridView.Rows[e.NewEditIndex].Cells[1].Text;
            this.gridView.OnBind();
        }

        public void gridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int num = (int) this.gridView.DataKeys[e.RowIndex].Value;
            string text = (this.gridView.Rows[e.RowIndex].Cells[0].Controls[0] as TextBox).Text;
            string str2 = (this.gridView.Rows[e.RowIndex].Cells[3].Controls[0] as TextBox).Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.ShowFailTip(this, "请输入敏感词！");
            }
            else
            {
                Maticsoft.Model.Settings.FilterWords model = new Maticsoft.Model.Settings.FilterWords {
                    WordPattern = text,
                    FilterId = num,
                    RepalceWord = str2
                };
                this.bll.Update(model);
                this.bll.ClearCache();
                this.gridView.EditIndex = -1;
                this.gridView.OnBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.btnSave.Visible = false;
                    this.liAdd.Visible = false;
                }
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

        private string Update()
        {
            JsonObject obj2 = new JsonObject();
            int filterId = Globals.SafeInt(base.Request.Form["FilterId"], 0);
            int num2 = Globals.SafeInt(base.Request.Form["ActionType"], 0);
            string str = base.Request.Params["Word"];
            string str2 = base.Request.Params["ReplaceWord"];
            Maticsoft.Model.Settings.FilterWords model = this.bll.GetModel(filterId);
            if (model == null)
            {
                obj2.Put("STATUS", "FAILED");
            }
            model.ActionType = num2;
            model.RepalceWord = str2;
            model.WordPattern = str;
            if (this.bll.Update(model))
            {
                obj2.Put("STATUS", "SUCCESS");
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            return obj2.ToString();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x176;
            }
        }
    }
}

