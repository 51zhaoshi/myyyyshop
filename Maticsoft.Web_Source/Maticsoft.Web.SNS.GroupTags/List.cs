namespace Maticsoft.Web.SNS.GroupTags
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
        protected int Act_AddData = 0x65;
        protected int Act_DelData = 0x67;
        protected int Act_DeleteList = 0x68;
        protected int Act_UpdateData = 0x66;
        private Maticsoft.BLL.SNS.GroupTags bll = new Maticsoft.BLL.SNS.GroupTags();
        protected Button btnDelete;
        protected Button btnSave;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal11;
        protected Literal Literal2;
        protected Literal Literal3;
        protected DropDownList radlIsRecommand;
        protected DropDownList radlStatus;
        protected HtmlTable tableAdd;
        protected TextBox txtKeyword;
        protected TextBox txtTagName;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[4].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[5].Visible = false;
            }
            this.gridView.DataSetSource = this.bll.GetSearchList(this.txtKeyword.Text.Trim());
        }

        protected void btnBatch_Click(object sender, EventArgs e)
        {
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteList(selIDlist))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除群组标签(TagIds=" + selIDlist + "）成功!", this);
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                }
                this.gridView.OnBind();
            }
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
                Maticsoft.BLL.SNS.GroupTags tags = new Maticsoft.BLL.SNS.GroupTags();
                Maticsoft.Model.SNS.GroupTags model = null;
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
                            model.IsRecommand = Globals.SafeInt(this.radlIsRecommand.SelectedValue, 0);
                            model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                            if (tags.Update(model))
                            {
                                this.txtTagName.Text = "";
                                this.gridView.OnBind();
                                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "List.aspx");
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
                    model = new Maticsoft.Model.SNS.GroupTags {
                        TagName = tagName,
                        IsRecommand = Globals.SafeInt(this.radlIsRecommand.SelectedValue, 0),
                        Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0)
                    };
                    int num = tags.Add(model);
                    if (num > 0)
                    {
                        this.txtTagName.Text = "";
                        MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK);
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "添加群组标签(TagId=" + num + ")成功!", this);
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
            if ((e.CommandName == "RecommandIndex") && (e.CommandArgument != null))
            {
                string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                if (((EnumHelper.RecommendType) Globals.SafeEnum<EnumHelper.RecommendType>(strArray[1], EnumHelper.RecommendType.None)) != EnumHelper.RecommendType.Home)
                {
                    this.bll.UpdateIsRecommand(1, strArray[0]);
                    this.gridView.OnBind();
                }
                else
                {
                    this.bll.UpdateIsRecommand(0, strArray[0]);
                    this.gridView.OnBind();
                }
            }
            if ((e.CommandName == "Status") && (e.CommandArgument != null))
            {
                string[] strArray2 = e.CommandArgument.ToString().Split(new char[] { ',' });
                if (((EnumHelper.Status) Globals.SafeEnum<EnumHelper.Status>(strArray2[1], EnumHelper.Status.Disabled)) != EnumHelper.Status.Enabled)
                {
                    this.bll.UpdateStatus(1, strArray2[0]);
                    this.gridView.OnBind();
                }
                else
                {
                    this.bll.UpdateStatus(0, strArray2[0]);
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
            this.bll.Delete(tagID);
            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除群组标签（TagId=" + tagID + "）成功!", this);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DeleteList)) && (base.GetPermidByActID(this.Act_DeleteList) != -1))
                {
                    this.liDel.Visible = false;
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
            Maticsoft.Model.SNS.GroupTags model = new Maticsoft.BLL.SNS.GroupTags().GetModel(this.TagID);
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
                return 100;
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

