namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public interface IBuilderConfigurator<TStageEnum>
    {
        void ApplyConfiguration(IBuilder<TStageEnum> builder);
    }
}

