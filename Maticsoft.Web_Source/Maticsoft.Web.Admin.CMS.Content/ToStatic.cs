namespace Maticsoft.Web.Admin.CMS.Content
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Maticsoft.Web.Components.Setting.CMS;
    using Resources;
    using System;
    using System.Collections;
    using System.Data;
    using System.Web;
    using System.Web.UI.WebControls;

    public class ToStatic : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.Content bll = new Maticsoft.BLL.CMS.Content();
        protected Button btnIndex;
        protected Button Button3;
        protected DropDownList ddlArticleUrl;
        protected DropDownList ddlClassUrl;
        protected DropDownList dropParentID;
        protected Literal Literal3;
        protected Literal Literal4;
        protected RadioButtonList radlStatus;
        private Maticsoft.BLL.SysManage.TaskQueue taskBll = new Maticsoft.BLL.SysManage.TaskQueue();
        protected TextBox txtCMSRoot;
        protected HiddenField txtFrom;
        protected HiddenField txtIsStatic;
        protected HiddenField txtTaskCount;
        protected Literal txtTaskDate;
        protected Literal txtTaskId;
        protected Literal txtTaskReCount;
        protected HiddenField txtTo;

        private void BindNode(int parentid, DataTable dt, string blank)
        {
            foreach (DataRow row in dt.Select("ParentID= " + parentid))
            {
                string str = row["ClassID"].ToString();
                string text = row["ClassName"].ToString();
                text = blank + "『" + text + "』";
                this.dropParentID.Items.Add(new ListItem(text, str));
                int num = int.Parse(str);
                string str3 = blank + "─";
                this.BindNode(num, dt, str3);
            }
        }

        private void BindTree()
        {
            this.dropParentID.Items.Clear();
            this.dropParentID.Items.Add(new ListItem(Site.All, ""));
            DataSet treeList = new ContentClass().GetTreeList("");
            if (!DataSetTools.DataSetIsNull(treeList))
            {
                DataTable dt = treeList.Tables[0];
                if (!DataTableTools.DataTableIsNull(dt))
                {
                    foreach (DataRow row in dt.Select("ParentID= " + 0))
                    {
                        string str = row["ClassID"].ToString();
                        string text = row["ClassName"].ToString();
                        row["ParentID"].ToString();
                        text = "╋" + text;
                        this.dropParentID.Items.Add(new ListItem(text, str));
                        int parentid = int.Parse(str);
                        string blank = "├";
                        this.BindNode(parentid, dt, blank);
                    }
                }
            }
            this.dropParentID.DataBind();
        }

        protected void btnIndex_Click(object sender, EventArgs e)
        {
            string virtualRequestUrl = "/Home/Index?type=1";
            if (GenerateHtml.HttpToStatic(virtualRequestUrl, "/index.html"))
            {
                MessageBox.ShowSuccessTip(this, "首页静态生成成功", "ToStatic.aspx");
            }
            else
            {
                MessageBox.ShowSuccessTip(this, "首页静态生成失败，请重试", "ToStatic.aspx");
            }
        }

        protected void btnRuleSet_Click(object sender, EventArgs e)
        {
            IDictionaryEnumerator enumerator = base.Cache.GetEnumerator();
            ArrayList list = new ArrayList();
            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Key.ToString());
            }
            foreach (string str in list)
            {
                base.Cache.Remove(str);
            }
            string[] keywords = new string[] { "/cms/", "/sns/", "/shop/", "/area/", "/tao/", "/com/", "/handlers/", "/content/", "/upload/", "/uploadfolder/" };
            string values = this.txtCMSRoot.Text.Trim();
            if (PageSetting.IsHasKeyword(values, keywords))
            {
                MessageBox.ShowFailTip(this, "填的静态化根目录中含有网站关键字，请重新填写");
            }
            else
            {
                if (!ConfigSystem.Exists("ArticleStaticRoot"))
                {
                    ConfigSystem.Add("ArticleStaticRoot", values, "文章静态化根目录地址，为空就默认为根目录下", ApplicationKeyType.CMS);
                }
                else
                {
                    ConfigSystem.Update("ArticleStaticRoot", values, "文章静态化根目录地址，为空就默认为根目录下");
                }
                if (!ConfigSystem.Exists("CMS_Static_ClassRule"))
                {
                    ConfigSystem.Add("CMS_Static_ClassRule", this.ddlClassUrl.SelectedValue, "文章静态化栏目URL规则，0：表示使用ID，1：表示使用栏目名称拼音，2：表示使用自定义优化", ApplicationKeyType.CMS);
                }
                else
                {
                    ConfigSystem.Update("CMS_Static_ClassRule", this.ddlClassUrl.SelectedValue, "文章静态化栏目URL规则，0：表示使用ID，1：表示使用栏目名称拼音，2：表示使用自定义优化");
                }
                if (!ConfigSystem.Exists("CMS_Static_ContentRule"))
                {
                    ConfigSystem.Add("CMS_Static_ContentRule", this.ddlArticleUrl.SelectedValue, "文章静态化内容URL规则，0：表示使用ID，1：表示使用文章名称拼音，2：表示使用自定义优化", ApplicationKeyType.CMS);
                }
                else
                {
                    ConfigSystem.Update("CMS_Static_ContentRule", this.ddlArticleUrl.SelectedValue, "文章静态化内容URL规则，0：表示使用ID，1：表示使用文章名称拼音，2：表示使用自定义优化");
                }
                MessageBox.ShowSuccessTip(this, "设置成功", "ToStatic.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindTree();
                this.txtTaskCount.Value = this.taskBll.GetRecordCount(" type=" + 0).ToString();
                this.txtTaskReCount.Text = this.taskBll.GetRecordCount(" type=" + 0 + " and Status=0").ToString();
                Maticsoft.Model.SysManage.TaskQueue lastModel = this.taskBll.GetLastModel(0);
                if (lastModel != null)
                {
                    this.txtTaskDate.Text = lastModel.RunDate.Value.ToString("yyyy-MM-dd");
                    this.txtTaskId.Text = (lastModel.ID + 1).ToString();
                }
                string valueByCache = ConfigSystem.GetValueByCache("ArticleIsStatic");
                this.radlStatus.SelectedValue = valueByCache;
                this.txtIsStatic.Value = valueByCache;
                this.txtCMSRoot.Text = ConfigSystem.GetValueByCache("ArticleStaticRoot");
                this.ddlClassUrl.SelectedValue = ConfigSystem.GetValueByCache("CMS_Static_ClassRule");
                this.ddlArticleUrl.SelectedValue = ConfigSystem.GetValueByCache("CMS_Static_ContentRule");
            }
        }

        protected void radlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();
            ArrayList list = new ArrayList();
            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Key.ToString());
            }
            foreach (string str in list)
            {
                HttpContext.Current.Cache.Remove(str);
            }
            string selectedValue = this.radlStatus.SelectedValue;
            if (ConfigSystem.Exists("ArticleIsStatic"))
            {
                ConfigSystem.Update("ArticleIsStatic", selectedValue, "CMS 文章是否静态化，true表示静态化，false表示不需要静态化");
            }
            else
            {
                ConfigSystem.Add("ArticleIsStatic", selectedValue, "CMS 文章是否静态化，true表示静态化，false表示不需要静态化");
            }
            base.Response.Redirect("ToStatic.aspx");
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xea;
            }
        }
    }
}

