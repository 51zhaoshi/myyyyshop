namespace Maticsoft.Web.Admin.SysManage
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class UpdateCount : PageBaseAdmin
    {
        protected CheckBox AblumsCheck;
        protected Button Button5;
        protected CheckBox CollocationCheck;
        protected CheckBox FansCkeck;
        protected CheckBox FavouritesCheck;
        protected CheckBox HotStarCheck;
        protected Label Label1;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected CheckBox ProductCheck;
        protected CheckBox ShareCheck;
        protected CheckBox ShareProductCheck;
        private StarRank StarRankBll = new StarRank();
        private Users user = new Users();
        private UsersExp UserExBll = new UsersExp();

        protected void btnAblums_Click(object sender, EventArgs e)
        {
            if (this.UserExBll.UpdateAblumsCount())
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新用户专辑数成功", this);
                MessageBox.ShowSuccessTip(this, "更新用户专辑数成功");
            }
            else
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新用户专辑数失败", this);
                MessageBox.ShowFailTip(this, "更新用户专辑数失败");
            }
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            if (this.FansCkeck.Checked && !this.user.UpdateFansAndFellowCount())
            {
                MessageBox.ShowFailTip(this, "更新用户粉丝数失败");
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新用户粉丝数失败", this);
            }
            if (this.ShareCheck.Checked && !this.UserExBll.UpdateShareCount())
            {
                MessageBox.ShowFailTip(this, "更新用户分享数失败");
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新用户粉丝数失败", this);
            }
            if (this.ProductCheck.Checked && !this.UserExBll.UpdateProductCount())
            {
                MessageBox.ShowFailTip(this, "更新用户商品数失败");
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新用户商品数失败", this);
            }
            if (this.FavouritesCheck.Checked && !this.UserExBll.UpdateFavouritesCount())
            {
                MessageBox.ShowFailTip(this, "更新用户喜欢数失败");
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新用户喜欢数失败", this);
            }
            if (this.AblumsCheck.Checked && !this.UserExBll.UpdateAblumsCount())
            {
                MessageBox.ShowFailTip(this, "更新用户专辑数失败");
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新用户专辑数失败", this);
            }
            if (this.HotStarCheck.Checked && !this.StarRankBll.AddHotStarRank())
            {
                MessageBox.ShowSuccessTip(this, "重新获取明星达人失败");
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "重新获取明星达人失败", this);
            }
            if (this.ShareProductCheck.Checked && !this.StarRankBll.AddShareProductRank())
            {
                MessageBox.ShowSuccessTip(this, "重新获取晒货达人失败");
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "重新获取晒货达人失败", this);
            }
            if (this.CollocationCheck.Checked && !this.StarRankBll.AddCollocationRank())
            {
                MessageBox.ShowSuccessTip(this, "重新获取搭配达人失败");
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "重新获取搭配达人失败", this);
            }
            MessageBox.ShowSuccessTip(this, "全部更新成功");
        }

        protected void btnCollocation_Click(object sender, EventArgs e)
        {
            if (!this.StarRankBll.AddCollocationRank())
            {
                MessageBox.ShowSuccessTip(this, "重新获取搭配达人失败");
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "重新获取搭配达人失败", this);
            }
        }

        protected void btnFans_Click(object sender, EventArgs e)
        {
            if (this.user.UpdateFansAndFellowCount())
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新用户粉丝数成功", this);
                MessageBox.ShowSuccessTip(this, "更新用户粉丝数成功");
            }
            else
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新用户粉丝数失败", this);
                MessageBox.ShowFailTip(this, "更新用户粉丝数失败");
            }
        }

        protected void btnFavourites_Click(object sender, EventArgs e)
        {
            if (this.UserExBll.UpdateFavouritesCount())
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新用户喜欢数成功", this);
                MessageBox.ShowSuccessTip(this, "更新用户喜欢数成功");
            }
            else
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新用户喜欢数失败", this);
                MessageBox.ShowFailTip(this, "更新用户喜欢数失败");
            }
        }

        protected void btnHotStar_Click(object sender, EventArgs e)
        {
            if (!this.StarRankBll.AddHotStarRank())
            {
                MessageBox.ShowSuccessTip(this, "重新获取明星达人失败");
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "重新获取明星达人失败", this);
            }
        }

        protected void btnProduct_Click(object sender, EventArgs e)
        {
            if (this.UserExBll.UpdateProductCount())
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新用户商品数成功", this);
                MessageBox.ShowSuccessTip(this, "更新用户商品数成功");
            }
            else
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新用户商品数失败", this);
                MessageBox.ShowFailTip(this, "更新用户商品数失败");
            }
        }

        protected void btnShare_Click(object sender, EventArgs e)
        {
            if (this.UserExBll.UpdateShareCount())
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新用户分享数成功", this);
                MessageBox.ShowSuccessTip(this, "更新用户分享数成功");
            }
            else
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新用户分享数失败", this);
                MessageBox.ShowFailTip(this, "更新用户分享数失败");
            }
        }

        protected void btnShareProduct_Click(object sender, EventArgs e)
        {
            if (!this.StarRankBll.AddShareProductRank())
            {
                MessageBox.ShowSuccessTip(this, "重新获取晒货达人失败");
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "重新获取晒货达人失败", this);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x24b;
            }
        }
    }
}

