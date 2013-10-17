namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;

    public interface IWmiPublisher
    {
        void Publish(ConfigurationSetting instance);
        void Revoke(ConfigurationSetting instance);
    }
}

