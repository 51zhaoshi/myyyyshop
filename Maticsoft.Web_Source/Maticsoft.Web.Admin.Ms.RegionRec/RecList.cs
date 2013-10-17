namespace Maticsoft.Web.Admin.Ms.RegionRec
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class RecList : PageBaseAdmin
    {
        protected DropDownList ddRegion;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal11;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        private Maticsoft.BLL.Ms.RegionRec recBll = new Maticsoft.BLL.Ms.RegionRec();
        private Maticsoft.BLL.Ms.Regions regionBll = new Maticsoft.BLL.Ms.Regions();

        public void BindData()
        {
            int num = Globals.SafeInt(this.ddRegion.SelectedValue, 0);
            if (num > 0)
            {
                this.gridView.DataSource = this.regionBll.GetList(" ParentId=" + num);
            }
            this.gridView.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.recBll.DeleteList(selIDlist))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipDelError);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void ddRegion_Change(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        private string Delete()
        {
            JsonObject obj2 = new JsonObject();
            int iD = Globals.SafeInt(base.Request.Form["RecId"], 0);
            if (this.recBll.Delete(iD))
            {
                obj2.Put("STATUS", "SUCCESS");
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            return obj2.ToString();
        }

        private void DoCallback()
        {
            string str = base.Request.Form["Action"];
            base.Response.Clear();
            base.Response.ContentType = "application/json";
            string s = string.Empty;
            string str3 = str;
            if (str3 != null)
            {
                if (!(str3 == "GetRecList"))
                {
                    if (str3 == "Delete")
                    {
                        s = this.Delete();
                    }
                }
                else
                {
                    s = this.GetRecList();
                }
            }
            base.Response.Write(s);
            base.Response.End();
        }

        private string GetRecList()
        {
            JsonObject obj2 = new JsonObject();
            List<Maticsoft.Model.Ms.RegionRec> modelList = this.recBll.GetModelList(" type=0");
            string str = "";
            if ((modelList != null) && (modelList.Count > 0))
            {
                int num = 0;
                foreach (Maticsoft.Model.Ms.RegionRec rec in modelList)
                {
                    if (num == 0)
                    {
                        str = rec.ID + "," + rec.RegionName;
                    }
                    else
                    {
                        str = string.Concat(new object[] { str, "|", rec.ID, ",", rec.RegionName });
                    }
                    num++;
                }
            }
            obj2.Put("STATUS", "SUCCESS");
            obj2.Put("DATA", str);
            return obj2.ToString();
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
            if (e.CommandName == "RegionRec")
            {
                int regionId = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                if (this.recBll.AddEx(regionId, 0) <= 0)
                {
                    MessageBox.ShowSuccessTip(this, "推荐失败！");
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
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.recBll.Delete((int) this.gridView.DataKeys[e.RowIndex].Value))
            {
                this.gridView.OnBind();
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(base.Request.Form["Callback"]) && (base.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!this.Page.IsPostBack)
            {
                if (this.Session["Style"] != null)
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if (base.Application[str] != null)
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
                this.ddRegion.DataSource = this.regionBll.GetPrivoces();
                this.ddRegion.DataTextField = "RegionName";
                this.ddRegion.DataValueField = "RegionId";
                this.ddRegion.DataBind();
                this.ddRegion.Items.Insert(0, new ListItem("请选择", ""));
                this.gridView.OnBind();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x144;
            }
        }
    }
}

