namespace Maticsoft.Web.Components
{
    using System;
    using System.IO;
    using System.Web.Hosting;

    public class MyWorkerRequest : SimpleWorkerRequest
    {
        private string localAdd;

        public MyWorkerRequest(string page, string query, TextWriter output, string address) : base(page, query, output)
        {
            this.localAdd = string.Empty;
            this.localAdd = address;
        }

        public override string GetLocalAddress()
        {
            return this.localAdd;
        }
    }
}

