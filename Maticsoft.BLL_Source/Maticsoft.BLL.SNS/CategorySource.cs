namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using Maticsoft.TaoBao.Request;
    using Maticsoft.TaoBao.Response;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class CategorySource
    {
        public int AddCount;
        private readonly ICategorySource dal = DASNS.CreateCategorySource();
        public int UpdateCount;

        public int Add(Maticsoft.Model.SNS.CategorySource model)
        {
            return this.dal.Add(model);
        }

        public bool AddCategory(Maticsoft.Model.SNS.CategorySource model)
        {
            return this.dal.AddCategory(model);
        }

        public void CategoryLoop(long CategoryId, string Path, int Depth)
        {
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("OpenAPI_TaoBaoAppkey");
            string appSecret = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("OpenAPI_TaobaoAppsecret");
            string serverUrl = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("OpenAPI_TaobaoApiUrl");
            Maticsoft.Model.SNS.CategorySource model = new Maticsoft.Model.SNS.CategorySource();
            ITopClient client = new DefaultTopClient(serverUrl, valueByCache, appSecret);
            ItemcatsGetRequest request = new ItemcatsGetRequest {
                Fields = "cid,parent_cid,name,is_parent",
                ParentCid = new long?(CategoryId)
            };
            ItemcatsGetResponse response = client.Execute<ItemcatsGetResponse>(request);
            if (response.ItemCats.Count > 0)
            {
                foreach (ItemCat cat in response.ItemCats)
                {
                    model.CategoryId = Globals.SafeInt(cat.Cid.ToString(), 0);
                    model.ParentID = Globals.SafeInt(cat.ParentCid.ToString(), 0);
                    if (!this.Exists(3, model.CategoryId))
                    {
                        if (string.IsNullOrEmpty(Path))
                        {
                            model.Path = cat.Cid.ToString();
                        }
                        else
                        {
                            model.Path = Path + "|" + cat.Cid.ToString();
                        }
                        model.Depth = Depth + 1;
                        model.CreatedDate = DateTime.Now;
                        model.CreatedUserID = 1;
                        model.Description = "暂无描述";
                        model.HasChildren = cat.IsParent;
                        model.IsMenu = false;
                        model.MenuIsShow = false;
                        model.MenuSequence = 0;
                        model.Name = cat.Name;
                        model.Status = 1;
                        model.Type = 0;
                        model.SourceId = 3;
                        this.Add(model);
                        this.AddCount++;
                    }
                    else if (this.IsUpdate((long) model.CategoryId, cat.Name, 3, model.ParentID))
                    {
                        if (string.IsNullOrEmpty(Path))
                        {
                            model.Path = cat.Cid.ToString();
                        }
                        else
                        {
                            model.Path = Path + "|" + cat.Cid.ToString();
                        }
                        model.Depth = Depth + 1;
                        model.CreatedDate = DateTime.Now;
                        model.CreatedUserID = 1;
                        model.Description = "暂无描述";
                        model.HasChildren = cat.IsParent;
                        model.IsMenu = false;
                        model.MenuIsShow = false;
                        model.MenuSequence = 0;
                        model.Name = cat.Name;
                        model.Status = 1;
                        model.Type = 0;
                        model.SourceId = 3;
                        this.Update(model);
                        this.UpdateCount++;
                    }
                    Thread currentThread = Thread.CurrentThread;
                    Thread.Sleep(500);
                    if (cat.IsParent)
                    {
                        this.CategoryLoop(cat.Cid, model.Path, model.Depth);
                    }
                }
            }
        }

        public List<Maticsoft.Model.SNS.CategorySource> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.CategorySource> list = new List<Maticsoft.Model.SNS.CategorySource>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.CategorySource item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int SourceId, int CategoryId)
        {
            return this.dal.Delete(SourceId, CategoryId);
        }

        public bool DeleteCategory(int categoryId)
        {
            return this.dal.DeleteCategory(categoryId);
        }

        public bool Exists(int SourceId, int CategoryId)
        {
            return this.dal.Exists(SourceId, CategoryId);
        }

        public void GetAllCategory()
        {
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("OpenAPI_TaoBaoAppkey");
            string appSecret = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("OpenAPI_TaobaoAppsecret");
            ITopClient client = new DefaultTopClient(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("OpenAPI_TaobaoApiUrl"), valueByCache, appSecret);
            TopatsItemcatsGetRequest request = new TopatsItemcatsGetRequest();
            client.Execute<TopatsItemcatsGetResponse>(request);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetCategoryList(string strWhere)
        {
            return this.dal.GetCategoryList(strWhere);
        }

        public List<Maticsoft.Model.SNS.CategorySource> GetCategorysByDepth(int depth)
        {
            return this.GetModelList("Depth = " + depth);
        }

        public DataSet GetCategorysByParentId(int parentCategoryId)
        {
            return this.GetList("ParentID = " + parentCategoryId);
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

        public Maticsoft.Model.SNS.CategorySource GetModel(int SourceId, int CategoryId)
        {
            return this.dal.GetModel(SourceId, CategoryId);
        }

        public Maticsoft.Model.SNS.CategorySource GetModelByCache(int SourceId, int CategoryId)
        {
            string cacheKey = "CategorySourceModel-" + SourceId + CategoryId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(SourceId, CategoryId);
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
            return (Maticsoft.Model.SNS.CategorySource) cache;
        }

        public List<Maticsoft.Model.SNS.CategorySource> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool IsExistedCate(int categoryid)
        {
            Maticsoft.BLL.SNS.CategorySource source = new Maticsoft.BLL.SNS.CategorySource();
            return (source.GetRecordCount("CategoryID=" + categoryid) > 0);
        }

        public bool IsUpdate(long CategoryId, string name, int SourceId, int ParentID)
        {
            return this.dal.IsUpdate(CategoryId, name, SourceId, ParentID);
        }

        public void ResetCategory(out int addCount, out int updateCount)
        {
            this.CategoryLoop(0L, "", 0);
            addCount = this.AddCount;
            updateCount = this.UpdateCount;
        }

        public bool Update(Maticsoft.Model.SNS.CategorySource model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateCategory(Maticsoft.Model.SNS.CategorySource model)
        {
            return this.dal.UpdateCategory(model);
        }

        public bool UpdateSNSCate(int CategoryId, int SNSCateId, bool IsLoop)
        {
            return this.dal.UpdateSNSCate(CategoryId, SNSCateId, IsLoop);
        }

        public bool UpdateSNSCateList(string ids, int SNSCateId, bool IsLoop)
        {
            return this.dal.UpdateSNSCateList(ids, SNSCateId, IsLoop);
        }
    }
}

