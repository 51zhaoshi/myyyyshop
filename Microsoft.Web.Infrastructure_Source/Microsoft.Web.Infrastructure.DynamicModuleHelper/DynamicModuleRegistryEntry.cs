namespace Microsoft.Web.Infrastructure.DynamicModuleHelper
{
    using System;

    internal sealed class DynamicModuleRegistryEntry
    {
        public readonly string Name;
        public readonly string Type;

        public DynamicModuleRegistryEntry(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }
    }
}

