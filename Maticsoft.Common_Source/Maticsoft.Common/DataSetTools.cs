namespace Maticsoft.Common
{
    using System;
    using System.Data;

    public class DataSetTools
    {
        public static bool DataSetIsNull(DataSet ds)
        {
            if (((ds != null) && (ds.Tables.Count != 0)) && ((ds.Tables[0].Rows.Count != 0) && (ds.Tables[0].Columns.Count != 0)))
            {
                return false;
            }
            return true;
        }

        public static DataRowCollection GetDataSetRows(DataSet ds)
        {
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0].Rows;
            }
            return null;
        }
    }
}

