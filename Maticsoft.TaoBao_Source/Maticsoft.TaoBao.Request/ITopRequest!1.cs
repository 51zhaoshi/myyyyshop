namespace Maticsoft.TaoBao.Request
{
    using System;
    using System.Collections.Generic;

    public interface ITopRequest<T> where T: TopResponse
    {
        string GetApiName();
        IDictionary<string, string> GetParameters();
        void Validate();
    }
}

