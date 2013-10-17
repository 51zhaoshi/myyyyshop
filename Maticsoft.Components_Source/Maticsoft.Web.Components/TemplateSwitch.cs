namespace Maticsoft.Web.Components
{
    using Maticsoft.ZipLib.Zip;
    using System;
    using System.IO;
    using System.Web;

    public static class TemplateSwitch
    {
        private static readonly string ApplicationMapPath = ((HttpContext.Current != null) ? HttpContext.Current.Server.MapPath(Globals.ApplicationPath) : string.Empty);
        private const string DEFAULT_ZIP = "default.zip";

        public static void SaveDefault()
        {
            string.IsNullOrEmpty(ApplicationMapPath);
        }

        public static bool Switch(string filePath)
        {
            if (string.IsNullOrEmpty(ApplicationMapPath))
            {
                return false;
            }
            if (!File.Exists(ApplicationMapPath + "default.zip"))
            {
                SaveDefault();
            }
            new FastZip { CreateEmptyDirectories = true }.ExtractZip(filePath, HttpContext.Current.Server.MapPath("/"), FastZip.Overwrite.Always, null, null, "[Content]|[Views]", true);
            return true;
        }
    }
}

