namespace Maticsoft.ViewModel.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Webdiyer.WebControls.Mvc;

    public class GroupInfo
    {
        public List<GroupUsers> AdminUserList { get; set; }

        public Groups Group { get; set; }

        public List<GroupTopics> NewTopicList { get; set; }

        public List<GroupUsers> NewUserList { get; set; }

        public PagedList<GroupTopics> TopicList { get; set; }

        public PagedList<GroupUsers> UserList { get; set; }
    }
}

