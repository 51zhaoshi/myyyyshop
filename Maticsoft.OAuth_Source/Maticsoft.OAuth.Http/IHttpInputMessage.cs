namespace Maticsoft.OAuth.Http
{
    using System.IO;

    public interface IHttpInputMessage
    {
        Stream Body { get; }

        HttpHeaders Headers { get; }
    }
}

