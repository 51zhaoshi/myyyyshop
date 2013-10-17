namespace Maticsoft.Web.Supplier.Products
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Wuqi.Webdiyer;

    public class ProductRecycle : PageBaseAdmin
    {
        protected int Act_AddData = 0x1dd;
        protected int Act_DelData = 0x1de;
        protected AspNetPager AspNetPager1;
        private Maticsoft.BLL.Shop.Products.ProductInfo bll = new Maticsoft.BLL.Shop.Products.ProductInfo();
        protected Button btnSearch;
        protected DataList DataListProduct;
        protected HtmlGenericControl liDel;
        protected HtmlGenericControl liDelAll;
        protected LinkButton LinkButton1;
        protected LinkButton LinkButton2;
        protected LinkButton LinkButton4;
        protected HtmlGenericControl liRevert;
        protected HtmlGenericControl liRevertAll;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal7;
        protected Literal Literal9;
        private Maticsoft.BLL.SysManage.TaskQueue taskBll = new Maticsoft.BLL.SysManage.TaskQueue();
        public static List<Maticsoft.Model.SysManage.TaskQueue> TaskList;
        protected TextBox txtKeyword;

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData();
        }

        public void BindData()
        {
            string str = InjectionFilter.SqlFilter(this.txtKeyword.Text.Trim());
            StringBuilder builder = new StringBuilder();
            builder.Append(" SaleStatus=2");
            if (!string.IsNullOrWhiteSpace(str))
            {
                builder.AppendFormat(" and ProductName like '%{0}%'", str);
            }
            this.AspNetPager1.RecordCount = this.bll.GetRecordCount(" SaleStatus=2");
            this.DataListProduct.DataSource = this.bll.GetListByPage(builder.ToString(), "AddedDate desc", this.AspNetPager1.StartRecordIndex, this.AspNetPager1.EndRecordIndex);
            this.DataListProduct.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (!string.IsNullOrWhiteSpace(selIDlist))
            {
                int result = 0;
                DataSet set = this.bll.DeleteProducts(selIDlist, out result);
                if ((set != null) && (set.Tables[0].Rows.Count > 0))
                {
                    this.PhysicalFileInfo(set.Tables[0]);
                }
            }
            this.BindData();
        }

        protected void btnRevert_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.bll.UpdateList(selIDlist, ProductSaleStatus.InStock);
                MessageBox.Show(this, "批量还原成功");
                this.BindData();
            }
        }

        protected void btnRevertAll_Click(object sender, EventArgs e)
        {
            if (this.bll.RevertAll())
            {
                MessageBox.ShowSuccessTip(this, "还原所有商品成功");
                this.BindData();
            }
            else
            {
                MessageBox.ShowFailTip(this, "还原所有商品失败");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void DataList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
            {
                HtmlGenericControl control = (HtmlGenericControl) e.Item.FindControl("btnRevert");
                control.Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                HtmlGenericControl control2 = (HtmlGenericControl) e.Item.FindControl("btnDel");
                control2.Visible = false;
            }
        }

        protected void DataList_RowCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Revert")
            {
                long productId = Convert.ToInt64(e.CommandArgument);
                if (this.bll.UpdateStatus(productId, 0))
                {
                    MessageBox.Show(this, "商品还原成功");
                    this.BindData();
                }
                else
                {
                    MessageBox.ShowFailTip(this, "商品还原失败");
                }
            }
            if (e.CommandName == "Delete")
            {
                string str = e.CommandArgument.ToString();
                if (!string.IsNullOrWhiteSpace(str) && PageValidate.IsDecimal(str))
                {
                    int result = 0;
                    DataSet set = this.bll.DeleteProducts(str, out result);
                    if ((set != null) && (set.Tables[0].Rows.Count > 0))
                    {
                        this.PhysicalFileInfo(set.Tables[0]);
                    }
                    MessageBox.Show(this, "商品删除成功");
                }
                this.BindData();
            }
        }

        private string DeleteAll()
        {
            JsonObject obj2 = new JsonObject();
            new StringBuilder();
            List<Maticsoft.Model.Shop.Products.ProductInfo> modelList = this.bll.GetModelList(" SaleStatus=2");
            this.taskBll.DeleteTask(1);
            TaskList = new List<Maticsoft.Model.SysManage.TaskQueue>();
            if ((modelList != null) && (modelList.Count > 0))
            {
                Maticsoft.Model.SysManage.TaskQueue model = null;
                int num = 1;
                foreach (Maticsoft.Model.Shop.Products.ProductInfo info in modelList)
                {
                    model = new Maticsoft.Model.SysManage.TaskQueue {
                        ID = num,
                        TaskId = (int) info.ProductId,
                        Status = 0,
                        Type = 1
                    };
                    if (!this.taskBll.Add(model))
                    {
                        break;
                    }
                    TaskList.Add(model);
                    num++;
                }
            }
            obj2.Put("STATUS", "SUCCESS");
            obj2.Put("DATA", modelList.Count);
            return obj2.ToString();
        }

        private void DeletePhysicalFile(string path)
        {
            FileHelper.DeleteFile(Maticsoft.Model.Ms.EnumHelper.AreaType.Shop, path);
        }

        private void DeleteProduct()
        {
            int TaskId = Globals.SafeInt(base.Request.Form["TaskId"], 0);
            Maticsoft.Model.SysManage.TaskQueue queue = TaskList.FirstOrDefault<Maticsoft.Model.SysManage.TaskQueue>(c => c.ID == TaskId);
            if (queue != null)
            {
                int result = 0;
                DataSet set = this.bll.DeleteProducts(queue.TaskId.ToString(), out result);
                if ((set != null) && (set.Tables[0].Rows.Count > 0))
                {
                    this.PhysicalFileInfo(set.Tables[0]);
                }
            }
        }

        private void DeleteTask()
        {
            this.taskBll.DeleteTask(1);
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
                if (!(str3 == "DeleteAll"))
                {
                    if (str3 == "DeleteProduct")
                    {
                        this.DeleteProduct();
                    }
                    else if (str3 == "DeleteTask")
                    {
                        this.DeleteTask();
                    }
                }
                else
                {
                    s = this.DeleteAll();
                }
            }
            base.Response.Write(s);
            base.Response.End();
        }

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.DataListProduct.Items.Count; i++)
            {
                CheckBox box = (CheckBox) this.DataListProduct.Items[i].FindControl("ckProduct");
                HiddenField field = (HiddenField) this.DataListProduct.Items[i].FindControl("hfProduct");
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    if (field.Value != null)
                    {
                        str = str + field.Value + ",";
                    }
                }
            }
            if (flag)
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
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
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.liRevertAll.Visible = false;
                    this.liRevert.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.liDel.Visible = false;
                    this.liDelAll.Visible = false;
                }
                if ((this.Session["Style"] != null) && (this.Session["Style"].ToString() != ""))
                {
                    string text1 = this.Session["Style"] + "xtable_bordercolorlight";
                }
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
                    if (((dt.Rows[i]["ImageURL"] != null) && (dt.Rows[i]["ImageURL"].ToString() != "")) && (dt.Rows[i]["ImageURL"].ToString() != "/Content/themes/base/Shop/images/none.png"))
                    {
                        this.DeletePhysicalFile(dt.Rows[i]["ImageURL"].ToString());
                    }
                    if (((dt.Rows[i]["ThumbnailUrl1"] != null) && (dt.Rows[i]["ThumbnailUrl1"].ToString() != "")) && (dt.Rows[i]["ThumbnailUrl1"].ToString() != "/Content/themes/base/Shop/images/none.png"))
                    {
                        this.DeletePhysicalFile(dt.Rows[i]["ThumbnailUrl1"].ToString());
                    }
                    if (((dt.Rows[i]["ThumbnailUrl2"] != null) && (dt.Rows[i]["ThumbnailUrl2"].ToString() != "")) && (dt.Rows[i]["ThumbnailUrl1"].ToString() != "/Content/themes/base/Shop/images/none.png"))
                    {
                        this.DeletePhysicalFile(dt.Rows[i]["ThumbnailUrl2"].ToString());
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
                return 0x1dc;
            }
        }
    }
}

