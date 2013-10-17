namespace Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
    using System;
    using System.ComponentModel;
    using System.Management.Instrumentation;

    [RunInstaller(true)]
    public class DataInstrumentationInstaller : DefaultManagementProjectInstaller
    {
        private IContainer components;

        public DataInstrumentationInstaller()
        {
            base.Installers.Add(new ReflectionInstaller<PerformanceCounterInstallerBuilder>());
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

