namespace Maticsoft.Web.Admin.SysManage
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class UserLog : PageBaseAdmin
    {
        protected int Act_DelData = 0x4b;
        protected int Act_DeleteList = 0x4a;
        protected Button btnDelete;
        protected Button btnSearch;
        protected Button Button1;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected TextBox txtDate;
        protected TextBox txtKeyword;

        public void BindData()
        {
            DataSet list = new DataSet();
            string strWhere = "";
            if (this.txtKeyword.Text.Trim() != "")
            {
                strWhere = " OPInfo like '%" + this.txtKeyword.Text.Trim() + "%'";
            }
            list = Maticsoft.BLL.SysManage.UserLog.GetList(strWhere);
            this.gridView.DataSetSource = list;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                Maticsoft.BLL.SysManage.UserLog.Delete(selIDlist);
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除用户日志", this);
                this.gridView.OnBind();
            }
        }

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            Maticsoft.BLL.SysManage.UserLog.Delete(Convert.ToDateTime(this.txtDate.Text));
            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("批量删除【{0}】之前用户日志", this.txtDate.Text), this);
            this.gridView.OnBind();
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
            int iID = (int) this.gridView.DataKeys[e.RowIndex].Value;
            Maticsoft.BLL.SysManage.UserLog.Delete(iID);
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
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.Button1.Visible = false;
                    this.txtDate.Visible = false;
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
                this.txtDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x49;
            }
        }
    }
}

