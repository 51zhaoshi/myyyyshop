namespace Maticsoft.IDAL.CMS
{
    using Maticsoft.Model.CMS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IClassType
    {
        bool Add(ClassType model);
        List<ClassType> DataTableToList(DataTable dt);
        bool Delete(int ClassTypeID);
        bool DeleteList(string ClassTypeIDlist);
        bool Exists(int ClassTypeID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int GetMaxId();
        ClassType GetModel(int ClassTypeID);
        bool Update(ClassType model);
    }
}

