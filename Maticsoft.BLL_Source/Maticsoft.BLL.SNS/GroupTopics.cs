namespace Maticsoft.BLL.SNS
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Settings;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Web;

    public class GroupTopics
    {
        private readonly IGroupTopics dal = DASNS.CreateGroupTopics();
        public const string KEY_BAN = "SNS_BAN_TOPIC_USERID-{0}";

        public int Add(Maticsoft.Model.SNS.GroupTopics model)
        {
            return this.dal.Add(model);
        }

        public int AddEx(Maticsoft.Model.SNS.GroupTopics TModel, long Pid)
        {
            Maticsoft.Model.SNS.Products pModel = new Maticsoft.Model.SNS.Products();
            Maticsoft.BLL.SNS.Products products2 = new Maticsoft.BLL.SNS.Products();
            if (Pid > 0L)
            {
                pModel.ProductID = Pid;
                pModel.CreateUserID = TModel.CreatedUserID;
                pModel.CreatedNickName = TModel.CreatedNickName;
                pModel.CreatedDate = DateTime.Now;
                pModel = products2.GetProductModel(pModel);
            }
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_product");
            pModel.Status = (valueByCache == "0") ? 1 : 0;
            bool boolValueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("SNS_Check_GroupTopic");
            TModel.Status = boolValueByCache ? 0 : 1;
            bool contains = false;
            if (FilterWords.ContainsModWords(TModel.Description))
            {
                pModel.Status = 0;
                TModel.Status = 0;
                contains = true;
            }
            else
            {
                TModel.Description = FilterWords.ReplaceWords(TModel.Description, out contains);
            }
            if (contains && BanUserCheck(TModel.CreatedUserID))
            {
                return -2;
            }
            Maticsoft.BLL.SNS.Groups groups = new Maticsoft.BLL.SNS.Groups();
            Maticsoft.Model.SNS.Groups model = new Maticsoft.Model.SNS.Groups();
            model = groups.GetModel(TModel.GroupID);
            TModel.GroupName = model.GroupName;
            return this.dal.AddEx(TModel, pModel);
        }

        public static bool BanUserCheck(int userId)
        {
            if (userId >= 1)
            {
                if ((BanTopicCount < 1) || (BanTopicTime < 1))
                {
                    return false;
                }
                string cacheKey = string.Format("SNS_BAN_TOPIC_USERID-{0}", userId);
                int num = Globals.SafeInt(DataCache.GetCache(cacheKey), 0);
                DataCache.SetCache(cacheKey, ++num, DateTime.Now.AddMinutes((double) BanTopicTime), TimeSpan.Zero);
                if (num > BanTopicCount)
                {
                    new User().UpdateActivity(userId, false);
                    return true;
                }
            }
            return false;
        }

        public static string CreateIDCode()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            DateTime time2 = Convert.ToDateTime("1970-01-01");
            TimeSpan span = (TimeSpan) (time - time2);
            return span.TotalMilliseconds.ToString("0");
        }

        public List<Maticsoft.Model.SNS.GroupTopics> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.GroupTopics> list = new List<Maticsoft.Model.SNS.GroupTopics>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.GroupTopics item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int TopicID)
        {
            return this.dal.Delete(TopicID);
        }

        public bool DeleteEx(int TopicID)
        {
            return this.dal.DeleteEx(TopicID);
        }

        public bool DeleteList(string TopicIDlist)
        {
            return this.dal.DeleteList(TopicIDlist);
        }

        public bool DeleteListEx(string TopicIDlist)
        {
            string[] strArray = TopicIDlist.Split(new char[] { ',' });
            string imageUrl = "";
            if (strArray.Length > 0)
            {
                foreach (string str2 in strArray)
                {
                    int topicID = Globals.SafeInt(str2, 0);
                    if (!this.dal.DeleteEx(topicID, out imageUrl))
                    {
                        return false;
                    }
                    FileManage.DeleteFile(HttpContext.Current.Server.MapPath(imageUrl));
                }
            }
            return true;
        }

        public bool Exists(int TopicID)
        {
            return this.dal.Exists(TopicID);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public int GetCountByKeyWord(string q, int GroupId = -1)
        {
            string strWhere = " Status=1 and (Title like '%" + q + "%' or Description like '%" + q + "%') ";
            if (GroupId > 0)
            {
                strWhere = strWhere + " and GroupID=" + GroupId;
            }
            return this.GetRecordCount(strWhere);
        }

        public int GetCountByType(int UserId, EnumHelper.UserGroupType Type)
        {
            string whereByType = this.GetWhereByType(UserId, Type);
            return this.GetRecordCount(whereByType);
        }

        public List<Maticsoft.Model.SNS.GroupTopics> GetHotListByGroup(int GroupId, int top)
        {
            string strWhere = " Status=1 ";
            if (GroupId > 0)
            {
                strWhere = strWhere + " and GroupID=" + GroupId;
            }
            DataSet set = this.dal.GetList(top, strWhere, "ReplyCount Desc");
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

        public List<Maticsoft.Model.SNS.GroupTopics> GetList4Model(int top, string strWhere, string filedOrder)
        {
            return this.DataTableToList(this.dal.GetListEx(top, strWhere, filedOrder).Tables[0]);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetListEx(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetListEx(Top, strWhere, filedOrder);
        }

        public Maticsoft.Model.SNS.GroupTopics GetModel(int TopicID)
        {
            return this.dal.GetModel(TopicID);
        }

        public Maticsoft.Model.SNS.GroupTopics GetModelByCache(int TopicID)
        {
            string cacheKey = "GroupTopicsModel-" + TopicID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(TopicID);
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
            return (Maticsoft.Model.SNS.GroupTopics) cache;
        }

        public List<Maticsoft.Model.SNS.GroupTopics> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.SNS.GroupTopics> GetNewTopListByGroup(int GroupId, int top)
        {
            return this.DataTableToList(this.GetListByPage(" Status=1 and GroupID=" + GroupId, "CreatedDate Desc", 0, top).Tables[0]);
        }

        public int GetRecommandCount(int GroupId)
        {
            return this.GetRecordCount(" Status=1 and GroupID=" + GroupId + " and IsRecomend=1");
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public List<Maticsoft.Model.SNS.GroupTopics> GetRecTopics(int Top = -1)
        {
            DataSet set = this.GetList(Top, "Status=1 and IsAdminRecommend=1", " Sequence");
            return this.DataTableToList(set.Tables[0]);
        }

        public DataSet GetSearchList(string Keywords)
        {
            string strWhere = string.Format(" Title like '%{0}%'", Keywords);
            string filedOrder = " TopicID DESC";
            return this.dal.GetListEx(0, strWhere, filedOrder);
        }

        public List<Maticsoft.Model.SNS.GroupTopics> GetTopicByUserId(int UserId, int top)
        {
            new List<Maticsoft.Model.SNS.GroupTopics>();
            return this.DataTableToList(this.GetListByPage("CreatedUserID=" + UserId, "ReplyCount desc", 0, top).Tables[0]);
        }

        public List<Maticsoft.Model.SNS.GroupTopics> GetTopicListPageByGroup(int GroupId, int StartIndex, int EndIndex, bool IsRecommand)
        {
            List<Maticsoft.Model.SNS.GroupTopics> list = new List<Maticsoft.Model.SNS.GroupTopics>();
            string strWhere = " Status=1 and GroupID=" + GroupId;
            if (IsRecommand)
            {
                strWhere = strWhere + " and IsRecomend=1";
            }
            return this.DataTableToList(this.GetListByPage(strWhere, "CreatedDate Desc", StartIndex, EndIndex).Tables[0]);
        }

        public List<Maticsoft.Model.SNS.GroupTopics> GetUserRelativeTopicByType(int UserId, EnumHelper.UserGroupType Type, int StartIndex, int EndIndex)
        {
            new List<Maticsoft.Model.SNS.GroupTopics>();
            string whereByType = this.GetWhereByType(UserId, Type);
            return this.DataTableToList(this.GetListByPage(whereByType, "CreatedDate Desc", StartIndex, EndIndex).Tables[0]);
        }

        public string GetWhereByType(int UserId, EnumHelper.UserGroupType Type)
        {
            string str = "GroupID in(select GroupID from SNS_GroupUsers where UserID=" + UserId + ")";
            switch (Type)
            {
                case EnumHelper.UserGroupType.UserGroup:
                    return ("GroupID in(select GroupID from SNS_GroupUsers where UserID=" + UserId + ")");

                case EnumHelper.UserGroupType.UserPostTopic:
                    return ("CreatedUserID=" + UserId);

                case EnumHelper.UserGroupType.UserReply:
                    return ("TopicID in (SELECT DISTINCT TopicID FROM SNS_GroupTopicReply WHERE  ReplyUserID=" + UserId + ")");

                case EnumHelper.UserGroupType.UserFav:
                    return ("TopicID in(select TopicID from SNS_GroupTopicFav where CreatedUserID=" + UserId + ")");
            }
            return str;
        }

        public List<Maticsoft.Model.SNS.GroupTopics> SearchTopicByKeyWord(int StartIndex, int EndIndex, string q, int GroupId, string orderby)
        {
            List<Maticsoft.Model.SNS.GroupTopics> list;
            string str2 = orderby;
            if (str2 != null)
            {
                if (!(str2 == "newreply"))
                {
                    if (str2 == "newpost")
                    {
                        orderby = "LastPostTime desc";
                        goto Label_003B;
                    }
                }
                else
                {
                    orderby = "CreatedDate desc";
                    goto Label_003B;
                }
            }
            orderby = "CreatedDate desc";
        Label_003B:
            list = new List<Maticsoft.Model.SNS.GroupTopics>();
            string strWhere = " Status=1 and (Title like '%" + q + "%' or Description like '%" + q + "%') ";
            if (GroupId > 0)
            {
                strWhere = strWhere + " and GroupID=" + GroupId;
            }
            return this.DataTableToList(this.GetListByPage(strWhere, orderby, StartIndex, EndIndex).Tables[0]);
        }

        public bool Update(Maticsoft.Model.SNS.GroupTopics model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateAdminRecommand(int TopicId, bool IsAdmin)
        {
            return this.dal.UpdateAdminRecommand(TopicId, IsAdmin);
        }

        public bool UpdatePVCount(int TopicID)
        {
            return this.dal.UpdatePVCount(TopicID);
        }

        public bool UpdateRecommand(int TopicId, int Recommand)
        {
            return this.dal.UpdateRecommand(TopicId, Recommand);
        }

        public bool UpdateStatusList(string IdsStr, EnumHelper.TopicStatus status)
        {
            return this.dal.UpdateStatusList(IdsStr, status);
        }

        public static int BanTopicCount
        {
            get
            {
                return Maticsoft.BLL.SysManage.ConfigSystem.GetIntValueByCache("SNS_BAN_TOPIC_COUNT");
            }
        }

        public static int BanTopicTime
        {
            get
            {
                return Maticsoft.BLL.SysManage.ConfigSystem.GetIntValueByCache("SNS_BAN_TOPIC_TIME");
            }
        }
    }
}

