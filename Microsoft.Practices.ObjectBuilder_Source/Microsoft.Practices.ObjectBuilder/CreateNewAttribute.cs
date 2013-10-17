namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
    public sealed class CreateNewAttribute : ParameterAttribute
    {
        public override IParameter CreateParameter(Type annotatedMemberType)
        {
            return new CreationParameter(annotatedMemberType, Guid.NewGuid().ToString());
        }
    }
}

