namespace Maticsoft.IDAL.Poll
{
    using Maticsoft.Model.Poll;
    using System;
    using System.Data;

    public interface IForms
    {
        int Add(Forms model);
        void Delete(int FormID);
        bool DeleteList(string FormIDlist);
        bool Exists(int FormID);
        DataSet GetList(string strWhere);
        int GetMaxId();
        Forms GetModel(int FormID);
        int Update(Forms model);
    }
}

