namespace Maticsoft.TaoBao.Parser
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class TopAttribute
    {
        public string ItemName { get; set; }

        public Type ItemType { get; set; }

        public string ListName { get; set; }

        public Type ListType { get; set; }

        public MethodInfo Method { get; set; }
    }
}

