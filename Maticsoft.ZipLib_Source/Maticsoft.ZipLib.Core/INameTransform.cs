namespace Maticsoft.ZipLib.Core
{
    using System;

    public interface INameTransform
    {
        string TransformDirectory(string name);
        string TransformFile(string name);
    }
}

