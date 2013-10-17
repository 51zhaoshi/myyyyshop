namespace Maticsoft.Components
{
    using System;

    public interface IApplicationOption
    {
        string AuthorizeCode { get; }

        string PageFootJs { get; }

        string SiteName { get; }

        string ThemeName { get; }

        string WebPowerBy { get; }

        string WebRecord { get; }
    }
}

