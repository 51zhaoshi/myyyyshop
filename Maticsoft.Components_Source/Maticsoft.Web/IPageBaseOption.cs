namespace Maticsoft.Web
{
    using System;

    public interface IPageBaseOption
    {
        string DefaultLogin { get; }

        string DefaultLoginAdmin { get; }

        string DefaultLoginEnterprise { get; }
    }
}

