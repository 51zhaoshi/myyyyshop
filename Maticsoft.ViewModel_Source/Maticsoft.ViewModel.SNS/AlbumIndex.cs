namespace Maticsoft.ViewModel.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;

    public class AlbumIndex : UserAlbums
    {
        private List<string> _topimages;

        public AlbumIndex(UserAlbums ua)
        {
            base.AlbumID = ua.AlbumID;
            base.AlbumName = ua.AlbumName;
            base.ChannelSequence = ua.ChannelSequence;
            base.CoverPhotoUrl = ua.CoverPhotoUrl;
            base.CoverTargetID = ua.CoverTargetID;
            base.CoverTargetType = ua.CoverTargetType;
            base.CreatedDate = ua.CreatedDate;
            base.CreatedNickName = ua.CreatedNickName;
            base.CreatedUserID = ua.CreatedUserID;
            base.Description = ua.Description;
            base.FavouriteCount = ua.FavouriteCount;
            base.IsRecommend = ua.IsRecommend;
            base.LastUpdatedDate = ua.LastUpdatedDate;
            base.PhotoCount = ua.PhotoCount;
            base.Privacy = ua.Privacy;
            base.PVCount = ua.PVCount;
            base.Sequence = ua.Sequence;
            base.Status = ua.Status;
            base.Tags = ua.Tags;
        }

        public List<string> TopImages
        {
            get
            {
                return this._topimages;
            }
            set
            {
                this._topimages = value;
            }
        }
    }
}

