namespace Maticsoft.Components.Filters
{
    using System;

    [Flags]
    public enum AccountType
    {
        Admin = 1,
        Agent = 4,
        Enterprise = 3,
        None = -1,
        User = 2
    }
}

