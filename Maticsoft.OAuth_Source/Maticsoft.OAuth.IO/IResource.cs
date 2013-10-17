namespace Maticsoft.OAuth.IO
{
    using System;
    using System.IO;

    public interface IResource
    {
        Stream GetStream();

        bool IsOpen { get; }

        System.Uri Uri { get; }
    }
}

