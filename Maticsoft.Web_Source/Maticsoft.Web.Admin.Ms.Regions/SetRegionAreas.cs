namespace Maticsoft.Web.Admin.Ms.Regions
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public class SetRegionAreas : PageBaseAdmin
    {
        protected DropDownList dropRegion;
        protected HiddenField hidColse;
        protected HiddenField hidRegionIDsLoad;
        protected HiddenField hidRegionValue;
        protected Literal Literal2;
        protected Literal Literal3;
        private Regions regBll = new Regions();
        protected Button SaveBut;

        private void BindData()
        {
            List<Regions> provinceList = this.regBll.GetProvinceList();
            this.dropRegion.DataSource = provinceList;
            this.dropRegion.DataTextField = "RegionName";
            this.dropRegion.DataValueField = "RegionId";
            this.dropRegion.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack && (this.Areaid > 0))
            {
                this.BindData();
                this.ShowLoad();
            }
        }

        protected void SaveBut_Click(object sender, EventArgs e)
        {
            string str = this.hidRegionValue.Value;
            string str2 = this.hidRegionIDsLoad.Value;
            if (str2 != str)
            {
                string[] dbvalue = str2.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] pagevalue = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string str3 = "";
                string str4 = "";
                Predicate<string> match = null;
                for (int i = 0; i < dbvalue.Length; i++)
                {
                    if (match == null)
                    {
                        match = xx => xx == dbvalue[i];
                    }
                    if (!Array.Exists<string>(pagevalue, match))
                    {
                        str4 = str4 + dbvalue[i] + ",";
                    }
                }
                Predicate<string> predicate2 = null;
                for (int i = 0; i < pagevalue.Length; i++)
                {
                    if (predicate2 == null)
                    {
                        predicate2 = xx => xx == pagevalue[i];
                    }
                    if (!Array.Exists<string>(dbvalue, predicate2))
                    {
                        str3 = str3 + pagevalue[i] + ",";
                    }
                }
                if (!string.IsNullOrWhiteSpace(str4))
                {
                    str4 = str4.TrimEnd(new char[] { ',' });
                    if (this.regBll.UpdateAreaID(str4, 0))
                    {
                        this.hidColse.Value = "1";
                    }
                }
                else
                {
                    this.hidColse.Value = "1";
                }
                if (string.IsNullOrWhiteSpace(str3))
                {
                    this.hidColse.Value = this.hidColse.Value + "2";
                }
                else
                {
                    str3 = str3.TrimEnd(new char[] { ',' });
                    if (this.regBll.UpdateAreaID(str3, this.Areaid))
                    {
                        MessageBox.ShowSuccessTip(this, "修改成功");
                        this.hidColse.Value = this.hidColse.Value + "2";
                    }
                }
            }
            else
            {
                this.hidColse.Value = "12";
            }
        }

        private void ShowLoad()
        {
            this.hidRegionIDsLoad.Value = this.regBll.GetRegionIDsByAreaId(this.Areaid);
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x14d;
            }
        }

        protected int Areaid
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.QueryString["areaid"]))
                {
                    num = Globals.SafeInt(base.Request.QueryString["areaid"], 0);
                }
                return num;
            }
        }
    }
}

