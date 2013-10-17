namespace Maticsoft.Web.Admin.Options
{
    using Maticsoft.BLL.Poll;
    using Maticsoft.Model.Poll;
    using Maticsoft.Web;
    using Maticsoft.Web.Admin;
    using Resources;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI.WebControls;

    public class ShowCount3D : PageBaseAdmin
    {
        public int allsum;
        protected GridView gridViewTopic;
        protected Label lblFormID;
        protected Label lblFormName;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        public string usercount = "0";

        public void BindData(int FormID)
        {
            Maticsoft.BLL.Poll.Options options = new Maticsoft.BLL.Poll.Options();
            DataTable table = options.GetCountList(FormID).Tables[0];
            this.ViewState["dtcount"] = table;
            DataSet listByForm = new Maticsoft.BLL.Poll.Topics().GetListByForm(FormID);
            this.gridViewTopic.DataSource = listByForm;
            this.gridViewTopic.DataBind();
        }

        private void BindItemData(GridView itemsview, int TopicID)
        {
            DataTable table = (DataTable) this.ViewState["dtcount"];
            object obj2 = table.Compute("sum(SubmitNum)", "");
            if (obj2.ToString() != "")
            {
                this.allsum = int.Parse(obj2.ToString());
            }
            table.Select("TopicID=" + TopicID);
            object obj3 = table.Compute("sum(SubmitNum)", "TopicID=" + TopicID);
            if (obj3.ToString() != "")
            {
                int.Parse(obj3.ToString());
            }
        }

        public int FormatCount(string count, string sumcount)
        {
            if ((count.Length < 1) || (sumcount.Length < 1))
            {
                return 0;
            }
            int num = int.Parse(sumcount);
            int num2 = int.Parse(count);
            if (num < 1)
            {
                return 0;
            }
            return ((num2 * 100) / num);
        }

        public int FormatImage(int swidth)
        {
            return (swidth * 4);
        }

        public string GetChartHtml(int TopicID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<chart caption='" + Poll.OptionsStatistics + "' subCaption='' xAxisName='" + Poll.Item + "' pieSliceDepth='30' showBorder='0' formatNumberScale='0' numberSuffix=' " + Poll.Votes + "' animation='1'>");
            DataRow[] rowArray = ((DataTable) this.ViewState["dtcount"]).Select("TopicID=" + TopicID);
            for (int i = 0; i < rowArray.Length; i++)
            {
                string str = rowArray[i]["Name"].ToString();
                string str2 = rowArray[i]["SubmitNum"].ToString();
                builder.AppendFormat("<set label='{0}' value='{1}' />", str, str2);
            }
            builder.Append("</chart>");
            return FusionCharts.RenderChart("/FusionCharts/Pie3D.swf", "", builder.ToString(), "PollSum" + TopicID.ToString(), "500", "300", false, false);
        }

        protected void gridViewTopic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["fid"] != null)) && (base.Request.Params["fid"].Trim() != ""))
            {
                string str = base.Request.Params["fid"];
                int formID = Convert.ToInt32(str);
                Maticsoft.Model.Poll.Forms model = new Maticsoft.BLL.Poll.Forms().GetModel(formID);
                this.lblFormName.Text = model.Name;
                this.lblFormID.Text = model.FormID.ToString();
                this.BindData(formID);
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x164;
            }
        }

        public Basic Master
        {
            get
            {
                return (Basic) base.Master;
            }
        }

        public class OptionList
        {
            public string count;
            public string name;
            public string totalcount;
        }
    }
}

