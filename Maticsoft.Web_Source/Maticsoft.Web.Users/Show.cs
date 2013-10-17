namespace Maticsoft.Web.Users
{
    using Maticsoft.BLL.Poll;
    using Maticsoft.Web;
    using Maticsoft.Web.Admin;
    using Resources;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        private Options blloption = new Options();
        private Topics blltop = new Topics();
        private UserPoll bllup = new UserPoll();
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Repeater rptVote;

        public string GetOptionName(int TopicID)
        {
            StringBuilder builder = new StringBuilder();
            if (this.ViewState["udt"] != null)
            {
                builder.Append(Poll.lblSelected);
                DataRow[] rowArray = ((DataTable) this.ViewState["udt"]).Select(" TopicID=" + TopicID);
                for (int i = 0; i < rowArray.Length; i++)
                {
                    int iD = Convert.ToInt32(rowArray[i]["OptionID"].ToString());
                    builder.Append(this.blloption.GetModelByCache(iD).Name + "&nbsp;&nbsp;&nbsp;&nbsp;");
                }
            }
            return builder.ToString();
        }

        public string GetTopicTitle(int TopicID)
        {
            return this.blltop.GetModelByCache(TopicID).Title;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["uid"] != null)) && (base.Request.Params["uid"].Trim() != ""))
            {
                string str = base.Request.Params["uid"];
                this.ShowInfo(Convert.ToInt32(str));
            }
        }

        public void rptVote_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }

        private void ShowInfo(int UserID)
        {
            DataTable table = this.bllup.GetListInnerJoin(UserID).Tables[0];
            this.ViewState["udt"] = table;
            this.rptVote.DataSource = table;
            this.rptVote.DataBind();
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x16d;
            }
        }

        public Basic Master
        {
            get
            {
                return (Basic) base.Master;
            }
        }
    }
}

