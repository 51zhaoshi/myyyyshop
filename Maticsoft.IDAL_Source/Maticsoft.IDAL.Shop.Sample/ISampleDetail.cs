namespace Maticsoft.IDAL.Shop.Sample
{
    using Maticsoft.Model.Shop.Sample;
    using System;
    using System.Data;

    public interface ISampleDetail
    {
        int Add(SampleDetail model);
        SampleDetail DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(int ID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SampleDetail GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(SampleDetail model);
    }
}

