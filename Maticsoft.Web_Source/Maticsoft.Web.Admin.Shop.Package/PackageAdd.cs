namespace Maticsoft.Web.Admin.Shop.Package
{
    using Maticsoft.BLL.Shop.Package;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Package;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.UI.WebControls;

    public class PackageAdd : PageBaseAdmin
    {
        private string BigImageSize = "800X800";
        private Maticsoft.BLL.Shop.Package.Package bll = new Maticsoft.BLL.Shop.Package.Package();
        protected Button btnCancle;
        protected Button btnSave;
        protected DropDownList ddlCategory;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RegularExpressionValidator RegularExpressionValidator1;
        private int RestrictPhotoSize = 0x9c4000;
        private string SavePath = "/Upload/Shop/Files/";
        private string SmallImageSize = "400X400";
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
            string fileName = this.uploadPhoto.PostedFile.FileName;
            if (!this.uploadPhoto.HasFile)
            {
                MessageBox.ShowSuccessTip(this, "请上传文件");
            }
            else if (this.uploadPhoto.PostedFile.ContentLength > this.RestrictPhotoSize)
            {
                MessageBox.ShowSuccessTip(this, "您上传的图片过大，请上传较小的文件");
            }
            else
            {
                string filename = base.Server.MapPath(this.SavePath) + fileName;
                this.uploadPhoto.PostedFile.SaveAs(filename);
                string smallImagePath = "";
                string bigImagePath = "";
                if (!this.ImageCut(fileName, this.SavePath, this.SmallImageSize, this.BigImageSize, out smallImagePath, out bigImagePath))
                {
                    MessageBox.ShowSuccessTip(this, "出现异常，请重试");
                }
                else
                {
                    string str5 = this.txtName.Text.Trim();
                    if (str5.Length == 0)
                    {
                        MessageBox.ShowServerBusyTip(this, "包装的名称不能为空！");
                    }
                    else
                    {
                        string str6 = this.txtRemark.Text.Trim();
                        if (str6.Length > 200)
                        {
                            MessageBox.ShowServerBusyTip(this, "备注不能超过200个字符！");
                        }
                        else
                        {
                            string str7 = this.txtDescription.Text.Trim();
                            if (str7.Length > 200)
                            {
                                MessageBox.ShowServerBusyTip(this, "描述不能超过200个字符！");
                            }
                            else
                            {
                                Maticsoft.Model.Shop.Package.Package model = new Maticsoft.Model.Shop.Package.Package {
                                    Name = str5,
                                    Remark = str6,
                                    PhotoUrl = this.SavePath + fileName,
                                    Description = str7,
                                    ThumbPhotoUrl = smallImagePath,
                                    NormalPhotoUrl = bigImagePath,
                                    CategoryId = Globals.SafeInt(this.ddlCategory.SelectedValue, 0)
                                };
                                int num2 = this.bll.Add(model);
                                if (num2 > 0)
                                {
                                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "添加包装(id=" + num2 + ")成功", this);
                                    MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "PackageList.aspx");
                                    base.Response.Redirect("PackageList.aspx");
                                }
                                else
                                {
                                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "添加包装(id=" + num2 + ")失败", this);
                                    MessageBox.ShowServerBusyTip(this, Site.TooltipSaveError);
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool ImageCut(string imgname, string uploadpath, string SmallImageSize, string BigImageSize, out string SmallImagePath, out string BigImagePath)
        {
            try
            {
                string str = "S_" + imgname;
                string thumbnailPath = HttpContext.Current.Server.MapPath(uploadpath + str);
                int width = 400;
                int height = 400;
                if ((SmallImageSize != null) && (SmallImageSize.Split(new char[] { 'X' }).Length > 1))
                {
                    string[] strArray = SmallImageSize.Split(new char[] { 'X' });
                    width = Globals.SafeInt(strArray[0], 400);
                    height = Globals.SafeInt(strArray[1], 400);
                }
                ImageTools.MakeThumbnail(HttpContext.Current.Server.MapPath(uploadpath + imgname), thumbnailPath, width, height, MakeThumbnailMode.W, InterpolationMode.High, SmoothingMode.HighQuality);
                string str3 = "B_" + imgname;
                string str4 = HttpContext.Current.Server.MapPath(uploadpath + str3);
                int num3 = 800;
                int num4 = 800;
                if ((BigImageSize != null) && (BigImageSize.Split(new char[] { 'X' }).Length > 1))
                {
                    string[] strArray2 = BigImageSize.Split(new char[] { 'X' });
                    num3 = Globals.SafeInt(strArray2[0], 800);
                    num4 = Globals.SafeInt(strArray2[1], 800);
                }
                ImageTools.MakeThumbnail(HttpContext.Current.Server.MapPath(uploadpath + imgname), str4, num3, num4, MakeThumbnailMode.W, InterpolationMode.High, SmoothingMode.HighQuality);
                SmallImagePath = uploadpath + str;
                BigImagePath = uploadpath + str3;
                return true;
            }
            catch (Exception)
            {
                SmallImagePath = "";
                BigImagePath = "";
                return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.BindCategoryData();
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)))
                {
                    MessageBox.ShowFailTip(this, "您没有此权限");
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1c4;
            }
        }
    }
}

