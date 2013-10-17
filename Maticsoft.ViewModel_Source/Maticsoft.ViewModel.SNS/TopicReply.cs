namespace Maticsoft.ViewModel.SNS
{
    using Maticsoft.Model.Members;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Webdiyer.WebControls.Mvc;

    public class TopicReply
    {
        public Groups Group { get; set; }

        public List<GroupTopics> HotTopic { get; set; }

        public GroupTopics Topic { get; set; }

        public UsersExpModel TopicPostUser { get; set; }

        public PagedList<GroupTopicReply> TopicsReply { get; set; }

        public List<Groups> UserJoinGroups { get; set; }

        public List<GroupTopics> UserPostTopics { get; set; }
    }
}

