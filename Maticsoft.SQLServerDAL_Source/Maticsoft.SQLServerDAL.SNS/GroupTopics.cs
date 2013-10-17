namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.Common;
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using Maticsoft.SQLServerDAL.SysManage;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class GroupTopics : IGroupTopics
    {
        public int Add(Maticsoft.Model.SNS.GroupTopics model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_GroupTopics(");
            builder.Append("CreatedUserID,CreatedNickName,GroupID,GroupName,Title,Description,IsRecomend,Sequence,ReplyCount,PvCount,DingCount,Status,IsTop,IsActive,IsAdminRecommend,ChannelSequence,HasReferUsers,PostExUrl,ImageUrl,VideoUrl,AudioUrl,ProductName,Price,ProductLinkUrl,Type,TargetID,FavCount,CreatedDate,LastReplyUserId,LastReplyNickName,LastPostTime,Tags)");
            builder.Append(" values (");
            builder.Append("@CreatedUserID,@CreatedNickName,@GroupID,@GroupName,@Title,@Description,@IsRecomend,@Sequence,@ReplyCount,@PvCount,@DingCount,@Status,@IsTop,@IsActive,@IsAdminRecommend,@ChannelSequence,@HasReferUsers,@PostExUrl,@ImageUrl,@VideoUrl,@AudioUrl,@ProductName,@Price,@ProductLinkUrl,@Type,@TargetID,@FavCount,@CreatedDate,@LastReplyUserId,@LastReplyNickName,@LastPostTime,@Tags)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@GroupName", SqlDbType.NVarChar, 50), new SqlParameter("@Title", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@IsRecomend", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@ReplyCount", SqlDbType.Int, 4), new SqlParameter("@PvCount", SqlDbType.Int, 4), new SqlParameter("@DingCount", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@IsTop", SqlDbType.Int, 4), new SqlParameter("@IsActive", SqlDbType.Bit, 1), new SqlParameter("@IsAdminRecommend", SqlDbType.Bit, 1), new SqlParameter("@ChannelSequence", SqlDbType.Int, 4), 
                new SqlParameter("@HasReferUsers", SqlDbType.Bit, 1), new SqlParameter("@PostExUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@VideoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@AudioUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@Price", SqlDbType.Decimal, 9), new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar, 500), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@FavCount", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@LastReplyUserId", SqlDbType.Int, 4), new SqlParameter("@LastReplyNickName", SqlDbType.NVarChar, 50), new SqlParameter("@LastPostTime", SqlDbType.DateTime), new SqlParameter("@Tags", SqlDbType.NVarChar, 100)
             };
            cmdParms[0].Value = model.CreatedUserID;
            cmdParms[1].Value = model.CreatedNickName;
            cmdParms[2].Value = model.GroupID;
            cmdParms[3].Value = model.GroupName;
            cmdParms[4].Value = model.Title;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.IsRecomend;
            cmdParms[7].Value = model.Sequence;
            cmdParms[8].Value = model.ReplyCount;
            cmdParms[9].Value = model.PvCount;
            cmdParms[10].Value = model.DingCount;
            cmdParms[11].Value = model.Status;
            cmdParms[12].Value = model.IsTop;
            cmdParms[13].Value = model.IsActive;
            cmdParms[14].Value = model.IsAdminRecommend;
            cmdParms[15].Value = model.ChannelSequence;
            cmdParms[0x10].Value = model.HasReferUsers;
            cmdParms[0x11].Value = model.PostExUrl;
            cmdParms[0x12].Value = model.ImageUrl;
            cmdParms[0x13].Value = model.VideoUrl;
            cmdParms[20].Value = model.AudioUrl;
            cmdParms[0x15].Value = model.ProductName;
            cmdParms[0x16].Value = model.Price;
            cmdParms[0x17].Value = model.ProductLinkUrl;
            cmdParms[0x18].Value = model.Type;
            cmdParms[0x19].Value = model.TargetID;
            cmdParms[0x1a].Value = model.FavCount;
            cmdParms[0x1b].Value = model.CreatedDate;
            cmdParms[0x1c].Value = model.LastReplyUserId;
            cmdParms[0x1d].Value = model.LastReplyNickName;
            cmdParms[30].Value = model.LastPostTime;
            cmdParms[0x1f].Value = model.Tags;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public int AddEx(Maticsoft.Model.SNS.GroupTopics Tmodel, Maticsoft.Model.SNS.Products PModel)
        {
            int num;
            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    object obj2 = "0";
                    object obj3 = "0";
                    Tmodel.Type = 0;
                    if (((PModel != null) && (PModel.ProductID > 0L)) || !string.IsNullOrEmpty(Tmodel.ImageUrl))
                    {
                        obj2 = DbHelperSQL.GetSingle4Trans(this.GenerateImageInfo(Tmodel, PModel), trans);
                        Tmodel.TargetID = new int?(Globals.SafeInt((obj2 != null) ? obj2.ToString() : "", 0));
                        Tmodel.Type = (PModel.ProductID > 0L) ? 2 : 1;
                    }
                    obj3 = DbHelperSQL.GetSingle4Trans(this.GenerateTopicInfo(Tmodel), trans);
                    DbHelperSQL.GetSingle4Trans(this.GenerateUpdateUserEx(Tmodel.CreatedUserID, Tmodel.Type), trans);
                    DbHelperSQL.GetSingle4Trans(this.GenerateUpdateGroupCount(Tmodel), trans);
                    trans.Commit();
                    num = Globals.SafeInt((obj3 != null) ? obj3.ToString() : "", 0);
                }
                catch (Exception)
                {
                    trans.Rollback();
                    num = 0;
                }
                finally
                {
                    if (trans != null)
                    {
                        trans.Dispose();
                    }
                }
            }
            return num;
        }

        public Maticsoft.Model.SNS.GroupTopics DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.GroupTopics topics = new Maticsoft.Model.SNS.GroupTopics();
            if (row != null)
            {
                if ((row["TopicID"] != null) && (row["TopicID"].ToString() != ""))
                {
                    topics.TopicID = int.Parse(row["TopicID"].ToString());
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    topics.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    topics.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if ((row["GroupID"] != null) && (row["GroupID"].ToString() != ""))
                {
                    topics.GroupID = int.Parse(row["GroupID"].ToString());
                }
                if (row["GroupName"] != null)
                {
                    topics.GroupName = row["GroupName"].ToString();
                }
                if (row["Title"] != null)
                {
                    topics.Title = row["Title"].ToString();
                }
                if (row["Description"] != null)
                {
                    topics.Description = row["Description"].ToString();
                }
                if ((row["IsRecomend"] != null) && (row["IsRecomend"].ToString() != ""))
                {
                    topics.IsRecomend = int.Parse(row["IsRecomend"].ToString());
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    topics.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if ((row["ReplyCount"] != null) && (row["ReplyCount"].ToString() != ""))
                {
                    topics.ReplyCount = int.Parse(row["ReplyCount"].ToString());
                }
                if ((row["PvCount"] != null) && (row["PvCount"].ToString() != ""))
                {
                    topics.PvCount = int.Parse(row["PvCount"].ToString());
                }
                if ((row["DingCount"] != null) && (row["DingCount"].ToString() != ""))
                {
                    topics.DingCount = int.Parse(row["DingCount"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    topics.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["IsTop"] != null) && (row["IsTop"].ToString() != ""))
                {
                    topics.IsTop = int.Parse(row["IsTop"].ToString());
                }
                if ((row["IsActive"] != null) && (row["IsActive"].ToString() != ""))
                {
                    if ((row["IsActive"].ToString() == "1") || (row["IsActive"].ToString().ToLower() == "true"))
                    {
                        topics.IsActive = true;
                    }
                    else
                    {
                        topics.IsActive = false;
                    }
                }
                if ((row["IsAdminRecommend"] != null) && (row["IsAdminRecommend"].ToString() != ""))
                {
                    if ((row["IsAdminRecommend"].ToString() == "1") || (row["IsAdminRecommend"].ToString().ToLower() == "true"))
                    {
                        topics.IsAdminRecommend = true;
                    }
                    else
                    {
                        topics.IsAdminRecommend = false;
                    }
                }
                if ((row["ChannelSequence"] != null) && (row["ChannelSequence"].ToString() != ""))
                {
                    topics.ChannelSequence = new int?(int.Parse(row["ChannelSequence"].ToString()));
                }
                if ((row["HasReferUsers"] != null) && (row["HasReferUsers"].ToString() != ""))
                {
                    if ((row["HasReferUsers"].ToString() == "1") || (row["HasReferUsers"].ToString().ToLower() == "true"))
                    {
                        topics.HasReferUsers = true;
                    }
                    else
                    {
                        topics.HasReferUsers = false;
                    }
                }
                if (row["PostExUrl"] != null)
                {
                    topics.PostExUrl = row["PostExUrl"].ToString();
                }
                if (row["ImageUrl"] != null)
                {
                    topics.ImageUrl = row["ImageUrl"].ToString();
                }
                if (row["VideoUrl"] != null)
                {
                    topics.VideoUrl = row["VideoUrl"].ToString();
                }
                if (row["AudioUrl"] != null)
                {
                    topics.AudioUrl = row["AudioUrl"].ToString();
                }
                if (row["ProductName"] != null)
                {
                    topics.ProductName = row["ProductName"].ToString();
                }
                if ((row["Price"] != null) && (row["Price"].ToString() != ""))
                {
                    topics.Price = new decimal?(decimal.Parse(row["Price"].ToString()));
                }
                if (row["ProductLinkUrl"] != null)
                {
                    topics.ProductLinkUrl = row["ProductLinkUrl"].ToString();
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    topics.Type = int.Parse(row["Type"].ToString());
                }
                if ((row["TargetID"] != null) && (row["TargetID"].ToString() != ""))
                {
                    topics.TargetID = new int?(int.Parse(row["TargetID"].ToString()));
                }
                if ((row["FavCount"] != null) && (row["FavCount"].ToString() != ""))
                {
                    topics.FavCount = int.Parse(row["FavCount"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    topics.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["LastReplyUserId"] != null) && (row["LastReplyUserId"].ToString() != ""))
                {
                    topics.LastReplyUserId = new int?(int.Parse(row["LastReplyUserId"].ToString()));
                }
                if (row["LastReplyNickName"] != null)
                {
                    topics.LastReplyNickName = row["LastReplyNickName"].ToString();
                }
                if ((row["LastPostTime"] != null) && (row["LastPostTime"].ToString() != ""))
                {
                    topics.LastPostTime = new DateTime?(DateTime.Parse(row["LastPostTime"].ToString()));
                }
                if (row["Tags"] != null)
                {
                    topics.Tags = row["Tags"].ToString();
                }
            }
            return topics;
        }

        public bool Delete(int TopicID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_GroupTopics ");
            builder.Append(" where TopicID=@TopicID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TopicID", SqlDbType.Int, 4) };
            cmdParms[0].Value = TopicID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteData(Maticsoft.Model.SNS.GroupTopics model)
        {
            int groupID = model.GroupID;
            int createdUserID = model.CreatedUserID;
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Groups set ");
            builder.Append("TopicCount=TopicCount-1");
            builder.Append(" where GroupID=@GroupID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4) };
            para[0].Value = model.GroupID;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update Accounts_UsersExp  set TopicCount=TopicCount-1 where UserID=@UserID");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = createdUserID;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("delete from SNS_GroupTopicReply ");
            builder3.Append(" where TopicID=@TopicID");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@TopicID", SqlDbType.Int, 4) };
            parameterArray3[0].Value = model.TopicID;
            CommandInfo info3 = new CommandInfo(builder3.ToString(), parameterArray3);
            cmdList.Add(info3);
            StringBuilder builder4 = new StringBuilder();
            builder4.Append("update Accounts_UsersExp  set FavTopicCount=FavTopicCount-1 where UserID in(select CreatedUserID from SNS_GroupTopicFav where TopicID=@TopicID) ");
            SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@TopicID", SqlDbType.Int, 4) };
            parameterArray4[0].Value = model.TopicID;
            CommandInfo info4 = new CommandInfo(builder4.ToString(), parameterArray4);
            cmdList.Add(info4);
            StringBuilder builder5 = new StringBuilder();
            builder5.Append("delete from SNS_GroupTopicFav ");
            builder5.Append(" where TopicID=@TopicID");
            SqlParameter[] parameterArray5 = new SqlParameter[] { new SqlParameter("@TopicID", SqlDbType.Int, 4) };
            parameterArray5[0].Value = model.TopicID;
            CommandInfo info5 = new CommandInfo(builder5.ToString(), parameterArray5);
            cmdList.Add(info5);
            StringBuilder builder6 = new StringBuilder();
            builder6.Append("delete from SNS_GroupTopics ");
            builder6.Append(" where TopicID=@TopicID");
            SqlParameter[] parameterArray6 = new SqlParameter[] { new SqlParameter("@TopicID", SqlDbType.Int, 4) };
            parameterArray6[0].Value = model.TopicID;
            CommandInfo info6 = new CommandInfo(builder6.ToString(), parameterArray6);
            cmdList.Add(info6);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteEx(int TopicID)
        {
            Maticsoft.Model.SNS.GroupTopics model = this.GetModel(TopicID);
            return ((model == null) || this.DeleteData(model));
        }

        public bool DeleteEx(int TopicID, out string ImageUrl)
        {
            Maticsoft.Model.SNS.GroupTopics model = this.GetModel(TopicID);
            if (model == null)
            {
                ImageUrl = "";
                return true;
            }
            ImageUrl = model.ImageUrl;
            return this.DeleteData(model);
        }

        public bool DeleteList(string TopicIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_GroupTopics ");
            builder.Append(" where TopicID in (" + TopicIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int TopicID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_GroupTopics");
            builder.Append(" where TopicID=@TopicID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TopicID", SqlDbType.Int, 50) };
            cmdParms[0].Value = TopicID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        private CommandInfo GenerateImageInfo(Maticsoft.Model.SNS.GroupTopics Tmodel, Maticsoft.Model.SNS.Products PModel)
        {
            if ((PModel != null) && (PModel.ProductID > 0L))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("insert into SNS_Products(");
                builder.Append("ProductName,Price,ProductSourceID,CategoryID,ProductUrl,CreateUserID,CreatedNickName,ThumbImageUrl,NormalImageUrl,Status,ShareDescription,CreatedDate,Tags,IsRecomend)");
                builder.Append(" values (");
                builder.Append("@ProductName,@Price,@ProductSourceID,@CategoryID,@ProductUrl,@CreateUserID,@CreatedNickName,@ThumbImageUrl,@NormalImageUrl,@Status,@ShareDescription,@CreatedDate,@Tags,@IsRecomend)");
                builder.Append(";select @@IDENTITY");
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@Price", SqlDbType.Decimal, 9), new SqlParameter("@ProductSourceID", SqlDbType.Int, 4), new SqlParameter("@CategoryID", SqlDbType.Int, 4), new SqlParameter("@ProductUrl", SqlDbType.NVarChar, 500), new SqlParameter("@CreateUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ShareDescription", SqlDbType.NVarChar, 200), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Tags", SqlDbType.NVarChar, 400), new SqlParameter("@IsRecomend", SqlDbType.NVarChar, 400) };
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
                parameterArray[13].Value = 0;
                Tmodel.ImageUrl = PModel.ThumbImageUrl;
                Tmodel.Price = PModel.Price;
                Tmodel.ProductLinkUrl = PModel.ProductUrl;
                Tmodel.ProductName = PModel.ProductName;
                new CommandInfo(builder.ToString(), parameterArray);
                return new CommandInfo(builder.ToString(), parameterArray, EffentNextType.ExcuteEffectRows);
            }
            if (string.IsNullOrEmpty(Tmodel.ImageUrl))
            {
                return null;
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("insert into SNS_Photos(");
            builder2.Append(" PhotoUrl,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,Type,ThumbImageUrl,NormalImageUrl)");
            builder2.Append(" values (");
            builder2.Append(" @PhotoUrl,@Description,@Status,@CreatedUserID,@CreatedNickName,@CreatedDate,@Type,@ThumbImageUrl,@NormalImageUrl)");
            builder2.Append(";select @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@PhotoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 200) };
            string[] strArray = string.IsNullOrEmpty(Tmodel.ImageUrl) ? null : Tmodel.ImageUrl.Split(new char[] { '|' });
            if ((strArray != null) && (strArray.Length >= 2))
            {
                para[0].Value = strArray[0];
                para[7].Value = strArray[1];
                Tmodel.ImageUrl = strArray[1];
            }
            else
            {
                para[0].Value = "";
                para[7].Value = "";
                para[8].Value = "";
            }
            para[1].Value = "";
            string str = new ConfigSystem().GetValue("SNS_PhotoDefaultStatus");
            if (!string.IsNullOrEmpty(str))
            {
                para[2].Value = Globals.SafeInt(str, 1);
            }
            else
            {
                para[2].Value = 3;
            }
            para[3].Value = Tmodel.CreatedUserID;
            para[4].Value = Tmodel.CreatedNickName;
            para[5].Value = Tmodel.CreatedDate;
            para[6].Value = 3;
            return new CommandInfo(builder2.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        public CommandInfo GenerateTopicInfo(Maticsoft.Model.SNS.GroupTopics Tmodel)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_GroupTopics(");
            builder.Append("CreatedUserID,CreatedNickName,GroupID,GroupName,Title,Description,IsRecomend,Sequence,ReplyCount,PvCount,DingCount,Status,IsTop,IsActive,IsAdminRecommend,ChannelSequence,HasReferUsers,PostExUrl,ImageUrl,VideoUrl,AudioUrl,ProductName,Price,ProductLinkUrl,Type,TargetID,FavCount,CreatedDate,LastReplyUserId,LastReplyNickName,LastPostTime,Tags)");
            builder.Append(" values (");
            builder.Append("@CreatedUserID,@CreatedNickName,@GroupID,@GroupName,@Title,@Description,@IsRecomend,@Sequence,@ReplyCount,@PvCount,@DingCount,@Status,@IsTop,@IsActive,@IsAdminRecommend,@ChannelSequence,@HasReferUsers,@PostExUrl,@ImageUrl,@VideoUrl,@AudioUrl,@ProductName,@Price,@ProductLinkUrl,@Type,@TargetID,@FavCount,@CreatedDate,@LastReplyUserId,@LastReplyNickName,@LastPostTime,@Tags)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@GroupName", SqlDbType.NVarChar, 50), new SqlParameter("@Title", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@IsRecomend", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@ReplyCount", SqlDbType.Int, 4), new SqlParameter("@PvCount", SqlDbType.Int, 4), new SqlParameter("@DingCount", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@IsTop", SqlDbType.Int, 4), new SqlParameter("@IsActive", SqlDbType.Bit, 1), new SqlParameter("@IsAdminRecommend", SqlDbType.Bit, 1), new SqlParameter("@ChannelSequence", SqlDbType.Int, 4), 
                new SqlParameter("@HasReferUsers", SqlDbType.Bit, 1), new SqlParameter("@PostExUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@VideoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@AudioUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@Price", SqlDbType.Decimal, 9), new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar, 500), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@FavCount", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@LastReplyUserId", SqlDbType.Int, 4), new SqlParameter("@LastReplyNickName", SqlDbType.NVarChar, 50), new SqlParameter("@LastPostTime", SqlDbType.DateTime), new SqlParameter("@Tags", SqlDbType.NVarChar, 100)
             };
            para[0].Value = Tmodel.CreatedUserID;
            para[1].Value = Tmodel.CreatedNickName;
            para[2].Value = Tmodel.GroupID;
            para[3].Value = Tmodel.GroupName;
            para[4].Value = Tmodel.Title;
            para[5].Value = Tmodel.Description;
            para[6].Value = Tmodel.IsRecomend;
            para[7].Value = Tmodel.Sequence;
            para[8].Value = Tmodel.ReplyCount;
            para[9].Value = Tmodel.PvCount;
            para[10].Value = Tmodel.DingCount;
            para[11].Value = Tmodel.Status;
            para[12].Value = Tmodel.IsTop;
            para[13].Value = Tmodel.IsActive;
            para[14].Value = Tmodel.IsAdminRecommend;
            para[15].Value = Tmodel.ChannelSequence;
            para[0x10].Value = Tmodel.HasReferUsers;
            para[0x11].Value = Tmodel.PostExUrl;
            para[0x12].Value = Tmodel.ImageUrl;
            para[0x13].Value = Tmodel.VideoUrl;
            para[20].Value = Tmodel.AudioUrl;
            para[0x15].Value = Tmodel.ProductName;
            para[0x16].Value = Tmodel.Price;
            para[0x17].Value = Tmodel.ProductLinkUrl;
            para[0x18].Value = Tmodel.Type;
            para[0x19].Value = Tmodel.TargetID;
            para[0x1a].Value = Tmodel.FavCount;
            para[0x1b].Value = Tmodel.CreatedDate;
            para[0x1c].Value = Tmodel.LastReplyUserId;
            para[0x1d].Value = Tmodel.LastReplyNickName;
            para[30].Value = Tmodel.LastPostTime;
            para[0x1f].Value = Tmodel.Tags;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        public CommandInfo GenerateUpdateGroupCount(Maticsoft.Model.SNS.GroupTopics Tmodel)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Groups set TopicCount=TopicCount+1 WHERE GroupID=@GroupID ");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4) };
            para[0].Value = Tmodel.GroupID;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        public CommandInfo GenerateUpdateUserEx(int UserId, int type)
        {
            CommandInfo info = new CommandInfo();
            if (type == 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("update Accounts_UsersExp set ShareCount=ShareCount+1,TopicCount=TopicCount+1 WHERE UserID=@UserID ");
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
                parameterArray[0].Value = UserId;
                return new CommandInfo(builder.ToString(), parameterArray, EffentNextType.ExcuteEffectRows);
            }
            if (type == 0)
            {
                StringBuilder builder2 = new StringBuilder();
                builder2.Append("update Accounts_UsersExp set ShareCount=ShareCount+1,ProductsCount=ProductsCount+1,TopicCount=TopicCount+1 WHERE UserID=@UserID ");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
                parameterArray2[0].Value = UserId;
                return new CommandInfo(builder2.ToString(), parameterArray2, EffentNextType.ExcuteEffectRows);
            }
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("update Accounts_UsersExp set TopicCount=TopicCount+1 WHERE UserID=@UserID ");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            para[0].Value = UserId;
            return new CommandInfo(builder3.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TopicID,CreatedUserID,CreatedNickName,GroupID,GroupName,Title,Description,IsRecomend,Sequence,ReplyCount,PvCount,DingCount,Status,IsTop,IsActive,IsAdminRecommend,ChannelSequence,HasReferUsers,PostExUrl,ImageUrl,VideoUrl,AudioUrl,ProductName,Price,ProductLinkUrl,Type,TargetID,FavCount,CreatedDate,LastReplyUserId,LastReplyNickName,LastPostTime,Tags ");
            builder.Append(" FROM SNS_GroupTopics ");
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
            builder.Append(" TopicID,CreatedUserID,CreatedNickName,GroupID,GroupName,Title,Description,IsRecomend,Sequence,ReplyCount,PvCount,DingCount,Status,IsTop,IsActive,IsAdminRecommend,ChannelSequence,HasReferUsers,PostExUrl,ImageUrl,VideoUrl,AudioUrl,ProductName,Price,ProductLinkUrl,Type,TargetID,FavCount,CreatedDate,LastReplyUserId,LastReplyNickName,LastPostTime,Tags ");
            builder.Append(" FROM SNS_GroupTopics ");
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
                builder.Append("order by T.TopicID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_GroupTopics T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListEx(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" * FROM SNS_GroupTopics ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                builder.Append(" order by " + filedOrder);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.GroupTopics GetModel(int TopicID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TopicID,CreatedUserID,CreatedNickName,GroupID,GroupName,Title,Description,IsRecomend,Sequence,ReplyCount,PvCount,DingCount,Status,IsTop,IsActive,IsAdminRecommend,ChannelSequence,HasReferUsers,PostExUrl,ImageUrl,VideoUrl,AudioUrl,ProductName,Price,ProductLinkUrl,Type,TargetID,FavCount,CreatedDate,LastReplyUserId,LastReplyNickName,LastPostTime,Tags from SNS_GroupTopics ");
            builder.Append(" where TopicID=@TopicID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TopicID", SqlDbType.Int, 4) };
            cmdParms[0].Value = TopicID;
            new Maticsoft.Model.SNS.GroupTopics();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM SNS_GroupTopics ");
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

        public bool Update(Maticsoft.Model.SNS.GroupTopics model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_GroupTopics set ");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("CreatedNickName=@CreatedNickName,");
            builder.Append("GroupID=@GroupID,");
            builder.Append("GroupName=@GroupName,");
            builder.Append("Title=@Title,");
            builder.Append("Description=@Description,");
            builder.Append("IsRecomend=@IsRecomend,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("ReplyCount=@ReplyCount,");
            builder.Append("PvCount=@PvCount,");
            builder.Append("DingCount=@DingCount,");
            builder.Append("Status=@Status,");
            builder.Append("IsTop=@IsTop,");
            builder.Append("IsActive=@IsActive,");
            builder.Append("IsAdminRecommend=@IsAdminRecommend,");
            builder.Append("ChannelSequence=@ChannelSequence,");
            builder.Append("HasReferUsers=@HasReferUsers,");
            builder.Append("PostExUrl=@PostExUrl,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("VideoUrl=@VideoUrl,");
            builder.Append("AudioUrl=@AudioUrl,");
            builder.Append("ProductName=@ProductName,");
            builder.Append("Price=@Price,");
            builder.Append("ProductLinkUrl=@ProductLinkUrl,");
            builder.Append("Type=@Type,");
            builder.Append("TargetID=@TargetID,");
            builder.Append("FavCount=@FavCount,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("LastReplyUserId=@LastReplyUserId,");
            builder.Append("LastReplyNickName=@LastReplyNickName,");
            builder.Append("LastPostTime=@LastPostTime,");
            builder.Append("Tags=@Tags");
            builder.Append(" where TopicID=@TopicID");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@GroupName", SqlDbType.NVarChar, 50), new SqlParameter("@Title", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@IsRecomend", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@ReplyCount", SqlDbType.Int, 4), new SqlParameter("@PvCount", SqlDbType.Int, 4), new SqlParameter("@DingCount", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@IsTop", SqlDbType.Int, 4), new SqlParameter("@IsActive", SqlDbType.Bit, 1), new SqlParameter("@IsAdminRecommend", SqlDbType.Bit, 1), new SqlParameter("@ChannelSequence", SqlDbType.Int, 4), 
                new SqlParameter("@HasReferUsers", SqlDbType.Bit, 1), new SqlParameter("@PostExUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@VideoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@AudioUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@Price", SqlDbType.Decimal, 9), new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar, 500), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@FavCount", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@LastReplyUserId", SqlDbType.Int, 4), new SqlParameter("@LastReplyNickName", SqlDbType.NVarChar, 50), new SqlParameter("@LastPostTime", SqlDbType.DateTime), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), 
                new SqlParameter("@TopicID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.CreatedUserID;
            cmdParms[1].Value = model.CreatedNickName;
            cmdParms[2].Value = model.GroupID;
            cmdParms[3].Value = model.GroupName;
            cmdParms[4].Value = model.Title;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.IsRecomend;
            cmdParms[7].Value = model.Sequence;
            cmdParms[8].Value = model.ReplyCount;
            cmdParms[9].Value = model.PvCount;
            cmdParms[10].Value = model.DingCount;
            cmdParms[11].Value = model.Status;
            cmdParms[12].Value = model.IsTop;
            cmdParms[13].Value = model.IsActive;
            cmdParms[14].Value = model.IsAdminRecommend;
            cmdParms[15].Value = model.ChannelSequence;
            cmdParms[0x10].Value = model.HasReferUsers;
            cmdParms[0x11].Value = model.PostExUrl;
            cmdParms[0x12].Value = model.ImageUrl;
            cmdParms[0x13].Value = model.VideoUrl;
            cmdParms[20].Value = model.AudioUrl;
            cmdParms[0x15].Value = model.ProductName;
            cmdParms[0x16].Value = model.Price;
            cmdParms[0x17].Value = model.ProductLinkUrl;
            cmdParms[0x18].Value = model.Type;
            cmdParms[0x19].Value = model.TargetID;
            cmdParms[0x1a].Value = model.FavCount;
            cmdParms[0x1b].Value = model.CreatedDate;
            cmdParms[0x1c].Value = model.LastReplyUserId;
            cmdParms[0x1d].Value = model.LastReplyNickName;
            cmdParms[30].Value = model.LastPostTime;
            cmdParms[0x1f].Value = model.Tags;
            cmdParms[0x20].Value = model.TopicID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateAdminRecommand(int TopicId, bool IsAdmin)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update SNS_GroupTopics set  ");
            if (IsAdmin)
            {
                builder.Append(" IsAdminRecommend=1 ");
            }
            else
            {
                builder.Append("IsAdminRecommend=0 ");
            }
            builder.Append("where TopicID=" + TopicId);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdatePVCount(int TopicID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update SNS_GroupTopics set PvCount=PvCount+1 ");
            builder.Append("where TopicID=" + TopicID);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdateRecommand(int TopicId, int Recommand)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update SNS_GroupTopics set  ");
            builder.Append("IsRecomend=" + Recommand);
            builder.Append("where TopicID=" + TopicId);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdateStatusList(string IdsStr, EnumHelper.TopicStatus status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_GroupTopics set Status=" + ((int) status) + " ");
            builder.Append(" where TopicID in (" + IdsStr + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

