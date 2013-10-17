namespace Maticsoft.TimerTask
{
    using Maticsoft.Components;
    using System;
    using System.Web;

    public class TaskHttpModule : IHttpModule
    {
        public void Dispose()
        {
            if (MvcApplication.IsInstall)
            {
                Task.Instance().Dispose();
            }
        }

        public void Init(HttpApplication context)
        {
            if (MvcApplication.IsInstall)
            {
                Task.Instance().Start();
            }
        }
    }
}

