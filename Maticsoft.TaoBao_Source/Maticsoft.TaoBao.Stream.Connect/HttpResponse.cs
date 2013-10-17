namespace Maticsoft.TaoBao.Stream.Connect
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    public class HttpResponse
    {
        private HttpWebRequest con;
        private StreamReader reader;
        private HttpWebResponse rsp;
        private Stream stream;

        public HttpResponse(HttpWebRequest con)
        {
            this.con = con;
            this.rsp = (HttpWebResponse) con.GetResponse();
            this.stream = this.rsp.GetResponseStream();
            this.reader = new StreamReader(this.stream, Encoding.UTF8);
        }

        public void Close()
        {
            if (this.reader != null)
            {
                this.reader.Close();
            }
            if (this.stream != null)
            {
                this.stream.Close();
            }
            if (this.rsp != null)
            {
                this.rsp.Close();
            }
        }

        public string GetMsg()
        {
            return this.reader.ReadLine();
        }

        public string GetResponseHeader(string name)
        {
            return this.con.Headers.Get(name);
        }
    }
}

