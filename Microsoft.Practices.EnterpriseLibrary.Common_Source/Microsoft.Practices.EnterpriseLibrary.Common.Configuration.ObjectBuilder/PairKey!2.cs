namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using System;

    internal class PairKey<TLeft, TRight>
    {
        private TLeft left;
        private TRight right;

        internal PairKey(TLeft left, TRight right)
        {
            this.left = left;
            this.right = right;
        }

        public override bool Equals(object obj)
        {
            PairKey<TLeft, TRight> key = obj as PairKey<TLeft, TRight>;
            if (key == null)
            {
                return false;
            }
            return (object.Equals(this.left, key.left) && object.Equals(this.right, key.right));
        }

        public override int GetHashCode()
        {
            int num = (this.left == null) ? 0 : this.left.GetHashCode();
            int num2 = (this.right == null) ? 0 : this.right.GetHashCode();
            return (num ^ num2);
        }

        public TLeft Left
        {
            get
            {
                return this.left;
            }
        }

        public TRight Right
        {
            get
            {
                return this.right;
            }
        }
    }
}

