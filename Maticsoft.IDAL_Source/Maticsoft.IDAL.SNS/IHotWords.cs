namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IHotWords
    {
        int Add(HotWords model);
        HotWords DataRowToModel(DataRow row);
        bool Delete();
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(string KeyWord);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxSequence();
        HotWords GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(HotWords model);
    }
}

