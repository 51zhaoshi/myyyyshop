namespace Maticsoft.TaoBao.Parser
{
    using System;

    public interface ITopParser
    {
        T Parse<T>(string body) where T: TopResponse;
    }
}

