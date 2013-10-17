namespace Maticsoft.Web.Admin.Accounts.Admin
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Actions : PageBaseAdmin
    {
        protected int Act_AddData = 0x2c;
        protected int Act_DelData = 0x2e;
        protected int Act_SetPerData = 0x2f;
        protected int Act_UpdateData = 0x2d;
        private Maticsoft.Accounts.Bus.Actions bll = new Maticsoft.Accounts.Bus.Actions();
        private Permissions bllperm = new Permissions();
        protected Button btnSave;
        protected Button btnSearch;
        protected HtmlGenericControl divAdd;
        protected DropDownList DropListCategory;
        protected DropDownList DropListCategory2;
        protected DropDownList DropListPermissions;
        protected DropDownList DropListPermissions2;
        protected GridViewEx gridView;
        protected Label lblToolTip;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected TextBox txtDescription;
        protected TextBox txtKeywords;

        public void BindData()
        {
            string strWhere = "";
            if (this.txtKeywords.Text.Trim() != "")
            {
                strWhere = "Description like '%" + this.txtKeywords.Text.Trim() + "%'";
            }
            this.gridView.DataSetSource = this.bll.GetList(strWhere);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtDescription.Text.Trim().Length > 0)
            {
                if (this.bll.Exists(this.txtDescription.Text.Trim()))
                {
                    MessageBox.ShowFailTip(this, Site.TooltipDataExist);
                    return;
                }
                if (this.DropListPermissions.SelectedIndex > 0)
                {
                    this.bll.Add(this.txtDescription.Text.Trim(), Convert.ToInt32(this.DropListPermissions.SelectedValue));
                }
                else
                {
                    this.bll.Add(this.txtDescription.Text.Trim());
                }
                this.gridView.OnBind();
            }
            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("添加功能行为：【{0}】", this.txtDescription.Text.Trim()), this);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        public void DropListCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DropListCategory.SelectedIndex > 0)
            {
                DataTable table = AccountsTool.GetPermissionsByCategory(Convert.ToInt32(this.DropListCategory.SelectedItem.Value)).Tables[0];
                this.DropListPermissions.DataSource = table;
                this.DropListPermissions.DataValueField = "PermissionID";
                this.DropListPermissions.DataTextField = "Description";
                this.DropListPermissions.DataBind();
                this.DropListPermissions.Items.Insert(0, Site.PleaseSelect);
                this.DropListPermissions.Visible = true;
            }
            else
            {
                this.DropListPermissions.Items.Clear();
            }
        }

        public void DropListCategory2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DropListCategory2.SelectedIndex > 0)
            {
                DataTable table = AccountsTool.GetPermissionsByCategory(Convert.ToInt32(this.DropListCategory2.SelectedItem.Value)).Tables[0];
                this.DropListPermissions2.DataSource = table;
                this.DropListPermissions2.DataValueField = "PermissionID";
                this.DropListPermissions2.DataTextField = "Description";
                this.DropListPermissions2.DataBind();
                this.DropListPermissions2.Items.Insert(0, Site.PleaseSelect);
                this.DropListPermissions2.Visible = true;
            }
            else
            {
                this.DropListPermissions.Items.Clear();
            }
        }

        protected void DropListPermissions2_Changed(object sender, EventArgs e)
        {
            if (this.DropListCategory2.SelectedIndex > 0)
            {
                if (this.DropListPermissions2.SelectedIndex <= 0)
                {
                    MessageBox.ShowFailTip(this, Site.TooltripPer);
                }
                else
                {
                    string selIDlist = this.GetSelIDlist();
                    if (selIDlist.Length > 0)
                    {
                        if ((this.DropListPermissions2.SelectedItem != null) && (this.DropListPermissions2.SelectedValue.Length > 0))
                        {
                            this.bll.AddPermission(selIDlist, Convert.ToInt32(this.DropListPermissions2.SelectedValue));
                        }
                        this.gridView.OnBind();
                        MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK);
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, Site.TooltripPerNum);
                    }
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量设置功能行为的权限", this);
                }
            }
            else
            {
                MessageBox.ShowFailTip(this, Site.TooltripPer);
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
                LinkButton button = (LinkButton) e.Row.FindControl("LinkButton3");
                if (((button != null) && !base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData))) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    button.Visible = false;
                }
                LinkButton button2 = (LinkButton) e.Row.FindControl("LinkButton4");
                if (((button2 != null) && !base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData))) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    button2.Visible = false;
                }
                if ((e.Row.RowIndex % 2) == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
                object obj2 = DataBinder.Eval(e.Row.DataItem, "PermissionID");
                if ((obj2 != null) && (obj2.ToString() != ""))
                {
                    int permissionId = Convert.ToInt32(obj2);
                    e.Row.Cells[3].Text = string.Concat(new object[] { "(", permissionId, ")", this.bllperm.GetPermissionName(permissionId) });
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.bll.Delete((int) this.gridView.DataKeys[e.RowIndex].Value);
            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除功能行为", this);
            this.gridView.OnBind();
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
                MessageBox.ShowFailTip(this, Site.TooltipNoNull);
            }
            else
            {
                this.bll.Update(int.Parse(s), text);
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("编辑功能行为：【{0}】", text), this);
                this.gridView.EditIndex = -1;
                this.gridView.OnBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.divAdd.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_SetPerData)) && (base.GetPermidByActID(this.Act_SetPerData) != -1))
                {
                    this.DropListCategory2.Visible = false;
                    this.DropListPermissions2.Visible = false;
                }
                DataTable table = AccountsTool.GetAllCategories().Tables[0];
                this.DropListCategory.DataSource = table;
                this.DropListCategory.DataValueField = "CategoryID";
                this.DropListCategory.DataTextField = "Description";
                this.DropListCategory.DataBind();
                this.DropListCategory.Items.Insert(0, Site.PleaseSelect);
                this.DropListCategory2.DataSource = table;
                this.DropListCategory2.DataValueField = "CategoryID";
                this.DropListCategory2.DataTextField = "Description";
                this.DropListCategory2.DataBind();
                this.DropListCategory2.Items.Insert(0, Site.PleaseSelect);
                this.DropListPermissions.Visible = false;
                this.DropListPermissions2.Visible = false;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xc3;
            }
        }
    }
}

