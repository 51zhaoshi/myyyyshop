namespace Maticsoft.Web.Admin.SNS.Posts
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class UserBlog : PageBaseAdmin
    {
        protected int Act_DelData = 0x19;
        private Maticsoft.BLL.SNS.UserBlog bll = new Maticsoft.BLL.SNS.UserBlog();
        protected Button btnChecked;
        protected Button btnIndexRec;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal11;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected TextBox txtBeginTime;
        protected TextBox txtEndTime;
        protected TextBox txtKeyword;
        protected TextBox txtPoster;

        public void BindData()
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(this.txtPoster.Text.Trim()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("UserName like '%{0}%' ", this.txtPoster.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtBeginTime.Text.Trim()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("  CreatedDate>='" + this.txtBeginTime.Text.Trim() + "'");
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(" CreatedDate<='" + this.txtEndTime.Text.Trim() + "'");
            }
            if (this.txtKeyword.Text.Length > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(" Title like '%" + this.txtKeyword.Text + "%' ");
            }
            DataSet set = this.bll.GetList(-1, builder.ToString(), " CreatedDate desc");
            this.gridView.DataSetSource = set;
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.UpdateStatusList(selIDlist, 1))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "操作失败！");
                }
                this.gridView.OnBind();
            }
        }

        protected void btnIndexRec_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.UpdateRecList(selIDlist, 1))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "操作失败！");
                }
                this.gridView.OnBind();
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

        private string GetSelIDTypelist(string type)
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.gridView.Rows[i].FindControl(this.gridView.CheckBoxID);
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    if (this.gridView.DataKeys[i].Values.Count > 1)
                    {
                        if ((this.gridView.DataKeys[i].Values[1].ToString() == type) && (type == "0"))
                        {
                            str = str + this.gridView.DataKeys[i].Values[0] + ",";
                        }
                        else if (this.gridView.DataKeys[i].Values[1].ToString() == type)
                        {
                            str = str + this.gridView.DataKeys[i].Values[2] + ",";
                        }
                    }
                }
            }
            if (flag && (str.Length > 0))
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
        }

        protected string GetStatus(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), 0))
                {
                    case 0:
                        return "未审核";

                    case 1:
                        return "已审核";

                    case 2:
                        return "未通过";
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
            if (e.CommandArgument.Equals("Delete"))
            {
                if ((e.CommandName == null) || (e.CommandName.ToString() == ""))
                {
                    return;
                }
                int blogID = Globals.SafeInt(e.CommandName, -1);
                if (this.bll.DeleteEx(blogID))
                {
                    MessageBox.ShowSuccessTip(this, "删除成功！");
                    this.gridView.OnBind();
                }
                else
                {
                    MessageBox.ShowFailTip(this, "删除失败，请稍候再试！");
                }
            }
            if ((e.CommandName == "RecommendIndex") && (e.CommandArgument != null))
            {
                string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                int id = Globals.SafeInt(strArray[0], 0);
                int rec = (Globals.SafeInt(strArray[1], 0) == 0) ? 1 : 0;
                if (this.bll.UpdateRec(id, rec))
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
                LinkButton button1 = (LinkButton) e.Row.FindControl("linkbtnDel");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPostBack = this.Page.IsPostBack;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x5b;
            }
        }
    }
}

