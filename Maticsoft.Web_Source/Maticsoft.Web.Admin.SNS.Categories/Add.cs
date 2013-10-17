namespace Maticsoft.Web.Admin.SNS.Categories
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        private Maticsoft.BLL.SNS.Categories bll = new Maticsoft.BLL.SNS.Categories();
        protected Button btnCancle;
        protected Button btnSave;
        protected Literal Literal2;
        protected Literal Literal3;
        protected SNSPhotoCateDropList PhotoCategory;
        protected RadioButtonList radlState;
        protected RadioButtonList rbIsMenuShow;
        protected RadioButtonList rbIsused;
        protected SNSCategoryDropList SNSCategory;
        protected TextBox textFontColor;
        protected TextBox txtAssociatedProductType;
        protected TextBox txtDescription;
        protected TextBox txtName;
        protected TextBox txtSeoDescription;
        protected TextBox txtSeoKeywords;
        protected TextBox txtSeoTitle;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx?type=" + this.type);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.btnSave.Enabled = true;
            this.btnCancle.Enabled = true;
            if (string.IsNullOrWhiteSpace(this.txtName.Text.Trim()))
            {
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
                MessageBox.ShowFailTip(this, "分类名称不能为空，在1至60个字符之间");
            }
            else
            {
                Maticsoft.Model.SNS.Categories model = new Maticsoft.Model.SNS.Categories {
                    Name = this.txtName.Text,
                    Description = this.txtDescription.Text
                };
                if (this.type == 1)
                {
                    if (!string.IsNullOrWhiteSpace(this.PhotoCategory.SelectedValue.Trim()))
                    {
                        model.ParentID = int.Parse(this.PhotoCategory.SelectedValue);
                    }
                    else
                    {
                        model.ParentID = 0;
                    }
                }
                else if (!string.IsNullOrWhiteSpace(this.SNSCategory.SelectedValue.Trim()))
                {
                    model.ParentID = int.Parse(this.SNSCategory.SelectedValue);
                }
                else
                {
                    model.ParentID = 0;
                }
                model.HasChildren = false;
                model.IsMenu = Globals.SafeBool(this.radlState.SelectedValue, false);
                model.CreatedUserID = base.CurrentUser.UserID;
                model.MenuIsShow = Globals.SafeBool(this.rbIsMenuShow.SelectedValue, false);
                model.MenuSequence = -1;
                model.Type = this.type;
                model.Status = Globals.SafeInt(this.rbIsused.SelectedValue, 0);
                model.FontColor = this.textFontColor.Text.Trim();
                model.Meta_Title = this.txtSeoTitle.Text;
                model.Meta_Keywords = this.txtSeoKeywords.Text;
                model.Meta_Description = this.txtSeoDescription.Text;
                if (this.bll.AddCategories(model))
                {
                    this.btnSave.Enabled = false;
                    this.btnCancle.Enabled = false;
                    base.Cache.Remove("GetAllCateByCache-" + this.type);
                    if ((this.Session["CategoryType"] != null) && (this.Session["CategoryType"].ToString() == "1"))
                    {
                        MessageBox.ShowSuccessTip(this, "添加成功,正在跳转...！", "list.aspx?type=1");
                    }
                    else
                    {
                        MessageBox.ShowSuccessTip(this, "添加成功,正在跳转...！", "list.aspx");
                    }
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "添加商品类别成功!", this);
                }
                else
                {
                    this.btnSave.Enabled = true;
                    this.btnCancle.Enabled = true;
                    MessageBox.ShowSuccessTip(this, "添加失败！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "添加商品类别失败!", this);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.type > 0)
            {
                this.SNSCategory.Visible = false;
                this.Literal2.Text = "添加图片分类";
                this.Literal3.Text = "为不同类型的图片创建不同的分类，方便您管理也方便顾客浏览 ";
            }
            else
            {
                this.PhotoCategory.Visible = false;
                this.Literal2.Text = "添加商品分类";
                this.Literal3.Text = "为不同类型的商品创建不同的分类，方便您管理也方便顾客浏览 ";
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                switch (this.type)
                {
                    case 0:
                        return 0x234;

                    case 1:
                        return 0x231;
                }
                return 0x234;
            }
        }

        public int type
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["type"]))
                {
                    num = Globals.SafeInt(base.Request.Params["type"], 0);
                }
                return num;
            }
        }
    }
}

