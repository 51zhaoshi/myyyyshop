namespace Maticsoft.ViewModel.SNS
{
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Webdiyer.WebControls.Mvc;

    public class VodeoList
    {
        public int CommentCount { get; set; }

        public int CommentPageSize { get; set; }

        public UsersExpModel UserModel { get; set; }

        public List<Posts> VodeoListWaterfall { get; set; }

        public PagedList<Posts> VodeoPagedList { get; set; }
    }
}

