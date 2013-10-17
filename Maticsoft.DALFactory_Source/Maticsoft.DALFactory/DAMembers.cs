namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL;
    using Maticsoft.IDAL.Members;
    using Maticsoft.IDAL.Ms;
    using System;

    public class DAMembers : DataAccessBase
    {
        public static IEntryForm CreateEntryForm()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Ms.EntryForm";
            return (IEntryForm) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IFeedback CreateFeedback()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Members.Feedback";
            return (IFeedback) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IFeedbackType CreateFeedbackType()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Members.FeedbackType";
            return (IFeedbackType) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IGuestbook CreateGuestbook()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Guestbook.Guestbook";
            return (IGuestbook) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IMailConfig CreateMailConfig()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".MailConfig";
            return (IMailConfig) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IPointsDetail CreatePointsDetail()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Members.PointsDetail";
            return (IPointsDetail) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IPointsLimit CreatePointsLimit()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Members.PointsLimit";
            return (IPointsLimit) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IPointsRule CreatePointsRule()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Members.PointsRule";
            return (IPointsRule) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISiteMessage CreateSiteMessage()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Members.SiteMessage";
            return (ISiteMessage) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISiteMessageLog CreateSiteMessageLog()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Members.SiteMessageLog";
            return (ISiteMessageLog) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IUserBind CreateUserBind()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Members.UserBind";
            return (IUserBind) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IUserInvite CreateUserInvite()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Members.UserInvite";
            return (IUserInvite) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IUserRank CreateUserRank()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Members.UserRank";
            return (IUserRank) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IUsers CreateUsers()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Members.Users";
            return (IUsers) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IUsersApprove CreateUsersApprove()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Members.UsersApprove";
            return (IUsersApprove) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IUsersExp CreateUsersExp()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Members.UsersExp";
            return (IUsersExp) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}

