namespace Maticsoft.OAuth.Tencent.QQ
{
    using Maticsoft.OAuth;
    using Maticsoft.OAuth.Rest.Client;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public interface IQConnect : IApiBinding
    {
        Task<JsonValue> GetUserProfileAsync();
        Task UpdateStatusAsync(string status);
        Task UploadStatusAsync(string status, FileInfo fileInfo);

        IRestOperations RestOperations { get; }
    }
}

