namespace Maticsoft.OAuth
{
    using System;

    public interface IApiBinding
    {
        bool IsAuthorized { get; }
    }
}

