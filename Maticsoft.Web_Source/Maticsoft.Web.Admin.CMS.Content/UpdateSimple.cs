namespace Maticsoft.Web.Admin.CMS.Content
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Maticsoft.Web.Validator;
    using Resources;
    using System;
    using System.Collections;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class UpdateSimple : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.Content bll = new Maticsoft.BLL.CMS.Content();
        private Maticsoft.BLL.CMS.ContentClass bllContentClass = new Maticsoft.BLL.CMS.ContentClass();
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkQQ;
        protected CheckBox chkSina;
        protected DropDownList dropClass;
        protected HiddenField hfClassID;
        protected HiddenField HiddenField_ICOPath;
        protected HiddenField HiddenField_IsDeleteAttachment;
        protected HiddenField HiddenField_IsModifyAttachment;
        protected HiddenField HiddenField_ISModifyImage;
        protected HiddenField HiddenField_OldAttachPath;
        protected Image imgUrl;
        protected Literal Literal1;
        protected Literal Literal14;
        protected Literal Literal2;
        protected Literal Literal20;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected RadioButtonList radlState;
        public string strClassID = string.Empty;
        protected HtmlTableRow trImageUrl;
        protected HtmlTableRow TrSummary;
        protected TextBox txtContent;
        protected TextBox txtSummary;
        protected TextBox txtTitle;
        protected HtmlGenericControl txtTitleTip;
        protected ValidateTarget ValidateTargetName;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        private void BindNode(int parentid, DataTable dt, string blank)
        {
            foreach (DataRow row in dt.Select("ParentID= " + parentid))
            {
                string str = Globals.SafeString(row["ClassID"], "0");
                string text = Globals.SafeString(row["ClassName"], "0");
                text = blank + "『" + text + "』";
                this.dropClass.Items.Add(new ListItem(text, str));
                int num = int.Parse(str);
                string str3 = blank + "─";
                this.BindNode(num, dt, str3);
            }
        }

        private void BindTree()
        {
            this.dropClass.Items.Clear();
            DataSet treeList = this.bllContentClass.GetTreeList("");
            if (!DataSetTools.DataSetIsNull(treeList))
            {
                DataTable dt = treeList.Tables[0];
                if (!DataTableTools.DataTableIsNull(dt))
                {
                    foreach (DataRow row in dt.Select("ParentID= " + 0))
                    {
                        string str = Globals.SafeString(row["ClassID"], "0");
                        string text = Globals.SafeString(row["ClassName"], "0");
                        Globals.SafeString(row["ParentID"], "0");
                        text = "╋" + text;
                        this.dropClass.Items.Add(new ListItem(text, str));
                        int parentid = int.Parse(str);
                        string blank = "├";
                        this.BindNode(parentid, dt, blank);
                    }
                }
            }
            this.dropClass.DataBind();
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("ListSimple.aspx?type=0");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int classID = Globals.SafeInt(this.dropClass.SelectedValue, 0);
            Maticsoft.Model.CMS.ContentClass modelByCache = this.bllContentClass.GetModelByCache(classID);
            if (string.IsNullOrWhiteSpace(this.txtTitle.Text.Trim()))
            {
                MessageBox.ShowFailTip(this, CMS.TitleErrorAddContent);
            }
            else if ((modelByCache != null) && !modelByCache.AllowAddContent)
            {
                MessageBox.ShowFailTip(this, CMS.ContentErrorAddContent);
            }
            else if (this.ContentID > 0)
            {
                Maticsoft.Model.CMS.Content model = this.bll.GetModel(this.ContentID);
                if (model == null)
                {
                    MessageBox.ShowServerBusyTip(this, CMS.ContentErrorNoContent, "ListSimple.aspx");
                }
                model.Title = Globals.HtmlEncode(this.txtTitle.Text);
                model.Summary = Globals.HtmlEncode(this.txtSummary.Text);
                model.LastEditUserID = new int?(base.CurrentUser.UserID);
                model.LastEditDate = new DateTime?(DateTime.Now);
                model.State = int.Parse(this.radlState.SelectedValue);
                model.ClassID = classID;
                model.Description = this.txtContent.Text;
                string oldValue = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));
                string newValue = string.Format("/Upload/CMS/Article/{0}/", DateTime.Now.ToString("yyyyMM"));
                string.Format("/Upload/CMS/Files/{0}/", DateTime.Now.ToString("yyyyMM"));
                ArrayList fileNameList = new ArrayList();
                if (!string.IsNullOrWhiteSpace(this.HiddenField_ISModifyImage.Value))
                {
                    string str3 = string.Format(this.HiddenField_ICOPath.Value, "");
                    fileNameList.Add(str3.Replace(oldValue, ""));
                    model.ImageUrl = str3.Replace(oldValue, newValue);
                }
                else
                {
                    model.ImageUrl = this.HiddenField_ICOPath.Value;
                }
                if (this.bll.Update(model))
                {
                    if (!string.IsNullOrWhiteSpace(this.HiddenField_ISModifyImage.Value))
                    {
                        FileManage.MoveFile(base.Server.MapPath(oldValue), base.Server.MapPath(newValue), fileNameList);
                    }
                    DataCache.DeleteCache("ContentModel-" + model.ContentID);
                    DataCache.DeleteCache("ContentModelEx-" + model.ContentID);
                    MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "ListSimple.aspx?type=0");
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipSaveError);
                }
            }
            else
            {
                MessageBox.ShowServerBusyTip(this, CMS.ContentErrorNoContent, "ListSimple.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindTree();
                if (this.ContentID > 0)
                {
                    this.ShowInfo();
                }
                else
                {
                    MessageBox.ShowServerBusyTip(this, CMS.ContentErrorNoContent, "ListSimple.aspx");
                }
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.CMS.Content model = this.bll.GetModel(this.ContentID);
            if (model != null)
            {
                this.txtTitle.Text = model.Title;
                this.txtSummary.Text = model.Summary;
                this.txtContent.Text = model.Description;
                this.radlState.SelectedValue = model.State.ToString();
                this.imgUrl.ImageUrl = "~/images/no_photo_s.png";
                this.dropClass.SelectedValue = model.ClassID.ToString();
                if (!string.IsNullOrWhiteSpace(model.ImageUrl))
                {
                    this.HiddenField_ICOPath.Value = this.imgUrl.ImageUrl = model.ImageUrl;
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xe5;
            }
        }

        protected int ContentID
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

