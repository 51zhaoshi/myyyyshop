namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public interface IBuilderAware
    {
        void OnBuiltUp(string id);
        void OnTearingDown();
    }
}

