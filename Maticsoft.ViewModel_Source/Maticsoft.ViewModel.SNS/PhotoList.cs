namespace Maticsoft.ViewModel.SNS
{
    using Maticsoft.Model.Members;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Webdiyer.WebControls.Mvc;

    public class PhotoList
    {
        private List<PostContent>[] _photoList4FourCol;
        private List<PostContent>[] _photoList4ThreeCol;
        public List<UserAlbums> AlbumsList;

        public UserAlbums AlbumModel { get; set; }

        public Categories CategoryInfo { get; set; }

        public int CommentCount { get; set; }

        public int CommentPageSize { get; set; }

        public List<Categories> PhotoCategory { get; set; }

        public List<PostContent>[] PhotoList4FourCol
        {
            get
            {
                int index;
                if (this._photoList4FourCol != null)
                {
                    return this._photoList4FourCol;
                }
                List<PostContent>[] list = new List<PostContent>[] { new List<PostContent>(), new List<PostContent>(), new List<PostContent>(), new List<PostContent>() };
                if (this.PhotoPagedList != null)
                {
                    index = 0;
                    this.PhotoPagedList.ForEach(delegate (PostContent image) {
                        if (index == 4)
                        {
                            index = 0;
                        }
                        list[index++].Add(image);
                    });
                }
                return list;
            }
            set
            {
                this._photoList4FourCol = value;
            }
        }

        public List<PostContent>[] PhotoList4ThreeCol
        {
            get
            {
                int index;
                if (this._photoList4ThreeCol != null)
                {
                    return this._photoList4ThreeCol;
                }
                List<PostContent>[] list = new List<PostContent>[] { new List<PostContent>(), new List<PostContent>(), new List<PostContent>() };
                if (this.PhotoPagedList != null)
                {
                    index = 0;
                    this.PhotoPagedList.ForEach(delegate (PostContent image) {
                        if (index == 3)
                        {
                            index = 0;
                        }
                        list[index++].Add(image);
                    });
                }
                return list;
            }
            set
            {
                this._photoList4ThreeCol = value;
            }
        }

        public List<PostContent> PhotoListWaterfall { get; set; }

        public PagedList<PostContent> PhotoPagedList { get; set; }

        public UsersExpModel UserModel { get; set; }

        public List<ZuiInPhoto> ZuiInList { get; set; }
    }
}

