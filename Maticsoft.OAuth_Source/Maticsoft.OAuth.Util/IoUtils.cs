namespace Maticsoft.OAuth.Util
{
    using System;
    using System.IO;

    internal sealed class IoUtils
    {
        public static void CopyStream(Stream source, Stream destination)
        {
            source.CopyTo(destination);
        }
    }
}

