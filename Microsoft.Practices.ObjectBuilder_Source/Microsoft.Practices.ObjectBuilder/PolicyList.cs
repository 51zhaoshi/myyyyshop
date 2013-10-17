namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections.Generic;

    public class PolicyList
    {
        private object lockObject = new object();
        private Dictionary<BuilderPolicyKey, IBuilderPolicy> policies = new Dictionary<BuilderPolicyKey, IBuilderPolicy>();

        public PolicyList(params PolicyList[] policiesToCopy)
        {
            if (policiesToCopy != null)
            {
                foreach (PolicyList list in policiesToCopy)
                {
                    this.AddPolicies(list);
                }
            }
        }

        public void AddPolicies(PolicyList policiesToCopy)
        {
            lock (this.lockObject)
            {
                if (policiesToCopy != null)
                {
                    foreach (KeyValuePair<BuilderPolicyKey, IBuilderPolicy> pair in policiesToCopy.policies)
                    {
                        this.policies[pair.Key] = pair.Value;
                    }
                }
            }
        }

        public void Clear<TPolicyInterface>(Type typePolicyAppliesTo, string idPolicyAppliesTo)
        {
            this.Clear(typeof(TPolicyInterface), typePolicyAppliesTo, idPolicyAppliesTo);
        }

        public void Clear(Type policyInterface, Type typePolicyAppliesTo, string idPolicyAppliesTo)
        {
            lock (this.lockObject)
            {
                this.policies.Remove(new BuilderPolicyKey(policyInterface, typePolicyAppliesTo, idPolicyAppliesTo));
            }
        }

        public void ClearAll()
        {
            lock (this.lockObject)
            {
                this.policies.Clear();
            }
        }

        public void ClearDefault<TPolicyInterface>()
        {
            this.ClearDefault(typeof(TPolicyInterface));
        }

        public void ClearDefault(Type policyInterface)
        {
            this.Clear(policyInterface, null, null);
        }

        public TPolicyInterface Get<TPolicyInterface>(Type typePolicyAppliesTo, string idPolicyAppliesTo) where TPolicyInterface: IBuilderPolicy
        {
            return (TPolicyInterface) this.Get(typeof(TPolicyInterface), typePolicyAppliesTo, idPolicyAppliesTo);
        }

        public IBuilderPolicy Get(Type policyInterface, Type typePolicyAppliesTo, string idPolicyAppliesTo)
        {
            BuilderPolicyKey key = new BuilderPolicyKey(policyInterface, typePolicyAppliesTo, idPolicyAppliesTo);
            lock (this.lockObject)
            {
                IBuilderPolicy policy;
                if (this.policies.TryGetValue(key, out policy))
                {
                    return policy;
                }
                BuilderPolicyKey key2 = new BuilderPolicyKey(policyInterface, null, null);
                if (this.policies.TryGetValue(key2, out policy))
                {
                    return policy;
                }
                return null;
            }
        }

        public void Set<TPolicyInterface>(TPolicyInterface policy, Type typePolicyAppliesTo, string idPolicyAppliesTo) where TPolicyInterface: IBuilderPolicy
        {
            this.Set(typeof(TPolicyInterface), policy, typePolicyAppliesTo, idPolicyAppliesTo);
        }

        public void Set(Type policyInterface, IBuilderPolicy policy, Type typePolicyAppliesTo, string idPolicyAppliesTo)
        {
            BuilderPolicyKey key = new BuilderPolicyKey(policyInterface, typePolicyAppliesTo, idPolicyAppliesTo);
            lock (this.lockObject)
            {
                this.policies[key] = policy;
            }
        }

        public void SetDefault<TPolicyInterface>(TPolicyInterface policy) where TPolicyInterface: IBuilderPolicy
        {
            this.SetDefault(typeof(TPolicyInterface), policy);
        }

        public void SetDefault(Type policyInterface, IBuilderPolicy policy)
        {
            this.Set(policyInterface, policy, null, null);
        }

        public int Count
        {
            get
            {
                lock (this.lockObject)
                {
                    return this.policies.Count;
                }
            }
        }
    }
}

