namespace Maticsoft.Web.Admin.CMS.Content
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SysManage;
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

    public class AddSimple : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.Content bll = new Maticsoft.BLL.CMS.Content();
        private Maticsoft.BLL.CMS.ContentClass bllContentClass = new Maticsoft.BLL.CMS.ContentClass();
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkQQ;
        protected CheckBox chkSina;
        protected DropDownList ddlType;
        protected HiddenField hfClassID;
        protected HiddenField HiddenField_ICOPath;
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
        private string uploadFolder = ConfigSystem.GetValueByCache("UploadFolder");
        protected ValidateTarget ValidateTargetName;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        private void BindNode(int parentid, DataTable dt, string blank)
        {
            foreach (DataRow row in dt.Select("ParentID= " + parentid))
            {
                string str = Globals.SafeString(row["ClassID"], "0");
                string text = Globals.SafeString(row["ClassName"], "0");
                text = blank + "『" + text + "』";
                this.ddlType.Items.Add(new ListItem(text, str));
                int num = int.Parse(str);
                string str3 = blank + "─";
                this.BindNode(num, dt, str3);
            }
        }

        private void BindTree()
        {
            this.ddlType.Items.Clear();
            new Maticsoft.BLL.CMS.ContentClass();
            DataSet treeList = this.bllContentClass.GetTreeList("");
            if (!DataSetTools.DataSetIsNull(treeList))
            {
                DataTable dt = treeList.Tables[0];
                this.ddlType.Items.Clear();
                if (!DataTableTools.DataTableIsNull(dt))
                {
                    foreach (DataRow row in dt.Select("ParentID= " + 0))
                    {
                        if (Globals.SafeBool(Globals.SafeString(row["AllowAddContent"], ""), false))
                        {
                            string str = Globals.SafeString(row["ClassID"], "0");
                            string text = Globals.SafeString(row["ClassName"], "0");
                            Globals.SafeString(row["ParentID"], "0");
                            text = "╋" + text;
                            this.ddlType.Items.Add(new ListItem(text, str));
                            int parentid = int.Parse(str);
                            string blank = "├";
                            this.BindNode(parentid, dt, blank);
                        }
                    }
                }
            }
            this.ddlType.DataBind();
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.hfClassID.Value))
            {
                base.Response.Redirect("ListSimple.aspx" + this.hfClassID.Value);
            }
            else
            {
                base.Response.Redirect("ListSimple.aspx?type=0");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int classID = Globals.SafeInt(this.ddlType.SelectedValue, 0);
            if (classID <= 0)
            {
                MessageBox.ShowFailTip(this, CMS.ContentErrorAddClass);
            }
            else
            {
                Maticsoft.Model.CMS.ContentClass model = this.bllContentClass.GetModel(classID);
                if (model != null)
                {
                    if (!model.AllowAddContent)
                    {
                        MessageBox.ShowFailTip(this, CMS.ContentErrorAddContent);
                        return;
                    }
                    if ((2 == model.ClassModel) && this.bll.ExistsByClassID(classID))
                    {
                        MessageBox.ShowFailTip(this, CMS.ContentErrorAddMoreContent);
                        return;
                    }
                }
                if (string.IsNullOrWhiteSpace(this.txtTitle.Text.Trim()))
                {
                    MessageBox.ShowFailTip(this, CMS.TitleErrorAddContent);
                }
                else if (this.bll.ExistTitle(this.txtTitle.Text.Trim()))
                {
                    MessageBox.ShowFailTip(this, CMS.ContentTooltipTitleExist);
                }
                else
                {
                    Maticsoft.Model.CMS.Content content;
                    content = new Maticsoft.Model.CMS.Content {
                        Title = Globals.HtmlEncode(this.txtTitle.Text),
                        SubTitle = Globals.HtmlEncode(this.txtTitle.Text),
                        Summary = Globals.HtmlEncode(this.txtSummary.Text),
                        CreatedUserID = base.CurrentUser.UserID,
                        LastEditDate = new DateTime?(content.CreatedDate = DateTime.Now),
                        LastEditUserID = new int?(base.CurrentUser.UserID),
                        PvCount = 0,
                        State = Globals.SafeInt(this.radlState.SelectedValue, 0),
                        ClassID = classID,
                        Description = this.txtContent.Text
                    };
                    string oldValue = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));
                    string newValue = string.Format("/Upload/CMS/Article/{0}/", DateTime.Now.ToString("yyyyMM"));
                    string.Format("/Upload/CMS/Files/{0}/", DateTime.Now.ToString("yyyyMM"));
                    ArrayList fileNameList = new ArrayList();
                    if (!string.IsNullOrWhiteSpace(this.HiddenField_ICOPath.Value))
                    {
                        string str3 = string.Format(this.HiddenField_ICOPath.Value, "");
                        fileNameList.Add(str3.Replace(oldValue, ""));
                        content.ImageUrl = str3.Replace(oldValue, newValue);
                    }
                    content.TotalComment = 0;
                    content.TotalSupport = 0;
                    content.TotalFav = 0;
                    content.TotalShare = 0;
                    int num2 = this.bll.Add(content);
                    if (0 < num2)
                    {
                        if (!string.IsNullOrWhiteSpace(this.HiddenField_ICOPath.Value))
                        {
                            FileManage.MoveFile(base.Server.MapPath(oldValue), base.Server.MapPath(newValue), fileNameList);
                        }
                        string str4 = "";
                        str4 = this.chkSina.Checked ? "3" : "";
                        if (this.chkQQ.Checked)
                        {
                            str4 = str4 + (string.IsNullOrWhiteSpace(str4) ? "13" : ",13");
                        }
                        UserBind bind = new UserBind();
                        string valueByCache = ConfigSystem.GetValueByCache("WeiBo_CMS_Url");
                        string url = "http://" + Globals.DomainFullName + string.Format(valueByCache, num2);
                        bind.SendWeiBo(-1, str4, content.Title, url, content.ImageUrl);
                        if (!string.IsNullOrWhiteSpace(this.ddlType.SelectedValue))
                        {
                            MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "ListSimple.aspx?classid=" + this.ddlType.SelectedValue);
                        }
                        else
                        {
                            MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "ListSimple.aspx?type=0");
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindTree();
                if (this.ClassID > 0)
                {
                    this.strClassID = this.hfClassID.Value = "?classid=" + this.ClassID;
                    this.ddlType.SelectedValue = this.ClassID.ToString();
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)))
                {
                    MessageBox.ShowAndBack(this, "您没有权限");
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xe4;
            }
        }

        public int ClassID
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["classid"];
                if (!string.IsNullOrWhiteSpace(str) && PageValidate.IsNumber(str))
                {
                    num = int.Parse(str);
                }
                return num;
            }
        }
    }
}

