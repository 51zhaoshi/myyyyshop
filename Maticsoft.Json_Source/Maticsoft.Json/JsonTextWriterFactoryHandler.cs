namespace Maticsoft.Json
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public delegate JsonWriter JsonTextWriterFactoryHandler(TextWriter writer, object options);
}

