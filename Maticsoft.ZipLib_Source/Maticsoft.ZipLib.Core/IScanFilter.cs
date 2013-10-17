namespace Maticsoft.ZipLib.Core
{
    using System;

    public interface IScanFilter
    {
        bool IsMatch(string name);
    }
}

