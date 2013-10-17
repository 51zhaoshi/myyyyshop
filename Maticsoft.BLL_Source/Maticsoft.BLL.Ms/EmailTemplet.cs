namespace Maticsoft.BLL.Ms
{
    using Maticsoft.BLL;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Common.DEncrypt;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model;
    using Maticsoft.Model.Members;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web;

    public class EmailTemplet
    {
        private Maticsoft.BLL.MailConfig config = new Maticsoft.BLL.MailConfig();
        private readonly IEmailTemplet dal = DAMs.CreateEmailTemplet();

        public int Add(Maticsoft.Model.Ms.EmailTemplet model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Ms.EmailTemplet> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Ms.EmailTemplet> list = new List<Maticsoft.Model.Ms.EmailTemplet>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Ms.EmailTemplet item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int TempletId)
        {
            return this.dal.Delete(TempletId);
        }

        public bool DeleteList(string TempletIdlist)
        {
            return this.dal.DeleteList(TempletIdlist);
        }

        public bool Exists(int TempletId)
        {
            return this.dal.Exists(TempletId);
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

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Ms.EmailTemplet GetModel(int TempletId)
        {
            return this.dal.GetModel(TempletId);
        }

        public Maticsoft.Model.Ms.EmailTemplet GetModelByCache(int TempletId)
        {
            string cacheKey = "EmailTempletModel-" + TempletId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(TempletId);
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
            return (Maticsoft.Model.Ms.EmailTemplet) cache;
        }

        public List<Maticsoft.Model.Ms.EmailTemplet> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public string ReplaceTag(string body, params string[][] values)
        {
            if ((values != null) && (values.Length >= 1))
            {
                foreach (string[] strArray in values)
                {
                    if (strArray.Length == 2)
                    {
                        body = body.Replace(strArray[0], Globals.HtmlEncode(strArray[1]));
                    }
                }
            }
            return body;
        }

        public bool SendFeedbackEmail(Maticsoft.Model.Members.Feedback FeedBackModel)
        {
            int templetId = Globals.SafeInt(ConfigSystem.GetValueByCache("EmailTemplet_Feedback"), 0);
            Maticsoft.Model.Ms.EmailTemplet modelByCache = this.GetModelByCache(templetId);
            if ((modelByCache != null) && (FeedBackModel != null))
            {
                string body = this.ReplaceTag(modelByCache.EmailBody, new string[][] { new string[] { "{Domain}", HttpContext.Current.Request.Url.Authority }, new string[] { "{CreatedDate}", DateTime.Now.ToString("yyyy-MM-dd") }, new string[] { "{Question}", FeedBackModel.Description }, new string[] { "{UserName}", FeedBackModel.UserName }, new string[] { "{ReplyResult}", FeedBackModel.Result }, new string[] { "{QuestionDate}", FeedBackModel.CreatedDate.ToString("yyyy-MM-dd") } });
                try
                {
                    MailSender.Send(FeedBackModel.UserEmail, modelByCache.EmailSubject, body);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool SendFindPwdEmail(string username, string EmailUrl)
        {
            string str = Guid.NewGuid().ToString().Replace("-", "");
            Maticsoft.BLL.SysManage.VerifyMail mail = new Maticsoft.BLL.SysManage.VerifyMail();
            Maticsoft.Model.SysManage.VerifyMail model = new Maticsoft.Model.SysManage.VerifyMail {
                UserName = username,
                KeyValue = str,
                CreatedDate = DateTime.Now,
                Status = 0,
                ValidityType = 1
            };
            mail.Add(model);
            int templetId = Globals.SafeInt(ConfigSystem.GetValueByCache("EmailTemplet_FindPwd"), 1);
            Maticsoft.Model.Ms.EmailTemplet modelByCache = this.GetModelByCache(templetId);
            if (modelByCache != null)
            {
                string body = this.ReplaceTag(modelByCache.EmailBody, new string[][] { new string[] { "{Domain}", HttpContext.Current.Request.Url.Authority }, new string[] { "{CreatedDate}", DateTime.Now.ToString("yyyy-MM-dd") }, new string[] { "{SecretKey}", str }, new string[] { "{UserName}", username } });
                try
                {
                    Maticsoft.Model.MailConfig config = this.config.GetModel();
                    if ((model != null) && !string.IsNullOrWhiteSpace(config.Mailaddress))
                    {
                        MailSender.Send(config.SMTPServer, config.Username, DESEncrypt.Decrypt(config.Password), config.Mailaddress, EmailUrl, "", "", modelByCache.EmailSubject, body, true, Encoding.UTF8, true, config.SMTPSSL, null);
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool SendGuestBookEmail(Maticsoft.Model.Members.Guestbook GuestBookModel)
        {
            Maticsoft.BLL.Members.Guestbook guestbook = new Maticsoft.BLL.Members.Guestbook();
            int templetId = Globals.SafeInt(ConfigSystem.GetValueByCache("EmailTemplet_GuestBook"), 0);
            Maticsoft.Model.Ms.EmailTemplet modelByCache = this.GetModelByCache(templetId);
            if (((modelByCache != null) && (GuestBookModel != null)) && (GuestBookModel.ParentID.HasValue && (GuestBookModel.ParentID.Value > 0)))
            {
                Maticsoft.Model.Members.Guestbook model = guestbook.GetModel(GuestBookModel.ParentID.Value);
                if (model != null)
                {
                    string body = this.ReplaceTag(modelByCache.EmailBody, new string[][] { new string[] { "{Domain}", HttpContext.Current.Request.Url.Authority }, new string[] { "{CreatedDate}", DateTime.Now.ToString("yyyy-MM-dd") }, new string[] { "{Description}", model.Description }, new string[] { "{UserName}", model.CreateNickName }, new string[] { "{ReplyResult}", GuestBookModel.Description }, new string[] { "{QuestionDate}", model.CreatedDate.ToString("yyyy-MM-dd") } });
                    try
                    {
                        MailSender.Send(GuestBookModel.CreatorEmail, modelByCache.EmailSubject, body);
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public bool SendInqueryEmail(int id, string EmailUrl)
        {
            int templetId = Globals.SafeInt(ConfigSystem.GetValueByCache("EmailTemplet_Inquery"), 0);
            this.GetModelByCache(templetId);
            return false;
        }

        public bool SendRegisterEmail(string username, string EmailUrl)
        {
            string str = Guid.NewGuid().ToString().Replace("-", "");
            Maticsoft.BLL.SysManage.VerifyMail mail = new Maticsoft.BLL.SysManage.VerifyMail();
            Maticsoft.Model.SysManage.VerifyMail model = new Maticsoft.Model.SysManage.VerifyMail {
                UserName = username,
                KeyValue = str,
                CreatedDate = DateTime.Now,
                Status = 0,
                ValidityType = 0
            };
            mail.Add(model);
            int templetId = Globals.SafeInt(ConfigSystem.GetValueByCache("EmailTemplet_Register"), 0);
            Maticsoft.Model.Ms.EmailTemplet modelByCache = this.GetModelByCache(templetId);
            if (modelByCache != null)
            {
                string body = this.ReplaceTag(modelByCache.EmailBody, new string[][] { new string[] { "{Domain}", HttpContext.Current.Request.Url.Authority }, new string[] { "{CreatedDate}", DateTime.Now.ToString("yyyy-MM-dd") }, new string[] { "{SecretKey}", str }, new string[] { "{UserName}", username } });
                try
                {
                    Maticsoft.Model.MailConfig config = this.config.GetModel();
                    if ((model != null) && !string.IsNullOrWhiteSpace(config.Mailaddress))
                    {
                        MailSender.Send(config.SMTPServer, config.Username, DESEncrypt.Decrypt(config.Password), config.Mailaddress, EmailUrl, "", "", modelByCache.EmailSubject, body, true, Encoding.UTF8, true, config.SMTPSSL, null);
                        return true;
                    }
                    return true;
                }
                catch (Exception exception)
                {
                    Maticsoft.Model.SysManage.ErrorLog log = new Maticsoft.Model.SysManage.ErrorLog {
                        Loginfo = "邮件发送失败！错误信息为：" + exception.StackTrace,
                        OPTime = DateTime.Now,
                        Url = "",
                        StackTrace = exception.StackTrace
                    };
                    Maticsoft.BLL.SysManage.ErrorLog.Add(log);
                    return false;
                }
            }
            return false;
        }

        public bool Update(Maticsoft.Model.Ms.EmailTemplet model)
        {
            return this.dal.Update(model);
        }
    }
}

