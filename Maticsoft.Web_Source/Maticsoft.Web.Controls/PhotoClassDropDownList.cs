namespace Maticsoft.Web.Controls
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;

    public class PhotoClassDropDownList : DropDownList
    {
        public PhotoClassDropDownList()
        {
            this.NullToDisplay = false;
            this.ParentId = null;
        }

        public override void DataBind()
        {
            this.Items.Clear();
            if (this.NullToDisplay)
            {
                this.Items.Add(new ListItem("", "0"));
            }
            DataSet list = new PhotoClass().GetList(this.ParentId.HasValue ? ("ParentId=" + this.ParentId) : "0");
            if ((list != null) && (list.Tables[0].Rows.Count > 0))
            {
                TreeBind.SetMultiLevelDropDownList(this, "ParentId", list.Tables[0]);
            }
            base.DataBind();
        }

        public bool NullToDisplay { get; set; }

        public int? ParentId { get; set; }
    }
}

