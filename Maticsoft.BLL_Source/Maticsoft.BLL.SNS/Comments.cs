namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using Maticsoft.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class Comments
    {
        private readonly IComments dal = DASNS.CreateComments();

        public DataSet AblumComment(int ablumId, string strWhere)
        {
            return this.dal.AblumComment(ablumId, strWhere);
        }

        public int Add(Maticsoft.Model.SNS.Comments model)
        {
            return this.dal.Add(model);
        }

        public int AddEx(Maticsoft.Model.SNS.Comments ComModel)
        {
            Maticsoft.Model.SNS.Products products;
            Maticsoft.BLL.SNS.Products products2;
            Maticsoft.BLL.SNS.ReferUsers users;
            if (FilterWords.ContainsModWords(ComModel.Description))
            {
                ComModel.Status = 0;
            }
            else
            {
                ComModel.Description = FilterWords.ReplaceWords(ComModel.Description);
            }
            int targetId = this.dal.AddEx(ComModel);
            switch (ComModel.Type)
            {
                case 0:
                    new Maticsoft.BLL.SNS.Posts().UpdateCommentCount(ComModel.TargetId);
                    goto Label_026A;

                case 1:
                {
                    Maticsoft.Model.SNS.Photos model = new Maticsoft.Model.SNS.Photos();
                    Maticsoft.BLL.SNS.Photos photos2 = new Maticsoft.BLL.SNS.Photos();
                    model = photos2.GetModel(ComModel.TargetId);
                    if (string.IsNullOrEmpty(model.TopCommentsId))
                    {
                        model.TopCommentsId = targetId.ToString();
                    }
                    else
                    {
                        string[] strArray2 = model.TopCommentsId.Split(new char[] { ',' });
                        if (strArray2.Length < 3)
                        {
                            model.TopCommentsId = model.TopCommentsId + "," + targetId;
                        }
                        if (strArray2.Length >= 3)
                        {
                            model.TopCommentsId = string.Concat(new object[] { targetId, ",", strArray2[0], ",", strArray2[1] });
                        }
                    }
                    photos2.Update(model);
                    goto Label_026A;
                }
                case 2:
                {
                    products = new Maticsoft.Model.SNS.Products();
                    products2 = new Maticsoft.BLL.SNS.Products();
                    products = products2.GetModel((long) ComModel.TargetId);
                    if (string.IsNullOrEmpty(products.TopCommentsId))
                    {
                        products.TopCommentsId = targetId.ToString();
                        break;
                    }
                    string[] strArray = products.TopCommentsId.Split(new char[] { ',' });
                    if (strArray.Length < 3)
                    {
                        products.TopCommentsId = products.TopCommentsId + "," + targetId;
                    }
                    if (strArray.Length >= 3)
                    {
                        products.TopCommentsId = string.Concat(new object[] { targetId, ",", strArray[0], ",", strArray[1] });
                    }
                    break;
                }
                case 3:
                    new Maticsoft.BLL.SNS.UserAlbums().UpdateCommentCount(ComModel.TargetId);
                    goto Label_026A;

                case 4:
                {
                    Maticsoft.BLL.SNS.UserBlog blog = new Maticsoft.BLL.SNS.UserBlog();
                    Maticsoft.BLL.SNS.Posts posts2 = new Maticsoft.BLL.SNS.Posts();
                    blog.UpdateCommentCount(ComModel.TargetId);
                    posts2.UpdateCommentCount(ComModel.TargetId);
                    goto Label_026A;
                }
                default:
                    new Maticsoft.BLL.SNS.Posts().UpdateCommentCount(ComModel.TargetId);
                    goto Label_026A;
            }
            products2.Update(products);
        Label_026A:
            users = new Maticsoft.BLL.SNS.ReferUsers();
            users.AddEx(ComModel.Description, EnumHelper.ReferType.Comment, targetId, "");
            return targetId;
        }

        public List<Maticsoft.Model.SNS.Comments> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Comments> list = new List<Maticsoft.Model.SNS.Comments>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.Comments item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int CommentID)
        {
            return this.dal.Delete(CommentID);
        }

        public bool DeleteComment(int ablumId, int commentId)
        {
            return this.dal.DeleteComment(ablumId, commentId);
        }

        public bool DeleteList(string CommentIDlist)
        {
            return this.dal.DeleteList(CommentIDlist);
        }

        public bool DeleteListEx(string CommentIDlist)
        {
            return this.dal.DeleteListEx(CommentIDlist);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.SNS.Comments> GetBlogComment(string strWhere, string orderBy, int top = -1)
        {
            DataSet set = this.GetList(top, strWhere, orderBy);
            List<Maticsoft.Model.SNS.Comments> list = this.DataTableToList(set.Tables[0]);
            if ((list != null) && (list.Count > 0))
            {
                Maticsoft.BLL.SNS.UserBlog blog = new Maticsoft.BLL.SNS.UserBlog();
                foreach (Maticsoft.Model.SNS.Comments comments in list)
                {
                    comments.UserBlog = blog.GetModelByCache(comments.TargetId);
                }
            }
            return list;
        }

        public List<Maticsoft.Model.SNS.Comments> GetCacheCommentByIds(string IdStr, int Type)
        {
            string cacheKey = "CacheCommentIds-" + IdStr + Type;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetCommentByIds(IdStr, Type);
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
            return (List<Maticsoft.Model.SNS.Comments>) cache;
        }

        public List<Maticsoft.Model.SNS.Comments> GetCommentByIds(string IdStr, int Type)
        {
            List<Maticsoft.Model.SNS.Comments> modelList = new List<Maticsoft.Model.SNS.Comments>();
            if (!string.IsNullOrEmpty(IdStr))
            {
                modelList = this.GetModelList("CommentID in (" + IdStr + ")");
            }
            return modelList;
        }

        public List<Maticsoft.Model.SNS.Comments> GetCommentByPage(int type, int TargetId, int StartIndex, int EndIndex)
        {
            return this.DataTableToList(this.GetListByPage(string.Concat(new object[] { " Status=1 and  Type=", type, " and TargetId=", TargetId }), "CommentID desc", StartIndex, EndIndex).Tables[0]);
        }

        public List<Maticsoft.Model.SNS.Comments> GetCommentByPost(Maticsoft.Model.SNS.Posts Post)
        {
            int num = Post.Type.Value;
            if (num == 3)
            {
                num = 0;
            }
            int num2 = (num == 0) ? Post.PostID : Post.TargetId;
            List<Maticsoft.Model.SNS.Comments> modelList = this.GetModelList(string.Concat(new object[] { " TargetId=", num2, " and Type=", num }));
            List<Maticsoft.Model.SNS.Comments> list2 = new List<Maticsoft.Model.SNS.Comments>();
            foreach (Maticsoft.Model.SNS.Comments comments in modelList)
            {
                comments.Description = ViewModelBase.RegexNickName(comments.Description);
                list2.Add(comments);
            }
            return list2;
        }

        public int GetCommentCount(int type, int TargetId)
        {
            return this.GetRecordCount(string.Concat(new object[] { " Status=1 and Type=", type, " and TargetId=", TargetId }));
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

        public Maticsoft.Model.SNS.Comments GetModel(int CommentID)
        {
            return this.dal.GetModel(CommentID);
        }

        public Maticsoft.Model.SNS.Comments GetModelByCache(int CommentID)
        {
            string cacheKey = "CommentsModel-" + CommentID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(CommentID);
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
            return (Maticsoft.Model.SNS.Comments) cache;
        }

        public List<Maticsoft.Model.SNS.Comments> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.SNS.Comments model)
        {
            return this.dal.Update(model);
        }
    }
}

