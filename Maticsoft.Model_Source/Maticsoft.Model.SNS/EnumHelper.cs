namespace Maticsoft.Model.SNS
{
    using System;

    public static class EnumHelper
    {
        public enum CommentType
        {
            Album = 3,
            Blog = 4,
            None = -1,
            Normal = 0,
            Photo = 1,
            Product = 2
        }

        public enum FansType
        {
            EachOher = 1,
            None = -1,
            Normal = 0
        }

        public enum FavoriteType
        {
            None = -1,
            Photo = 0,
            Product = 1
        }

        public enum FLinkType
        {
            Image,
            Text
        }

        public enum GroupRecommend
        {
            None,
            Index,
            Selective
        }

        public enum GroupStatus
        {
            Checked = 1,
            CheckedUnPass = 2,
            none = -1,
            UnCheck = 0
        }

        public enum GroupUserRole
        {
            Admin = 1,
            Leader = 2,
            none = -1,
            Normal = 0
        }

        public enum GroupUserStatus
        {
            Checked = 1,
            ForbidSpeak = 2,
            none = -1,
            UnCheck = 0
        }

        public enum ImageType
        {
            None = -1,
            Photo = 0,
            Product = 1
        }

        public enum MsgType
        {
            Comment = 3,
            None = -1,
            Private = 1,
            Refer = 2,
            System = 0
        }

        public enum NavigationType
        {
            Default,
            TufenXiang
        }

        public enum PhotoStatus
        {
            AlreadyChecked = 1,
            CategoryDefined = 4,
            CategoryUnDefined = 3,
            CheckedPass = 2,
            None = -1,
            UnChecked = 0
        }

        public enum PhotoType
        {
            Collocation = 1,
            Group = 3,
            NetBuyPhoto = 2,
            None = -1,
            ShareGoods = 0
        }

        public enum PostContentType
        {
            Blog = 4,
            None = -1,
            Normal = 0,
            Photo = 1,
            Product = 2,
            Video = 3
        }

        public enum PostStatus
        {
            AlreadyChecked = 1,
            AlreadyDel = 3,
            CheckedUnPass = 2,
            None = -1,
            UnChecked = 0
        }

        public enum PostType
        {
            All = 0,
            Blog = 9,
            EachOther = 5,
            Fellow = 1,
            None = -1,
            OnePost = 3,
            Photo = 6,
            Product = 7,
            ReferMe = 4,
            User = 2,
            Video = 8
        }

        public enum ProductStatus
        {
            AlreadyChecked = 1,
            CategoryDefined = 4,
            CategoryUnDefined = 3,
            CheckedPass = 2,
            None = -1,
            UnChecked = 0
        }

        public enum QueryType
        {
            Count = 0,
            List = 1,
            None = -1
        }

        public enum RecommendType
        {
            None,
            Home,
            Channel
        }

        public enum ReferType
        {
            Comment = 1,
            None = -1,
            Post = 0
        }

        public enum Status
        {
            Disabled = 0,
            Enabled = 1,
            None = -1
        }

        public enum SwapSequenceIndex
        {
            Down = 0,
            None = -1,
            Up = 1
        }

        public enum TargetType
        {
            None = -1,
            Normal = 0,
            Photo = 1,
            Product = 2
        }

        public enum TopicOparationType
        {
            CancellRecommend = 0,
            Delete = 2,
            None = -1,
            Recommend = 1
        }

        public enum TopicRecommendType
        {
            None = -1,
            NoneRecommend = 0,
            Recommend = 1
        }

        public enum TopicStatus
        {
            Checked = 1,
            CheckedUnPass = 2,
            none = -1,
            UnCheck = 0
        }

        public enum UserGroupType
        {
            None = -1,
            UserFav = 3,
            UserGroup = 0,
            UserPostTopic = 1,
            UserReply = 2
        }

        public enum WebSiteType
        {
            DangDang = 5,
            JingDong = 4,
            None = -1,
            TaoBao = 3
        }
    }
}

