namespace Maticsoft.Email.EmailJob
{
    using System;
    using System.Web;

    public class EmailJobSevice : IHttpModule
    {
        public void Dispose()
        {
            Jobs.Instance().Stop();
        }

        public void Init(HttpApplication context)
        {
            Jobs.Instance().Start();
        }
    }
}

