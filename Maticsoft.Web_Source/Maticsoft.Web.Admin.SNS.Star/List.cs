namespace Maticsoft.Web.Admin.SNS.Star
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
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
        protected int Act_DelData = 0x26a;
        private Maticsoft.BLL.SNS.Star bll = new Maticsoft.BLL.SNS.Star();
        protected Button btnDelete;
        protected Button btnSearch;
        protected Button btnUpdateState;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal7;
        private UsersExp manage = new UsersExp();
        protected TextBox txtKeyword;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[5].Visible = false;
            }
            DataSet list = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.txtKeyword.Text.Trim() != "")
            {
                builder.AppendFormat("NickName like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            list = this.bll.GetList(builder.ToString());
            this.gridView.DataSetSource = list;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteListEx(selIDlist))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除达人（id=" + selIDlist + "）成功", this);
                }
                else
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除达人（id=" + selIDlist + "）失败", this);
                }
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void btnUpdateState_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.UpdateStateList(selIDlist, 1))
                {
                    new UsersExp();
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量审核达人（id=" + selIDlist + "）成功", this);
                    MessageBox.ShowSuccessTip(this, "批量设置成功！");
                }
                else
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量审核达人（id=" + selIDlist + "）失败", this);
                }
                this.gridView.OnBind();
            }
        }

        private void DeletePhysicalFile(string path)
        {
            FileHelper.DeleteFile(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS, path);
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

        protected string GetStarType(object target)
        {
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            Maticsoft.BLL.SNS.StarType type = new Maticsoft.BLL.SNS.StarType();
            int typeID = Globals.SafeInt(target.ToString(), 0);
            Maticsoft.Model.SNS.StarType model = type.GetModel(typeID);
            if (model != null)
            {
                return model.TypeName;
            }
            return "";
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
                int num = 0;
                string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                num = Globals.SafeInt(strArray[0], 0);
                int num2 = Globals.SafeInt(strArray[1], 0);
                Globals.SafeInt(strArray[2], 0);
                if (num2 == 0)
                {
                    this.bll.UpdateStateList(num.ToString(), 1);
                }
                else
                {
                    this.bll.UpdateStateList(num.ToString(), 0);
                }
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
                LinkButton button1 = (LinkButton) e.Row.FindControl("LinkButton1");
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int num = (int) this.gridView.DataKeys[e.RowIndex].Value;
            if (this.bll.DeleteListEx(num.ToString()))
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除达人（id=" + num + "）成功", this);
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
            }
            this.gridView.OnBind();
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
                this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x269;
            }
        }
    }
}

