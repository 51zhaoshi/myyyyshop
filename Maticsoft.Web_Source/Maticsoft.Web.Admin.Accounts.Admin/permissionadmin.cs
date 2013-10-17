namespace Maticsoft.Web.Admin.Accounts.Admin
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class permissionadmin : PageBaseAdmin
    {
        protected int Act_AddPerData = 0x29;
        protected int Act_AddPerType = 0x27;
        protected int Act_DelData = 40;
        protected int Act_DeleteList = 0x25;
        protected int Act_RemoveData = 0x2b;
        protected int Act_ShowReservedPerm = 14;
        protected int Act_UpdateData = 0x2a;
        private Permissions bllPerm = new Permissions();
        protected Button btnDelete;
        protected Button btnDeleteClass;
        protected Button btnSaveCategories;
        protected Button btnSavePermissions;
        protected TextBox CategoriesName;
        protected DropDownList ClassList;
        protected DropDownList droplistCategories;
        protected GridViewEx gridView;
        protected Label lbltip1;
        protected Label lbltip2;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected TextBox PermissionsName;
        private List<string> ReservedPermIDs = StringPlus.GetStrArray(ConfigSystem.GetValueByCache("ReservedPermIDs"), ',', true);

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[2].Visible = false;
            }
            if ((this.ClassList.SelectedItem != null) && (this.ClassList.SelectedValue.Length > 0))
            {
                DataSet permissionsByCategory = AccountsTool.GetPermissionsByCategory(int.Parse(this.ClassList.SelectedValue));
                this.gridView.DataSetSource = permissionsByCategory;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<int> selIDlist = this.GetSelIDlist();
            if (selIDlist.Count != 0)
            {
                string tooltipDelOK = Site.TooltipDelOK;
                foreach (int num in selIDlist)
                {
                    try
                    {
                        this.bllPerm.Delete(num);
                        continue;
                    }
                    catch
                    {
                        tooltipDelOK = num.ToString() + "," + tooltipDelOK;
                        continue;
                    }
                }
                if (tooltipDelOK != Site.TooltipDelOK)
                {
                    tooltipDelOK = tooltipDelOK + Site.TooltipUpdateError;
                }
                MessageBox.ShowSuccessTip(this, tooltipDelOK);
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除权限数据", this);
                this.gridView.OnBind();
            }
        }

        public void btnDeleteClass_Click(object sender, EventArgs e)
        {
            if ((this.ClassList.SelectedItem != null) && (this.ClassList.SelectedValue.Length > 0))
            {
                int categoryID = int.Parse(this.ClassList.SelectedValue);
                PermissionCategories categories = new PermissionCategories();
                if (!categories.ExistsPerm(categoryID))
                {
                    categories.Delete(categoryID);
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("删除权限类别：【{0}】", this.ClassList.SelectedItem.Text), this);
                    this.CategoriesDatabind();
                    if (this.ClassList.SelectedItem != null)
                    {
                        this.gridView.OnBind();
                    }
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipPermCateNoDelete);
                }
            }
        }

        public void btnSaveCategories_Click(object sender, EventArgs e)
        {
            string description = this.CategoriesName.Text.Trim();
            if (description != "")
            {
                new PermissionCategories().Create(description);
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("添加权限类别：【{0}】", this.CategoriesName.Text.Trim()), this);
                this.CategoriesDatabind();
                if (this.ClassList.SelectedItem != null)
                {
                    this.gridView.OnBind();
                }
                this.CategoriesName.Text = "";
            }
            else
            {
                MessageBox.ShowFailTip(this, Site.TooltipNoNull);
            }
        }

        public void btnSavePermissions_Click(object sender, EventArgs e)
        {
            string description = this.PermissionsName.Text.Trim();
            if (description != "")
            {
                int pcID = int.Parse(this.ClassList.SelectedValue);
                this.bllPerm.Create(pcID, description);
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("添加权限数据：【{0}】", this.PermissionsName.Text.Trim()), this);
                if (this.ClassList.SelectedItem != null)
                {
                    this.gridView.OnBind();
                }
                this.PermissionsName.Text = "";
            }
            else
            {
                MessageBox.ShowFailTip(this, Site.TooltipNoNull);
            }
        }

        public void CategoriesDatabind()
        {
            DataSet allCategories = AccountsTool.GetAllCategories();
            this.ClassList.DataSource = allCategories;
            this.ClassList.DataTextField = "Description";
            this.ClassList.DataValueField = "CategoryID";
            this.ClassList.DataBind();
            this.droplistCategories.DataSource = allCategories;
            this.droplistCategories.DataTextField = "Description";
            this.droplistCategories.DataValueField = "CategoryID";
            this.droplistCategories.DataBind();
            this.droplistCategories.Items.Insert(0, new ListItem("移动到...", ""));
        }

        public void ClassList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void droplistCategories_Changed(object sender, EventArgs e)
        {
            List<int> selIDlist = this.GetSelIDlist();
            if (selIDlist.Count != 0)
            {
                string arrayStr = StringPlus.GetArrayStr(selIDlist);
                if (!string.IsNullOrWhiteSpace(this.droplistCategories.SelectedValue))
                {
                    int categoryID = Globals.SafeInt(this.droplistCategories.SelectedValue, 0);
                    this.bllPerm.UpdateCategory(arrayStr, categoryID);
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量移动权限数据", this);
                    this.gridView.OnBind();
                }
            }
        }

        private List<int> GetSelIDlist()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.gridView.Rows[i].FindControl(this.gridView.CheckBoxID);
                if (((box != null) && box.Checked) && (this.gridView.DataKeys[i].Value != null))
                {
                    list.Add(Convert.ToInt32(this.gridView.DataKeys[i].Value));
                }
            }
            return list;
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
                if ((e.Row.RowIndex % 2) == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
                object obj2 = DataBinder.Eval(e.Row.DataItem, "PermissionID");
                if (((obj2 != null) && (obj2.ToString() != "")) && (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_ShowReservedPerm)) && this.ReservedPermIDs.Contains(obj2.ToString())))
                {
                    e.Row.Visible = false;
                }
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
                MessageBox.ShowFailTip(this, Site.TooltipNoNull);
            }
            else
            {
                this.bllPerm.Update(int.Parse(s), text);
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("编辑权限数据：【{0}】", text), this);
                this.gridView.EditIndex = -1;
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
                    this.btnDelete.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddPerType)) && (base.GetPermidByActID(this.Act_AddPerType) != -1))
                {
                    this.btnSaveCategories.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.btnDeleteClass.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddPerData)) && (base.GetPermidByActID(this.Act_AddPerData) != -1))
                {
                    this.btnSavePermissions.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_RemoveData)) && (base.GetPermidByActID(this.Act_RemoveData) != -1))
                {
                    this.droplistCategories.Visible = false;
                }
                this.CategoriesDatabind();
                if (this.ClassList.SelectedItem != null)
                {
                    this.gridView.OnBind();
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
                return 0x26;
            }
        }
    }
}

