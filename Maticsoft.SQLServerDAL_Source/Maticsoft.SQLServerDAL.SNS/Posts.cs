namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.Common;
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Posts : IPosts
    {
        public int Add(Maticsoft.Model.SNS.Posts model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_Posts(");
            builder.Append("CreatedUserID,CreatedNickName,OriginalID,ForwardedID,Description,HasReferUsers,CommentCount,ForwardCount,Type,PostExUrl,VideoUrl,AudioUrl,ImageUrl,TargetId,TopicTitle,Price,ProductLinkUrl,ProductName,FavCount,UserIP,Status,CreatedDate,IsRecommend,Sequence,Tags)");
            builder.Append(" values (");
            builder.Append("@CreatedUserID,@CreatedNickName,@OriginalID,@ForwardedID,@Description,@HasReferUsers,@CommentCount,@ForwardCount,@Type,@PostExUrl,@VideoUrl,@AudioUrl,@ImageUrl,@TargetId,@TopicTitle,@Price,@ProductLinkUrl,@ProductName,@FavCount,@UserIP,@Status,@CreatedDate,@IsRecommend,@Sequence,@Tags)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@OriginalID", SqlDbType.Int, 4), new SqlParameter("@ForwardedID", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@HasReferUsers", SqlDbType.Bit, 1), new SqlParameter("@CommentCount", SqlDbType.Int, 4), new SqlParameter("@ForwardCount", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@PostExUrl", SqlDbType.NVarChar, 200), new SqlParameter("@VideoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@AudioUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@TopicTitle", SqlDbType.NVarChar, 200), new SqlParameter("@Price", SqlDbType.Decimal, 9), 
                new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar, 500), new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@FavCount", SqlDbType.Int, 4), new SqlParameter("@UserIP", SqlDbType.NVarChar, 15), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@IsRecommend", SqlDbType.Bit, 1), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 100)
             };
            cmdParms[0].Value = model.CreatedUserID;
            cmdParms[1].Value = model.CreatedNickName;
            cmdParms[2].Value = model.OriginalID;
            cmdParms[3].Value = model.ForwardedID;
            cmdParms[4].Value = model.Description;
            cmdParms[5].Value = model.HasReferUsers;
            cmdParms[6].Value = model.CommentCount;
            cmdParms[7].Value = model.ForwardCount;
            cmdParms[8].Value = model.Type;
            cmdParms[9].Value = model.PostExUrl;
            cmdParms[10].Value = model.VideoUrl;
            cmdParms[11].Value = model.AudioUrl;
            cmdParms[12].Value = model.ImageUrl;
            cmdParms[13].Value = model.TargetId;
            cmdParms[14].Value = model.TopicTitle;
            cmdParms[15].Value = model.Price;
            cmdParms[0x10].Value = model.ProductLinkUrl;
            cmdParms[0x11].Value = model.ProductName;
            cmdParms[0x12].Value = model.FavCount;
            cmdParms[0x13].Value = model.UserIP;
            cmdParms[20].Value = model.Status;
            cmdParms[0x15].Value = model.CreatedDate;
            cmdParms[0x16].Value = model.IsRecommend;
            cmdParms[0x17].Value = model.Sequence;
            cmdParms[0x18].Value = model.Tags;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.Posts AddBlogPost(Maticsoft.Model.SNS.Posts model, Maticsoft.Model.SNS.UserBlog blogModel, bool CreatePost)
        {
            Maticsoft.Model.SNS.Posts posts;
            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        model.TargetId = Globals.SafeInt(DbHelperSQL.GetSingle4Trans(this.GenerateBlog(blogModel), transaction).ToString(), -1);
                        if (CreatePost)
                        {
                            model.PostID = Globals.SafeInt(DbHelperSQL.GetSingle4Trans(this.GeneratePostInfo(model), transaction).ToString(), 0);
                        }
                        transaction.Commit();
                        posts = model;
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        posts = null;
                    }
                }
            }
            return posts;
        }

        public int AddForwardPost(Maticsoft.Model.SNS.Posts model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_Posts(");
            builder.Append("Type,TopicTitle,CreatedUserID,UserIP,Status,CreatedDate,CreatedNickName,OriginalID,ForwardedID,Description,HasReferUsers)");
            builder.Append(" values (");
            builder.Append("@Type,@TopicTitle,@CreatedUserID,@UserIP,@Status,@CreatedDate,@CreatedNickName,@OriginalID,@ForwardedID,@Description,@HasReferUsers)");
            builder.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@TopicTitle", SqlDbType.NVarChar, 200), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@UserIP", SqlDbType.NVarChar, 15), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@OriginalID", SqlDbType.Int, 4), new SqlParameter("@ForwardedID", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@HasReferUsers", SqlDbType.Bit, 1), new SqlParameter("@ReturnValue", SqlDbType.Int) };
            para[0].Value = model.Type;
            para[1].Value = model.TopicTitle;
            para[2].Value = model.CreatedUserID;
            para[3].Value = model.UserIP;
            para[4].Value = model.Status;
            para[5].Value = model.CreatedDate;
            para[6].Value = model.CreatedNickName;
            para[7].Value = model.OriginalID;
            para[8].Value = model.ForwardedID;
            para[9].Value = model.Description;
            para[10].Value = model.HasReferUsers;
            para[11].Direction = ParameterDirection.Output;
            List<CommandInfo> cmdList = new List<CommandInfo>();
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update SNS_Posts");
            builder2.Append(" set ForwardCount=ForwardCount+1");
            int originalID = model.OriginalID;
            builder2.Append(" where PostID in( " + ((originalID == model.ForwardedID) ? model.OriginalID.ToString() : (model.OriginalID + "," + model.ForwardedID)) + " )");
            item = new CommandInfo(builder2.ToString(), null);
            cmdList.Add(item);
            DbHelperSQL.ExecuteSqlTran(cmdList);
            return (int) para[11].Value;
        }

        public Maticsoft.Model.SNS.Posts AddPost(Maticsoft.Model.SNS.Posts model, int AlbumId, long Pid, int PhotoCateId, Maticsoft.Model.SNS.Products PModel, int RecommandStateInt, string photoAdress, string mapLng, string mapLat, bool CreatePost)
        {
            Maticsoft.Model.SNS.Posts posts;
            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        if ((model.Type == 1) || (model.Type == 2))
                        {
                            model.TargetId = Globals.SafeInt(DbHelperSQL.GetSingle4Trans(this.GenerateImageInfo(model, PModel, AlbumId, Pid, PhotoCateId, RecommandStateInt, photoAdress, mapLng, mapLat), transaction).ToString(), -1);
                            DbHelperSQL.GetSingle4Trans(this.GenerateAblumInfo(model, AlbumId, model.TargetId), transaction);
                            DbHelperSQL.GetSingle4Trans(this.GenerateUpdateUserEx(model.CreatedUserID), transaction);
                            DbHelperSQL.GetSingle4Trans(this.GenerateUpdateAlbum(AlbumId), transaction);
                        }
                        if (CreatePost)
                        {
                            model.PostID = Globals.SafeInt(DbHelperSQL.GetSingle4Trans(this.GeneratePostInfo(model), transaction).ToString(), 0);
                        }
                        transaction.Commit();
                        posts = model;
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        posts = null;
                    }
                }
            }
            return posts;
        }

        public Maticsoft.Model.SNS.Posts DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Posts posts = new Maticsoft.Model.SNS.Posts();
            if (row != null)
            {
                if ((row["PostID"] != null) && (row["PostID"].ToString() != ""))
                {
                    posts.PostID = int.Parse(row["PostID"].ToString());
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    posts.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    posts.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if ((row["OriginalID"] != null) && (row["OriginalID"].ToString() != ""))
                {
                    posts.OriginalID = int.Parse(row["OriginalID"].ToString());
                }
                if ((row["ForwardedID"] != null) && (row["ForwardedID"].ToString() != ""))
                {
                    posts.ForwardedID = new int?(int.Parse(row["ForwardedID"].ToString()));
                }
                if (row["Description"] != null)
                {
                    posts.Description = row["Description"].ToString();
                }
                if ((row["HasReferUsers"] != null) && (row["HasReferUsers"].ToString() != ""))
                {
                    if ((row["HasReferUsers"].ToString() == "1") || (row["HasReferUsers"].ToString().ToLower() == "true"))
                    {
                        posts.HasReferUsers = true;
                    }
                    else
                    {
                        posts.HasReferUsers = false;
                    }
                }
                if ((row["CommentCount"] != null) && (row["CommentCount"].ToString() != ""))
                {
                    posts.CommentCount = int.Parse(row["CommentCount"].ToString());
                }
                if ((row["ForwardCount"] != null) && (row["ForwardCount"].ToString() != ""))
                {
                    posts.ForwardCount = int.Parse(row["ForwardCount"].ToString());
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    posts.Type = new int?(int.Parse(row["Type"].ToString()));
                }
                if (row["PostExUrl"] != null)
                {
                    posts.PostExUrl = row["PostExUrl"].ToString();
                }
                if (row["VideoUrl"] != null)
                {
                    posts.VideoUrl = row["VideoUrl"].ToString();
                }
                if (row["AudioUrl"] != null)
                {
                    posts.AudioUrl = row["AudioUrl"].ToString();
                }
                if (row["ImageUrl"] != null)
                {
                    posts.ImageUrl = row["ImageUrl"].ToString();
                }
                if ((row["TargetId"] != null) && (row["TargetId"].ToString() != ""))
                {
                    posts.TargetId = int.Parse(row["TargetId"].ToString());
                }
                if (row["TopicTitle"] != null)
                {
                    posts.TopicTitle = row["TopicTitle"].ToString();
                }
                if ((row["Price"] != null) && (row["Price"].ToString() != ""))
                {
                    posts.Price = new decimal?(decimal.Parse(row["Price"].ToString()));
                }
                if (row["ProductLinkUrl"] != null)
                {
                    posts.ProductLinkUrl = row["ProductLinkUrl"].ToString();
                }
                if (row["ProductName"] != null)
                {
                    posts.ProductName = row["ProductName"].ToString();
                }
                if ((row["FavCount"] != null) && (row["FavCount"].ToString() != ""))
                {
                    posts.FavCount = new int?(int.Parse(row["FavCount"].ToString()));
                }
                if (row["UserIP"] != null)
                {
                    posts.UserIP = row["UserIP"].ToString();
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    posts.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    posts.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["IsRecommend"] != null) && (row["IsRecommend"].ToString() != ""))
                {
                    if ((row["IsRecommend"].ToString() == "1") || (row["IsRecommend"].ToString().ToLower() == "true"))
                    {
                        posts.IsRecommend = true;
                    }
                    else
                    {
                        posts.IsRecommend = false;
                    }
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    posts.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["Tags"] != null)
                {
                    posts.Tags = row["Tags"].ToString();
                }
            }
            return posts;
        }

        public bool Delete(int PostID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Posts ");
            builder.Append(" where PostID=@PostID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PostID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PostID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteEx(int PostID)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            new Maticsoft.SQLServerDAL.SNS.Comments();
            Maticsoft.Model.SNS.Posts model = new Maticsoft.Model.SNS.Posts();
            model = this.GetModel(PostID);
            int? type = model.Type;
            if (type.HasValue)
            {
                if ((type.Value == 0) || (type.Value == 3))
                {
                    this.DeleteNormal(sqllist, PostID);
                    StringBuilder builder = new StringBuilder();
                    builder.Append("delete from SNS_Comments ");
                    builder.Append(" where TargetId=@TargetId  AND Type=@Type");
                    SqlParameter[] para = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
                    para[0].Value = PostID;
                    para[1].Value = type;
                    CommandInfo item = new CommandInfo(builder.ToString(), para);
                    sqllist.Add(item);
                    if (DbHelperSQL.ExecuteSqlTran(sqllist) <= 0)
                    {
                        return false;
                    }
                    return true;
                }
                if (type.Value == 1)
                {
                    int num;
                    new Maticsoft.SQLServerDAL.SNS.Photos().DeleteListEx(model.TargetId.ToString(), out num);
                    if (num == 1)
                    {
                        return true;
                    }
                }
                else if (type.Value == 2)
                {
                    int num2;
                    new Maticsoft.SQLServerDAL.SNS.Products().DeleteListEx(model.TargetId.ToString(), out num2);
                    if (num2 == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool DeleteList(string PostIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Posts ");
            builder.Append(" where PostID in (" + PostIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool DeleteListByNormalPost(string PostIDs)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("Delete SNS_Posts  Where PostID in (" + PostIDs + ")");
            CommandInfo item = new CommandInfo(builder.ToString(), null);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("delete from SNS_Comments ");
            builder2.Append(" where TargetId in(select PostID from SNS_Posts where PostId in (" + PostIDs + "))  AND Type=0");
            CommandInfo info2 = new CommandInfo(builder2.ToString(), null);
            cmdList.Add(info2);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public void DeleteNormal(List<CommandInfo> sqllist, int PostID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Delete SNS_Posts  Where PostID=@PostID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@PostID", SqlDbType.Int, 4) };
            para[0].Value = PostID;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            sqllist.Add(item);
        }

        private CommandInfo GenerateAblumInfo(Maticsoft.Model.SNS.Posts model, int AlbumId, int TargetId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_UserAlbumDetail(");
            builder.Append("AlbumID,TargetID,Type,Description,AlbumUserId)");
            builder.Append(" values (");
            builder.Append("@AlbumID,@TargetID,@Type,@Description,@AlbumUserId)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4), new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@AlbumUserId", SqlDbType.Int, 4) };
            para[0].Value = AlbumId;
            para[1].Value = TargetId;
            para[2].Value = (model.Type == 1) ? 0 : 1;
            para[3].Value = model.Description;
            para[4].Value = model.CreatedUserID;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        public CommandInfo GenerateBlog(Maticsoft.Model.SNS.UserBlog blogModel)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_UserBlog(");
            builder.Append("Title,Summary,Description,UserID,UserName,LinkUrl,Status,Keywords,Recomend,Attachment,Remark,PvCount,TotalComment,TotalFav,TotalShare,CreatedDate)");
            builder.Append(" values (");
            builder.Append("@Title,@Summary,@Description,@UserID,@UserName,@LinkUrl,@Status,@Keywords,@Recomend,@Attachment,@Remark,@PvCount,@TotalComment,@TotalFav,@TotalShare,@CreatedDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@Title", SqlDbType.NVarChar, 100), new SqlParameter("@Summary", SqlDbType.NVarChar, 300), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NVarChar, 100), new SqlParameter("@LinkUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Keywords", SqlDbType.NVarChar, 50), new SqlParameter("@Recomend", SqlDbType.Int, 4), new SqlParameter("@Attachment", SqlDbType.NVarChar, 200), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@PvCount", SqlDbType.Int, 4), new SqlParameter("@TotalComment", SqlDbType.Int, 4), new SqlParameter("@TotalFav", SqlDbType.Int, 4), new SqlParameter("@TotalShare", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime) };
            para[0].Value = blogModel.Title;
            para[1].Value = blogModel.Summary;
            para[2].Value = blogModel.Description;
            para[3].Value = blogModel.UserID;
            para[4].Value = blogModel.UserName;
            para[5].Value = blogModel.LinkUrl;
            para[6].Value = blogModel.Status;
            para[7].Value = blogModel.Keywords;
            para[8].Value = 0;
            para[9].Value = blogModel.Attachment;
            para[10].Value = blogModel.Remark;
            para[11].Value = 0;
            para[12].Value = 0;
            para[13].Value = 0;
            para[14].Value = 0;
            para[15].Value = blogModel.CreatedDate;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        private CommandInfo GenerateImageInfo(Maticsoft.Model.SNS.Posts model, Maticsoft.Model.SNS.Products PModel, int AlbumId, long Pid, int PhotoCateId, int RecommandStateInt, string photoAddress, string mapLng, string mapLat)
        {
            if (model.Type == 0)
            {
                return null;
            }
            if (model.Type == 2)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("insert into SNS_Products(");
                builder.Append("ProductName,Price,ProductSourceID,CategoryID,ProductUrl,CreateUserID,CreatedNickName,ThumbImageUrl,NormalImageUrl,Status,ShareDescription,CreatedDate,Tags,IsRecomend)");
                builder.Append(" values (");
                builder.Append("@ProductName,@Price,@ProductSourceID,@CategoryID,@ProductUrl,@CreateUserID,@CreatedNickName,@ThumbImageUrl,@NormalImageUrl,@Status,@ShareDescription,@CreatedDate,@Tags,@IsRecomend)");
                builder.Append(";select @@IDENTITY");
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@Price", SqlDbType.Decimal, 9), new SqlParameter("@ProductSourceID", SqlDbType.Int, 4), new SqlParameter("@CategoryID", SqlDbType.Int, 4), new SqlParameter("@ProductUrl", SqlDbType.NVarChar, 500), new SqlParameter("@CreateUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ShareDescription", SqlDbType.NVarChar, 200), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Tags", SqlDbType.NVarChar, 400), new SqlParameter("@IsRecomend", SqlDbType.Int, 4) };
                parameterArray[0].Value = PModel.ProductName;
                parameterArray[1].Value = PModel.Price;
                parameterArray[2].Value = PModel.ProductSourceID;
                parameterArray[3].Value = PModel.CategoryID;
                parameterArray[4].Value = PModel.ProductUrl;
                parameterArray[5].Value = PModel.CreateUserID;
                parameterArray[6].Value = PModel.CreatedNickName;
                parameterArray[7].Value = PModel.ThumbImageUrl;
                parameterArray[8].Value = PModel.NormalImageUrl;
                parameterArray[9].Value = PModel.Status;
                parameterArray[10].Value = PModel.ShareDescription;
                parameterArray[11].Value = PModel.CreatedDate;
                parameterArray[12].Value = PModel.Tags;
                parameterArray[13].Value = RecommandStateInt;
                return new CommandInfo(builder.ToString(), parameterArray, EffentNextType.ExcuteEffectRows);
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("insert into SNS_Photos(");
            builder2.Append(" PhotoUrl,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,ThumbImageUrl,NormalImageUrl,IsRecomend,MapLng,MapLat,PhotoAddress,Type)");
            builder2.Append(" values (");
            builder2.Append(" @PhotoUrl,@Description,@Status,@CreatedUserID,@CreatedNickName,@CreatedDate,@CategoryId,@ThumbImageUrl,@NormalImageUrl,@IsRecomend,@MapLng,@MapLat,@PhotoAddress,@Type)");
            builder2.Append(";select @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@PhotoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@IsRecomend", SqlDbType.Int, 4), new SqlParameter("@MapLng", SqlDbType.NVarChar, 200), new SqlParameter("@MapLat", SqlDbType.NVarChar, 200), new SqlParameter("@PhotoAddress", SqlDbType.NVarChar, 300), new SqlParameter("@Type", SqlDbType.Int, 4) };
            if (!string.IsNullOrWhiteSpace(model.ImageUrl) && (model.ImageUrl.Split(new char[] { '|' }).Length >= 2))
            {
                para[0].Value = model.ImageUrl.Split(new char[] { '|' })[0];
                para[7].Value = model.ImageUrl.Split(new char[] { '|' })[1];
            }
            else
            {
                para[0].Value = "";
                para[7].Value = "";
            }
            para[1].Value = model.Description;
            if (model.Status == 1)
            {
                para[2].Value = 1;
            }
            else
            {
                para[2].Value = 0;
            }
            para[3].Value = model.CreatedUserID;
            para[4].Value = model.CreatedNickName;
            para[5].Value = model.CreatedDate;
            para[6].Value = PhotoCateId;
            para[9].Value = RecommandStateInt;
            para[10].Value = mapLng;
            para[11].Value = mapLat;
            para[12].Value = photoAddress;
            para[13].Value = 0;
            return new CommandInfo(builder2.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        private CommandInfo GeneratePostInfo(Maticsoft.Model.SNS.Posts model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_Posts(");
            builder.Append("CreatedUserID,CreatedNickName,Description,HasReferUsers,Type,PostExUrl,VideoUrl,AudioUrl,ImageUrl,TargetId,TopicTitle,Price,ProductLinkUrl,ProductName,UserIP,Status,CreatedDate)");
            builder.Append(" values (");
            builder.Append("@CreatedUserID,@CreatedNickName,@Description,@HasReferUsers,@Type,@PostExUrl,@VideoUrl,@AudioUrl,@ImageUrl,@TargetId,@TopicTitle,@Price,@ProductLinkUrl,@ProductName,@UserIP,@Status,@CreatedDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@HasReferUsers", SqlDbType.Bit, 1), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@PostExUrl", SqlDbType.NVarChar, 200), new SqlParameter("@VideoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@AudioUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@TopicTitle", SqlDbType.NVarChar, 200), new SqlParameter("@Price", SqlDbType.Decimal, 9), new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar, 500), new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@UserIP", SqlDbType.NVarChar, 15), new SqlParameter("@Status", SqlDbType.Int, 4), 
                new SqlParameter("@CreatedDate", SqlDbType.DateTime)
             };
            para[0].Value = model.CreatedUserID;
            para[1].Value = model.CreatedNickName;
            para[2].Value = model.Description;
            para[3].Value = model.HasReferUsers;
            para[4].Value = model.Type;
            para[5].Value = model.PostExUrl;
            para[6].Value = model.VideoUrl;
            para[7].Value = model.AudioUrl;
            if ((!string.IsNullOrEmpty(model.ImageUrl) && (model.ImageUrl.Split(new char[] { '|' }).Length >= 2)) && (model.Type != 2))
            {
                para[8].Value = model.ImageUrl.Split(new char[] { '|' })[1];
            }
            else
            {
                para[8].Value = model.ImageUrl;
            }
            para[9].Value = model.TargetId;
            para[10].Value = model.TopicTitle;
            para[11].Value = model.Price;
            para[12].Value = model.ProductLinkUrl;
            para[13].Value = model.ProductName;
            para[14].Value = model.UserIP;
            para[15].Value = model.Status;
            para[0x10].Value = model.CreatedDate;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        public CommandInfo GenerateUpdateAlbum(int AlbumId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserAlbums set ");
            builder.Append("PhotoCount=PhotoCount+1 ");
            builder.Append(" where AlbumID=@AlbumID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            para[0].Value = AlbumId;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        public CommandInfo GenerateUpdateUserEx(int UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_UsersExp set ShareCount=ShareCount+1,ProductsCount=ProductsCount+1 WHERE UserID=@UserID ");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            para[0].Value = UserId;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select PostID,CreatedUserID,CreatedNickName,OriginalID,ForwardedID,Description,HasReferUsers,CommentCount,ForwardCount,Type,PostExUrl,VideoUrl,AudioUrl,ImageUrl,TargetId,TopicTitle,Price,ProductLinkUrl,ProductName,FavCount,UserIP,Status,CreatedDate,IsRecommend,Sequence,Tags ");
            builder.Append(" FROM SNS_Posts ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" PostID,CreatedUserID,CreatedNickName,OriginalID,ForwardedID,Description,HasReferUsers,CommentCount,ForwardCount,Type,PostExUrl,VideoUrl,AudioUrl,ImageUrl,TargetId,TopicTitle,Price,ProductLinkUrl,ProductName,FavCount,UserIP,Status,CreatedDate,IsRecommend,Sequence,Tags ");
            builder.Append(" FROM SNS_Posts ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.PostID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_Posts T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.Posts GetModel(int PostID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 PostID,CreatedUserID,CreatedNickName,OriginalID,ForwardedID,Description,HasReferUsers,CommentCount,ForwardCount,Type,PostExUrl,VideoUrl,AudioUrl,ImageUrl,TargetId,TopicTitle,Price,ProductLinkUrl,ProductName,FavCount,UserIP,Status,CreatedDate,IsRecommend,Sequence,Tags from SNS_Posts ");
            builder.Append(" where PostID=@PostID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PostID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PostID;
            new Maticsoft.Model.SNS.Posts();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public DataSet GetPostUserIds(string ids)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select CreatedUserID from SNS_Posts  where PostID IN (" + ids + ") ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM SNS_Posts ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Maticsoft.Model.SNS.Posts model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Posts set ");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("CreatedNickName=@CreatedNickName,");
            builder.Append("OriginalID=@OriginalID,");
            builder.Append("ForwardedID=@ForwardedID,");
            builder.Append("Description=@Description,");
            builder.Append("HasReferUsers=@HasReferUsers,");
            builder.Append("CommentCount=@CommentCount,");
            builder.Append("ForwardCount=@ForwardCount,");
            builder.Append("Type=@Type,");
            builder.Append("PostExUrl=@PostExUrl,");
            builder.Append("VideoUrl=@VideoUrl,");
            builder.Append("AudioUrl=@AudioUrl,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("TargetId=@TargetId,");
            builder.Append("TopicTitle=@TopicTitle,");
            builder.Append("Price=@Price,");
            builder.Append("ProductLinkUrl=@ProductLinkUrl,");
            builder.Append("ProductName=@ProductName,");
            builder.Append("FavCount=@FavCount,");
            builder.Append("UserIP=@UserIP,");
            builder.Append("Status=@Status,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("IsRecommend=@IsRecommend,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("Tags=@Tags");
            builder.Append(" where PostID=@PostID");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@OriginalID", SqlDbType.Int, 4), new SqlParameter("@ForwardedID", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@HasReferUsers", SqlDbType.Bit, 1), new SqlParameter("@CommentCount", SqlDbType.Int, 4), new SqlParameter("@ForwardCount", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@PostExUrl", SqlDbType.NVarChar, 200), new SqlParameter("@VideoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@AudioUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@TopicTitle", SqlDbType.NVarChar, 200), new SqlParameter("@Price", SqlDbType.Decimal, 9), 
                new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar, 500), new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@FavCount", SqlDbType.Int, 4), new SqlParameter("@UserIP", SqlDbType.NVarChar, 15), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@IsRecommend", SqlDbType.Bit, 1), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@PostID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.CreatedUserID;
            cmdParms[1].Value = model.CreatedNickName;
            cmdParms[2].Value = model.OriginalID;
            cmdParms[3].Value = model.ForwardedID;
            cmdParms[4].Value = model.Description;
            cmdParms[5].Value = model.HasReferUsers;
            cmdParms[6].Value = model.CommentCount;
            cmdParms[7].Value = model.ForwardCount;
            cmdParms[8].Value = model.Type;
            cmdParms[9].Value = model.PostExUrl;
            cmdParms[10].Value = model.VideoUrl;
            cmdParms[11].Value = model.AudioUrl;
            cmdParms[12].Value = model.ImageUrl;
            cmdParms[13].Value = model.TargetId;
            cmdParms[14].Value = model.TopicTitle;
            cmdParms[15].Value = model.Price;
            cmdParms[0x10].Value = model.ProductLinkUrl;
            cmdParms[0x11].Value = model.ProductName;
            cmdParms[0x12].Value = model.FavCount;
            cmdParms[0x13].Value = model.UserIP;
            cmdParms[20].Value = model.Status;
            cmdParms[0x15].Value = model.CreatedDate;
            cmdParms[0x16].Value = model.IsRecommend;
            cmdParms[0x17].Value = model.Sequence;
            cmdParms[0x18].Value = model.Tags;
            cmdParms[0x19].Value = model.PostID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateCommentCount(int postId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update SNS_Posts Set CommentCount=CommentCount+1 where PostId=@PostId");
            builder.Append(" ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PostId", SqlDbType.Int, 4) };
            cmdParms[0].Value = postId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateFavCount(int postId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update SNS_Posts Set FavCount=FavCount+1 where PostId=@PostId");
            builder.Append(" ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PostId", SqlDbType.Int, 4) };
            cmdParms[0].Value = postId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public int UpdateForwardCount(string StrWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update SNS_Posts Set ForWardCount=ForWardCount+1");
            builder.Append(" (");
            if (!string.IsNullOrEmpty(StrWhere.Trim()))
            {
                builder.Append("where PostId in(" + StrWhere + ")");
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public void UpdateStatus(List<CommandInfo> sqllist, int PostID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE SNS_Posts SET STATUS=@STATUS Where PostID=@PostID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@STATUS", SqlDbType.Int, 4), new SqlParameter("@PostID", SqlDbType.Int, 4) };
            para[0].Value = 3;
            para[1].Value = PostID;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            sqllist.Add(item);
        }

        public bool UpdateStatusList(string PostIds, int Status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { "UPDATE SNS_Posts SET STATUS=", Status, " Where PostID in (", PostIds, ")" }));
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdateToDel(int PostID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Posts set ");
            builder.Append("PostExUrl=@PostExUrl,");
            builder.Append("VideoUrl=@VideoUrl,");
            builder.Append("AudioUrl=@AudioUrl,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("Status=@Status,");
            builder.Append("Description=@Description");
            builder.Append(" where PostID=@PostID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PostExUrl", SqlDbType.NVarChar, 200), new SqlParameter("@VideoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@AudioUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@PostID", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = "";
            cmdParms[1].Value = "";
            cmdParms[2].Value = "";
            cmdParms[3].Value = "";
            cmdParms[4].Value = 3;
            cmdParms[5].Value = PostID;
            cmdParms[6].Value = "此动态已删除";
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

