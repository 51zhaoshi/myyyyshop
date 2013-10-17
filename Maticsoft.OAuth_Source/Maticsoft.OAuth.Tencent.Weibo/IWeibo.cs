namespace Maticsoft.OAuth.Tencent.Weibo
{
    using Maticsoft.OAuth;
    using Maticsoft.OAuth.Rest.Client;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public interface IWeibo : IApiBinding
    {
        Task<JsonValue> GetUserProfileAsync();
        Task UpdateStatusAsync(string status);
        Task UploadStatusAsync(string status, FileInfo fileInfo);

        IRestOperations RestOperations { get; }
    }
}

