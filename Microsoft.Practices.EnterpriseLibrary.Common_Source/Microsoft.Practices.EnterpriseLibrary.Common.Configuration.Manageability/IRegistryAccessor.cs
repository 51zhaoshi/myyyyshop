namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    public interface IRegistryAccessor
    {
        IRegistryKey CurrentUser { get; }

        IRegistryKey LocalMachine { get; }
    }
}

