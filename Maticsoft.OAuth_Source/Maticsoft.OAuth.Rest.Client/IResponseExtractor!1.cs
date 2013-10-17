namespace Maticsoft.OAuth.Rest.Client
{
    using Maticsoft.OAuth.Http.Client;

    public interface IResponseExtractor<T> where T: class
    {
        T ExtractData(IClientHttpResponse response);
    }
}

