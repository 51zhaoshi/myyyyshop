namespace Maticsoft.Common.Mail
{
    using System;

    internal static class Pop3Commands
    {
        public const string Crlf = "\r\n";
        public const string Dele = "DELE ";
        public const string List = "LIST ";
        public const string Noop = "NOOP\r\n";
        public const string Pass = "PASS ";
        public const string Quit = "QUIT\r\n";
        public const string Retr = "RETR ";
        public const string Rset = "RSET\r\n";
        public const string Stat = "STAT\r\n";
        public const string Top = "TOP ";
        public const string User = "USER ";
    }
}

