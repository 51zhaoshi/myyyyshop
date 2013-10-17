namespace Maticsoft.Web.Admin.Shop.WholeSale
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Shop.Sales;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Sales;
    using Maticsoft.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class UpdateRule : PageBaseAdmin
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
            Maticsoft.Model.Shop.Sales.SalesRule model = this.ruleBll.GetModel(this.RuleId);
            model.RuleName = this.txtRuleName.Text;
            model.RuleMode = Globals.SafeInt(this.radMode.SelectedValue, 0);
            model.RuleUnit = Globals.SafeInt(this.radUnit.SelectedValue, 0);
            model.Status = Globals.SafeInt(this.radStatus.SelectedValue, 0);
            model.CreatedUserID = base.CurrentUser.UserID;
            string str = this.hfItems.Value;
            if (string.IsNullOrWhiteSpace(str))
            {
                MessageBox.ShowFailTip(this, "请填写优惠规则项");
            }
            else if (this.ruleBll.Update(model))
            {
                string[] strArray = str.Split(new char[] { ',' });
                int num = Globals.SafeInt(this.radItemType.SelectedValue, 0);
                this.itemBll.DeleteByRuleId(model.RuleId);
                foreach (string str2 in strArray)
                {
                    Maticsoft.Model.Shop.Sales.SalesItem item = new Maticsoft.Model.Shop.Sales.SalesItem {
                        ItemType = num,
                        UnitValue = Globals.SafeInt(str2.Split(new char[] { '|' })[0], 0),
                        RateValue = Globals.SafeInt(str2.Split(new char[] { '|' })[1], 0),
                        RuleId = model.RuleId
                    };
                    this.itemBll.Add(item);
                }
                this.userRankBll.DeleteByRuleId(model.RuleId);
                for (int i = 0; i < this.ChkUserRank.Items.Count; i++)
                {
                    if (this.ChkUserRank.Items[i].Selected)
                    {
                        Maticsoft.Model.Shop.Sales.SalesUserRank rank = new Maticsoft.Model.Shop.Sales.SalesUserRank {
                            RankId = Globals.SafeInt(this.ChkUserRank.Items[i].Value, 0),
                            RuleId = model.RuleId
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
                            RuleId = model.RuleId
                        };
                        this.userRankBll.Add(rank2);
                    }
                }
                MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
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
                this.ShowInfo(this.RuleId);
            }
        }

        private void ShowInfo(int RuleId)
        {
            List<int> list2 = (from c in this.userRankBll.GetModelList(" RuleId=" + RuleId) select c.RankId).ToList<int>();
            for (int i = 0; i < this.ChkUserRank.Items.Count; i++)
            {
                if (list2.Contains(Globals.SafeInt(this.ChkUserRank.Items[i].Value, 0)))
                {
                    this.ChkUserRank.Items[i].Selected = true;
                }
            }
            for (int j = 0; j < this.ChkDealerRank.Items.Count; j++)
            {
                if (list2.Contains(Globals.SafeInt(this.ChkDealerRank.Items[j].Value, 0)))
                {
                    this.ChkDealerRank.Items[j].Selected = true;
                }
            }
            Maticsoft.Model.Shop.Sales.SalesRule model = this.ruleBll.GetModel(RuleId);
            this.txtRuleName.Text = model.RuleName;
            this.radMode.SelectedValue = model.RuleMode.ToString();
            this.radStatus.SelectedValue = model.Status.ToString();
            this.radUnit.SelectedValue = model.RuleUnit.ToString();
            List<Maticsoft.Model.Shop.Sales.SalesItem> modelList = this.itemBll.GetModelList(" RuleId=" + RuleId);
            string str = "";
            if ((modelList != null) && (modelList.Count > 0))
            {
                this.radItemType.SelectedValue = modelList[0].ItemType.ToString();
                foreach (Maticsoft.Model.Shop.Sales.SalesItem item in modelList)
                {
                    if (string.IsNullOrWhiteSpace(str))
                    {
                        str = item.UnitValue + "|" + item.RateValue;
                    }
                    else
                    {
                        str = string.Concat(new object[] { str, ",", item.UnitValue, "|", item.RateValue });
                    }
                }
            }
            this.hfItems.Value = str;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x22e;
            }
        }

        public int RuleId
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

