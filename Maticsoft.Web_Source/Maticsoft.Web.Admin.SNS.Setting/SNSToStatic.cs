namespace Maticsoft.Web.Admin.SNS.Setting
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using System;
    using System.Collections;
    using System.Web;
    using System.Web.UI.WebControls;

    public class SNSToStatic : PageBaseAdmin
    {
        protected Button Button5;
        protected CheckBox ckAlbum;
        protected CheckBox ckGroup;
        protected CheckBox ckIndex;
        protected HiddenField Hidden_SelectValue;
        protected Literal Literal3;
        protected Literal Literal4;
        protected RadioButtonList radlStatus;
        private Maticsoft.BLL.SysManage.TaskQueue taskBll = new Maticsoft.BLL.SysManage.TaskQueue();
        protected TextBox txtFrom;
        protected HiddenField txtIsStatic;
        protected HiddenField txtTaskCount;
        protected HiddenField txtTaskCount_C;
        protected Literal txtTaskDate;
        protected Literal txtTaskDate_C;
        protected Literal txtTaskId;
        protected Literal txtTaskId_C;
        protected Literal txtTaskReCount;
        protected Literal txtTaskReCount_C;
        protected TextBox txtTo;

        protected void btnIndex_Click(object sender, EventArgs e)
        {
            string virtualRequestUrl = "";
            string str2 = "";
            string str3 = "";
            bool flag = true;
            if (this.ckIndex.Checked)
            {
                if (Maticsoft.Components.MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    virtualRequestUrl = "/Home/Index?RequestType=1";
                    str2 = "/Group/Index";
                    str3 = "/Album/Index";
                }
                else
                {
                    virtualRequestUrl = "/SNS/Home/Index?RequestType=1";
                    str2 = "/SNS/Group/Index";
                    str3 = "/SNS/Album/Index";
                }
                if (!GenerateHtml.HttpToStatic(virtualRequestUrl, "/index.html"))
                {
                    flag = false;
                }
                if (!GenerateHtml.HttpToStatic(str2, "/group.html"))
                {
                    flag = false;
                }
                if (!GenerateHtml.HttpToStatic(str3, "/album.html"))
                {
                    flag = false;
                }
                if (flag)
                {
                    MessageBox.ShowSuccessTip(this, "静态生成成功", "SNSToStatic.aspx");
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, "静态生成失败，请重试", "SNSToStatic.aspx");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.txtTaskCount.Value = this.taskBll.GetRecordCount(" type=" + 4).ToString();
                this.txtTaskReCount.Text = this.taskBll.GetRecordCount(" type=" + 4 + " and Status=0").ToString();
                Maticsoft.Model.SysManage.TaskQueue lastModel = this.taskBll.GetLastModel(4);
                if (lastModel != null)
                {
                    this.txtTaskDate.Text = lastModel.RunDate.Value.ToString("yyyy-MM-dd");
                    this.txtTaskId.Text = (lastModel.ID + 1).ToString();
                }
                else
                {
                    this.txtTaskDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    this.txtTaskId.Text = "1";
                }
                this.txtTaskCount_C.Value = this.taskBll.GetRecordCount(" type=" + 5).ToString();
                this.txtTaskReCount_C.Text = this.taskBll.GetRecordCount(" type=" + 5 + " and Status=0").ToString();
                Maticsoft.Model.SysManage.TaskQueue queue2 = this.taskBll.GetLastModel(5);
                if (queue2 != null)
                {
                    this.txtTaskDate_C.Text = queue2.RunDate.Value.ToString("yyyy-MM-dd");
                    this.txtTaskId_C.Text = (queue2.ID + 1).ToString();
                }
                else
                {
                    this.txtTaskDate_C.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    this.txtTaskId_C.Text = "1";
                }
                string str = ConfigSystem.GetValue("SNSIsStatic");
                this.radlStatus.SelectedValue = str;
                this.txtIsStatic.Value = str;
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
            if (ConfigSystem.Exists("SNSIsStatic"))
            {
                ConfigSystem.Update("SNSIsStatic", selectedValue, "是否静态化，true表示静态化，false表示不需要静态化");
            }
            else
            {
                ConfigSystem.Add("SNSIsStatic", selectedValue, "是否静态化，true表示静态化，false表示不需要静态化", ApplicationKeyType.System);
            }
            base.Response.Redirect("SNSToStatic.aspx");
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x265;
            }
        }
    }
}

