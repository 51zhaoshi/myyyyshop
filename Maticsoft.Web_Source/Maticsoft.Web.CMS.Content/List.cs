namespace Maticsoft.Web.CMS.Content
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
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
        protected int Act_AddData = 0xe7;
        protected int Act_DelData = 0xe9;
        protected int Act_UpdateData = 0xe8;
        private Maticsoft.BLL.CMS.Content bll = new Maticsoft.BLL.CMS.Content();
        protected Button btnDelete;
        protected Button btnSearch;
        protected DropDownList dropParentID;
        protected DropDownList dropType;
        protected GridViewEx gridView;
        protected HtmlGenericControl liAdd;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal11;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        public string strClassID = string.Empty;
        protected TextBox txtKeyword;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[11].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[12].Visible = false;
            }
            StringBuilder builder = new StringBuilder();
            string selectedValue = this.dropParentID.SelectedValue;
            if (!string.IsNullOrWhiteSpace(this.ContentStatus) && PageValidate.IsNumber(this.ContentStatus))
            {
                builder.AppendFormat(" state={0}", this.ContentStatus);
            }
            if (!string.IsNullOrWhiteSpace(this.txtKeyword.Text.Trim()))
            {
                if (builder.Length > 1)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("Title like '%{0}%'", InjectionFilter.QuoteFilter(this.txtKeyword.Text.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(this.dropParentID.SelectedValue))
            {
                if (builder.Length > 1)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("ClassID = {0} ", selectedValue);
            }
            if (this.ClassID > 0)
            {
                if (builder.Length > 1)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat(" ClassID={0} ", this.ClassID);
            }
            this.gridView.DataSetSource = this.bll.GetListByView(0, builder.ToString(), "ContentID desc");
        }

        private void BindNode(int parentid, DataTable dt, string blank)
        {
            foreach (DataRow row in dt.Select("ParentID= " + parentid))
            {
                string str = row["ClassID"].ToString();
                string text = row["ClassName"].ToString();
                text = blank + "『" + text + "』";
                this.dropParentID.Items.Add(new ListItem(text, str));
                int num = int.Parse(str);
                string str3 = blank + "─";
                this.BindNode(num, dt, str3);
            }
        }

        private void BindTree()
        {
            this.dropParentID.Items.Clear();
            this.dropParentID.Items.Add(new ListItem(Site.All, ""));
            DataSet treeList = new ContentClass().GetTreeList("");
            if (!DataSetTools.DataSetIsNull(treeList))
            {
                DataTable dt = treeList.Tables[0];
                if (!DataTableTools.DataTableIsNull(dt))
                {
                    foreach (DataRow row in dt.Select("ParentID= " + 0))
                    {
                        string str = row["ClassID"].ToString();
                        string text = row["ClassName"].ToString();
                        row["ParentID"].ToString();
                        text = "╋" + text;
                        this.dropParentID.Items.Add(new ListItem(text, str));
                        int parentid = int.Parse(str);
                        string blank = "├";
                        this.BindNode(parentid, dt, blank);
                    }
                }
            }
            this.dropParentID.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteList(selIDlist))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                    foreach (string str2 in selIDlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        DataCache.DeleteCache("ContentModelEx-" + str2);
                        DataCache.DeleteCache("ContentModel-" + str2);
                    }
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipDelError);
                }
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected string GetContentState(object target)
        {
            string str2;
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString()) == null))
            {
                return str;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    if (str2 != "2")
                    {
                        return str;
                    }
                    return CMS.ContentdrpDraft;
                }
            }
            else
            {
                return Site.btnApproveText;
            }
            return Site.Unaudited;
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

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if ((e.CommandName == "SetRec") && (e.CommandArgument != null))
            {
                string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                int id = Globals.SafeInt(strArray[0], 0);
                bool flag = Globals.SafeBool(strArray[1], false);
                if (this.bll.SetRec(id, !flag))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                    this.gridView.OnBind();
                }
            }
            if ((e.CommandName == "SetHot") && (e.CommandArgument != null))
            {
                string[] strArray2 = e.CommandArgument.ToString().Split(new char[] { ',' });
                int num2 = Globals.SafeInt(strArray2[0], 0);
                bool flag2 = Globals.SafeBool(strArray2[1], false);
                if (this.bll.SetHot(num2, !flag2))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                    this.gridView.OnBind();
                }
            }
            if ((e.CommandName == "SetColor") && (e.CommandArgument != null))
            {
                string[] strArray3 = e.CommandArgument.ToString().Split(new char[] { ',' });
                int num3 = Globals.SafeInt(strArray3[0], 0);
                bool flag3 = Globals.SafeBool(strArray3[1], false);
                if (this.bll.SetColor(num3, !flag3))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                    this.gridView.OnBind();
                }
            }
            if ((e.CommandName == "SetTop") && (e.CommandArgument != null))
            {
                string[] strArray4 = e.CommandArgument.ToString().Split(new char[] { ',' });
                int num4 = Globals.SafeInt(strArray4[0], 0);
                bool flag4 = Globals.SafeBool(strArray4[1], false);
                if (this.bll.SetTop(num4, !flag4))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
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
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.bll.Delete((int) this.gridView.DataKeys[e.RowIndex].Value))
            {
                this.gridView.OnBind();
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
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
                    this.liAdd.Visible = false;
                }
                this.BindTree();
                if (this.ClassID > 0)
                {
                    this.strClassID = "?classid=" + this.ClassID;
                    this.dropParentID.SelectedValue = this.ClassID.ToString();
                    this.dropParentID.Enabled = false;
                }
                if (this.Session["Style"] != null)
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if (base.Application[str] != null)
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
            }
        }

        protected void Type_Changed(object sender, EventArgs e)
        {
            string msg = string.Empty;
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                switch (this.dropType.SelectedValue)
                {
                    case "1":
                        if (this.bll.UpdateList(selIDlist, 0))
                        {
                            msg = Site.TooltipBatchUpdateOK;
                        }
                        goto Label_0175;

                    case "2":
                        if (this.bll.UpdateList(selIDlist, 2))
                        {
                            msg = Site.TooltipBatchUpdateOK;
                        }
                        goto Label_0175;

                    case "3":
                        if (this.bll.UpdateList(selIDlist, 1))
                        {
                            msg = Site.TooltipBatchUpdateOK;
                        }
                        goto Label_0175;

                    case "4":
                        if (this.bll.SetRecList(selIDlist))
                        {
                            msg = "批量设置推荐文章成功";
                        }
                        goto Label_0175;

                    case "5":
                        if (this.bll.SetHotList(selIDlist))
                        {
                            msg = "批量设置热门文章成功";
                        }
                        goto Label_0175;

                    case "6":
                        if (this.bll.SetColorList(selIDlist))
                        {
                            msg = "批量设置醒目文章成功";
                        }
                        goto Label_0175;

                    case "7":
                        if (this.bll.SetTopList(selIDlist))
                        {
                            msg = "批量设置置顶文章成功";
                        }
                        goto Label_0175;
                }
            }
            return;
        Label_0175:
            if (msg != "")
            {
                foreach (string str3 in selIDlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    DataCache.DeleteCache("ContentModelEx-" + str3);
                    DataCache.DeleteCache("ContentModel-" + str3);
                }
                MessageBox.ShowSuccessTip(this, msg);
                this.gridView.OnBind();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xe3;
            }
        }

        public int ClassID
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["classid"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    num = Globals.SafeInt(str, 0);
                }
                return num;
            }
        }

        private string ContentStatus
        {
            get
            {
                string str = string.Empty;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["type"]))
                {
                    str = base.Request.Params["type"];
                }
                return str;
            }
        }
    }
}

