namespace Maticsoft.Web.Admin.SysManage
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class ConfigSystem : PageBaseAdmin
    {
        protected int Act_AddConfigType = 0x3a;
        protected int Act_AddData = 0x3b;
        protected int Act_DelData = 0x3d;
        protected int Act_UpdateData = 60;
        protected Button btnRestartApp;
        protected Button btnSave;
        protected Button btnSaveType;
        protected Button btnSearch;
        protected copyright Copyright1;
        protected DropDownList dropConfigType;
        protected DropDownList dropConfigTypeSearch;
        protected HtmlForm form1;
        protected GridViewEx gridView;
        protected Label lblToolTip;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected HtmlTable TableAdd;
        protected TextBox txtDescription;
        protected TextBox txtKeyName;
        protected TextBox txtKeyWord;
        protected TextBox txtTypeName;
        protected TextBox txtValue;

        private void BindConfigType()
        {
            DataSet list = new ConfigType().GetList("");
            this.dropConfigType.DataSource = list;
            this.dropConfigType.DataTextField = "TypeName";
            this.dropConfigType.DataValueField = "KeyType";
            this.dropConfigType.DataBind();
            this.dropConfigTypeSearch.DataSource = list;
            this.dropConfigTypeSearch.DataTextField = "TypeName";
            this.dropConfigTypeSearch.DataValueField = "KeyType";
            this.dropConfigTypeSearch.DataBind();
            this.dropConfigTypeSearch.Items.Insert(0, Site.PleaseSelect);
        }

        public void BindData()
        {
            DataSet list = new DataSet();
            string strWhere = "";
            if (this.dropConfigTypeSearch.SelectedIndex > 0)
            {
                strWhere = string.Format(" KeyType={0} ", this.dropConfigTypeSearch.SelectedValue);
            }
            if (this.txtKeyWord.Text.Trim() != "")
            {
                if (strWhere.Length > 1)
                {
                    strWhere = strWhere + " and ";
                }
                strWhere = strWhere + string.Format(" KeyName like '%{0}%' ", this.txtKeyWord.Text.Trim());
            }
            list = Maticsoft.BLL.SysManage.ConfigSystem.GetList(strWhere);
            this.gridView.DataSetSource = list;
        }

        protected void btnRestartApp_Click(object sender, EventArgs e)
        {
            HttpRuntime.UnloadAppDomain();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtKeyName.Text.Trim() == "")
            {
                this.lblToolTip.ForeColor = Color.Red;
                this.lblToolTip.Text = SysManage.ErrorKeyNameNotNull;
            }
            else if (this.txtValue.Text.Trim() == "")
            {
                this.lblToolTip.ForeColor = Color.Red;
                this.lblToolTip.Text = SysManage.ErrorValueNotNull;
            }
            else if (this.txtDescription.Text.Trim() == "")
            {
                this.lblToolTip.ForeColor = Color.Red;
                this.lblToolTip.Text = SysManage.fieldDescription + SysManage.ErrorContentNotNull;
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add(this.txtKeyName.Text.Trim(), this.txtValue.Text.Trim(), this.txtDescription.Text.Trim(), Globals.SafeEnum<ApplicationKeyType>(this.dropConfigType.SelectedValue, ApplicationKeyType.System));
                MessageBox.ShowSuccessTip(this, Site.TooltipAddSuccess);
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("添加系统参数：【{0}】", this.txtKeyName.Text.Trim()), this);
                this.gridView.OnBind();
                this.lblToolTip.Text = "";
                this.txtKeyName.Text = "";
                this.txtValue.Text = "";
                this.txtDescription.Text = "";
            }
        }

        protected void btnSaveType_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.txtTypeName.Text.Trim()))
            {
                new ConfigType().Add(this.txtTypeName.Text.Trim());
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("添加参数类别：【{0}】", this.txtTypeName.Text.Trim()), this);
                this.txtTypeName.Text = "";
                this.BindConfigType();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
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
                    if (this.gridView.Rows[i].Cells[8].Text != "")
                    {
                        str = str + this.gridView.Rows[i].Cells[8].Text + ",";
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
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int iD = (int) this.gridView.DataKeys[e.RowIndex].Value;
            Maticsoft.BLL.SysManage.ConfigSystem.Delete(iD);
            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除系统参数", this);
            this.gridView.OnBind();
            MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
        }

        public void gridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gridView.EditIndex = e.NewEditIndex;
            this.gridView.OnBind();
        }

        public void gridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string s = this.gridView.DataKeys[e.RowIndex].Values[0].ToString();
            string text = (this.gridView.Rows[e.RowIndex].Cells[0].Controls[0] as TextBox).Text;
            string str3 = (this.gridView.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox).Text;
            string description = (this.gridView.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox).Text;
            if (str3.Length == 0)
            {
                MessageBox.Show(this, Site.TooltipNoNull);
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update(int.Parse(s), text, str3, description);
                this.gridView.EditIndex = -1;
                this.gridView.OnBind();
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("编辑系统参数：【{0}】", text), this);
                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddConfigType)) && (base.GetPermidByActID(this.Act_AddConfigType) != -1))
                {
                    this.txtTypeName.Visible = false;
                    this.btnSaveType.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.TableAdd.Visible = false;
                }
                this.BindConfigType();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x39;
            }
        }
    }
}

