namespace Maticsoft.Model.SNS
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PostContent
    {
        public List<Comments> CommentList = new List<Comments>();
        public string StaticUrl = "";

        public static PostContent CreateInstance<T>(T target) where T: class, new()
        {
            PostContent content = new PostContent();
            if (target is Photos)
            {
                Photos photos = target as Photos;
                content.TargetId = photos.PhotoID;
                content.TargetName = photos.PhotoName;
                content.TargetDescription = photos.Description;
                content.CommentCount = photos.CommentCount;
                content.FavouriteCount = photos.FavouriteCount;
                content.ThumbImageUrl = photos.ThumbImageUrl;
                content.Type = 1;
                content.TargetType = photos.Type;
                content.TopCommentsId = photos.TopCommentsId;
                return content;
            }
            if (!(target is Products))
            {
                throw new NotSupportedException();
            }
            Products products = target as Products;
            content.TargetId = products.ProductID;
            content.TargetName = products.ProductName;
            content.TargetDescription = products.ShareDescription;
            content.CommentCount = products.CommentCount;
            content.FavouriteCount = products.FavouriteCount;
            content.ThumbImageUrl = products.ThumbImageUrl;
            content.Type = 2;
            content.TopCommentsId = products.TopCommentsId;
            return content;
        }

        public int CommentCount { get; set; }

        public int FavouriteCount { get; set; }

        public decimal Price { get; set; }

        public string TargetDescription { get; set; }

        public long TargetId { get; set; }

        public string TargetName { get; set; }

        public int TargetType { get; set; }

        public string ThumbImageUrl { get; set; }

        public string TopCommentsId { get; set; }

        public int Type { get; set; }
    }
}

