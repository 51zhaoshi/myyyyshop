namespace Maticsoft.Web.Admin.CMS.Setting
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public class StoreConfig : PageBaseAdmin
    {
        protected int Act_UpdateData = 0x71;
        protected Button btnReset;
        protected Button btnSave;
        protected RadioButton rdtLocal;
        protected RadioButton rdtWeb;
        protected HiddenField thumbList;
        protected TextBox txtOperaterName;
        protected TextBox txtOperaterPassword;
        protected TextBox txtPhotoDomain;
        protected TextBox txtSpaceName;

        private void BoundData()
        {
            if (ConfigSystem.GetValueByCache("CMS_ImageStoreWay") == "1")
            {
                this.rdtWeb.Checked = true;
            }
            else
            {
                this.rdtLocal.Checked = true;
            }
            this.txtOperaterName.Text = ConfigSystem.GetValueByCache("CMS_YouPaiYunOperaterName");
            this.txtOperaterPassword.Text = ConfigSystem.GetValueByCache("CMS_YouPaiOperaterPassword");
            this.txtSpaceName.Text = ConfigSystem.GetValueByCache("CMS_YouPaiSpaceName");
            this.txtPhotoDomain.Text = ConfigSystem.GetValueByCache("CMS_YouPaiPhotoDomain");
            List<Maticsoft.Model.Ms.ThumbnailSize> thumSizeList = Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.CMS, "");
            if ((thumSizeList != null) && (thumSizeList.Count > 0))
            {
                int num = 0;
                foreach (Maticsoft.Model.Ms.ThumbnailSize size in thumSizeList)
                {
                    if (num == 0)
                    {
                        this.thumbList.Value = size.ThumName + "&" + size.CloudSizeName;
                    }
                    else
                    {
                        this.thumbList.Value = this.thumbList.Value + "," + size.ThumName + "&" + size.CloudSizeName;
                    }
                    num++;
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.BoundData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.UpdataData("CMS_ImageStoreWay", this.rdtWeb.Checked ? "1" : "0", "图片存储的方式[1:网上存储 0：本地存储]");
                this.UpdataData("CMS_YouPaiYunOperaterName", this.txtOperaterName.Text.Trim(), "网络又拍云存储照片【操作者名称】");
                if (!string.IsNullOrWhiteSpace(this.txtOperaterPassword.Text))
                {
                    this.UpdataData("CMS_YouPaiOperaterPassword", this.txtOperaterPassword.Text.Trim(), "网络又拍云存储照片【操作者密码】");
                }
                this.UpdataData("CMS_YouPaiSpaceName", this.txtSpaceName.Text.Trim(), "网络又拍云存储照片【空间名称】");
                this.UpdataData("CMS_YouPaiPhotoDomain", this.txtPhotoDomain.Text.Trim(), "网络又拍云存储照片域名");
                string str = this.thumbList.Value;
                Maticsoft.BLL.Ms.ThumbnailSize size = new Maticsoft.BLL.Ms.ThumbnailSize();
                foreach (string str2 in str.Split(new char[] { ',' }))
                {
                    Maticsoft.Model.Ms.ThumbnailSize model = size.GetModel(str2.Split(new char[] { '&' })[0].Trim());
                    if ((model != null) && (str2.Split(new char[] { '&' }).Length >= 2))
                    {
                        model.CloudSizeName = str2.Split(new char[] { '&' })[1];
                        size.Update(model);
                    }
                }
                IDictionaryEnumerator enumerator = base.Cache.GetEnumerator();
                ArrayList list = new ArrayList();
                while (enumerator.MoveNext())
                {
                    list.Add(enumerator.Key.ToString());
                }
                foreach (string str3 in list)
                {
                    base.Cache.Remove(str3);
                }
                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "StoreConfig.aspx");
            }
            catch (Exception)
            {
                MessageBox.ShowSuccessTip(this, Site.TooltipTryAgainLater, "StoreConfig.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.btnSave.Visible = false;
            }
            if (!base.IsPostBack)
            {
                this.BoundData();
            }
        }

        public bool UpdataData(string Key, string Value, string Description)
        {
            try
            {
                if (ConfigSystem.Exists(Key))
                {
                    ConfigSystem.Update(Key, Value, ApplicationKeyType.OpenAPI);
                }
                else
                {
                    ConfigSystem.Add(Key, Value, Description, ApplicationKeyType.OpenAPI);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x70;
            }
        }
    }
}

