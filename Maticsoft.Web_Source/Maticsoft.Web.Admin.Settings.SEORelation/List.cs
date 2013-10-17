namespace Maticsoft.Web.Admin.Settings.SEORelation
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Model.Settings;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 0x18a;
        protected int Act_DelData = 0x18c;
        protected int Act_UpdateData = 0x18b;
        private Maticsoft.BLL.Settings.SEORelation bll = new Maticsoft.BLL.Settings.SEORelation();
        protected Button btnDelete;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected HtmlGenericControl liAdd;
        protected HtmlGenericControl liDel;
        protected Literal Literal2;
        protected TextBox txtKeyword;

        public void BindData()
        {
            DataSet list = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.txtKeyword.Text.Trim() != "")
            {
                builder.AppendFormat("KeyName like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            list = this.bll.GetList(builder.ToString());
            this.gridView.DataSetSource = list;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.bll.DeleteList(selIDlist);
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
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
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    LinkButton button = (LinkButton) e.Row.FindControl("lbtnDel");
                    button.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    LinkButton button2 = (LinkButton) e.Row.FindControl("lbtnModify");
                    button2.Visible = false;
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int relationID = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.Delete(relationID);
            this.gridView.OnBind();
        }

        public void gridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gridView.EditIndex = e.NewEditIndex;
            this.gridView.OnBind();
        }

        public void gridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int num = (int) this.gridView.DataKeys[e.RowIndex].Value;
            string text = (this.gridView.Rows[e.RowIndex].Cells[0].Controls[0] as TextBox).Text;
            string str2 = (this.gridView.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox).Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.ShowFailTip(this, "请输入链接文字！");
                e.Cancel = true;
                this.gridView.OnBind();
            }
            else if (string.IsNullOrWhiteSpace(str2))
            {
                MessageBox.ShowFailTip(this, "请输入链接地址！");
                e.Cancel = true;
                this.gridView.OnBind();
            }
            else
            {
                CheckBox box = this.gridView.Rows[e.RowIndex].FindControl("IsCMS") as CheckBox;
                bool flag = false;
                if (box != null)
                {
                    flag = box.Checked;
                }
                CheckBox box2 = this.gridView.Rows[e.RowIndex].FindControl("IsShop") as CheckBox;
                bool flag2 = false;
                if (box2 != null)
                {
                    flag2 = box2.Checked;
                }
                CheckBox box3 = this.gridView.Rows[e.RowIndex].FindControl("IsSNS") as CheckBox;
                bool flag3 = false;
                if (box3 != null)
                {
                    flag3 = box3.Checked;
                }
                CheckBox box4 = this.gridView.Rows[e.RowIndex].FindControl("IsComment") as CheckBox;
                bool flag4 = false;
                if (box4 != null)
                {
                    flag4 = box4.Checked;
                }
                CheckBox box5 = this.gridView.Rows[e.RowIndex].FindControl("IsActive") as CheckBox;
                bool flag5 = false;
                if (box5 != null)
                {
                    flag5 = box5.Checked;
                }
                Maticsoft.Model.Settings.SEORelation model = new Maticsoft.Model.Settings.SEORelation {
                    RelationID = num,
                    KeyName = text,
                    LinkURL = str2,
                    IsCMS = flag,
                    IsShop = flag2,
                    IsSNS = flag3,
                    IsComment = flag4,
                    IsActive = flag5
                };
                this.bll.Update(model);
                this.gridView.EditIndex = -1;
                this.gridView.OnBind();
            }
        }

        private bool IsUrl(string s)
        {
            string pattern = @"^(http|https|ftp|rtsp|mms):(\/\/|\\\\)[A-Za-z0-9%\-_@]+\.[A-Za-z0-9%\-_@]+[A-Za-z0-9\.\/=\?%\-&_~`@:\+!;]*$";
            return Regex.IsMatch(s, pattern, RegexOptions.IgnoreCase);
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
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x188;
            }
        }
    }
}

