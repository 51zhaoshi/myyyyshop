namespace Maticsoft.OAuth
{
    using System;
    using System.Collections.Generic;

    public class ParameterComparer : IComparer<Parameter>
    {
        public int Compare(Parameter x, Parameter y)
        {
            if (x.Name == y.Name)
            {
                return string.Compare(x.Value, y.Value);
            }
            return string.Compare(x.Name, y.Name);
        }
    }
}

