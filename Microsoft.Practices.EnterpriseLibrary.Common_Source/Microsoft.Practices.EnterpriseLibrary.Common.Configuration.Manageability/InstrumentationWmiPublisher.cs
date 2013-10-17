namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;
    using System.Management.Instrumentation;

    internal class InstrumentationWmiPublisher : IWmiPublisher
    {
        public void Publish(ConfigurationSetting instance)
        {
            System.Management.Instrumentation.Instrumentation.Publish(instance);
        }

        public void Revoke(ConfigurationSetting instance)
        {
            System.Management.Instrumentation.Instrumentation.Revoke(instance);
        }
    }
}

