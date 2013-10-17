namespace Maticsoft.TaoBao.Stream
{
    using System;

    public class TopCometSysErrorException : TopCometException
    {
        public TopCometSysErrorException()
        {
        }

        public TopCometSysErrorException(string message) : base(message)
        {
        }

        public TopCometSysErrorException(string message, Exception cause) : base(message, cause)
        {
        }
    }
}

