namespace Maticsoft.BLL.CMS
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class Comment
    {
        private readonly IComment dal = DataAccess<IComment>.Create("CMS.Comment");

        public int Add(Maticsoft.Model.CMS.Comment model)
        {
            return this.dal.Add(model);
        }

        public int AddEx(Maticsoft.Model.CMS.Comment model)
        {
            if (FilterWords.ContainsModWords(model.Description))
            {
                model.State = false;
            }
            return this.dal.AddEx(model);
        }

        public int AddTran(Maticsoft.Model.CMS.Comment model)
        {
            return this.dal.AddTran(model);
        }

        public List<Maticsoft.Model.CMS.Comment> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.CMS.Comment> list = new List<Maticsoft.Model.CMS.Comment>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.CMS.Comment item = this.dal.DataRowToModel(dt.Rows[i]);
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

        public bool Exists(int ID)
        {
            return this.dal.Exists(ID);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.CMS.Comment> GetComments(int ContentId, int StartIndex, int EndIndex)
        {
            DataSet set = this.GetListByPage(" State=1 and ContentId=" + ContentId, " CreatedDate desc ", StartIndex, EndIndex);
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

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.CMS.Comment GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.CMS.Comment GetModelByCache(int ID)
        {
            string cacheKey = "CommentModel-" + ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ID);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.CMS.Comment) cache;
        }

        public List<Maticsoft.Model.CMS.Comment> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.CMS.Comment> GetModelList(int top, int id, int typeid)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" State=1 and ContentId={0} and TypeId ={1}", id, typeid);
            DataSet set = this.dal.GetList(top, builder.ToString(), " ID desc  ");
            return this.DataTableToList(set.Tables[0]);
        }

        public List<Maticsoft.Model.CMS.Comment> GetModelList(int Top, string strWhere, string filedOrder)
        {
            return this.DataTableToList(this.dal.GetListEx(Top, strWhere, filedOrder).Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.CMS.Comment model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateList(string IDlist, int state)
        {
            return this.dal.UpdateList(IDlist, state);
        }
    }
}

