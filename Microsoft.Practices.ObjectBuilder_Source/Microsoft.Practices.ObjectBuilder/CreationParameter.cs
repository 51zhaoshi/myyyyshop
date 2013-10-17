namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public class CreationParameter : KnownTypeParameter
    {
        private string creationID;

        public CreationParameter(Type type) : this(type, null)
        {
        }

        public CreationParameter(Type type, string id) : base(type)
        {
            this.creationID = id;
        }

        public override object GetValue(IBuilderContext context)
        {
            return context.HeadOfChain.BuildUp(context, base.type, null, this.creationID);
        }
    }
}

