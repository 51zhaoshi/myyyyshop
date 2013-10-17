namespace Maticsoft.TaoBao.Stream
{
    using Maticsoft.TaoBao;
    using System;

    public class TopCometException : TopException
    {
        public TopCometException()
        {
        }

        public TopCometException(string message) : base(message)
        {
        }

        public TopCometException(string message, Exception cause) : base(message, cause)
        {
        }
    }
}

