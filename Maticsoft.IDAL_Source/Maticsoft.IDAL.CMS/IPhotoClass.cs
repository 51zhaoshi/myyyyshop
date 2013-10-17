namespace Maticsoft.IDAL.CMS
{
    using Maticsoft.Model.CMS;
    using System;
    using System.Data;

    public interface IPhotoClass
    {
        void Add(PhotoClass model);
        bool Delete(int ClassID);
        bool DeleteList(string ClassIDlist);
        bool Exists(int ClassID);
        bool ExistsByClassName(string ClassName);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int GetMaxId();
        int GetMaxSequence();
        PhotoClass GetModel(int ClassID);
        bool Update(PhotoClass model);
    }
}

