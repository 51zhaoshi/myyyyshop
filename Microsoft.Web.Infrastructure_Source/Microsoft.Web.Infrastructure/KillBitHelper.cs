namespace Microsoft.Web.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Security;
    using System.Security.Permissions;

    [SecurityCritical]
    internal sealed class KillBitHelper : IDisposable
    {
        private const string _killBitFwdLink = "http://go.microsoft.com/fwlink/?LinkID=204759";
        private const string _killBitRegKeyName = @"SOFTWARE\Microsoft\ASP.NET\Security\DisableLoadList\";
        private readonly IRegistryKey _localMachine;
        private static readonly Version _thisAssemblyFileVersion = GetThisAssemblyFileVersion();

        public KillBitHelper() : this(new RegistryKeyWrapper(Registry.LocalMachine))
        {
        }

        internal KillBitHelper(IRegistryKey localMachine)
        {
            this._localMachine = localMachine;
        }

        [SecuritySafeCritical]
        public void Dispose()
        {
            this._localMachine.Dispose();
        }

        private List<Version> GetKillBittedVersions()
        {
            List<Version> list = new List<Version>();
            string pathList = this._localMachine.ToString() + @"\SOFTWARE\Microsoft\ASP.NET\Security\DisableLoadList\";
            new RegistryPermission(RegistryPermissionAccess.Read, pathList).Assert();
            try
            {
                IRegistryKey key = this._localMachine.OpenSubKey(@"SOFTWARE\Microsoft\ASP.NET\Security\DisableLoadList\");
                if (key == null)
                {
                    return list;
                }
                using (key)
                {
                    foreach (string str2 in key.GetSubKeyNames())
                    {
                        if (str2.StartsWith("Microsoft.Web.Infrastructure", StringComparison.OrdinalIgnoreCase))
                        {
                            IRegistryKey key2 = key.OpenSubKey(str2);
                            using (key2)
                            {
                                object obj2 = key2.GetValue("Flags");
                                if ((obj2 is int) && (((int) obj2) != 0))
                                {
                                    string str3 = (string) key2.GetValue("FileVersion");
                                    if (!string.IsNullOrEmpty(str3))
                                    {
                                        list.Add(Version.Parse(str3));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
            return list;
        }

        private static Version GetThisAssemblyFileVersion()
        {
            return Version.Parse(CommonAssemblies.MicrosoftWebInfrastructure.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false).Cast<AssemblyFileVersionAttribute>().Single<AssemblyFileVersionAttribute>().Version);
        }

        internal bool IsThisAssemblyKillBitted()
        {
            return this.GetKillBittedVersions().Any<Version>(new Func<Version, bool>(KillBitHelper.KillBitMatchesThisAssemblyVersion));
        }

        private static bool KillBitMatchesThisAssemblyVersion(Version killBittedVersion)
        {
            return (((_thisAssemblyFileVersion.Major == killBittedVersion.Major) && (_thisAssemblyFileVersion.Minor == killBittedVersion.Minor)) && (_thisAssemblyFileVersion.Build <= killBittedVersion.Build));
        }

        public void ThrowIfKillBitIsSet()
        {
            if (this.IsThisAssemblyKillBitted())
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Kill bit was set. Please see {0} for more information.", new object[] { "http://go.microsoft.com/fwlink/?LinkID=204759" }));
            }
        }
    }
}

