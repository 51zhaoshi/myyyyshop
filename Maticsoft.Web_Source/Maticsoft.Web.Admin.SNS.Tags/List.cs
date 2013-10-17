namespace Maticsoft.Web.Admin.SNS.Tags
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 140;
        protected int Act_DelData = 0x8e;
        protected int Act_DeleteList = 0x8f;
        protected int Act_UpdateData = 0x8d;
        private Maticsoft.BLL.SNS.Tags bll = new Maticsoft.BLL.SNS.Tags();
        protected Button btnDelete;
        protected Button btnSave;
        protected Button btnSearch;
        protected DropDownList dropIsRecommand;
        protected DropDownList dropStatus;
        protected DropDownList dropTypeId;
        protected GridViewEx gridView;
        protected LinkButton lbtnDelete;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
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
                this.gridView.Columns[6].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[7].Visible = false;
            }
            StringBuilder builder = new StringBuilder();
            if (this.txtKeyword.Text.Trim() != "")
            {
                if (builder.Length > 1)
                {
                    builder.Append(" and  ");
                }
                builder.AppendFormat(" TagName like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            if (builder.Length > 1)
            {
                builder.Append(" and  ");
            }
            builder.AppendFormat(" TypeId in (select ID from SNS_TagType where Cid>=0)", new object[0]);
            this.gridView.DataSetSource = this.bll.GetListEx(builder.ToString());
            this.gridView.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtTagName.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "标签的名称不能为空！");
            }
            else
            {
                int num = Globals.SafeInt(this.dropTypeId.SelectedValue, 0);
                if (num == 0)
                {
                    MessageBox.ShowServerBusyTip(this, "请选择类别！");
                }
                else
                {
                    Maticsoft.Model.SNS.Tags model = null;
                    if (this.TagID > 0)
                    {
                        model = this.bll.GetModel(this.TagID);
                        if (model != null)
                        {
                            model.TagName = str;
                            model.TypeId = num;
                            model.IsRecommand = int.Parse(this.radlIsRecommand.SelectedValue);
                            model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                            if (this.bll.Update(model))
                            {
                                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "List.aspx");
                            }
                            else
                            {
                                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateError);
                            }
                        }
                    }
                    else
                    {
                        model = new Maticsoft.Model.SNS.Tags {
                            TagName = str,
                            TypeId = num,
                            IsRecommand = int.Parse(this.radlIsRecommand.SelectedValue),
                            Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0)
                        };
                        if (this.bll.Add(model) > 0)
                        {
                            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "增加商品标签成功", this);
                            MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "List.aspx");
                        }
                        else
                        {
                            MessageBox.ShowFailTip(this, Site.TooltipSaveError);
                        }
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void dropIsRecommand_Changed(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if ((selIDlist.Trim().Length != 0) && ((this.dropIsRecommand.SelectedValue != "-1") && this.bll.UpdateIsRecommand(Globals.SafeInt(this.dropIsRecommand.SelectedValue, 0), selIDlist)))
            {
                MessageBox.ShowSuccessTip(this, Site.TooltipBatchUpdateOK);
                this.gridView.OnBind();
            }
        }

        protected void dropStatus_Changed(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if ((selIDlist.Trim().Length != 0) && ((this.dropStatus.SelectedValue != "-1") && this.bll.UpdateStatus(Globals.SafeInt(this.dropStatus.SelectedValue, 0), selIDlist)))
            {
                MessageBox.ShowSuccessTip(this, Site.TooltipBatchUpdateOK);
                this.gridView.OnBind();
            }
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
                LinkButton button1 = (LinkButton) e.Row.FindControl("lbtnDelete");
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int tagID = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.Delete(tagID);
            MessageBox.ShowSuccessTip(this, "删除成功！");
            this.gridView.OnBind();
        }

        public string IsRecommand(object target)
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

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if ((selIDlist.Trim().Length != 0) && this.bll.DeleteList(selIDlist))
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除商品标签(id=" + selIDlist + ")成功", this);
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
                this.TagTypeBindData();
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.SNS.Tags model = this.bll.GetModel(this.TagID);
            if (model != null)
            {
                this.txtTagName.Text = model.TagName;
                this.dropTypeId.SelectedValue = model.TypeId.ToString();
                this.radlIsRecommand.SelectedValue = model.IsRecommand.ToString();
                this.radlStatus.SelectedValue = model.Status.ToString();
            }
        }

        public void TagTypeBindData()
        {
            this.dropTypeId.DataSource = new Maticsoft.BLL.SNS.TagType().GetList("Cid>=0");
            this.dropTypeId.DataTextField = "TypeName";
            this.dropTypeId.DataValueField = "ID";
            this.dropTypeId.DataBind();
            this.dropTypeId.Items.Insert(0, new ListItem("--请选择--", "0"));
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x8b;
            }
        }

        protected int TagID
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    num = Globals.SafeInt(str, 0);
                }
                return num;
            }
        }
    }
}

