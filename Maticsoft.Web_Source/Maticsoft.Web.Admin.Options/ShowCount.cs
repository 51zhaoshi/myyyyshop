namespace Maticsoft.Web.Admin.Options
{
    using Maticsoft.BLL.Poll;
    using Maticsoft.Model.Poll;
    using Maticsoft.Web;
    using Maticsoft.Web.Admin;
    using System;
    using System.Collections;
    using System.Data;
    using System.Web.UI.WebControls;

    public class ShowCount : PageBaseAdmin
    {
        public string alluser = "0";
        public string formuser = "0";
        protected GridView gridViewTopic;
        protected Label lblFormID;
        protected Label lblFormName;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        public string polluser = "0";

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
            DataRow[] rowArray = table.Select("TopicID=" + TopicID);
            object obj2 = table.Compute("sum(SubmitNum)", "TopicID=" + TopicID);
            int num = 0;
            if (obj2.ToString() != "")
            {
                num = int.Parse(obj2.ToString());
            }
            ArrayList list = new ArrayList();
            for (int i = 0; i < rowArray.Length; i++)
            {
                string str = rowArray[i]["Name"].ToString();
                int num3 = int.Parse(rowArray[i]["SubmitNum"].ToString());
                OptionList list2 = new OptionList {
                    name = str,
                    count = num3.ToString(),
                    totalcount = num.ToString()
                };
                list.Add(list2);
            }
            itemsview.DataSource = list;
            itemsview.DataBind();
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

        protected void gridViewTopic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView itemsview = (GridView) e.Row.FindControl("gridViewOption");
            if (itemsview != null)
            {
                this.BindItemData(itemsview, int.Parse(this.gridViewTopic.DataKeys[e.Row.RowIndex].Value.ToString()));
            }
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
                Maticsoft.BLL.Poll.UserPoll poll = new Maticsoft.BLL.Poll.UserPoll();
                this.polluser = poll.GetUserByForm(0).ToString();
                this.formuser = poll.GetUserByForm(formID).ToString();
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

