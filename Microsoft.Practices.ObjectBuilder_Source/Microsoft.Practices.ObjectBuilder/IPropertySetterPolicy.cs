namespace Microsoft.Practices.ObjectBuilder
{
    using System.Collections.Generic;

    public interface IPropertySetterPolicy : IBuilderPolicy
    {
        Dictionary<string, IPropertySetterInfo> Properties { get; }
    }
}

