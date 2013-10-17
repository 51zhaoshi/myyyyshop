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
    using System.Text;

    public class GroupTopicReply : IGroupTopicReply
    {
        public int Add(Maticsoft.Model.SNS.GroupTopicReply model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_GroupTopicReply(");
            builder.Append("GroupID,ReplyType,ReplyNickName,ReplyUserID,OriginalID,OrginalDes,OrginalUserID,OrgianlNickName,TopicID,Description,HasReferUsers,PhotoUrl,TargetId,Type,ProductUrl,ProductName,ReplyExUrl,ProductLinkUrl,FavCount,Price,UserIP,Status,CreatedDate)");
            builder.Append(" values (");
            builder.Append("@GroupID,@ReplyType,@ReplyNickName,@ReplyUserID,@OriginalID,@OrginalDes,@OrginalUserID,@OrgianlNickName,@TopicID,@Description,@HasReferUsers,@PhotoUrl,@TargetId,@Type,@ProductUrl,@ProductName,@ReplyExUrl,@ProductLinkUrl,@FavCount,@Price,@UserIP,@Status,@CreatedDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@ReplyType", SqlDbType.Int, 4), new SqlParameter("@ReplyNickName", SqlDbType.NVarChar, 50), new SqlParameter("@ReplyUserID", SqlDbType.Int, 4), new SqlParameter("@OriginalID", SqlDbType.Int, 4), new SqlParameter("@OrginalDes", SqlDbType.NVarChar), new SqlParameter("@OrginalUserID", SqlDbType.Int, 4), new SqlParameter("@OrgianlNickName", SqlDbType.NVarChar, 50), new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@HasReferUsers", SqlDbType.Bit, 1), new SqlParameter("@PhotoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@ProductUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), 
                new SqlParameter("@ReplyExUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar, 500), new SqlParameter("@FavCount", SqlDbType.Int, 4), new SqlParameter("@Price", SqlDbType.Decimal, 9), new SqlParameter("@UserIP", SqlDbType.NVarChar, 15), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime)
             };
            cmdParms[0].Value = model.GroupID;
            cmdParms[1].Value = model.ReplyType;
            cmdParms[2].Value = model.ReplyNickName;
            cmdParms[3].Value = model.ReplyUserID;
            cmdParms[4].Value = model.OriginalID;
            cmdParms[5].Value = model.OrginalDes;
            cmdParms[6].Value = model.OrginalUserID;
            cmdParms[7].Value = model.OrgianlNickName;
            cmdParms[8].Value = model.TopicID;
            cmdParms[9].Value = model.Description;
            cmdParms[10].Value = model.HasReferUsers;
            cmdParms[11].Value = model.PhotoUrl;
            cmdParms[12].Value = model.TargetId;
            cmdParms[13].Value = model.Type;
            cmdParms[14].Value = model.ProductUrl;
            cmdParms[15].Value = model.ProductName;
            cmdParms[0x10].Value = model.ReplyExUrl;
            cmdParms[0x11].Value = model.ProductLinkUrl;
            cmdParms[0x12].Value = model.FavCount;
            cmdParms[0x13].Value = model.Price;
            cmdParms[20].Value = model.UserIP;
            cmdParms[0x15].Value = model.Status;
            cmdParms[0x16].Value = model.CreatedDate;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public int AddEx(Maticsoft.Model.SNS.GroupTopicReply Tmodel, Maticsoft.Model.SNS.Products PModel)
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
                    if (((PModel != null) && (PModel.ProductID > 0L)) || !string.IsNullOrEmpty(Tmodel.PhotoUrl))
                    {
                        obj2 = DbHelperSQL.GetSingle4Trans(this.GenerateImageInfo(Tmodel, PModel), trans);
                        Tmodel.TargetId = new int?(Globals.SafeInt((obj2 != null) ? obj2.ToString() : "", 0));
                        Tmodel.Type = new int?((PModel.ProductID > 0L) ? 2 : 1);
                        DbHelperSQL.GetSingle4Trans(this.GenerateUpdateUserEx(Tmodel.ReplyUserID, Tmodel.Type.Value), trans);
                    }
                    obj3 = DbHelperSQL.GetSingle4Trans(this.GenerateTopicReplyInfo(Tmodel), trans);
                    DbHelperSQL.GetSingle4Trans(this.GenerateUpdateGroupEx(Tmodel.GroupID), trans);
                    DbHelperSQL.GetSingle4Trans(this.GenerateUpdateTopicEx(Tmodel), trans);
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

        public Maticsoft.Model.SNS.GroupTopicReply DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.GroupTopicReply reply = new Maticsoft.Model.SNS.GroupTopicReply();
            if (row != null)
            {
                if ((row["ReplyID"] != null) && (row["ReplyID"].ToString() != ""))
                {
                    reply.ReplyID = int.Parse(row["ReplyID"].ToString());
                }
                if ((row["GroupID"] != null) && (row["GroupID"].ToString() != ""))
                {
                    reply.GroupID = int.Parse(row["GroupID"].ToString());
                }
                if ((row["ReplyType"] != null) && (row["ReplyType"].ToString() != ""))
                {
                    reply.ReplyType = int.Parse(row["ReplyType"].ToString());
                }
                if (row["ReplyNickName"] != null)
                {
                    reply.ReplyNickName = row["ReplyNickName"].ToString();
                }
                if ((row["ReplyUserID"] != null) && (row["ReplyUserID"].ToString() != ""))
                {
                    reply.ReplyUserID = int.Parse(row["ReplyUserID"].ToString());
                }
                if ((row["OriginalID"] != null) && (row["OriginalID"].ToString() != ""))
                {
                    reply.OriginalID = int.Parse(row["OriginalID"].ToString());
                }
                if (row["OrginalDes"] != null)
                {
                    reply.OrginalDes = row["OrginalDes"].ToString();
                }
                if ((row["OrginalUserID"] != null) && (row["OrginalUserID"].ToString() != ""))
                {
                    reply.OrginalUserID = int.Parse(row["OrginalUserID"].ToString());
                }
                if (row["OrgianlNickName"] != null)
                {
                    reply.OrgianlNickName = row["OrgianlNickName"].ToString();
                }
                if ((row["TopicID"] != null) && (row["TopicID"].ToString() != ""))
                {
                    reply.TopicID = int.Parse(row["TopicID"].ToString());
                }
                if (row["Description"] != null)
                {
                    reply.Description = row["Description"].ToString();
                }
                if ((row["HasReferUsers"] != null) && (row["HasReferUsers"].ToString() != ""))
                {
                    if ((row["HasReferUsers"].ToString() == "1") || (row["HasReferUsers"].ToString().ToLower() == "true"))
                    {
                        reply.HasReferUsers = true;
                    }
                    else
                    {
                        reply.HasReferUsers = false;
                    }
                }
                if (row["PhotoUrl"] != null)
                {
                    reply.PhotoUrl = row["PhotoUrl"].ToString();
                }
                if ((row["TargetId"] != null) && (row["TargetId"].ToString() != ""))
                {
                    reply.TargetId = new int?(int.Parse(row["TargetId"].ToString()));
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    reply.Type = new int?(int.Parse(row["Type"].ToString()));
                }
                if (row["ProductUrl"] != null)
                {
                    reply.ProductUrl = row["ProductUrl"].ToString();
                }
                if (row["ProductName"] != null)
                {
                    reply.ProductName = row["ProductName"].ToString();
                }
                if (row["ReplyExUrl"] != null)
                {
                    reply.ReplyExUrl = row["ReplyExUrl"].ToString();
                }
                if (row["ProductLinkUrl"] != null)
                {
                    reply.ProductLinkUrl = row["ProductLinkUrl"].ToString();
                }
                if ((row["FavCount"] != null) && (row["FavCount"].ToString() != ""))
                {
                    reply.FavCount = int.Parse(row["FavCount"].ToString());
                }
                if ((row["Price"] != null) && (row["Price"].ToString() != ""))
                {
                    reply.Price = new decimal?(decimal.Parse(row["Price"].ToString()));
                }
                if (row["UserIP"] != null)
                {
                    reply.UserIP = row["UserIP"].ToString();
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    reply.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    reply.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
            }
            return reply;
        }

        public bool Delete(int ReplyID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_GroupTopicReply ");
            builder.Append(" where ReplyID=@ReplyID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReplyID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ReplyID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteEx(int ReplyId)
        {
            Maticsoft.Model.SNS.GroupTopicReply model = this.GetModel(ReplyId);
            if (model == null)
            {
                return false;
            }
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Groups set ");
            builder.Append("TopicReplyCount=TopicReplyCount-1");
            builder.Append(" where GroupID=@GroupID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4) };
            para[0].Value = model.GroupID;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update SNS_GroupTopics set ");
            builder2.Append("ReplyCount=ReplyCount-1");
            builder2.Append(" where TopicID=@TopicID");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@TopicID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = model.TopicID;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("delete from SNS_GroupTopicReply ");
            builder3.Append(" where ReplyID=@ReplyID");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@ReplyID", SqlDbType.Int, 4) };
            parameterArray3[0].Value = model.ReplyID;
            CommandInfo info3 = new CommandInfo(builder3.ToString(), parameterArray3);
            cmdList.Add(info3);
            StringBuilder builder4 = new StringBuilder();
            builder4.Append("update SNS_GroupTopicReply set ");
            builder4.Append("OrginalDes=@OrginalDes,");
            builder4.Append("OrginalUserID=@OrginalUserID,");
            builder4.Append("OrgianlNickName=@OrgianlNickName,");
            builder4.Append("PhotoUrl=@PhotoUrl,");
            builder4.Append("TargetId=@TargetId,");
            builder4.Append("ProductUrl=@ProductUrl,");
            builder4.Append("ProductName=@ProductName,");
            builder4.Append("ReplyExUrl=@ReplyExUrl,");
            builder4.Append("ProductLinkUrl=@ProductLinkUrl");
            builder4.Append(" where OriginalID=@OriginalID");
            SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@OriginalID", SqlDbType.Int, 4), new SqlParameter("@OrginalDes", SqlDbType.NVarChar), new SqlParameter("@OrginalUserID", SqlDbType.Int, 4), new SqlParameter("@OrgianlNickName", SqlDbType.NVarChar, 50), new SqlParameter("@PhotoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@ProductUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@ReplyExUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar, 500) };
            parameterArray4[0].Value = model.ReplyID;
            parameterArray4[1].Value = "";
            parameterArray4[2].Value = 0;
            parameterArray4[3].Value = "";
            parameterArray4[4].Value = "";
            parameterArray4[5].Value = 0;
            parameterArray4[6].Value = "";
            parameterArray4[7].Value = "";
            parameterArray4[8].Value = "";
            parameterArray4[9].Value = "";
            CommandInfo info4 = new CommandInfo(builder4.ToString(), parameterArray4);
            cmdList.Add(info4);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteList(string ReplyIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_GroupTopicReply ");
            builder.Append(" where ReplyID in (" + ReplyIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool DeleteListEx(string ReplyIDlist)
        {
            string[] strArray = ReplyIDlist.Split(new char[] { ',' });
            if (strArray.Length > 0)
            {
                foreach (string str in strArray)
                {
                    int replyId = Globals.SafeInt(str, 0);
                    if (!this.DeleteEx(replyId))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int ForwardReply(Maticsoft.Model.SNS.GroupTopicReply TModel)
        {
            int num;
            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    object obj2 = "0";
                    obj2 = DbHelperSQL.GetSingle4Trans(this.GenerateForwardReplyInfo(TModel), trans);
                    DbHelperSQL.GetSingle4Trans(this.GenerateUpdateGroupInfo(TModel), trans);
                    DbHelperSQL.GetSingle4Trans(this.GenerateUpdateUserExInfo(TModel), trans);
                    DbHelperSQL.GetSingle4Trans(this.GenerateUpdateTopicInfo(TModel), trans);
                    trans.Commit();
                    num = Globals.SafeInt((obj2 != null) ? obj2.ToString() : "", 0);
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

        private CommandInfo GenerateForwardReplyInfo(Maticsoft.Model.SNS.GroupTopicReply model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_GroupTopicReply(");
            builder.Append("GroupID,ReplyType,ReplyNickName,ReplyUserID,OriginalID,OrginalDes,OrginalUserID,OrgianlNickName,TopicID,Description,HasReferUsers,PhotoUrl,TargetId,Type,ProductUrl,ProductName,ReplyExUrl,ProductLinkUrl,FavCount,Price,UserIP,Status,CreatedDate)");
            builder.Append(" values (");
            builder.Append("@GroupID,@ReplyType,@ReplyNickName,@ReplyUserID,@OriginalID,@OrginalDes,@OrginalUserID,@OrgianlNickName,@TopicID,@Description,@HasReferUsers,@PhotoUrl,@TargetId,@Type,@ProductUrl,@ProductName,@ReplyExUrl,@ProductLinkUrl,@FavCount,@Price,@UserIP,@Status,@CreatedDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@ReplyType", SqlDbType.Int, 4), new SqlParameter("@ReplyNickName", SqlDbType.NVarChar, 50), new SqlParameter("@ReplyUserID", SqlDbType.Int, 4), new SqlParameter("@OriginalID", SqlDbType.Int, 4), new SqlParameter("@OrginalDes", SqlDbType.NVarChar), new SqlParameter("@OrginalUserID", SqlDbType.Int, 4), new SqlParameter("@OrgianlNickName", SqlDbType.NVarChar, 50), new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@HasReferUsers", SqlDbType.Bit, 1), new SqlParameter("@PhotoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@ProductUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), 
                new SqlParameter("@ReplyExUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar, 500), new SqlParameter("@FavCount", SqlDbType.Int, 4), new SqlParameter("@Price", SqlDbType.Decimal, 9), new SqlParameter("@UserIP", SqlDbType.NVarChar, 15), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime)
             };
            para[0].Value = model.GroupID;
            para[1].Value = model.ReplyType;
            para[2].Value = model.ReplyNickName;
            para[3].Value = model.ReplyUserID;
            para[4].Value = model.OriginalID;
            para[5].Value = model.OrginalDes;
            para[6].Value = model.OrginalUserID;
            para[7].Value = model.OrgianlNickName;
            para[8].Value = model.TopicID;
            para[9].Value = model.Description;
            para[10].Value = model.HasReferUsers;
            para[11].Value = model.PhotoUrl;
            para[12].Value = model.TargetId;
            para[13].Value = model.Type;
            para[14].Value = model.ProductUrl;
            para[15].Value = model.ProductName;
            para[0x10].Value = model.ReplyExUrl;
            para[0x11].Value = model.ProductLinkUrl;
            para[0x12].Value = model.FavCount;
            para[0x13].Value = model.Price;
            para[20].Value = model.UserIP;
            para[0x15].Value = model.Status;
            para[0x16].Value = model.CreatedDate;
            new CommandInfo(builder.ToString(), para);
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        private CommandInfo GenerateImageInfo(Maticsoft.Model.SNS.GroupTopicReply Tmodel, Maticsoft.Model.SNS.Products PModel)
        {
            if ((PModel != null) && (PModel.ProductID > 0L))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("insert into SNS_Products(");
                builder.Append("ProductName,Price,ProductSourceID,CategoryID,ProductUrl,CreateUserID,CreatedNickName,ThumbImageUrl,NormalImageUrl,Status,ShareDescription,CreatedDate,Tags,IsRecomend)");
                builder.Append(" values (");
                builder.Append("@ProductName,@Price,@ProductSourceID,@CategoryID,@ProductUrl,@CreateUserID,@CreatedNickName,@ThumbImageUrl,@NormalImageUrl,@Status,@ShareDescription,@CreatedDate,@Tags,@IsRecomend)");
                builder.Append(";select @@IDENTITY");
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), new SqlParameter("@Price", SqlDbType.Decimal, 9), new SqlParameter("@ProductSourceID", SqlDbType.Int, 4), new SqlParameter("@CategoryID", SqlDbType.Int, 4), new SqlParameter("@ProductUrl", SqlDbType.NVarChar, 200), new SqlParameter("@CreateUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ShareDescription", SqlDbType.NVarChar, 200), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Tags", SqlDbType.NVarChar, 400), new SqlParameter("@IsRecomend", SqlDbType.NVarChar, 400) };
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
                Tmodel.PhotoUrl = PModel.ThumbImageUrl;
                Tmodel.ProductName = PModel.ProductName;
                Tmodel.ProductLinkUrl = PModel.ProductUrl;
                Tmodel.Price = PModel.Price;
                new CommandInfo(builder.ToString(), parameterArray);
                return new CommandInfo(builder.ToString(), parameterArray, EffentNextType.ExcuteEffectRows);
            }
            if (string.IsNullOrEmpty(Tmodel.PhotoUrl))
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
            string[] strArray = string.IsNullOrEmpty(Tmodel.PhotoUrl) ? null : Tmodel.PhotoUrl.Split(new char[] { '|' });
            if ((strArray != null) && (strArray.Length >= 2))
            {
                para[0].Value = strArray[0];
                para[7].Value = strArray[1];
                Tmodel.PhotoUrl = strArray[0];
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
            para[3].Value = Tmodel.ReplyUserID;
            para[4].Value = Tmodel.ReplyNickName;
            para[5].Value = Tmodel.CreatedDate;
            para[6].Value = 3;
            return new CommandInfo(builder2.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        public CommandInfo GenerateTopicReplyInfo(Maticsoft.Model.SNS.GroupTopicReply model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_GroupTopicReply(");
            builder.Append("GroupID,ReplyType,ReplyNickName,ReplyUserID,OriginalID,OrginalDes,OrginalUserID,OrgianlNickName,TopicID,Description,HasReferUsers,PhotoUrl,TargetId,Type,ProductUrl,ProductName,ReplyExUrl,ProductLinkUrl,FavCount,Price,UserIP,Status,CreatedDate)");
            builder.Append(" values (");
            builder.Append("@GroupID,@ReplyType,@ReplyNickName,@ReplyUserID,@OriginalID,@OrginalDes,@OrginalUserID,@OrgianlNickName,@TopicID,@Description,@HasReferUsers,@PhotoUrl,@TargetId,@Type,@ProductUrl,@ProductName,@ReplyExUrl,@ProductLinkUrl,@FavCount,@Price,@UserIP,@Status,@CreatedDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@ReplyType", SqlDbType.Int, 4), new SqlParameter("@ReplyNickName", SqlDbType.NVarChar, 50), new SqlParameter("@ReplyUserID", SqlDbType.Int, 4), new SqlParameter("@OriginalID", SqlDbType.Int, 4), new SqlParameter("@OrginalDes", SqlDbType.NVarChar), new SqlParameter("@OrginalUserID", SqlDbType.Int, 4), new SqlParameter("@OrgianlNickName", SqlDbType.NVarChar, 50), new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@HasReferUsers", SqlDbType.Bit, 1), new SqlParameter("@PhotoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@ProductUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), 
                new SqlParameter("@ReplyExUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar, 500), new SqlParameter("@FavCount", SqlDbType.Int, 4), new SqlParameter("@Price", SqlDbType.Decimal, 9), new SqlParameter("@UserIP", SqlDbType.NVarChar, 15), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime)
             };
            para[0].Value = model.GroupID;
            para[1].Value = model.ReplyType;
            para[2].Value = model.ReplyNickName;
            para[3].Value = model.ReplyUserID;
            para[4].Value = 0;
            para[5].Value = model.OrginalDes;
            para[6].Value = model.OrginalUserID;
            para[7].Value = model.OrgianlNickName;
            para[8].Value = model.TopicID;
            para[9].Value = model.Description;
            para[10].Value = model.HasReferUsers;
            para[11].Value = model.PhotoUrl;
            para[12].Value = model.TargetId;
            para[13].Value = model.Type;
            para[14].Value = model.ProductUrl;
            para[15].Value = model.ProductName;
            para[0x10].Value = model.ReplyExUrl;
            para[0x11].Value = model.ProductLinkUrl;
            para[0x12].Value = 0;
            para[0x13].Value = model.Price;
            para[20].Value = model.UserIP;
            para[0x15].Value = model.Status;
            para[0x16].Value = model.CreatedDate;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        public CommandInfo GenerateUpdateGroupEx(int GroupID)
        {
            CommandInfo info = new CommandInfo();
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Groups set TopicReplyCount=TopicReplyCount+1 WHERE GroupID=@GroupID ");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4) };
            para[0].Value = GroupID;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        private CommandInfo GenerateUpdateGroupInfo(Maticsoft.Model.SNS.GroupTopicReply model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Groups set TopicCount=TopicCount+1,TopicReplyCount=TopicReplyCount+1 ");
            builder.Append(" where GroupID=@GroupID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4) };
            para[0].Value = model.GroupID;
            new CommandInfo(builder.ToString(), para);
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        public CommandInfo GenerateUpdateTopicEx(Maticsoft.Model.SNS.GroupTopicReply TopicReply)
        {
            CommandInfo info = new CommandInfo();
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_GroupTopics set ReplyCount=ReplyCount+1,LastReplyNickName=@LastReplyNickName ,LastReplyUserId=@LastReplyUserId,LastPostTime=@LastPostTime WHERE TopicID=@TopicID ");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@LastReplyNickName", SqlDbType.NVarChar, 200), new SqlParameter("@LastReplyUserId", SqlDbType.Int, 4), new SqlParameter("@LastPostTime", SqlDbType.DateTime), new SqlParameter("@TopicID", SqlDbType.Int, 4) };
            para[0].Value = TopicReply.ReplyNickName;
            para[1].Value = TopicReply.ReplyUserID;
            para[2].Value = DateTime.Now;
            para[3].Value = TopicReply.TopicID;
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        private CommandInfo GenerateUpdateTopicInfo(Maticsoft.Model.SNS.GroupTopicReply model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_GroupTopics set ReplyCount=ReplyCount+1");
            builder.Append(" where TopicID=@TopicID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@TopicID", SqlDbType.Int, 4) };
            para[0].Value = model.TopicID;
            new CommandInfo(builder.ToString(), para);
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
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update Accounts_UsersExp set ShareCount=ShareCount+1,ProductsCount=ProductsCount+1,TopicCount=TopicCount+1 WHERE UserID=@UserID ");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            para[0].Value = UserId;
            return new CommandInfo(builder2.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        private CommandInfo GenerateUpdateUserExInfo(Maticsoft.Model.SNS.GroupTopicReply model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_UsersExp set TopicCount=TopicCount+1 ");
            builder.Append(" where UserID=@UserID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            para[0].Value = model.ReplyUserID;
            new CommandInfo(builder.ToString(), para);
            return new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ReplyID,GroupID,ReplyType,ReplyNickName,ReplyUserID,OriginalID,OrginalDes,OrginalUserID,OrgianlNickName,TopicID,Description,HasReferUsers,PhotoUrl,TargetId,Type,ProductUrl,ProductName,ReplyExUrl,ProductLinkUrl,FavCount,Price,UserIP,Status,CreatedDate ");
            builder.Append(" FROM SNS_GroupTopicReply ");
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
            builder.Append(" ReplyID,GroupID,ReplyType,ReplyNickName,ReplyUserID,OriginalID,OrginalDes,OrginalUserID,OrgianlNickName,TopicID,Description,HasReferUsers,PhotoUrl,TargetId,Type,ProductUrl,ProductName,ReplyExUrl,ProductLinkUrl,FavCount,Price,UserIP,Status,CreatedDate ");
            builder.Append(" FROM SNS_GroupTopicReply ");
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
                builder.Append("order by T.ReplyID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_GroupTopicReply T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListEx(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM  (SELECT tp.*,gp.GroupName,gp.Title FROM SNS_GroupTopicReply tp LEFT JOIN SNS_GroupTopics gp ON tp.TopicID=gp.TopicID) temp");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.GroupTopicReply GetModel(int ReplyID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ReplyID,GroupID,ReplyType,ReplyNickName,ReplyUserID,OriginalID,OrginalDes,OrginalUserID,OrgianlNickName,TopicID,Description,HasReferUsers,PhotoUrl,TargetId,Type,ProductUrl,ProductName,ReplyExUrl,ProductLinkUrl,FavCount,Price,UserIP,Status,CreatedDate from SNS_GroupTopicReply ");
            builder.Append(" where ReplyID=@ReplyID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReplyID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ReplyID;
            new Maticsoft.Model.SNS.GroupTopicReply();
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
            builder.Append("select count(1) FROM SNS_GroupTopicReply ");
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

        public bool Update(Maticsoft.Model.SNS.GroupTopicReply model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_GroupTopicReply set ");
            builder.Append("GroupID=@GroupID,");
            builder.Append("ReplyType=@ReplyType,");
            builder.Append("ReplyNickName=@ReplyNickName,");
            builder.Append("ReplyUserID=@ReplyUserID,");
            builder.Append("OriginalID=@OriginalID,");
            builder.Append("OrginalDes=@OrginalDes,");
            builder.Append("OrginalUserID=@OrginalUserID,");
            builder.Append("OrgianlNickName=@OrgianlNickName,");
            builder.Append("TopicID=@TopicID,");
            builder.Append("Description=@Description,");
            builder.Append("HasReferUsers=@HasReferUsers,");
            builder.Append("PhotoUrl=@PhotoUrl,");
            builder.Append("TargetId=@TargetId,");
            builder.Append("Type=@Type,");
            builder.Append("ProductUrl=@ProductUrl,");
            builder.Append("ProductName=@ProductName,");
            builder.Append("ReplyExUrl=@ReplyExUrl,");
            builder.Append("ProductLinkUrl=@ProductLinkUrl,");
            builder.Append("FavCount=@FavCount,");
            builder.Append("Price=@Price,");
            builder.Append("UserIP=@UserIP,");
            builder.Append("Status=@Status,");
            builder.Append("CreatedDate=@CreatedDate");
            builder.Append(" where ReplyID=@ReplyID");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@ReplyType", SqlDbType.Int, 4), new SqlParameter("@ReplyNickName", SqlDbType.NVarChar, 50), new SqlParameter("@ReplyUserID", SqlDbType.Int, 4), new SqlParameter("@OriginalID", SqlDbType.Int, 4), new SqlParameter("@OrginalDes", SqlDbType.NVarChar), new SqlParameter("@OrginalUserID", SqlDbType.Int, 4), new SqlParameter("@OrgianlNickName", SqlDbType.NVarChar, 50), new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@HasReferUsers", SqlDbType.Bit, 1), new SqlParameter("@PhotoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@ProductUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ProductName", SqlDbType.NVarChar, 200), 
                new SqlParameter("@ReplyExUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ProductLinkUrl", SqlDbType.NVarChar, 500), new SqlParameter("@FavCount", SqlDbType.Int, 4), new SqlParameter("@Price", SqlDbType.Decimal, 9), new SqlParameter("@UserIP", SqlDbType.NVarChar, 15), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@ReplyID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.GroupID;
            cmdParms[1].Value = model.ReplyType;
            cmdParms[2].Value = model.ReplyNickName;
            cmdParms[3].Value = model.ReplyUserID;
            cmdParms[4].Value = model.OriginalID;
            cmdParms[5].Value = model.OrginalDes;
            cmdParms[6].Value = model.OrginalUserID;
            cmdParms[7].Value = model.OrgianlNickName;
            cmdParms[8].Value = model.TopicID;
            cmdParms[9].Value = model.Description;
            cmdParms[10].Value = model.HasReferUsers;
            cmdParms[11].Value = model.PhotoUrl;
            cmdParms[12].Value = model.TargetId;
            cmdParms[13].Value = model.Type;
            cmdParms[14].Value = model.ProductUrl;
            cmdParms[15].Value = model.ProductName;
            cmdParms[0x10].Value = model.ReplyExUrl;
            cmdParms[0x11].Value = model.ProductLinkUrl;
            cmdParms[0x12].Value = model.FavCount;
            cmdParms[0x13].Value = model.Price;
            cmdParms[20].Value = model.UserIP;
            cmdParms[0x15].Value = model.Status;
            cmdParms[0x16].Value = model.CreatedDate;
            cmdParms[0x17].Value = model.ReplyID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStatusList(string IdsStr, EnumHelper.TopicStatus status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_GroupTopicReply set Status=" + ((int) status) + " ");
            builder.Append(" where ReplyID in (" + IdsStr + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}

