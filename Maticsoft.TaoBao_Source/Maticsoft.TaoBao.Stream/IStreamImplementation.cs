namespace Maticsoft.TaoBao.Stream
{
    using System;

    public interface IStreamImplementation
    {
        void Close();
        bool IsAlive();
        void NextMsg();
        void OnException(Exception ex);
        string ParseLine(string msg);
    }
}

