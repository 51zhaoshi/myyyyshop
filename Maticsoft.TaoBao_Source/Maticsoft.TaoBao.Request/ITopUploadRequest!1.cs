namespace Maticsoft.TaoBao.Request
{
    using System.Collections.Generic;

    public interface ITopUploadRequest<T> : ITopRequest<T> where T: TopResponse
    {
        IDictionary<string, FileItem> GetFileParameters();
    }
}

