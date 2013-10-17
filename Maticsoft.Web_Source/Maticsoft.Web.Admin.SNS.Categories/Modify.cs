namespace Maticsoft.Web.Admin.SNS.Categories
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.SNS.Categories bll = new Maticsoft.BLL.SNS.Categories();
        protected Button btnCancle;
        protected Button btnSave;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList radlState;
        protected RadioButtonList rbIsMenuShow;
        protected RadioButtonList rbIsused;
        protected TextBox textFontColor;
        protected TextBox txtDescription;
        protected TextBox txtName;
        protected TextBox txtSeoDescription;
        protected TextBox txtSeoKeywords;
        protected TextBox txtSeoTitle;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            if (this.Type == 1)
            {
                base.Response.Redirect("list.aspx?type=1");
            }
            else
            {
                base.Response.Redirect("list.aspx");
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            this.btnSave.Enabled = false;
            this.btnCancle.Enabled = false;
            if (string.IsNullOrWhiteSpace(this.txtName.Text.Trim()))
            {
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
                MessageBox.ShowFailTip(this, "分类名称不能为空，在1至60个字符之间");
            }
            else
            {
                string text = this.txtName.Text;
                string str2 = this.txtDescription.Text;
                string str3 = this.textFontColor.Text;
                Maticsoft.Model.SNS.Categories model = this.bll.GetModel(this.CategoryId);
                model.Name = text;
                model.Description = str2;
                model.FontColor = str3;
                model.Status = Globals.SafeInt(this.rbIsused.SelectedValue, 0);
                model.MenuIsShow = Globals.SafeBool(this.rbIsMenuShow.SelectedValue, false);
                model.IsMenu = Globals.SafeBool(this.radlState.SelectedValue, false);
                if (this.bll.Update(model))
                {
                    base.Cache.Remove("GetAllCateByCache-" + this.Type);
                    if (this.Type == 1)
                    {
                        MessageBox.ShowSuccessTip(this, "保存成功！", "list.aspx?type=1");
                    }
                    else
                    {
                        MessageBox.ShowSuccessTip(this, "保存成功！", "list.aspx");
                    }
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "修改商品类别(CateGoryID=" + model.CategoryId + ")成功!", this);
                }
                else
                {
                    this.btnSave.Enabled = true;
                    this.btnCancle.Enabled = true;
                    MessageBox.ShowSuccessTip(this, "保存失败！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "修改商品类别(CateGoryID=" + model.CategoryId + ")失败!", this);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.CategoryId > -1)
                {
                    this.ShowInfo(this.CategoryId);
                }
                if (this.Type == 1)
                {
                    this.Literal2.Text = "编辑图片分类信息";
                    this.Literal3.Text = "您可以编辑图片分类信息";
                }
                else
                {
                    this.Literal2.Text = "编辑商品分类信息";
                    this.Literal3.Text = "您可以编辑商品分类信息";
                }
            }
        }

        private void ShowInfo(int CategoryId)
        {
            Maticsoft.Model.SNS.Categories model = new Maticsoft.BLL.SNS.Categories().GetModel(CategoryId);
            this.txtName.Text = model.Name;
            this.txtDescription.Text = model.Description;
            this.textFontColor.Text = model.FontColor;
            this.txtSeoTitle.Text = model.Meta_Title;
            this.txtSeoKeywords.Text = model.Meta_Keywords;
            this.txtSeoDescription.Text = model.Meta_Description;
            if (model.MenuIsShow)
            {
                this.rbIsMenuShow.SelectedIndex = 0;
            }
            else
            {
                this.rbIsMenuShow.SelectedIndex = 1;
            }
            if (model.IsMenu)
            {
                this.radlState.SelectedIndex = 0;
            }
            else
            {
                this.radlState.SelectedIndex = 1;
            }
            if (model.Status == 0)
            {
                this.rbIsused.SelectedIndex = 1;
            }
            else
            {
                this.rbIsused.SelectedIndex = 0;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                switch (this.Type)
                {
                    case 0:
                        return 0x235;

                    case 1:
                        return 0x232;
                }
                return 0x235;
            }
        }

        public int CategoryId
        {
            get
            {
                int num = -1;
                if ((base.Request.Params["id"] != null) && (base.Request.Params["id"].Trim() != ""))
                {
                    num = Globals.SafeInt(base.Request.Params["id"], -1);
                }
                return num;
            }
        }

        public int Type
        {
            get
            {
                int num = 0;
                if (this.Session["CategoryType"] != null)
                {
                    num = Globals.SafeInt(this.Session["CategoryType"].ToString(), 0);
                }
                return num;
            }
        }
    }
}

