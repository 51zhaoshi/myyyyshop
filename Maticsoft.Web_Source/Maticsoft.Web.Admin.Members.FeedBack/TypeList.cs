namespace Maticsoft.Web.Admin.Members.FeedBack
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class TypeList : PageBaseAdmin
    {
        protected int Act_AddData = 0x11a;
        protected int Act_DelData = 0x11d;
        protected int Act_UpdateData = 0x11b;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected LinkButton lbtnDelete;
        protected HtmlGenericControl LiAdd;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal4;
        protected TextBox txtKeyword;
        private FeedbackType typeBll = new FeedbackType();

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[2].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[3].Visible = false;
            }
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(this.txtKeyword.Text))
            {
                builder.AppendFormat(" TypeName like '%{0}%' ", this.txtKeyword.Text);
            }
            this.gridView.DataSetSource = this.typeBll.GetList(builder.ToString());
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
            DataControlRowType rowType = e.Row.RowType;
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int typeId = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.typeBll.Delete(typeId);
            this.gridView.OnBind();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if ((selIDlist.Trim().Length != 0) && this.typeBll.DeleteList(selIDlist))
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除反馈类型（id=" + selIDlist + "）成功", this);
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                this.gridView.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.liDel.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.LiAdd.Visible = false;
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
                return 0x117;
            }
        }
    }
}

