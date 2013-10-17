namespace Resources
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode, CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "10.0.0.0")]
    internal class Shop
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Shop()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static string ImageSizes
        {
            get
            {
                return ResourceManager.GetString("ImageSizes", resourceCulture);
            }
        }

        internal static string NormalImageHeight
        {
            get
            {
                return ResourceManager.GetString("NormalImageHeight", resourceCulture);
            }
        }

        internal static string NormalImageWidth
        {
            get
            {
                return ResourceManager.GetString("NormalImageWidth", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Resources.Shop", Assembly.Load("App_GlobalResources"));
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static string ThumbImageHeight
        {
            get
            {
                return ResourceManager.GetString("ThumbImageHeight", resourceCulture);
            }
        }

        internal static string ThumbImageWidth
        {
            get
            {
                return ResourceManager.GetString("ThumbImageWidth", resourceCulture);
            }
        }
    }
}

