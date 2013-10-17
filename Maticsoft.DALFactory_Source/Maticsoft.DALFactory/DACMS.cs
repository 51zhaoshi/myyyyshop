namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.CMS;
    using System;

    public sealed class DACMS : DataAccessBase
    {
        public static IClassType CreateClassType()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".CMS.ClassType";
            return (IClassType) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IContentClass CreateComment()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".CMS.Comment";
            return (IContentClass) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IContent CreateContent()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".CMS.Content";
            return (IContent) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IContentClass CreateContentClass()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".CMS.ContentClass";
            return (IContentClass) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IPhoto CreatePhoto()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".CMS.Photo";
            return (IPhoto) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IPhotoAlbum CreatePhotoAlbum()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".CMS.PhotoAlbum";
            return (IPhotoAlbum) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IPhotoClass CreatePhotoClass()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".CMS.PhotoClass";
            return (IPhotoClass) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IVideo CreateVideo()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".CMS.Video";
            return (IVideo) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IVideoAlbum CreateVideoAlbum()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".CMS.VideoAlbum";
            return (IVideoAlbum) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IVideoClass CreateVideoClass()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".CMS.VideoClass";
            return (IVideoClass) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}

