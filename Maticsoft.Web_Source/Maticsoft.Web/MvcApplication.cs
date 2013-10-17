namespace Maticsoft.Web
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Components;
    using Maticsoft.ViewEngine;
    using System;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class MvcApplication : Maticsoft.Components.MvcApplication
    {
        static MvcApplication()
        {
            Maticsoft.Components.MvcApplication.ApplicationOption = new ApplicationOption();
        }

        protected override void ApplicationStart()
        {
            if (Maticsoft.Components.MvcApplication.IsInstall)
            {
                ViewEngines.Engines.Clear();
                ViewEngines.Engines.Add(new ThemeViewEngine(Maticsoft.Components.MvcApplication.ThemeName));
            }
            AssemblyName name = Assembly.GetExecutingAssembly().GetName();
            Maticsoft.Components.MvcApplication.Version = string.Concat(new object[] { name.Version.Major, ".", name.Version.Minor, (name.Version.Build > 0) ? ("." + name.Version.Build) : string.Empty });
            string assemblyProduct = this.AssemblyProduct;
            Maticsoft.Components.MvcApplication.ProductInfo = "Maticsoft" + (string.IsNullOrWhiteSpace(assemblyProduct) ? "FK" : (" " + assemblyProduct));
        }

        public override void RegisterIgnoreRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("pay/{*pathInfo}");
            routes.IgnoreRoute("tools/{*pathInfo}");
            base.RegisterIgnoreRoutes(routes);
        }

        public string AssemblyProduct
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (customAttributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyProductAttribute) customAttributes[0]).Product;
            }
        }

        protected override string MainArea
        {
            get
            {
                return ConfigSystem.GetValue("MainArea");
            }
        }
    }
}

