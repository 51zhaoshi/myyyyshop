namespace Maticsoft.Web.CMS.ContentClass
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Maticsoft.Web.Validator;
    using Resources;
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.ContentClass bll = new Maticsoft.BLL.CMS.ContentClass();
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkAllowAddContent;
        protected CheckBox chkAllowSubclass;
        protected DropDownList dropClassTypeID;
        protected DropDownList dropParentID;
        protected HiddenField HiddenField_ICOPath;
        protected Image imgUrl;
        protected Label lblClassID;
        protected Literal lblHelpSecret;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal12;
        protected Literal Literal13;
        protected Literal Literal14;
        protected Literal Literal15;
        protected Literal Literal16;
        protected Literal Literal17;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected RadioButtonList radlClassModel;
        protected RadioButtonList radlState;
        protected RegularExpressionValidator RegularExpressionValidator2;
        protected RequiredFieldValidator RequiredFieldValidator3;
        protected TextBox txtClassIndex;
        protected TextBox txtClassName;
        protected HtmlGenericControl txtClassNameTip;
        protected TextBox txtDescription;
        protected TextBox txtIndexChar;
        protected TextBox txtKeywords;
        protected TextBox txtOrders;
        protected TextBox txtPageModelName;
        protected TextBox txtRemark;
        protected ValidateTarget ValidateTargetName;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        public void BindClassTypeID()
        {
            DataSet allList = new Maticsoft.BLL.CMS.ClassType().GetAllList();
            if (!DataSetTools.DataSetIsNull(allList))
            {
                this.dropClassTypeID.DataSource = allList;
                this.dropClassTypeID.DataTextField = "ClassTypeName";
                this.dropClassTypeID.DataValueField = "ClassTypeID";
                this.dropClassTypeID.DataBind();
            }
        }

        private void BindNode(int parentid, DataTable dt, string blank)
        {
            foreach (DataRow row in dt.Select("ParentID= " + parentid))
            {
                string str = row["ClassID"].ToString();
                string text = row["ClassName"].ToString();
                text = blank + "『" + text + "』";
                if (!(str == base.Request.Params["id"]))
                {
                    this.dropParentID.Items.Add(new ListItem(text, str));
                    int num = int.Parse(str);
                    string str3 = blank + "─";
                    this.BindNode(num, dt, str3);
                }
            }
        }

        private void BindTree()
        {
            this.dropParentID.Items.Clear();
            this.dropParentID.Items.Add(new ListItem(CMS.CCTooltipNoParent, "0"));
            DataSet treeList = this.bll.GetTreeList("");
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
                        if (!(str == base.Request.Params["id"].ToLower()))
                        {
                            this.dropParentID.Items.Add(new ListItem(text, str));
                            int parentid = int.Parse(str);
                            string blank = "├";
                            this.BindNode(parentid, dt, blank);
                        }
                    }
                }
            }
            this.dropParentID.DataBind();
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("List.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int classID = 0;
            if (string.IsNullOrWhiteSpace(this.txtClassName.Text.Trim()))
            {
                MessageBox.ShowFailTip(this, "请输入栏目名称!");
            }
            else
            {
                if (this.dropParentID.SelectedIndex > 0)
                {
                    classID = int.Parse(this.dropParentID.SelectedValue);
                    Maticsoft.Model.CMS.ContentClass model = this.bll.GetModel(classID);
                    if ((model != null) && !model.AllowSubclass)
                    {
                        MessageBox.ShowFailTip(this, CMS.CCErrorAddClass);
                        return;
                    }
                }
                Maticsoft.Model.CMS.ContentClass modelByCache = this.bll.GetModelByCache(this.ClassID);
                if (modelByCache != null)
                {
                    modelByCache.ClassName = Globals.HtmlEncode(this.txtClassName.Text);
                    modelByCache.ClassIndex = this.txtClassIndex.Text;
                    modelByCache.Sequence = Globals.SafeInt(this.txtOrders.Text, 0);
                    modelByCache.ParentId = new int?(classID);
                    modelByCache.State = int.Parse(this.radlState.SelectedValue);
                    modelByCache.AllowSubclass = this.chkAllowSubclass.Checked;
                    modelByCache.AllowAddContent = this.chkAllowAddContent.Checked;
                    if (!string.IsNullOrWhiteSpace(this.HiddenField_ICOPath.Value))
                    {
                        modelByCache.ImageUrl = this.HiddenField_ICOPath.Value;
                    }
                    else
                    {
                        modelByCache.ImageUrl = this.HiddenField_ICOPath.Value;
                    }
                    modelByCache.Description = Globals.HtmlEncode(this.txtDescription.Text);
                    modelByCache.Keywords = Globals.HtmlEncode(this.txtKeywords.Text);
                    modelByCache.ClassTypeID = Globals.SafeInt(this.dropClassTypeID.SelectedValue, 0);
                    modelByCache.ClassModel = Globals.SafeInt(this.radlClassModel.SelectedValue, 0);
                    modelByCache.PageModelName = Globals.HtmlEncode(this.txtPageModelName.Text);
                    modelByCache.CreatedDate = DateTime.Now;
                    modelByCache.CreatedUserID = base.CurrentUser.UserID;
                    modelByCache.IndexChar = Globals.HtmlEncode(this.txtIndexChar.Text);
                    modelByCache.Remark = Globals.HtmlEncode(this.txtRemark.Text);
                    if (this.bll.Update(modelByCache))
                    {
                        this.btnCancle.Enabled = false;
                        this.btnSave.Enabled = false;
                        MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "List.aspx");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, Site.TooltipSaveError);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindTree();
                this.BindClassTypeID();
                if (this.ClassID > 0)
                {
                    this.ShowInfo();
                }
                else
                {
                    MessageBox.ShowServerBusyTip(this, CMS.ContentErrorNoContent, "List.aspx");
                }
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.CMS.ContentClass model = this.bll.GetModel(this.ClassID);
            if (model != null)
            {
                this.lblClassID.Text = model.ClassID.ToString();
                this.txtClassName.Text = Globals.HtmlDecode(model.ClassName);
                this.txtClassIndex.Text = model.ClassIndex;
                this.txtOrders.Text = model.Sequence.ToString();
                if (model.ParentId.HasValue)
                {
                    this.dropParentID.SelectedValue = model.ParentId.ToString();
                }
                this.radlState.SelectedValue = model.State.ToString();
                this.chkAllowSubclass.Checked = model.AllowSubclass;
                this.chkAllowAddContent.Checked = model.AllowAddContent;
                this.HiddenField_ICOPath.Value = model.ImageUrl;
                this.imgUrl.ImageUrl = model.ImageUrl;
                this.txtDescription.Text = Globals.HtmlDecode(model.Description);
                this.txtKeywords.Text = Globals.HtmlDecode(model.Keywords);
                this.dropClassTypeID.SelectedValue = model.ClassTypeID.ToString();
                this.radlClassModel.SelectedValue = model.ClassModel.ToString();
                this.txtPageModelName.Text = Globals.HtmlDecode(model.PageModelName);
                this.txtRemark.Text = Globals.HtmlDecode(model.Remark);
                this.txtIndexChar.Text = Globals.HtmlDecode(model.IndexChar);
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xde;
            }
        }

        public int ClassID
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(str) && PageValidate.IsNumber(str))
                {
                    num = int.Parse(str);
                }
                return num;
            }
        }
    }
}

