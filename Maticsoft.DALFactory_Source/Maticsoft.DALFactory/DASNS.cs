namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.SNS;
    using System;

    public sealed class DASNS : DataAccessBase
    {
        public static IAlbumType CreateAlbumType()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.AlbumType";
            return (IAlbumType) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ICategories CreateCategories()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.Categories";
            return (ICategories) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ICategorySource CreateCategorySource()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.CategorySource";
            return (ICategorySource) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IComments CreateComments()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.Comments";
            return (IComments) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IFellowTopics CreateFellowTopics()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.FellowTopics";
            return (IFellowTopics) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IGradeConfig CreateGradeConfig()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.GradeConfig";
            return (IGradeConfig) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IGroups CreateGroups()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.Groups";
            return (IGroups) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IGroupTags CreateGroupTags()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.GroupTags";
            return (IGroupTags) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IGroupTopicFav CreateGroupTopicFav()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.GroupTopicFav";
            return (IGroupTopicFav) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IGroupTopicReply CreateGroupTopicReply()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.GroupTopicReply";
            return (IGroupTopicReply) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IGroupTopics CreateGroupTopics()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.GroupTopics";
            return (IGroupTopics) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IGroupUsers CreateGroupUsers()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.GroupUsers";
            return (IGroupUsers) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IGuestBook CreateGuestBook()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.GuestBook";
            return (IGuestBook) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IHotWords CreateHotWords()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.HotWords";
            return (IHotWords) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IOnLineShopPhoto CreateOnLineShopPhoto()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.OnLineShopPhoto";
            return (IOnLineShopPhoto) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IPhotos CreatePhotos()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.Photos";
            return (IPhotos) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IPhotoTags CreatePhotoTags()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.PhotoTags";
            return (IPhotoTags) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IPosts CreatePosts()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.Posts";
            return (IPosts) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IPostsTopics CreatePostsTopics()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.PostsTopics";
            return (IPostsTopics) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProducts CreateProducts()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.Products";
            return (IProducts) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProductSources CreateProductSources()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.ProductSources";
            return (IProductSources) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IReferUsers CreateReferUsers()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.ReferUsers";
            return (IReferUsers) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IReport CreateReport()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.Report";
            return (IReport) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IReportType CreateReportType()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.ReportType";
            return (IReportType) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISearchWordLog CreateSearchWordLog()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.SearchWordLog";
            return (ISearchWordLog) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISearchWordTop CreateSearchWordTop()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.SearchWordTop";
            return (ISearchWordTop) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IStar CreateStar()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.Star";
            return (IStar) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IStarRank CreateStarRank()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.StarRank";
            return (IStarRank) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IStarType CreateStarType()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.StarType";
            return (IStarType) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ITags CreateTags()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.Tags";
            return (ITags) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ITagType CreateTagType()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.TagType";
            return (ITagType) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IUserAlbumDetail CreateUserAlbumDetail()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.UserAlbumDetail";
            return (IUserAlbumDetail) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IUserAlbums CreateUserAlbums()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.UserAlbums";
            return (IUserAlbums) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IUserAlbumsType CreateUserAlbumsType()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.UserAlbumsType";
            return (IUserAlbumsType) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IUserBlog CreateUserBlog()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.UserBlog";
            return (IUserBlog) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IUserFavAlbum CreateUserFavAlbum()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.UserFavAlbum";
            return (IUserFavAlbum) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IUserFavourite CreateUserFavourite()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.UserFavourite";
            return (IUserFavourite) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IUserShip CreateUserShip()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.UserShip";
            return (IUserShip) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IUserShipCategories CreateUserShipCategories()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.UserShipCategories";
            return (IUserShipCategories) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IVideos CreateVideos()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.Videos";
            return (IVideos) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IVisiteLogs CreateVisiteLogs()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SNS.VisiteLogs";
            return (IVisiteLogs) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}

