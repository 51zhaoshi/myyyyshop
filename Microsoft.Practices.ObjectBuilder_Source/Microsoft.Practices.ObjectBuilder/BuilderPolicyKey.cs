namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct BuilderPolicyKey
    {
        private Type PolicyType;
        private Type BuildType;
        private string BuildID;
        public BuilderPolicyKey(Type policyType, Type typePolicyAppliesTo, string idPolicyAppliesTo)
        {
            this.PolicyType = policyType;
            this.BuildType = typePolicyAppliesTo;
            this.BuildID = idPolicyAppliesTo;
        }
    }
}

