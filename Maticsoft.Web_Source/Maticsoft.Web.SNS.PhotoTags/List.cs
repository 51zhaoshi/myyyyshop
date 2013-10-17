namespace Maticsoft.Web.SNS.PhotoTags
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 0x7e;
        protected int Act_DelData = 0x80;
        protected int Act_DeleteList = 0x81;
        protected int Act_UpdateData = 0x7f;
        private Maticsoft.BLL.SNS.PhotoTags bll = new Maticsoft.BLL.SNS.PhotoTags();
        protected Button btnDelete;
        protected Button btnRecommand;
        protected Button btnSave;
        protected Button btnSearch;
        protected DropDownList dropIsRecommand;
        protected DropDownList dropStatus;
        protected GridViewEx gridView;
        protected LinkButton lbtnDelete;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList radlIsRecommand;
        protected RadioButtonList radlStatus;
        protected HtmlTable tableAdd;
        protected TextBox txtKeyword;
        protected TextBox txtTagName;

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
            this.gridView.DataSetSource = this.bll.GetList("TagName like '%" + this.txtKeyword.Text.TrimEnd(new char[0]) + "%'");
        }

        protected void btnRecommand_Click(object sender, EventArgs e)
        {
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string tagName = this.txtTagName.Text.Trim();
            if (tagName.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "名称不能为空！");
            }
            else
            {
                Maticsoft.BLL.SNS.PhotoTags tags = new Maticsoft.BLL.SNS.PhotoTags();
                Maticsoft.Model.SNS.PhotoTags model = null;
                if (this.TagID > 0)
                {
                    model = tags.GetModel(this.TagID);
                    if (model != null)
                    {
                        if (tags.Exists(model.TagID, tagName))
                        {
                            MessageBox.ShowServerBusyTip(this, "标签已存在！");
                        }
                        else
                        {
                            model.TagName = tagName;
                            model.IsRecommand = new int?(Globals.SafeInt(this.radlIsRecommand.SelectedValue, 0));
                            model.Status = new int?(Globals.SafeInt(this.radlStatus.SelectedValue, 0));
                            model.CreatedDate = new DateTime?(DateTime.Now);
                            if (tags.Update(model))
                            {
                                this.txtTagName.Text = "";
                                this.gridView.OnBind();
                                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "PhotoTags.aspx");
                            }
                            else
                            {
                                MessageBox.ShowFailTip(this, Site.TooltipUpdateError);
                            }
                        }
                    }
                }
                else if (tags.Exists(tagName))
                {
                    MessageBox.ShowServerBusyTip(this, "标签已存在！");
                }
                else
                {
                    model = new Maticsoft.Model.SNS.PhotoTags {
                        TagName = tagName,
                        IsRecommand = new int?(Globals.SafeInt(this.radlIsRecommand.SelectedValue, 0)),
                        Status = new int?(Globals.SafeInt(this.radlStatus.SelectedValue, 0)),
                        CreatedDate = new DateTime?(DateTime.Now)
                    };
                    if (tags.Add(model) > 0)
                    {
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "增加图片标签成功", this);
                        this.txtTagName.Text = "";
                        MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK);
                        this.gridView.OnBind();
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, Site.TooltipSaveError);
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void dropStatus_Changed(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if ((selIDlist.Trim().Length != 0) && ((this.dropStatus.SelectedValue != "-1") && this.bll.UpdateStatus(Globals.SafeInt(this.dropStatus.SelectedValue, 0), selIDlist)))
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量更新图片标签(TagsID=" + selIDlist + ")成功", this);
                MessageBox.ShowSuccessTip(this, Site.TooltipBatchUpdateOK);
                this.gridView.OnBind();
            }
        }

        public string GetIsRecommand(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        return "推荐";

                    case 1:
                        return "不推荐";
                }
            }
            return str;
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

        public string GetStatus(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        return "不可用";

                    case 1:
                        return "可用";
                }
            }
            return str;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if ((e.CommandName == "Status") && (e.CommandArgument != null))
            {
                string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                if (((EnumHelper.Status) Globals.SafeEnum<EnumHelper.Status>(strArray[1], EnumHelper.Status.Disabled)) != EnumHelper.Status.Enabled)
                {
                    this.bll.UpdateStatus(1, strArray[0]);
                    this.gridView.OnBind();
                }
                else
                {
                    this.bll.UpdateStatus(0, strArray[0]);
                    this.gridView.OnBind();
                }
            }
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
            int tagID = (int) this.gridView.DataKeys[e.RowIndex].Value;
            if (this.bll.Delete(tagID))
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除图片标签(id=" + tagID + ")成功", this);
            }
            this.gridView.OnBind();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if ((selIDlist.Trim().Length != 0) && this.bll.DeleteList(selIDlist))
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除图片标签(id=" + selIDlist + ")成功", this);
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                this.gridView.OnBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DeleteList)) && (base.GetPermidByActID(this.Act_DeleteList) != -1))
                {
                    this.liDel.Visible = false;
                    this.lbtnDelete.Visible = false;
                    this.btnDelete.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.tableAdd.Visible = false;
                }
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.SNS.PhotoTags model = new Maticsoft.BLL.SNS.PhotoTags().GetModel(this.TagID);
            if (model != null)
            {
                this.txtTagName.Text = model.TagName;
                this.radlIsRecommand.SelectedValue = model.IsRecommand.ToString();
                this.radlStatus.SelectedValue = model.Status.ToString();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x7d;
            }
        }

        public int TagID
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(str) && PageValidate.IsNumber(str))
                {
                    num = int.Parse(str);
                }
                return num;
            }
        }
    }
}

