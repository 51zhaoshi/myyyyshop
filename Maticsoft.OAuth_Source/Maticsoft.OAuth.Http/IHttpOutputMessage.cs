namespace Maticsoft.OAuth.Http
{
    using System;

    public interface IHttpOutputMessage
    {
        Action<Stream> Body { set; }

        HttpHeaders Headers { get; }
    }
}

