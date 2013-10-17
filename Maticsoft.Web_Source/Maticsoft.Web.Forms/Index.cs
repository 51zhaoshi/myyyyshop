namespace Maticsoft.Web.Forms
{
    using Maticsoft.BLL.Poll;
    using Maticsoft.Common;
    using Maticsoft.Model.Poll;
    using Maticsoft.Web;
    using Maticsoft.Web.Admin;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Index : PageBaseAdmin
    {
        protected int Act_AddData = 0x15f;
        protected int Act_DelData = 0x161;
        protected int Act_UpdateData = 0x160;
        private Maticsoft.BLL.Poll.Forms bll = new Maticsoft.BLL.Poll.Forms();
        protected Button btnAdd;
        protected Button btnDelete;
        protected CheckBox chkIsActive;
        protected GridViewEx gridView;
        protected Label Label1;
        protected HtmlGenericControl liAdd;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal8;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected RequiredFieldValidator RequiredFieldValidator2;
        protected TextBox txtDescription;
        protected TextBox txtName;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[5].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[7].Visible = false;
            }
            DataSet list = new DataSet();
            list = this.bll.GetList(new StringBuilder().ToString());
            this.gridView.DataSetSource = list;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                MessageBox.ShowFailTip(this, Poll.ErrorFormsNameNull);
            }
            else if (string.IsNullOrWhiteSpace(this.txtDescription.Text))
            {
                MessageBox.ShowFailTip(this, Poll.ErrorFormsExplainNull);
            }
            else
            {
                string text = this.txtName.Text;
                string str2 = this.txtDescription.Text;
                Maticsoft.Model.Poll.Forms model = new Maticsoft.Model.Poll.Forms {
                    Name = text,
                    Description = str2,
                    IsActive = this.chkIsActive.Checked
                };
                Maticsoft.BLL.Poll.Forms forms2 = new Maticsoft.BLL.Poll.Forms();
                if (forms2.Add(model) > 0)
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipAddSuccess);
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipSaveError);
                }
                this.gridView.OnBind();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteList(selIDlist))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipDelError);
                }
                this.gridView.OnBind();
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
            int formID = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.Delete(formID);
            this.gridView.OnBind();
        }

        public string IsActive(object obj)
        {
            if (obj != null)
            {
                if (string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    return "否";
                }
                if (Convert.ToBoolean(obj))
                {
                    return "是";
                }
            }
            return "否";
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
                    this.btnAdd.Visible = false;
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
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 350;
            }
        }

        public Basic Master
        {
            get
            {
                return (Basic) base.Master;
            }
        }
    }
}

