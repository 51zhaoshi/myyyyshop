namespace Maticsoft.TimerTask.Model
{
    using System;

    public interface IAction
    {
        void Run(string[] args);
    }
}

