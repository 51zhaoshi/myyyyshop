namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public class LifetimeEventArgs : EventArgs
    {
        private object item;

        public LifetimeEventArgs(object item)
        {
            this.item = item;
        }

        public object Item
        {
            get
            {
                return this.item;
            }
        }
    }
}

