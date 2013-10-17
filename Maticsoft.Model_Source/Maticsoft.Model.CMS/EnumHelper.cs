namespace Maticsoft.Model.CMS
{
    using System;

    public static class EnumHelper
    {
        public enum CommentType
        {
            Content = 3,
            None = -1,
            Photo = 1,
            Video = 2
        }

        public enum ContentRec
        {
            Color = 2,
            Hot = 1,
            None = -1,
            Recomend = 0,
            Top = 3
        }

        public enum ContentStateType
        {
            Approve = 0,
            Draft = 1,
            None = -1,
            Unaudited = 2
        }
    }
}

