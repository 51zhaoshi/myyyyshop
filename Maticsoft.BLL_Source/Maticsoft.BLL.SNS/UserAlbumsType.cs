namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UserAlbumsType
    {
        private readonly IUserAlbumsType dal = DASNS.CreateUserAlbumsType();

        public bool Add(Maticsoft.Model.SNS.UserAlbumsType model)
        {
            return this.dal.Add(model);
        }

        public bool AddEx(Maticsoft.Model.SNS.UserAlbumsType model)
        {
            return (!this.Exists(model.AlbumsID, model.TypeID) && this.dal.Add(model));
        }

        public List<Maticsoft.Model.SNS.UserAlbumsType> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.UserAlbumsType> list = new List<Maticsoft.Model.SNS.UserAlbumsType>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.UserAlbumsType item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int AlbumsID, int TypeID)
        {
            return this.dal.Delete(AlbumsID, TypeID);
        }

        public bool Exists(int AlbumsID, int TypeID)
        {
            return this.dal.Exists(AlbumsID, TypeID);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
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

        public Maticsoft.Model.SNS.UserAlbumsType GetModel(int AlbumsID, int TypeID)
        {
            return this.dal.GetModel(AlbumsID, TypeID);
        }

        public Maticsoft.Model.SNS.UserAlbumsType GetModelByCache(int AlbumsID, int TypeID)
        {
            string cacheKey = "UserAlbumsTypeModel-" + AlbumsID + TypeID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(AlbumsID, TypeID);
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
            return (Maticsoft.Model.SNS.UserAlbumsType) cache;
        }

        public Maticsoft.Model.SNS.UserAlbumsType GetModelByUserId(int AlbumsID, int UserId)
        {
            return this.dal.GetModelByUserId(AlbumsID, UserId);
        }

        public List<Maticsoft.Model.SNS.UserAlbumsType> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.SNS.UserAlbumsType model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateType(Maticsoft.Model.SNS.UserAlbumsType model)
        {
            return this.dal.UpdateType(model);
        }
    }
}

