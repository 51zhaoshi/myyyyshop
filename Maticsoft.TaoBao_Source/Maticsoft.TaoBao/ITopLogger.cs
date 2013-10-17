namespace Maticsoft.TaoBao
{
    using System;

    public interface ITopLogger
    {
        void Error(string message);
        void Info(string message);
        void Warn(string message);
    }
}

