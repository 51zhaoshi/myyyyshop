namespace Maticsoft.ZipLib.Zip
{
    using Maticsoft.ZipLib.Core;
    using System;

    public interface IEntryFactory
    {
        ZipEntry MakeDirectoryEntry(string directoryName);
        ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem);
        ZipEntry MakeFileEntry(string fileName);
        ZipEntry MakeFileEntry(string fileName, bool useFileSystem);

        INameTransform NameTransform { get; set; }
    }
}

