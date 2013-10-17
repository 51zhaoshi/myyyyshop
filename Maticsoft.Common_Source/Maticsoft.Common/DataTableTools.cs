namespace Maticsoft.Common
{
    using System;
    using System.Data;

    public class DataTableTools
    {
        public static bool DataTableIsNull(DataTable dt)
        {
            if (((dt != null) && (dt.Columns.Count != 0)) && (dt.Rows.Count != 0))
            {
                return false;
            }
            return true;
        }
    }
}

