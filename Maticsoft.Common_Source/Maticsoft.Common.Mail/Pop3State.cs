namespace Maticsoft.Common.Mail
{
    using System;

    [Flags]
    public enum Pop3State
    {
        Authorization = 1,
        Transaction = 2,
        Unknown = 0,
        Update = 4
    }
}

