namespace Maticsoft.Web.Admin.Advertisement
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.Settings;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class SingleList : PageBaseAdmin
    {
        protected int Act_AddData = 0xab;
        protected int Act_DelData = 0xad;
        protected int Act_DeleteList = 0xae;
        protected int Act_UpdateData = 0xac;
        private string AdHeight = "0";
        public string ADPositionId = "0";
        private string AdWidth = "0";
        private Maticsoft.BLL.Settings.Advertisement bll = new Maticsoft.BLL.Settings.Advertisement();
        protected Button btnDelete;
        protected GridViewEx gridView;
        protected HtmlGenericControl liAdd;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal3;
        protected Literal litTitle;

        private void AltInfo()
        {
            Maticsoft.Model.Settings.AdvertisePosition model = new Maticsoft.BLL.Settings.AdvertisePosition().GetModel(this.AdPositionID);
            if (model != null)
            {
                this.litTitle.Text = model.AdvPositionName;
                this.AdWidth = model.Width.HasValue ? model.Width.Value.ToString() : "autopx";
                this.AdHeight = model.Height.HasValue ? model.Height.Value.ToString() : "autopx";
            }
        }

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[9].Visible = false;
            }
            DataSet list = new DataSet();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" AdvPositionId={0}", this.AdPositionID);
            list = this.bll.GetList(builder.ToString());
            this.gridView.DataSetSource = list;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteList(selIDlist))
                {
                    MessageBox.ShowSuccessTip(this, "删除成功！");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "删除失败！");
                }
                string url = string.Format("SingleList.aspx?id={0}", this.AdPositionID);
                base.Response.Redirect(url);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected string ConvertContentType(object obj)
        {
            if (obj != null)
            {
                switch (obj.ToString())
                {
                    case "0":
                        return "文字";

                    case "1":
                        return "图片";

                    case "2":
                        return "Flash";

                    case "3":
                        return "Html代码";
                }
            }
            return "未定义的显示类型";
        }

        public string GetEnName(object obj)
        {
            if (obj != null)
            {
                int enterpriseID = Globals.SafeInt(obj.ToString(), 0);
                Maticsoft.Model.Ms.Enterprise model = new Maticsoft.BLL.Ms.Enterprise().GetModel(enterpriseID);
                if (model != null)
                {
                    return model.Name;
                }
            }
            return "广告主不存在";
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
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }
            Literal literal = (Literal) e.Row.FindControl("litContent");
            Literal literal2 = (Literal) e.Row.FindControl("litState");
            if (literal2 != null)
            {
                string str = DataBinder.Eval(e.Row.DataItem, "State").ToString();
                if (str == null)
                {
                    goto Label_00C8;
                }
                if (!(str == "0"))
                {
                    if (str == "1")
                    {
                        literal2.Text = "有效";
                        goto Label_00D3;
                    }
                    if (str == "-1")
                    {
                        literal2.Text = "欠费停止";
                        goto Label_00D3;
                    }
                    goto Label_00C8;
                }
                literal2.Text = "无效";
            }
            goto Label_00D3;
        Label_00C8:
            literal2.Text = "";
        Label_00D3:
            if (literal == null)
            {
                return;
            }
            HiddenField field = (HiddenField) e.Row.FindControl("hfShowType");
            switch (field.Value)
            {
                case "0":
                    literal.Text = "<div style=\"height:70;width:110;\" >" + DataBinder.Eval(e.Row.DataItem, "AlternateText").ToString() + "</div>";
                    return;

                case "1":
                    literal.Text = "<div style=\"height:70;width:110;\" ><img src=\"" + DataBinder.Eval(e.Row.DataItem, "FileUrl").ToString() + "\" height=\"70\" width=\"110\" /></div>";
                    return;

                case "2":
                    literal.Text = "<div style=\"height:70;width:110;\" ><a href='" + DataBinder.Eval(e.Row.DataItem, "FileUrl").ToString() + "'><img src=\"/admin/images/logo.gif\" height=\"70\" width=\"110\" /></a></div>";
                    return;

                case "3":
                    if (DataBinder.Eval(e.Row.DataItem, "AdvHtml").ToString().Contains("baidu"))
                    {
                        literal.Text = "百度广告脚本";
                        return;
                    }
                    if (DataBinder.Eval(e.Row.DataItem, "AdvHtml").ToString().Contains("google"))
                    {
                        literal.Text = "谷歌广告脚本";
                        return;
                    }
                    literal.Text = "自定义广告脚本";
                    return;
            }
            literal.Text = "";
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ADPositionId = this.AdPositionID.ToString();
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DeleteList)) && (base.GetPermidByActID(this.Act_DeleteList) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.liAdd.Visible = false;
                }
                this.AltInfo();
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
                return 370;
            }
        }

        public int AdPositionID
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["id"]))
                {
                    num = Globals.SafeInt(base.Request.Params["id"], 0);
                }
                return num;
            }
        }
    }
}

