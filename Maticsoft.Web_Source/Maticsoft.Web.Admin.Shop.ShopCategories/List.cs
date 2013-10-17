namespace Maticsoft.Web.Admin.Shop.ShopCategories
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 0x20d;
        protected int Act_DelData = 0x20f;
        protected int Act_UpdateData = 0x20e;
        private Maticsoft.BLL.Shop.Products.CategoryInfo bll = new Maticsoft.BLL.Shop.Products.CategoryInfo();
        protected Button btnSearch;
        protected Button btnUpdateSeq;
        protected DropDownList ddCateList;
        protected GridView gridView;
        protected HtmlGenericControl liAdd;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected ScriptManager ScriptManager1;
        protected UpdatePanel UpdatePanel1;

        public void BindData()
        {
            Func<Maticsoft.Model.Shop.Products.CategoryInfo, bool> predicate = null;
            List<Maticsoft.Model.Shop.Products.CategoryInfo> allCateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> orderList = new List<Maticsoft.Model.Shop.Products.CategoryInfo>();
            int cateId = Globals.SafeInt(this.ddCateList.SelectedValue, 0);
            List<Maticsoft.Model.Shop.Products.CategoryInfo> list3 = new List<Maticsoft.Model.Shop.Products.CategoryInfo>();
            if (cateId == 0)
            {
                list3 = (from c in allCateList
                    where c.Depth == 1
                    orderby c.DisplaySequence
                    select c).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
            }
            else
            {
                if (predicate == null)
                {
                    predicate = c => c.CategoryId == cateId;
                }
                list3 = allCateList.Where<Maticsoft.Model.Shop.Products.CategoryInfo>(predicate).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
            }
            foreach (Maticsoft.Model.Shop.Products.CategoryInfo info in list3)
            {
                orderList = this.CateOrder(info, allCateList, orderList);
            }
            this.gridView.DataSource = orderList;
            this.gridView.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void btnUpdateSeq_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                int cid = Globals.SafeInt(this.gridView.DataKeys[i].Value.ToString(), 0);
                TextBox box = (TextBox) this.gridView.Rows[i].FindControl("TextBox1");
                int seq = Globals.SafeInt(box.Text, 0);
                if ((cid > 0) && (seq > 0))
                {
                    this.bll.UpdateSeqByCid(seq, cid);
                }
            }
            base.Cache.Remove("GetAllCateList-CateList");
        }

        private List<Maticsoft.Model.Shop.Products.CategoryInfo> CateOrder(Maticsoft.Model.Shop.Products.CategoryInfo model, List<Maticsoft.Model.Shop.Products.CategoryInfo> AllCateList, List<Maticsoft.Model.Shop.Products.CategoryInfo> orderList)
        {
            Func<Maticsoft.Model.Shop.Products.CategoryInfo, bool> predicate = null;
            orderList.Add(model);
            if (model.HasChildren)
            {
                if (predicate == null)
                {
                    predicate = c => c.ParentCategoryId == model.CategoryId;
                }
                foreach (Maticsoft.Model.Shop.Products.CategoryInfo info in from c in AllCateList.Where<Maticsoft.Model.Shop.Products.CategoryInfo>(predicate)
                    orderby c.DisplaySequence
                    select c)
                {
                    this.CateOrder(info, AllCateList, orderList);
                }
            }
            return orderList;
        }

        private void DeletePhysicalFile(string path)
        {
            FileHelper.DeleteFile(EnumHelper.AreaType.Shop, path);
        }

        private void DoCallback()
        {
            string str3;
            string str = base.Request.Form["Action"];
            base.Response.Clear();
            base.Response.ContentType = "application/json";
            string s = string.Empty;
            if (((str3 = str) != null) && (str3 == "UpdateSeqNum"))
            {
                s = this.UpdateSeqNum();
            }
            else
            {
                s = this.UpdateSeqNum();
            }
            base.Response.Write(s);
            base.Response.End();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.DataBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow) ((Control) e.CommandSource).NamingContainer).RowIndex;
            int categoryId = (int) this.gridView.DataKeys[rowIndex].Value;
            if (e.CommandName == "Fall")
            {
                this.bll.SwapCategorySequence(categoryId, SwapSequenceIndex.Up);
            }
            if (e.CommandName == "Rise")
            {
                this.bll.SwapCategorySequence(categoryId, SwapSequenceIndex.Down);
            }
            if (e.CommandName == "UpdateSeq")
            {
                TextBox box = (TextBox) this.gridView.Rows[rowIndex].FindControl("TextBox1");
                int seq = Globals.SafeInt(box.Text, 0);
                this.bll.UpdateSeqByCid(seq, categoryId);
            }
            base.Cache.Remove("GetAllCateList-CateList");
            this.BindData();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    LinkButton button = (LinkButton) e.Row.FindControl("linkDel");
                    button.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    HtmlGenericControl control = (HtmlGenericControl) e.Row.FindControl("lbtnModify");
                    control.Visible = false;
                }
                int num = (int) DataBinder.Eval(e.Row.DataItem, "Depth");
                string str = DataBinder.Eval(e.Row.DataItem, "Name").ToString();
                e.Row.Cells[0].CssClass = "productcag" + num.ToString();
                if (num != 1)
                {
                    HtmlGenericControl control2 = e.Row.FindControl("spShowImage") as HtmlGenericControl;
                    control2.Visible = false;
                }
                Label label = e.Row.FindControl("lblName") as Label;
                label.Text = str;
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int num;
            DataSet set = this.bll.DeleteCategory((int) this.gridView.DataKeys[e.RowIndex].Value, out num);
            if (set != null)
            {
                this.PhysicalFileInfo(set.Tables[0]);
            }
            MessageBox.ShowSuccessTip(this, "删除成功！");
            base.Cache.Remove("GetAllCateList-CateList");
            this.BindData();
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
                if ((this.Session["Style"] != null) && (this.Session["Style"].ToString() != ""))
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.liAdd.Visible = false;
                }
                List<Maticsoft.Model.Shop.Products.CategoryInfo> list2 = (from c in Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList()
                    where c.Depth == 1
                    orderby c.DisplaySequence
                    select c).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
                this.ddCateList.DataSource = list2;
                this.ddCateList.DataTextField = "Name";
                this.ddCateList.DataValueField = "CategoryId";
                this.ddCateList.DataBind();
                this.ddCateList.Items.Insert(0, new ListItem("全部", "0"));
                this.BindData();
            }
        }

        private void PhysicalFileInfo(DataTable dt)
        {
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (((dt.Rows[i]["ImageUrl"] != null) && (dt.Rows[i]["ImageUrl"].ToString() != "")) && (dt.Rows[i]["ImageUrl"].ToString() != "/Content/themes/base/Shop/images/none.png"))
                    {
                        this.DeletePhysicalFile(dt.Rows[i]["ImageUrl"].ToString());
                    }
                }
            }
        }

        private bool RegURL(string path)
        {
            Regex regex = new Regex("^[a-zA-z]+://(//w+(-//w+)*)(//.(//w+(-//w+)*))*(//?//S*)?$");
            return regex.Match(path).Success;
        }

        private string UpdateSeqNum()
        {
            JsonObject obj2 = new JsonObject();
            int cid = Globals.SafeInt(base.Request.Form["CategoryId"], 0);
            int seq = Globals.SafeInt(base.Request.Form["UpdateValue"], 0);
            if ((cid == 0) || (seq == 0))
            {
                obj2.Put("STATUS", "FAILED");
            }
            else if (this.bll.UpdateSeqByCid(seq, cid))
            {
                base.Cache.Remove("GetAllCateList-CateList");
                obj2.Put("STATUS", "SUCCESS");
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            return obj2.ToString();
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x20c;
            }
        }
    }
}

