namespace Maticsoft.TaoBao.Jushita
{
    using Maticsoft.TaoBao.Request;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class JushitaRequest : ITopRequest<JushitaResponse>
    {
        public string GetApiName()
        {
            return this.ApiName;
        }

        public IDictionary<string, string> GetParameters()
        {
            return this.Parameters;
        }

        public void Validate()
        {
        }

        public string ApiName { get; set; }

        public IDictionary<string, string> Parameters { get; set; }
    }
}

