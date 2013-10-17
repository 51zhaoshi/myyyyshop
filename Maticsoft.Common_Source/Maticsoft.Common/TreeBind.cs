namespace Maticsoft.Common
{
    using System;
    using System.Data;
    using System.Web.UI.WebControls;

    public class TreeBind
    {
        private static void BindNode(DropDownList dropDownList, string parentColName, string parentid, DataTable dt, string blank)
        {
            foreach (DataRow row in dt.Select(parentColName + "= " + parentid))
            {
                string str = row[0].ToString();
                dropDownList.Items.Add(new ListItem(blank + "『" + Globals.HtmlDecode(row[1].ToString()) + "』", str));
                BindNode(dropDownList, parentColName, str, dt, blank + "─");
            }
        }

        public static void SetMultiLevelDropDownList(DropDownList dropDownList, string parentColName, DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Select(parentColName + "= " + 0))
            {
                string str = row[0].ToString();
                dropDownList.Items.Add(new ListItem("╋" + Globals.HtmlDecode(row[1].ToString()), str));
                BindNode(dropDownList, parentColName, str, dataTable, "├");
            }
        }
    }
}

