namespace Maticsoft.Web.Admin.Shop.WholeSale
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Shop.Sales;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Sales;
    using Maticsoft.Web;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class AddRule : PageBaseAdmin
    {
        protected Button btnSave;
        protected CheckBoxList ChkDealerRank;
        protected CheckBoxList ChkUserRank;
        protected HiddenField hfItems;
        protected HiddenField hfSuccess;
        private Maticsoft.BLL.Shop.Sales.SalesItem itemBll = new Maticsoft.BLL.Shop.Sales.SalesItem();
        protected Literal lblTitle;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal21;
        protected Literal Literal23;
        protected RadioButtonList radItemType;
        protected RadioButtonList radMode;
        protected RadioButtonList radStatus;
        protected RadioButtonList radUnit;
        private UserRank rankBll = new UserRank();
        private Maticsoft.BLL.Shop.Sales.SalesRule ruleBll = new Maticsoft.BLL.Shop.Sales.SalesRule();
        protected ScriptManager ScriptManager1;
        protected TextBox txtRuleName;
        private Maticsoft.BLL.Shop.Sales.SalesUserRank userRankBll = new Maticsoft.BLL.Shop.Sales.SalesUserRank();

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Sales.SalesRule model = new Maticsoft.Model.Shop.Sales.SalesRule {
                RuleName = this.txtRuleName.Text,
                RuleMode = Globals.SafeInt(this.radMode.SelectedValue, 0),
                RuleUnit = Globals.SafeInt(this.radUnit.SelectedValue, 0),
                Status = Globals.SafeInt(this.radStatus.SelectedValue, 0),
                CreatedDate = DateTime.Now,
                CreatedUserID = base.CurrentUser.UserID
            };
            string str = this.hfItems.Value;
            if (string.IsNullOrWhiteSpace(str))
            {
                MessageBox.ShowFailTip(this, "请填写优惠规则项");
            }
            else
            {
                int num = this.ruleBll.Add(model);
                if (num > 0)
                {
                    string[] strArray = str.Split(new char[] { ',' });
                    int num2 = Globals.SafeInt(this.radItemType.SelectedValue, 0);
                    foreach (string str2 in strArray)
                    {
                        Maticsoft.Model.Shop.Sales.SalesItem item = new Maticsoft.Model.Shop.Sales.SalesItem {
                            ItemType = num2,
                            UnitValue = Globals.SafeInt(str2.Split(new char[] { '|' })[0], 0),
                            RateValue = Globals.SafeInt(str2.Split(new char[] { '|' })[1], 0),
                            RuleId = num
                        };
                        this.itemBll.Add(item);
                    }
                    for (int i = 0; i < this.ChkUserRank.Items.Count; i++)
                    {
                        if (this.ChkUserRank.Items[i].Selected)
                        {
                            Maticsoft.Model.Shop.Sales.SalesUserRank rank = new Maticsoft.Model.Shop.Sales.SalesUserRank {
                                RankId = Globals.SafeInt(this.ChkUserRank.Items[i].Value, 0),
                                RuleId = num
                            };
                            this.userRankBll.Add(rank);
                        }
                    }
                    for (int j = 0; j < this.ChkDealerRank.Items.Count; j++)
                    {
                        if (this.ChkDealerRank.Items[j].Selected)
                        {
                            Maticsoft.Model.Shop.Sales.SalesUserRank rank2 = new Maticsoft.Model.Shop.Sales.SalesUserRank {
                                RankId = Globals.SafeInt(this.ChkUserRank.Items[j].Value, 0),
                                RuleId = num
                            };
                            this.userRankBll.Add(rank2);
                        }
                    }
                    MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                DataSet list = this.rankBll.GetList(" RankType=0");
                this.ChkUserRank.DataSource = list;
                this.ChkUserRank.DataTextField = "Name";
                this.ChkUserRank.DataValueField = "RankId";
                this.ChkUserRank.DataBind();
                this.ChkDealerRank.DataSource = this.rankBll.GetList(" RankType=1");
                this.ChkDealerRank.DataTextField = "Name";
                this.ChkDealerRank.DataValueField = "RankId";
                this.ChkDealerRank.DataBind();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x22d;
            }
        }
    }
}

