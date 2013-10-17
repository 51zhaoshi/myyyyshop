namespace Maticsoft.Json
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate Type TypeResolutionHandler(string typeName, bool throwOnError, bool ignoreCase);
}

