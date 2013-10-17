namespace Maticsoft.IDAL.Shop.Sample
{
    using Maticsoft.Model.Shop.Sample;
    using System;
    using System.Data;

    public interface ISample
    {
        int Add(Maticsoft.Model.Shop.Sample.Sample model);
        Maticsoft.Model.Shop.Sample.Sample DataRowToModel(DataRow row);
        bool Delete(int SampleId);
        bool DeleteList(string SampleIdlist);
        bool Exists(int SampleId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        Maticsoft.Model.Shop.Sample.Sample GetModel(int SampleId);
        int GetRecordCount(string strWhere);
        bool Update(Maticsoft.Model.Shop.Sample.Sample model);
    }
}

