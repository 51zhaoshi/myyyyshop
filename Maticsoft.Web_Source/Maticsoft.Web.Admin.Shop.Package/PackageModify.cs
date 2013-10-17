namespace Maticsoft.Web.Admin.Shop.Package
{
    using Maticsoft.BLL.Shop.Package;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Package;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Data;
    using System.Web.UI.WebControls;

    public class PackageModify : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Package.Package bll = new Maticsoft.BLL.Shop.Package.Package();
        protected Button btnCancle;
        protected Button btnSave;
        protected DropDownList ddlCategory;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RegularExpressionValidator RegularExpressionValidator1;
        protected TextBox txtDescription;
        protected TextBox txtName;
        protected TextBox txtRemark;
        protected FileUpload uploadPhoto;

        private void BindCategoryData()
        {
            DataSet list = new Maticsoft.BLL.Shop.Package.PackageCategory().GetList("");
            if (!DataSetTools.DataSetIsNull(list))
            {
                this.ddlCategory.DataSource = list;
                this.ddlCategory.DataTextField = "Name";
                this.ddlCategory.DataValueField = "CategoryId";
                this.ddlCategory.DataBind();
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("PackageList.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string path = "/Upload/Shop/Files/";
            int num = 0x9c4000;
            string fileName = this.uploadPhoto.PostedFile.FileName;
            string filename = "";
            if (this.uploadPhoto.HasFile)
            {
                if (this.uploadPhoto.PostedFile.ContentLength > num)
                {
                    MessageBox.ShowSuccessTip(this, "您上传的图片过大，请上传较小的文件");
                    return;
                }
                filename = base.Server.MapPath(path) + fileName;
                this.uploadPhoto.PostedFile.SaveAs(filename);
            }
            string str4 = this.txtName.Text.Trim();
            string str5 = this.txtDescription.Text.Trim();
            string str6 = this.txtRemark.Text.Trim();
            if (str4.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "包装的名称不能为空！");
            }
            else if (str6.Length > 200)
            {
                MessageBox.ShowServerBusyTip(this, "备注不能超过200个字符！");
            }
            else if (str5.Length > 200)
            {
                MessageBox.ShowServerBusyTip(this, "描述不能超过200个字符！");
            }
            else
            {
                Maticsoft.Model.Shop.Package.Package model = this.bll.GetModel(this.Id);
                model.Name = str4;
                model.Remark = str6;
                model.PhotoUrl = string.IsNullOrEmpty(filename) ? model.PhotoUrl : (model.PhotoUrl = path + fileName);
                model.Description = str5;
                model.CategoryId = Globals.SafeInt(this.ddlCategory.SelectedValue, 0);
                if (this.bll.Update(model))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新包装(id=" + model.PackageId + ")成功", this);
                    MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "PackageList.aspx");
                    base.Response.Redirect("PackageList.aspx");
                }
                else
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新包装(id=" + this.ID + ")失败", this);
                    MessageBox.ShowServerBusyTip(this, Site.TooltipSaveError);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.BindCategoryData();
                this.ShowInfo(this.Id);
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)))
                {
                    MessageBox.ShowFailTip(this, "您没有此权限");
                }
            }
        }

        private void ShowInfo(int packageId)
        {
            Maticsoft.Model.Shop.Package.Package model = new Maticsoft.BLL.Shop.Package.Package().GetModel(packageId);
            this.txtName.Text = model.Name;
            this.txtRemark.Text = model.Remark;
            this.txtDescription.Text = model.Description;
            this.ddlCategory.SelectedValue = model.CategoryId.ToString();
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1c5;
            }
        }

        public int Id
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

