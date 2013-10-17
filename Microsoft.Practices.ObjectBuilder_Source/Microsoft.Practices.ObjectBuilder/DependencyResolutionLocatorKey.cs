namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public sealed class DependencyResolutionLocatorKey
    {
        private string id;
        private System.Type type;

        public DependencyResolutionLocatorKey() : this(null, null)
        {
        }

        public DependencyResolutionLocatorKey(System.Type type, string id)
        {
            this.type = type;
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            DependencyResolutionLocatorKey key = obj as DependencyResolutionLocatorKey;
            if (key == null)
            {
                return false;
            }
            return (object.Equals(this.type, key.type) && object.Equals(this.id, key.id));
        }

        public override int GetHashCode()
        {
            int num = (this.type == null) ? 0 : this.type.GetHashCode();
            int num2 = (this.id == null) ? 0 : this.id.GetHashCode();
            return (num ^ num2);
        }

        public string ID
        {
            get
            {
                return this.id;
            }
        }

        public System.Type Type
        {
            get
            {
                return this.type;
            }
        }
    }
}

