namespace Maticsoft.Web.Admin.SNS.Report
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_ApproveList = 0x9e;
        protected int Act_DelData = 0x9c;
        protected int Act_DeleteList = 0x9d;
        private Report bll = new Report();
        protected Button btnAlreadyDone;
        protected Button btnDelete;
        protected Button btnReportFalse;
        protected Button btnReportTrue;
        protected Button btnReportUnKnow;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal12;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        private Posts Postbll = new Posts();
        protected DropDownList rdostatus;
        protected DropDownList rdoType;
        protected TextBox txtKeyWord;
        protected TextBox txtName;

        private void BatchActionManage(string idlist)
        {
            Products products = new Products();
            Photos photos = new Photos();
            int result = 0;
            int num2 = 0;
            foreach (string str in idlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] strArray2 = str.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray2[0] == "0")
                {
                    this.Postbll.DeleteListByNormalPost(strArray2[1], true, base.CurrentUser.UserID);
                }
                else if (strArray2[0] == "1")
                {
                    DataSet set = photos.DeleteListEx(strArray2[1], out result, true, base.CurrentUser.UserID);
                    if (set != null)
                    {
                        this.PhysicalFileInfo(set.Tables[0]);
                    }
                }
                else
                {
                    DataSet set2 = products.DeleteListEx(strArray2[1], out num2, false, 1);
                    if (set2 != null)
                    {
                        this.PhysicalFileInfo(set2.Tables[0]);
                    }
                }
            }
        }

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[9].Visible = false;
            }
            DataSet searchList = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (Globals.SafeInt(this.rdostatus.SelectedValue, -1) > -1)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("  SNSR.Status={0}", this.rdostatus.SelectedValue);
            }
            if (Globals.SafeInt(this.rdoType.SelectedValue, -1) > -1)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("  TargetType={0}", this.rdoType.SelectedValue);
            }
            if (!string.IsNullOrWhiteSpace(builder.ToString()))
            {
                if (!string.IsNullOrWhiteSpace(this.txtName.Text))
                {
                    builder.AppendFormat(" AND CreatedNickName  like '%{0}%'", this.txtName.Text);
                }
            }
            else if (!string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                builder.AppendFormat(" CreatedNickName  like '%{0}%'", this.txtName.Text);
            }
            if (this.txtKeyWord.Text.Trim() != "")
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("Description like '%{0}%'", this.txtKeyWord.Text.Trim());
            }
            searchList = this.bll.GetSearchList(builder.ToString());
            this.gridView.DataSetSource = searchList;
        }

        protected void btnAlreadyDone_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if ((selIDlist.Trim().Length != 0) && this.bll.UpdateReportStatus(1, selIDlist))
            {
                this.SendMsg();
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量处理举报（id=" + selIDlist + "）成功", this);
                MessageBox.ShowSuccessTip(this, "批量处理成功！");
                this.gridView.OnBind();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if ((selIDlist.Trim().Length != 0) && this.bll.DeleteList(selIDlist))
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除举报（id=" + selIDlist + "）成功", this);
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                this.gridView.OnBind();
            }
        }

        protected void btnReportFalse_Click(object sender, EventArgs e)
        {
            string selPostIDlist = this.GetSelPostIDlist();
            if (selPostIDlist.Trim().Length != 0)
            {
                string selIDlist = this.GetSelIDlist();
                if (selIDlist.Trim().Length != 0)
                {
                    this.BatchActionManage(selPostIDlist);
                    this.bll.UpdateReportStatus(2, selIDlist);
                    MessageBox.ShowSuccessTip(this, "批量处理成功！");
                    this.gridView.OnBind();
                }
            }
        }

        protected void btnReportTrue_Click(object sender, EventArgs e)
        {
            string selPostIDlist = this.GetSelPostIDlist();
            if (selPostIDlist.Trim().Length != 0)
            {
                string selIDlist = this.GetSelIDlist();
                if (selIDlist.Trim().Length != 0)
                {
                    this.BatchActionManage(selPostIDlist);
                    this.bll.UpdateReportStatus(1, selIDlist);
                    MessageBox.ShowSuccessTip(this, "批量处理成功！");
                    this.gridView.OnBind();
                }
            }
        }

        protected void btnReportUnKnow_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                string selPostIDlist = this.GetSelPostIDlist();
                if (selPostIDlist.Trim().Length != 0)
                {
                    this.BatchActionManage(selPostIDlist);
                    this.bll.UpdateReportStatus(3, selIDlist);
                    MessageBox.ShowSuccessTip(this, "批量处理成功！");
                    this.gridView.OnBind();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        private void DeletePhysicalFile(string path)
        {
            FileHelper.DeleteFile(EnumHelper.AreaType.SNS, path);
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

        private string GetSelPostIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.gridView.Rows[i].FindControl(this.gridView.CheckBoxID);
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    HiddenField field = (HiddenField) this.gridView.Rows[i].FindControl("HiddenField_PostId");
                    HiddenField field2 = (HiddenField) this.gridView.Rows[i].FindControl("HiddenField_TagTypeId");
                    if (field != null)
                    {
                        string str2 = str;
                        str = str2 + field2.Value + "_" + field.Value + ",";
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
                        return "未处理";

                    case 1:
                        return "举报属实已删除";

                    case 2:
                        return "虚假举报已忽略";

                    case 3:
                        return "举报内容核实中";
                }
            }
            return str;
        }

        public string GetTargetType(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        return "动态";

                    case 1:
                        return "图片";

                    case 2:
                        return "商品";
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
                LinkButton button1 = (LinkButton) e.Row.FindControl("lbtnDel");
                LinkButton button2 = (LinkButton) e.Row.FindControl("lbtnDelPost");
                Literal literal1 = (Literal) e.Row.FindControl("litShow");
                object obj2 = DataBinder.Eval(e.Row.DataItem, "TargetType");
                DataBinder.Eval(e.Row.DataItem, "ID");
                DataBinder.Eval(e.Row.DataItem, "TargetID");
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int iD = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.Delete(iD);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DeleteList)) && (base.GetPermidByActID(this.Act_DeleteList) != -1))
                {
                    this.btnDelete.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_ApproveList)) && (base.GetPermidByActID(this.Act_ApproveList) != -1))
                {
                    this.btnAlreadyDone.Visible = false;
                }
            }
        }

        private void PhysicalFileInfo(DataTable dt)
        {
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if ((dt.Rows[i]["TargetImageURL"] != null) && (dt.Rows[i]["TargetImageURL"].ToString() != ""))
                    {
                        this.DeletePhysicalFile(dt.Rows[i]["TargetImageURL"].ToString());
                    }
                    if ((dt.Rows[i]["ThumbImageUrl"] != null) && (dt.Rows[i]["ThumbImageUrl"].ToString() != ""))
                    {
                        this.DeletePhysicalFile(dt.Rows[i]["ThumbImageUrl"].ToString());
                    }
                    if ((dt.Rows[i]["NormalImageUrl"] != null) && (dt.Rows[i]["NormalImageUrl"].ToString() != ""))
                    {
                        this.DeletePhysicalFile(dt.Rows[i]["NormalImageUrl"].ToString());
                    }
                }
            }
        }

        private void SendMsg()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.gridView.Rows[i].FindControl(this.gridView.CheckBoxID);
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    HiddenField field = (HiddenField) this.gridView.Rows[i].FindControl("HiddenField_UserId");
                    if (field != null)
                    {
                        str = str + field.Value + ",";
                    }
                }
            }
            if (flag)
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            Maticsoft.Model.Members.SiteMessage model = new Maticsoft.Model.Members.SiteMessage();
            Maticsoft.BLL.Members.SiteMessage message2 = new Maticsoft.BLL.Members.SiteMessage();
            model.Title = "系统管理员通知";
            model.Content = "您好，您的举报信息我们已经收到并处理，非常感谢您对我们工作的支持。";
            model.SenderID = new int?(base.CurrentUser.UserID);
            model.SendTime = new DateTime?(DateTime.Now);
            model.ReaderIsDel = false;
            model.ReceiverIsRead = false;
            model.SenderIsDel = false;
            foreach (string str2 in str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                model.ReceiverID = new int?(int.Parse(str2));
                message2.Add(model);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x9b;
            }
        }
    }
}

