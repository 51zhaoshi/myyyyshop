namespace Maticsoft.Json
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public delegate JsonReader JsonTextReaderFactoryHandler(TextReader reader, object options);
}

