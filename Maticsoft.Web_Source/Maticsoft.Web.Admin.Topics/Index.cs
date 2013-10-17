namespace Maticsoft.Web.Admin.Topics
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
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Index : PageBaseAdmin
    {
        protected int Act_AddData = 0x166;
        protected int Act_DelData = 0x167;
        private Maticsoft.BLL.Poll.Topics bll = new Maticsoft.BLL.Poll.Topics();
        protected Button btnAdd;
        protected Button btnBack;
        protected Button btnDelete;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected Label Label1;
        protected Label lblFormID;
        protected Label lblFormName;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected RadioButton radbtnTrueName;
        protected RadioButtonList radbtnType;
        protected RadioButton RadioButton2;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected TextBox txtKey;
        protected TextBox txtTitle;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[4].Visible = false;
            }
            string str = "";
            if ((this.Session["strWhereTopics"] != null) && (this.Session["strWhereTopics"].ToString() != ""))
            {
                str = str + this.Session["strWhereTopics"].ToString();
            }
            DataSet list = new DataSet();
            list = this.bll.GetList(str.ToString());
            this.gridView.DataSetSource = list;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string text = this.txtTitle.Text;
            int num = int.Parse(this.radbtnType.SelectedValue);
            int num2 = int.Parse(this.lblFormID.Text);
            Maticsoft.Model.Poll.Topics model = new Maticsoft.Model.Poll.Topics {
                Title = text,
                Type = new int?(num),
                FormID = new int?(num2)
            };
            Maticsoft.BLL.Poll.Topics topics2 = new Maticsoft.BLL.Poll.Topics();
            if (topics2.Exists(num2, text))
            {
                MessageBox.ShowFailTip(this, Poll.ErrorTopicExists);
            }
            else
            {
                if (topics2.Add(model) > 0)
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK);
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipSaveError);
                }
                this.Session["strWhereTopics"] = " FormID= " + this.lblFormID.Text;
                this.gridView.OnBind();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("../forms/Index.aspx");
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
            string str = this.txtKey.Text.Trim();
            string str2 = "";
            if (this.radbtnTrueName.Checked)
            {
                if (str != "")
                {
                    str2 = str2 + " and (Title like'%" + str + "%')";
                }
            }
            else if (str != "")
            {
                str2 = str2 + " and (ID =" + str + ")";
            }
            if (str2 != "")
            {
                this.Session["strWhereTopics"] = " FormID= " + this.lblFormID.Text + str2;
            }
            else
            {
                this.Session["strWhereTopics"] = " FormID= " + this.lblFormID.Text;
            }
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
            string str = this.gridView.DataKeys[e.RowIndex].Value.ToString();
            new Maticsoft.BLL.Poll.Topics().Delete(Convert.ToInt32(str));
            MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
            this.gridView.OnBind();
        }

        protected void gridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.btnAdd.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
                string str = base.Request.Params["fid"];
                if (!string.IsNullOrWhiteSpace(str) && PageValidate.IsNumber(str))
                {
                    Maticsoft.Model.Poll.Forms model = new Maticsoft.BLL.Poll.Forms().GetModel(Convert.ToInt32(str));
                    if (model != null)
                    {
                        this.lblFormID.Text = model.FormID.ToString();
                        this.Session["strWhereTopics"] = " FormID= " + model.FormID;
                        this.lblFormName.Text = model.Name;
                    }
                }
                if ((this.Session["Style"] != null) && (this.Session["Style"].ToString() != ""))
                {
                    string str2 = this.Session["Style"] + "xtable_bordercolorlight";
                    if ((base.Application[str2] != null) && (base.Application[str2].ToString() != ""))
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str2].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str2].ToString());
                    }
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x165;
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

