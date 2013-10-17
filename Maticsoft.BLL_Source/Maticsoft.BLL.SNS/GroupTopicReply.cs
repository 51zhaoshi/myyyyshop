namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class GroupTopicReply
    {
        private readonly IGroupTopicReply dal = DASNS.CreateGroupTopicReply();

        public int Add(Maticsoft.Model.SNS.GroupTopicReply model)
        {
            return this.dal.Add(model);
        }

        public int AddEx(Maticsoft.Model.SNS.GroupTopicReply TModel, long Pid)
        {
            Maticsoft.Model.SNS.Products pModel = new Maticsoft.Model.SNS.Products();
            Maticsoft.BLL.SNS.Products products2 = new Maticsoft.BLL.SNS.Products();
            if (Pid > 0L)
            {
                pModel.ProductID = Pid;
                pModel.CreateUserID = TModel.ReplyUserID;
                pModel.CreatedNickName = TModel.ReplyNickName;
                pModel.CreatedDate = DateTime.Now;
                pModel = products2.GetProductModel(pModel);
            }
            if (FilterWords.ContainsModWords(TModel.Description))
            {
                pModel.Status = 0;
                TModel.Status = 0;
            }
            else
            {
                TModel.Description = FilterWords.ReplaceWords(TModel.Description);
            }
            return this.dal.AddEx(TModel, pModel);
        }

        public List<Maticsoft.Model.SNS.GroupTopicReply> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.GroupTopicReply> list = new List<Maticsoft.Model.SNS.GroupTopicReply>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.GroupTopicReply item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ReplyID)
        {
            return this.dal.Delete(ReplyID);
        }

        public bool DeleteEx(int ReplyID)
        {
            return this.dal.DeleteEx(ReplyID);
        }

        public bool DeleteList(string ReplyIDlist)
        {
            return this.dal.DeleteList(ReplyIDlist);
        }

        public bool DeleteListEx(string TopicIDlist)
        {
            return this.dal.DeleteListEx(TopicIDlist);
        }

        public int ForwardReply(int ReplyId, string Des, int CurrentUserId, string NickName)
        {
            Maticsoft.Model.SNS.GroupTopicReply model = this.GetModel(ReplyId);
            Maticsoft.Model.SNS.GroupTopicReply tModel = new Maticsoft.Model.SNS.GroupTopicReply {
                OriginalID = ReplyId,
                Description = Des,
                OrginalDes = model.Description,
                CreatedDate = DateTime.Now,
                GroupID = model.GroupID,
                OrgianlNickName = model.ReplyNickName,
                ReplyUserID = CurrentUserId,
                TopicID = model.TopicID,
                PhotoUrl = (model.OriginalID > 0) ? "" : model.PhotoUrl,
                ProductLinkUrl = (model.OriginalID > 0) ? "" : model.ProductLinkUrl,
                ProductName = (model.OriginalID > 0) ? "" : model.ProductName,
                ProductUrl = (model.OriginalID > 0) ? "" : model.ProductUrl,
                Price = (model.OriginalID > 0) ? 0 : model.Price,
                TargetId = model.TargetId,
                Type = model.Type,
                ReplyNickName = NickName,
                Type = -1
            };
            return this.dal.ForwardReply(tModel);
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

        public DataSet GetListEx(string strWhere)
        {
            return this.dal.GetListEx(strWhere);
        }

        public Maticsoft.Model.SNS.GroupTopicReply GetModel(int ReplyID)
        {
            return this.dal.GetModel(ReplyID);
        }

        public Maticsoft.Model.SNS.GroupTopicReply GetModelByCache(int ReplyID)
        {
            string cacheKey = "GroupTopicReplyModel-" + ReplyID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ReplyID);
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
            return (Maticsoft.Model.SNS.GroupTopicReply) cache;
        }

        public List<Maticsoft.Model.SNS.GroupTopicReply> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public List<Maticsoft.Model.SNS.GroupTopicReply> GetTopicReplyByTopic(int TopicId, int StartIndex, int EndIndex)
        {
            return this.DataTableToList(this.GetListByPage(" Status=1 and TopicID =" + TopicId, "CreatedDate Desc", StartIndex, EndIndex).Tables[0]);
        }

        public bool Update(Maticsoft.Model.SNS.GroupTopicReply model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateStatusList(string IdsStr, EnumHelper.TopicStatus status)
        {
            return this.dal.UpdateStatusList(IdsStr, status);
        }
    }
}

