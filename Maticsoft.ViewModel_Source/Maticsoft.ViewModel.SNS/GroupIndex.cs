namespace Maticsoft.ViewModel.SNS
{
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class GroupIndex
    {
        public List<Groups> HotGroupList { get; set; }

        public List<Groups> MyGroupList { get; set; }

        public List<GroupTopics> NewGroupTopicList { get; set; }

        public List<Groups> ProGroupList { get; set; }

        public List<Groups> TopGroupList { get; set; }

        public UsersExpModel UserModel { get; set; }
    }
}

