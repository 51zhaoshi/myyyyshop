namespace Maticsoft.TaoBao
{
    using Maticsoft.TaoBao.Request;
    using System;

    public interface ITopClient
    {
        T Execute<T>(ITopRequest<T> request) where T: TopResponse;
        T Execute<T>(ITopRequest<T> request, string session) where T: TopResponse;
        T Execute<T>(ITopRequest<T> request, string session, DateTime timestamp) where T: TopResponse;
    }
}

