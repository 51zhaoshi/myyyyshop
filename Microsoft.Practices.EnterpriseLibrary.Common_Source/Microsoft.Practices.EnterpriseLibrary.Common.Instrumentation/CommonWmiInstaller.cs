namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;
    using System.ComponentModel;
    using System.Management.Instrumentation;

    [RunInstaller(true)]
    public class CommonWmiInstaller : DefaultManagementProjectInstaller
    {
        private IContainer components;

        public CommonWmiInstaller()
        {
            this.InitializeComponent();
            base.Installers.Add(new ReflectionInstaller<EventLogInstallerBuilder>());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }
    }
}

