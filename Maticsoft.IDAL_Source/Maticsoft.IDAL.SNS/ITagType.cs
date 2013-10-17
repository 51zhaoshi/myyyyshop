namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface ITagType
    {
        int Add(TagType model);
        TagType DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(string TypeName);
        DataSet GetAllListEX();
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        TagType GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool RelationSNSCate(int tagTypeId, int SNSCategoryId);
        bool Update(TagType model);
    }
}

