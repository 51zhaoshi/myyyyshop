namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.Poll;
    using System;

    public sealed class DAPoll : DataAccessBase
    {
        public static IForms CreateForms()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Poll.Forms";
            return (IForms) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IOptions CreateOptions()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Poll.Options";
            return (IOptions) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IPollUsers CreatePollUsers()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Poll.PollUsers";
            return (IPollUsers) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IReply CreateReply()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Poll.Reply";
            return (IReply) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ITopics CreateTopics()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Poll.Topics";
            return (ITopics) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IUserPoll CreateUserPoll()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Poll.UserPoll";
            return (IUserPoll) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}

