namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Settings;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using Maticsoft.TaoBao.Request;
    using Maticsoft.TaoBao.Response;
    using Maticsoft.ViewModel;
    using Maticsoft.ViewModel.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class Posts
    {
        private readonly IPosts dal = DASNS.CreatePosts();

        public int Add(Maticsoft.Model.SNS.Posts model)
        {
            return this.dal.Add(model);
        }

        public Maticsoft.Model.SNS.Posts AddBlogPost(Maticsoft.Model.SNS.Posts Post, Maticsoft.Model.SNS.UserBlog blogModel, bool CreatePost = true)
        {
            Post.Type = 4;
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("chk_check_word");
            if (!string.IsNullOrEmpty(valueByCache))
            {
                Post.Status = (valueByCache == "0") ? 1 : 0;
            }
            else
            {
                Post.Status = 1;
            }
            Post.Description = "<a target='_blank' style='color: #FF7CAE' href='{BlogUrl} '>" + Post.Description + "</a>";
            if (FilterWords.ContainsModWords(Post.Description))
            {
                Post.Status = 0;
            }
            else
            {
                Post.Description = FilterWords.ReplaceWords(Post.Description);
            }
            Maticsoft.Model.SNS.Posts posts = this.dal.AddBlogPost(Post, blogModel, CreatePost);
            Maticsoft.BLL.SNS.ReferUsers users = new Maticsoft.BLL.SNS.ReferUsers();
            if (posts != null)
            {
                users.AddEx(Post.Description, EnumHelper.ReferType.Post, posts.PostID, "");
            }
            return posts;
        }

        public int AddForwardPost(Maticsoft.Model.SNS.Posts model)
        {
            int targetId = 0;
            Maticsoft.BLL.SNS.ReferUsers users = new Maticsoft.BLL.SNS.ReferUsers();
            if (FilterWords.ContainsModWords(model.Description))
            {
                model.Status = 0;
            }
            else
            {
                model.Description = FilterWords.ReplaceWords(model.Description);
            }
            targetId = this.dal.AddForwardPost(model);
            users.AddEx(model.Description, EnumHelper.ReferType.Post, targetId, model.CreatedNickName);
            return targetId;
        }

        public Maticsoft.Model.SNS.Posts AddNormalPost(Maticsoft.Model.SNS.Posts Post)
        {
            Post.Type = 0;
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("chk_check_word");
            if (!string.IsNullOrEmpty(valueByCache))
            {
                Post.Status = (valueByCache == "0") ? 1 : 0;
            }
            else
            {
                Post.Status = 1;
            }
            if (FilterWords.ContainsModWords(Post.Description))
            {
                Post.Status = 0;
            }
            else
            {
                Post.Description = FilterWords.ReplaceWords(Post.Description);
            }
            int num = this.dal.Add(Post);
            Post.PostID = num;
            Maticsoft.Model.SNS.Posts posts = Post;
            Maticsoft.BLL.SNS.ReferUsers users = new Maticsoft.BLL.SNS.ReferUsers();
            if (posts != null)
            {
                users.AddEx(Post.Description, EnumHelper.ReferType.Post, posts.PostID, "");
            }
            return posts;
        }

        public Maticsoft.Model.SNS.Posts AddPost(Maticsoft.Model.SNS.Posts Post, int AblumId, long Pid, int PhotoCateId, string PhotoAddress = "", string MapLng = "", string MapLat = "", bool CreatePost = true)
        {
            Maticsoft.Model.SNS.Products pModel = new Maticsoft.Model.SNS.Products();
            Maticsoft.BLL.SNS.Tags tags = new Maticsoft.BLL.SNS.Tags();
            if (Post.Type == 2)
            {
                ITopClient topClient = TaoBaoConfig.GetTopClient();
                TaobaokeItemsDetailGetRequest request = new TaobaokeItemsDetailGetRequest {
                    Fields = "num_iid,title,price,num_iid,title,cid,nick,desc,price,post_fee,express_fee,ems_fee,item_img.url,click_url,shop_click_url,num,props_name,detail_url,pic_url",
                    NumIids = Pid.ToString()
                };
                TaobaokeItemsDetailGetResponse response = topClient.Execute<TaobaokeItemsDetailGetResponse>(request);
                Maticsoft.BLL.SNS.CategorySource source = new Maticsoft.BLL.SNS.CategorySource();
                Item item = new Item();
                item = (response.TaobaokeItemDetails.Count > 0) ? response.TaobaokeItemDetails[0].Item : null;
                pModel.ProductUrl = (response.TaobaokeItemDetails.Count > 0) ? response.TaobaokeItemDetails[0].ClickUrl : "";
                if (response.TaobaokeItemDetails.Count < 1)
                {
                    ItemGetRequest request2 = new ItemGetRequest {
                        Fields = "num_iid,title,price,num_iid,title,cid,nick,desc,price,item_img.url,click_url,shop_click_url,num,props_name,detail_url,pic_url",
                        NumIid = new long?(Pid)
                    };
                    item = topClient.Execute<ItemGetResponse>(request2).Item;
                    pModel.ProductUrl = item.DetailUrl;
                }
                Maticsoft.Model.SNS.CategorySource model = source.GetModel(3, Convert.ToInt32(item.Cid));
                pModel.CategoryID = (model != null) ? model.SnsCategoryId : 0;
                pModel.NormalImageUrl = item.PicUrl;
                pModel.ThumbImageUrl = item.PicUrl + "_300x300.jpg";
                pModel.Price = new decimal?(Globals.SafeDecimal(item.Price, (decimal) 0M));
                pModel.ProductID = Pid;
                pModel.ProductName = item.Title;
                Post.ProductName = pModel.ProductName;
                pModel.ProductSourceID = 3;
                pModel.CreatedDate = DateTime.Now;
                pModel.CreatedNickName = Post.CreatedNickName;
                pModel.CreateUserID = Post.CreatedUserID;
                string str = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_product");
                if (!string.IsNullOrEmpty(str))
                {
                    pModel.Status = (str == "0") ? 1 : 0;
                    Post.Status = (str == "0") ? 1 : 0;
                }
                else
                {
                    pModel.Status = 1;
                    Post.Status = 1;
                }
                if (FilterWords.ContainsModWords(Post.Description))
                {
                    pModel.Status = 0;
                    Post.Status = 0;
                }
                else
                {
                    Post.Description = FilterWords.ReplaceWords(Post.Description);
                }
                pModel.ShareDescription = Post.Description;
                ItemGetRequest request3 = new ItemGetRequest {
                    Fields = "props_name",
                    NumIid = new long?(Pid)
                };
                string propsName = topClient.Execute<ItemGetResponse>(request3).Item.PropsName;
                pModel.Tags = tags.GetTagStr(propsName);
                Post.ProductName = pModel.ProductName;
                if (!string.IsNullOrEmpty(Post.Description))
                {
                    Post.Description = Post.Description + "</br><a target='_blank' style='color: #FF7CAE' href='{ProductUrl} '>" + Post.ProductName + "</a>";
                }
                else
                {
                    Post.Description = "<a target='_blank' style='color: #FF7CAE' href='{ProductUrl} '>" + Post.ProductName + "</a>";
                }
            }
            else if (Post.Type == 0)
            {
                string str3;
                if (!string.IsNullOrEmpty(Post.ImageUrl))
                {
                    str3 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_photo");
                    if (!string.IsNullOrEmpty(str3))
                    {
                        Post.Status = (str3 == "0") ? 1 : 0;
                    }
                    else
                    {
                        Post.Status = 1;
                    }
                }
                else if (!string.IsNullOrEmpty(Post.VideoUrl) && (Post.VideoUrl.Length > 5))
                {
                    Post.Type = 3;
                    str3 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_video");
                    if (!string.IsNullOrEmpty(str3))
                    {
                        Post.Status = (str3 == "0") ? 1 : 0;
                    }
                    else
                    {
                        Post.Status = 1;
                    }
                }
                else if (!string.IsNullOrEmpty(Post.AudioUrl))
                {
                    str3 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_audio");
                    if (!string.IsNullOrEmpty(str3))
                    {
                        Post.Status = (str3 == "0") ? 1 : 0;
                    }
                    else
                    {
                        Post.Status = 1;
                    }
                }
                else
                {
                    str3 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("chk_check_word");
                    if (!string.IsNullOrEmpty(str3))
                    {
                        Post.Status = (str3 == "0") ? 1 : 0;
                    }
                    else
                    {
                        Post.Status = 1;
                    }
                }
            }
            else if ((Post.Type == 1) && !string.IsNullOrEmpty(Post.ImageUrl))
            {
                string str4 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_picture");
                if (!string.IsNullOrEmpty(str4))
                {
                    Post.Status = (str4 == "0") ? 1 : 0;
                }
                else
                {
                    Post.Status = 1;
                }
            }
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ProductAndPhotoRecommandState");
            int recommandStateInt = (valueByCache != null) ? Globals.SafeInt(valueByCache, 0) : 0;
            if (FilterWords.ContainsModWords(Post.Description))
            {
                Post.Status = 0;
            }
            else
            {
                Post.Description = FilterWords.ReplaceWords(Post.Description);
            }
            Maticsoft.Model.SNS.Posts posts = this.dal.AddPost(Post, AblumId, Pid, PhotoCateId, pModel, recommandStateInt, PhotoAddress, MapLng, MapLat, CreatePost);
            Maticsoft.BLL.SNS.ReferUsers users = new Maticsoft.BLL.SNS.ReferUsers();
            if (posts != null)
            {
                users.AddEx(Post.Description, EnumHelper.ReferType.Post, posts.PostID, "");
            }
            return posts;
        }

        public List<Maticsoft.Model.SNS.Posts> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Posts> list = new List<Maticsoft.Model.SNS.Posts>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.Posts item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int PostID)
        {
            return this.dal.Delete(PostID);
        }

        public bool DeleteEx(int PostID, bool IsSendMess = false, int SendUserID = 1)
        {
            Maticsoft.Model.SNS.Posts modelByCache = this.GetModelByCache(PostID);
            bool flag = this.dal.DeleteEx(PostID);
            if (flag && IsSendMess)
            {
                new SiteMessage().AddMessageByUser(SendUserID, modelByCache.CreatedUserID, "动态删除", "您分享的动态涉嫌非法内容，管理员已删除！ 如有疑问，请联系网站管理员");
            }
            return flag;
        }

        public bool DeleteList(string PostIDlist)
        {
            return this.dal.DeleteList(PostIDlist);
        }

        public bool DeleteListByNormalPost(string PostIDs, bool IsSendMess = false, int SendUserID = 1)
        {
            List<int> postUserIds = this.GetPostUserIds(PostIDs);
            bool flag = this.dal.DeleteListByNormalPost(PostIDs);
            if (flag && IsSendMess)
            {
                SiteMessage message = new SiteMessage();
                foreach (int num in postUserIds)
                {
                    message.AddMessageByUser(SendUserID, num, "动态删除", "您分享的动态涉嫌非法内容，管理员已删除！ 如有疑问，请联系网站管理员");
                }
            }
            return flag;
        }

        public bool DeleteListEx(string PostIDs)
        {
            if (!string.IsNullOrEmpty(PostIDs))
            {
                foreach (string str in PostIDs.Split(new char[] { ',' }))
                {
                    if (!this.dal.DeleteEx(Globals.SafeInt(str, 0)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.SNS.Posts> GetAudioListByPage(int uid, int startIndex, int endIndex)
        {
            string strWhere = string.Format(" Status=1 and AudioUrl IS NOT NULL AND AudioUrl <>'' ", new object[0]);
            if (uid > 0)
            {
                strWhere = strWhere + " and CreatedUserID=" + uid;
            }
            DataSet set = this.dal.GetListByPage(strWhere, "CreatedDate desc", startIndex, endIndex);
            return this.DataTableToList(set.Tables[0]);
        }

        public int GetCountByPostType(int UserId, EnumHelper.PostType PostType, bool includeProduct)
        {
            string strWhere = "";
            switch (PostType)
            {
                case EnumHelper.PostType.All:
                    strWhere = " Status=" + 1;
                    break;

                case EnumHelper.PostType.Fellow:
                    strWhere = string.Concat(new object[] { "Status=", 1, " and CreatedUserId in ( SELECT  PassiveUserID  FROM  SNS_UserShip WHERE  ActiveUserID=", UserId, " UNION SELECT ", UserId, ")" });
                    break;

                case EnumHelper.PostType.User:
                    strWhere = string.Concat(new object[] { " Status=", 1, " and CreatedUserId=", UserId });
                    break;

                case EnumHelper.PostType.ReferMe:
                    strWhere = string.Concat(new object[] { " Status=", 1, " and PostID in (SELECT  TagetID FROM SNS_ReferUsers  WHERE  ReferUserID=", UserId, " and Type=0)" });
                    break;

                case EnumHelper.PostType.EachOther:
                    strWhere = string.Concat(new object[] { " Status=", 1, " and CreatedUserId in (SELECT PassiveUserID  FROM SNS_UserShip WHERE  ActiveUserID=", UserId, " and Type=1)" });
                    break;

                case EnumHelper.PostType.Photo:
                    strWhere = string.Concat(new object[] { " Status=", 1, " and CreatedUserId=", UserId, " and Type=", 1 });
                    break;

                case EnumHelper.PostType.Product:
                    strWhere = string.Concat(new object[] { " Status=", 1, " and CreatedUserId=", UserId, " and Type=", 2 });
                    break;

                case EnumHelper.PostType.Video:
                    strWhere = string.Concat(new object[] { " Status=", 1, " and CreatedUserId=", UserId, " and VideoUrl is not null" });
                    break;

                case EnumHelper.PostType.Blog:
                    strWhere = string.Concat(new object[] { " Status=", 1, " and CreatedUserId=", UserId, " and Type=", 4 });
                    break;

                default:
                    strWhere = "";
                    break;
            }
            if (!includeProduct)
            {
                strWhere = strWhere + " and Type<>" + 2;
            }
            return this.GetRecordCount(strWhere);
        }

        public List<Maticsoft.ViewModel.SNS.Posts> GetForPostByPostId(int PostId, bool includeProduct)
        {
            return this.GetPostByType(0, 0, 1, EnumHelper.PostType.OnePost, PostId, includeProduct);
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

        public Maticsoft.Model.SNS.Posts GetModel(int PostID)
        {
            return this.dal.GetModel(PostID);
        }

        public Maticsoft.Model.SNS.Posts GetModelByCache(int PostID)
        {
            string cacheKey = "PostsModel-" + PostID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(PostID);
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
            return (Maticsoft.Model.SNS.Posts) cache;
        }

        public List<Maticsoft.Model.SNS.Posts> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.ViewModel.SNS.Posts> GetPostByType(int UserId, int StartIndex, int EndIndex, EnumHelper.PostType Type, bool includeProduct = true)
        {
            return this.GetPostByType(UserId, StartIndex, EndIndex, Type, 0, includeProduct);
        }

        public List<Maticsoft.ViewModel.SNS.Posts> GetPostByType(int UserId, int StartIndex, int EndIndex, EnumHelper.PostType Type, int PostId, bool includeProduct = true)
        {
            List<Maticsoft.Model.SNS.Posts> list = new List<Maticsoft.Model.SNS.Posts>();
            List<Maticsoft.Model.SNS.Posts> modelList = new List<Maticsoft.Model.SNS.Posts>();
            Dictionary<int, Maticsoft.Model.SNS.Posts> dictionary = new Dictionary<int, Maticsoft.Model.SNS.Posts>();
            List<Maticsoft.ViewModel.SNS.Posts> list3 = new List<Maticsoft.ViewModel.SNS.Posts>();
            string strWhere = "";
            string str2 = "";
            switch (Type)
            {
                case EnumHelper.PostType.All:
                    strWhere = " Status=" + 1;
                    break;

                case EnumHelper.PostType.Fellow:
                    strWhere = string.Concat(new object[] { " Status=", 1, " and CreatedUserId in (SELECT  PassiveUserID  FROM  SNS_UserShip WHERE  ActiveUserID=", UserId, " UNION SELECT ", UserId, ")" });
                    break;

                case EnumHelper.PostType.User:
                    strWhere = string.Concat(new object[] { " Status=", 1, " and CreatedUserId=", UserId });
                    break;

                case EnumHelper.PostType.OnePost:
                    strWhere = " PostID=" + PostId;
                    break;

                case EnumHelper.PostType.ReferMe:
                    strWhere = string.Concat(new object[] { " Status=", 1, " and PostID in (SELECT  TagetID FROM SNS_ReferUsers  WHERE  ReferUserID=", UserId, " and Type=0)" });
                    break;

                case EnumHelper.PostType.EachOther:
                    strWhere = string.Concat(new object[] { " Status=", 1, " and CreatedUserId in ( SELECT PassiveUserID  FROM  dbo.SNS_UserShip WHERE  ActiveUserID=", UserId, " and Type=1)" });
                    break;

                case EnumHelper.PostType.Photo:
                    strWhere = string.Concat(new object[] { " Status=", 1, " and CreatedUserId=", UserId, " and Type=", 1 });
                    break;

                case EnumHelper.PostType.Product:
                    strWhere = string.Concat(new object[] { " Status=", 1, " and CreatedUserId=", UserId, " and Type=", 2 });
                    break;

                case EnumHelper.PostType.Video:
                    strWhere = string.Concat(new object[] { " Status=", 1, " and CreatedUserId=", UserId, " and VideoUrl is not null" });
                    break;

                case EnumHelper.PostType.Blog:
                    strWhere = string.Concat(new object[] { " Status=", 1, " and CreatedUserId=", UserId, " and Type=", 4 });
                    break;

                default:
                    strWhere = "Status=" + 1;
                    break;
            }
            if (!includeProduct)
            {
                strWhere = strWhere + " and Type<>" + 2;
            }
            list = this.DataTableToList(this.GetListByPage(strWhere, "PostId Desc", StartIndex, EndIndex).Tables[0]);
            int[] values = (from item in list
                where item.OriginalID != 0
                select item.OriginalID).Distinct<int>().ToArray<int>();
            string str3 = string.Join<int>(",", values);
            if (!string.IsNullOrEmpty(str3))
            {
                str2 = "PostId in(" + str3 + ")";
                modelList = this.GetModelList(str2);
            }
            foreach (Maticsoft.Model.SNS.Posts posts in modelList)
            {
                dictionary.Add(posts.PostID, posts);
            }
            foreach (Maticsoft.Model.SNS.Posts posts2 in list)
            {
                Maticsoft.ViewModel.SNS.Posts posts3 = new Maticsoft.ViewModel.SNS.Posts();
                posts2.Description = ViewModelBase.RegexNickName(posts2.Description);
                posts3.Post = posts2;
                if (dictionary.ContainsKey(posts2.OriginalID))
                {
                    posts3.OrigPost = dictionary[posts2.OriginalID];
                    posts3.OrigPost.Description = ViewModelBase.RegexNickName(posts3.OrigPost.Description);
                }
                list3.Add(posts3);
            }
            if (Type == EnumHelper.PostType.ReferMe)
            {
                new Maticsoft.BLL.SNS.ReferUsers().UpdateReferStateToRead(UserId, 0);
            }
            return list3;
        }

        public List<int> GetPostUserIds(string ids)
        {
            DataSet postUserIds = this.dal.GetPostUserIds(ids);
            List<int> list = new List<int>();
            if ((postUserIds != null) && (postUserIds.Tables.Count > 0))
            {
                for (int i = 0; i < postUserIds.Tables[0].Rows.Count; i++)
                {
                    if ((postUserIds.Tables[0].Rows[i]["CreatedUserID"] != null) && (postUserIds.Tables[0].Rows[i]["CreatedUserID"].ToString() != ""))
                    {
                        list.Add(int.Parse(postUserIds.Tables[0].Rows[i]["CreatedUserID"].ToString()));
                    }
                }
            }
            return list;
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public List<Maticsoft.Model.SNS.Posts> GetScrollPost(int top, EnumHelper.PostContentType PostType)
        {
            DataSet set = new DataSet();
            if (PostType == EnumHelper.PostContentType.None)
            {
                set = this.GetList(top, " ", "CreatedDate Desc");
            }
            else
            {
                set = this.GetList(top, "Type=" + ((int) PostType), " CreatedDate Desc");
            }
            return this.DataTableToList(set.Tables[0]);
        }

        public DataSet GetSearchList(string keywords, int type)
        {
            string strWhere = string.Format(" STATUS<>{0}", 3);
            if (type > -1)
            {
                strWhere = strWhere + string.Format(" AND Type={0} ", type);
            }
            if (keywords.Length > 0)
            {
                strWhere = strWhere + string.Format(" AND Description like '%{0}%'", keywords);
            }
            strWhere = strWhere + " ORDER BY PostID DESC";
            return this.dal.GetList(strWhere);
        }

        public List<Maticsoft.Model.SNS.Posts> GetTopPost(int top, int Type)
        {
            return this.DataTableToList(this.dal.GetListByPage("Type=" + Type, "CreatedDate Desc", 0, top).Tables[0]);
        }

        public List<Maticsoft.Model.SNS.Posts> GetVideoList(int uid, int top)
        {
            string strWhere = string.Format(" type=3 and Status=1", new object[0]);
            if (uid > 0)
            {
                strWhere = strWhere + " and CreatedUserID=" + uid;
            }
            DataSet set = this.dal.GetList(top, strWhere, "CreatedDate desc");
            return this.DataTableToList(set.Tables[0]);
        }

        public List<Maticsoft.Model.SNS.Posts> GetVideoListByPage(int uid, int startIndex, int endIndex)
        {
            string strWhere = string.Format(" type=3 and Status=1", new object[0]);
            if (uid > 0)
            {
                strWhere = strWhere + " and CreatedUserID=" + uid;
            }
            DataSet set = this.dal.GetListByPage(strWhere, "CreatedDate desc", startIndex, endIndex);
            return this.DataTableToList(set.Tables[0]);
        }

        public List<Maticsoft.Model.SNS.Posts> GetVideoListByPageCache(int uid, int startIndex, int endIndex)
        {
            string cacheKey = string.Concat(new object[] { "GetVideoListByPageCache", uid, startIndex, endIndex });
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetVideoListByPage(uid, startIndex, endIndex);
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
            return (List<Maticsoft.Model.SNS.Posts>) cache;
        }

        public DataSet GetVideoSearchList(string KeyWord)
        {
            string strWhere = "Type=0 and VideoUrl is not null and VideoUrl <>''";
            if (!string.IsNullOrWhiteSpace(KeyWord))
            {
                strWhere = strWhere + "and Description like '%" + KeyWord + "%'";
            }
            return this.GetList(strWhere);
        }

        public int PostForWard(string PostContent, int Origid, int ForWardid, int OrigUserId, string OrigNickName, int CurrentUserID, string CurrentNickName, string UserIp)
        {
            new Users();
            Maticsoft.BLL.SNS.ReferUsers users = new Maticsoft.BLL.SNS.ReferUsers();
            Maticsoft.Model.SNS.ReferUsers model = new Maticsoft.Model.SNS.ReferUsers();
            Maticsoft.Model.SNS.Posts posts = new Maticsoft.Model.SNS.Posts {
                CreatedDate = DateTime.Now,
                Description = PostContent,
                CreatedNickName = CurrentNickName,
                CreatedUserID = CurrentUserID,
                ForwardedID = new int?(ForWardid),
                HasReferUsers = PostContent.Contains<char>('@') ? true : false,
                OriginalID = (Origid == 0) ? ForWardid : Origid,
                UserIP = UserIp,
                Status = 1
            };
            int targetId = this.AddForwardPost(posts);
            users.AddEx(PostContent, EnumHelper.ReferType.Post, targetId, "");
            model.CreatedDate = DateTime.Now;
            model.IsRead = false;
            model.ReferUserID = OrigUserId;
            model.ReferNickName = OrigNickName;
            model.Type = 0;
            model.TagetID = targetId;
            users.Add(model);
            return targetId;
        }

        public bool Update(Maticsoft.Model.SNS.Posts model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateCommentCount(int postId)
        {
            return this.dal.UpdateCommentCount(postId);
        }

        public bool UpdateFavCount(int postId)
        {
            return this.dal.UpdateFavCount(postId);
        }

        public int UpdateForwardCount(string StrWhere)
        {
            return this.dal.UpdateForwardCount(StrWhere);
        }

        public bool UpdateStatusList(string PostIds, int Status)
        {
            return this.dal.UpdateStatusList(PostIds, Status);
        }

        public bool UpdateToDel(int PostID)
        {
            return this.dal.UpdateToDel(PostID);
        }
    }
}

