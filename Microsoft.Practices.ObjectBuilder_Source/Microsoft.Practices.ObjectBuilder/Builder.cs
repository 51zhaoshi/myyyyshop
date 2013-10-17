namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public class Builder : BuilderBase<BuilderStage>
    {
        public Builder() : this(null)
        {
        }

        public Builder(IBuilderConfigurator<BuilderStage> configurator)
        {
            base.Strategies.AddNew<TypeMappingStrategy>(BuilderStage.PreCreation);
            base.Strategies.AddNew<SingletonStrategy>(BuilderStage.PreCreation);
            base.Strategies.AddNew<ConstructorReflectionStrategy>(BuilderStage.PreCreation);
            base.Strategies.AddNew<PropertyReflectionStrategy>(BuilderStage.PreCreation);
            base.Strategies.AddNew<MethodReflectionStrategy>(BuilderStage.PreCreation);
            base.Strategies.AddNew<CreationStrategy>(BuilderStage.Creation);
            base.Strategies.AddNew<PropertySetterStrategy>(BuilderStage.Initialization);
            base.Strategies.AddNew<MethodExecutionStrategy>(BuilderStage.Initialization);
            base.Strategies.AddNew<BuilderAwareStrategy>(BuilderStage.PostInitialization);
            base.Policies.SetDefault<ICreationPolicy>(new DefaultCreationPolicy());
            if (configurator != null)
            {
                configurator.ApplyConfiguration(this);
            }
        }
    }
}

