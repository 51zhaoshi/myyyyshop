namespace Maticsoft.Web.Controls
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;

    public class VideoClassDropDownList : DropDownList
    {
        public VideoClassDropDownList()
        {
            this.NullToDisplay = false;
            this.ParentId = null;
        }

        private void BindNode(string parentColName, string parentid, DataTable dt, string blank)
        {
            foreach (DataRow row in dt.Select(parentColName + "= " + parentid))
            {
                string str = row[0].ToString();
                this.Items.Add(new ListItem(blank + "『" + Globals.HtmlDecode(row[1].ToString()) + "』", str));
                this.BindNode(parentColName, str, dt, blank + "─");
            }
        }

        public override void DataBind()
        {
            this.Items.Clear();
            if (this.NullToDisplay)
            {
                this.Items.Add(new ListItem("--  请选择 --", "0"));
            }
            DataSet list = new VideoClass().GetList("");
            if (!DataSetTools.DataSetIsNull(list))
            {
                this.SetMultiLevelDropDownList("ParentId", list.Tables[0]);
            }
            base.DataBind();
        }

        public void SetMultiLevelDropDownList(string parentColName, DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Select(parentColName + "= 0"))
            {
                string str = row[0].ToString();
                this.Items.Add(new ListItem("╋" + Globals.HtmlDecode(row[1].ToString()), str));
                this.BindNode(parentColName, str, dataTable, "├");
            }
        }

        public bool NullToDisplay { get; set; }

        public int? ParentId { get; set; }
    }
}

