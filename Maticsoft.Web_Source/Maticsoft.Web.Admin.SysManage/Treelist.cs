namespace Maticsoft.Web.Admin.SysManage
{
    using Maticsoft.BLL.SysManage;
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

    public class Treelist : PageBaseAdmin
    {
        protected int Act_AddData = 0x35;
        protected int Act_DelData = 0x37;
        protected int Act_SetPerData = 0x38;
        protected int Act_UpdateData = 0x36;
        private SysTree bll = new SysTree();
        protected Button btnDelete;
        protected Button btnSearch;
        protected DropDownList ddlStatus;
        protected GridViewEx gridView;
        protected HtmlGenericControl liAdd;
        protected HtmlGenericControl liDel;
        protected DropDownList listTarget;
        protected DropDownList listTarget2;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected TextBox txtKeyword;

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
            string str = "";
            this.Session["TreelistPageIndex"] = 0;
            if ((this.Session["strWhereTreelist"] != null) && (this.Session["strWhereTreelist"].ToString() != ""))
            {
                str = " AND " + this.Session["strWhereTreelist"];
            }
            DataSet treeList = new DataSet();
            treeList = this.bll.GetTreeList("TreeType=" + this.TreeType + str);
            this.gridView.DataSetSource = treeList;
            this.Session["TreelistPageIndex"] = this.gridView.PageIndex;
        }

        private void BindNode(int parentid, DataTable dt, string blank)
        {
            foreach (DataRow row in dt.Select("ParentID= " + parentid))
            {
                string str = row["NodeID"].ToString();
                string text = row["TreeText"].ToString();
                row["PermissionID"].ToString();
                text = blank + "『" + text + "』";
                if (dt.Select("ParentID= " + str).Length > 0)
                {
                    this.listTarget.Items.Add(new ListItem(text, str));
                }
                this.listTarget2.Items.Add(new ListItem(text, str));
                int num = int.Parse(str);
                string str3 = blank + "─";
                this.BindNode(num, dt, str3);
            }
        }

        private void BiudTree(int treeType)
        {
            DataTable table;
            SysTree tree = new SysTree();
            if (treeType > -1)
            {
                table = tree.GetTreeList("TreeType = " + treeType).Tables[0];
            }
            else
            {
                table = tree.GetTreeList("").Tables[0];
            }
            this.listTarget.Items.Clear();
            this.listTarget2.Items.Clear();
            this.listTarget.Items.Add(new ListItem(SysManage.listTargetOne, "-1"));
            this.listTarget.Items.Add(new ListItem(SysManage.listTargetTwo, "0"));
            this.listTarget2.Items.Add(new ListItem(SysManage.listTargetTwo, "0"));
            foreach (DataRow row in table.Select("ParentID= " + 0))
            {
                string str = row["NodeID"].ToString();
                string text = row["TreeText"].ToString();
                row["ParentID"].ToString();
                row["PermissionID"].ToString();
                text = "╋" + text;
                this.listTarget.Items.Add(new ListItem(text, str));
                this.listTarget2.Items.Add(new ListItem(text, str));
                int parentid = int.Parse(str);
                string blank = "├";
                this.BindNode(parentid, table, blank);
            }
            this.listTarget.DataBind();
            this.listTarget2.DataBind();
            this.listTarget2.Items.Insert(0, new ListItem("移动到...", ""));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.bll.DelTreeNodes(selIDlist);
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除菜单", this);
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Session["strWhereTreelist"] = "";
            StringBuilder builder = new StringBuilder();
            string str = "";
            int num = -1;
            if ((this.listTarget.SelectedItem != null) && (this.listTarget.SelectedIndex > 0))
            {
                num = Convert.ToInt32(this.listTarget.SelectedValue);
                builder.AppendFormat("parentid={0}", num);
                str = "and";
            }
            if (!string.IsNullOrWhiteSpace(this.ddlStatus.SelectedValue))
            {
                builder.AppendFormat(" {0} Enabled='{1}'", str, this.ddlStatus.SelectedValue);
                str = "and";
            }
            if (this.txtKeyword.Text.Trim() != "")
            {
                builder.AppendFormat(" {0} TreeText like '%{1}%'", str, this.txtKeyword.Text.Trim());
            }
            if (builder.Length > 0)
            {
                this.Session["strWhereTreelist"] = builder.ToString();
            }
            this.gridView.OnBind();
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
                    str = str + this.gridView.Rows[i].Cells[1].Text + ",";
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
            if ((e.CommandName == "Enabled") && (e.CommandArgument != null))
            {
                int nodeid = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                this.bll.UpdateEnabled(nodeid);
                this.gridView.OnBind();
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
            int nodeid = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.DelTreeNode(nodeid);
            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除菜单", this);
            this.gridView.OnBind();
        }

        protected void listTarget2_Changed(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                int parentID = -1;
                if (!string.IsNullOrWhiteSpace(this.listTarget2.SelectedValue))
                {
                    parentID = Globals.SafeInt(this.listTarget2.SelectedValue, 0);
                    this.bll.MoveNodes(selIDlist, parentID);
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量移动菜单", this);
                }
                this.gridView.OnBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && (this.TreeType >= 0))
            {
                if (this.TreeType == 0)
                {
                    this.Act_AddData = 0x35;
                    this.Act_UpdateData = 0x36;
                    this.Act_DelData = 0x37;
                    this.Act_SetPerData = 0x38;
                }
                else
                {
                    this.Act_AddData = 0x45;
                    this.Act_UpdateData = 70;
                    this.Act_DelData = 0x47;
                    this.Act_SetPerData = 0x48;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_DeleteList)) && (base.GetPermidByActID(base.Act_DeleteList) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.liAdd.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_SetPerData)) && (base.GetPermidByActID(this.Act_SetPerData) != -1))
                {
                    this.listTarget2.Visible = false;
                    this.listTarget2.Visible = false;
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
                this.BiudTree(this.TreeType);
                if (!string.IsNullOrWhiteSpace(base.Request.Params["all"]))
                {
                    this.Session["strWhereTreelist"] = "";
                    this.Session["TreelistPageIndex"] = 0;
                }
                if ((this.Session["TreelistPageIndex"] != null) && (this.Session["TreelistPageIndex"].ToString() != ""))
                {
                    this.gridView.PageIndex = (int) this.Session["TreelistPageIndex"];
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x34;
            }
        }

        public int TreeType
        {
            get
            {
                return Globals.SafeInt(base.Request.Params["TreeType"], -1);
            }
        }
    }
}

