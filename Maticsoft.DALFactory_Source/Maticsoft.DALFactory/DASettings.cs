namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.Settings;
    using System;

    public sealed class DASettings : DataAccessBase
    {
        public static IAdvertisement CreateAdvertisement()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Settings.Advertisement";
            return (IAdvertisement) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IAdvertisePosition CreateAdvertisePosition()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Settings.AdvertisePosition";
            return (IAdvertisePosition) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IFilterWords CreateFilterWords()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Settings.FilterWords";
            return (IFilterWords) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IFriendlyLink CreateFriendlyLink()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Settings.FriendlyLink";
            return (IFriendlyLink) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IMainMenus CreateMainMenus()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Settings.MainMenus";
            return (IMainMenus) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISEORelation CreateSEORelation()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Settings.SEORelation";
            return (ISEORelation) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}

