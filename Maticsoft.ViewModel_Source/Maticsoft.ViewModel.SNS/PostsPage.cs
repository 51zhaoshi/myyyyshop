namespace Maticsoft.ViewModel.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PostsPage
    {
        public List<AlbumType> AlbumTypeList = new List<AlbumType>();
        public List<Maticsoft.ViewModel.SNS.Posts> DataList = new List<Maticsoft.ViewModel.SNS.Posts>();

        public int DataCount { get; set; }

        public string NickName { get; set; }

        public int PageSize { get; set; }

        public PostsSet Setting { get; set; }

        public string Type { get; set; }

        public int UserID { get; set; }
    }
}

