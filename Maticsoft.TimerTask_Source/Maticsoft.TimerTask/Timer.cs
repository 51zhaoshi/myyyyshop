namespace Maticsoft.TimerTask
{
    using Maticsoft.TimerTask.Model;
    using System;
    using System.Timers;

    public class Timer : System.Timers.Timer
    {
        private readonly Maticsoft.TimerTask.Model.TaskTimer _taskTimer;

        public Timer(Maticsoft.TimerTask.Model.TaskTimer taskTimer, DateTime executeTime, Action<string[]> action, Action<Maticsoft.TimerTask.Model.TaskTimer> callback, string[] args)
        {
            ElapsedEventHandler handler = null;
            ElapsedEventHandler handler2 = null;
            this._taskTimer = taskTimer;
            TimeSpan span = (TimeSpan) (executeTime - DateTime.Now);
            double totalMilliseconds = span.TotalMilliseconds;
            if (totalMilliseconds >= 2147483647.0)
            {
                throw new ArgumentOutOfRangeException("执行时间超过最大值 24天!");
            }
            if (handler == null)
            {
                handler = delegate (object obj, ElapsedEventArgs e) {
                    action(args);
                };
            }
            base.Elapsed += handler;
            if (handler2 == null)
            {
                handler2 = delegate (object obj, ElapsedEventArgs e) {
                    callback(this._taskTimer);
                };
            }
            base.Elapsed += handler2;
            base.AutoReset = false;
            base.Interval = ((totalMilliseconds > 0.0) && (totalMilliseconds < 2147483647.0)) ? totalMilliseconds : 100.0;
            base.Enabled = true;
        }

        public Maticsoft.TimerTask.Model.TaskTimer TaskTimer
        {
            get
            {
                return this._taskTimer;
            }
        }
    }
}

