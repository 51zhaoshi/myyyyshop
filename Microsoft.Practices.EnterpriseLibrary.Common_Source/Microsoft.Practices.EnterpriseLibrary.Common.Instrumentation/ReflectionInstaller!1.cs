namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;
    using System.Collections;
    using System.Configuration.Install;
    using System.Reflection;

    public class ReflectionInstaller<TInstallerBuilder> : Installer where TInstallerBuilder: AbstractInstallerBuilder
    {
        public override void Install(IDictionary stateSaver)
        {
            this.PrepareInstaller();
            base.Install(stateSaver);
        }

        private void PrepareInstaller()
        {
            string path = base.Context.Parameters["assemblypath"];
            Type[] types = Assembly.LoadFile(path).GetTypes();
            ((TInstallerBuilder) Activator.CreateInstance(typeof(TInstallerBuilder), new object[] { types })).Fill(this);
        }

        public override void Uninstall(IDictionary stateSaver)
        {
            this.PrepareInstaller();
            base.Uninstall(stateSaver);
        }
    }
}

