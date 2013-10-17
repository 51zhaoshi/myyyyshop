namespace Maticsoft.Web.Admin.Ms.WeiBo
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class WeiBoTaskList : PageBaseAdmin
    {
        protected int Act_DelData = 0x15b;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected HiddenField hfImage;
        protected HiddenField hfWeiboCount;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        private WeiBoTaskMsg taskMsgBll = new WeiBoTaskMsg();
        protected TextBox txtKeyword;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[3].Visible = false;
            }
            DataSet set = new DataSet();
            string strWhere = "";
            string text = this.txtKeyword.Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                strWhere = " WeiboMsg  like '%" + text + "%' ";
            }
            set = this.taskMsgBll.GetList(-1, strWhere, " CreateDate  desc");
            if (set != null)
            {
                this.gridView.DataSetSource = set;
            }
        }

        public void btnDeleteWeibo_Click()
        {
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

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow) ((Control) e.CommandSource).NamingContainer).RowIndex;
            int num1 = (int) this.gridView.DataKeys[rowIndex].Value;
            this.BindData();
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
            int weiBoTaskId = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.taskMsgBll.Delete(weiBoTaskId);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPostBack = base.IsPostBack;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x159;
            }
        }
    }
}

