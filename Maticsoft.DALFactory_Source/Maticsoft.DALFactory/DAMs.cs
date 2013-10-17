namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.Ms;
    using System;

    public sealed class DAMs : DataAccessBase
    {
        public static IEmailTemplet CreateEmailTemplet()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Ms.EmailTemplet";
            return (IEmailTemplet) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IEnterprise CreateEnterprise()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Ms.Enterprise";
            return (IEnterprise) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IRegionAreas CreateRegionAreas()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Ms.RegionAreas";
            return (IRegionAreas) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IRegionRec CreateRegionRec()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Ms.RegionRec";
            return (IRegionRec) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IRegions CreateRegions()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Ms.Regions";
            return (IRegions) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ITheme CreateTheme()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Ms.Theme";
            return (ITheme) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IThumbnailSize CreateThumbnailSize()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Ms.ThumbnailSize";
            return (IThumbnailSize) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IWeiBoMsg CreateWeiBoMsg()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Ms.WeiBoMsg";
            return (IWeiBoMsg) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IWeiBoTaskMsg CreateWeiBoTaskMsg()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Ms.WeiBoTaskMsg";
            return (IWeiBoTaskMsg) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}

