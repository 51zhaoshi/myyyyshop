namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class AlbumType
    {
        private readonly IAlbumType dal = DASNS.CreateAlbumType();

        public int Add(Maticsoft.Model.SNS.AlbumType model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.AlbumType> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.AlbumType> list = new List<Maticsoft.Model.SNS.AlbumType>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.AlbumType item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ID)
        {
            return this.dal.Delete(ID);
        }

        public bool DeleteList(string IDlist)
        {
            return this.dal.DeleteList(IDlist);
        }

        public bool Exists(string TypeName)
        {
            return this.dal.Exists(TypeName);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.SNS.AlbumType> GetIndexList()
        {
            DataSet set = this.dal.GetList(-1, " MenuIsShow=1 and Status=1 and AlbumsCount>0", "MenuSequence");
            return this.DataTableToList(set.Tables[0]);
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Maticsoft.Model.SNS.AlbumType GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.SNS.AlbumType GetModelByCache(int ID)
        {
            string cacheKey = "AlbumTypeModel-" + ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ID);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.SNS.AlbumType) cache;
        }

        public List<Maticsoft.Model.SNS.AlbumType> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.SNS.AlbumType> GetModelListByCache(EnumHelper.Status Status)
        {
            string cacheKey = "AlbumTypeList_" + ((int) Status);
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetModelList(" Status=" + ((int) Status));
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (List<Maticsoft.Model.SNS.AlbumType>) cache;
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetSearchList(string Keywords)
        {
            return this.dal.GetList(0, string.Format(" TypeName like '%{0}%'", Keywords), "MenuSequence");
        }

        public List<Maticsoft.Model.SNS.AlbumType> GetTypeList(int albumId)
        {
            DataSet typeList = this.dal.GetTypeList(albumId);
            return this.DataTableToList(typeList.Tables[0]);
        }

        public bool Update(Maticsoft.Model.SNS.AlbumType model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateIsMenu(int IsMenu, string IdList)
        {
            return this.dal.UpdateIsMenu(IsMenu, IdList);
        }

        public bool UpdateMenuIsShow(int MenuIsShow, string IdList)
        {
            return this.dal.UpdateMenuIsShow(MenuIsShow, IdList);
        }

        public bool UpdateStatus(int Status, string IdList)
        {
            return this.dal.UpdateStatus(Status, IdList);
        }
    }
}

