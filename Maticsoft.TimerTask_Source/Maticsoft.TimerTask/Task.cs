namespace Maticsoft.TimerTask
{
    using Maticsoft.TimerTask.BLL;
    using Maticsoft.TimerTask.Model;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class Task : IDisposable
    {
        private bool _disposed;
        private List<Maticsoft.TimerTask.Timer> _listTask = new List<Maticsoft.TimerTask.Timer>();
        private static readonly Task _task = new Task();

        private Task()
        {
        }

        public static void Add(DateTime executeTime, Action<string[]> action, string[] args)
        {
            DateTime now = DateTime.Now;
            if (executeTime < DateTime.Now)
            {
                executeTime = now.AddSeconds(1.0);
            }
            Maticsoft.TimerTask.Model.TaskTimer timer2 = new Maticsoft.TimerTask.Model.TaskTimer {
                IsSingle = true,
                ExecuteType = action.GetType().FullName,
                ExecuteTime = executeTime
            };
            TimeSpan span = (TimeSpan) (executeTime - now);
            timer2.Interval = (decimal) span.TotalMilliseconds;
            timer2.Params = args;
            Maticsoft.TimerTask.Model.TaskTimer model = timer2;
            model.ID = Maticsoft.TimerTask.BLL.TaskTimer.Add(model);
            Instance().CurrentTasks.Add(new Maticsoft.TimerTask.Timer(model, executeTime, action, new Action<Maticsoft.TimerTask.Model.TaskTimer>(Task.CallBack), args));
        }

        public static void CallBack(Maticsoft.TimerTask.Model.TaskTimer taskTimer)
        {
            Maticsoft.TimerTask.BLL.TaskTimer.Delete(taskTimer.ID);
        }

        public void Dispose()
        {
            if (!this._disposed)
            {
                lock (this)
                {
                    this._listTask.ForEach(delegate (Maticsoft.TimerTask.Timer item) {
                        if (item != null)
                        {
                            item.Dispose();
                        }
                    });
                    this._listTask.Clear();
                    this._disposed = true;
                }
            }
        }

        public static Task Instance()
        {
            return _task;
        }

        public void Start()
        {
            Task task;
            bool lockTaken = false;
            try
            {
                Type type;
                IAction action;
                Monitor.Enter(task = this, ref lockTaken);
                this._listTask.Clear();
                List<Maticsoft.TimerTask.Model.TaskTimer> modelList = Maticsoft.TimerTask.BLL.TaskTimer.GetModelList("");
                if ((modelList != null) && (modelList.Count >= 1))
                {
                    modelList.ForEach(delegate (Maticsoft.TimerTask.Model.TaskTimer item) {
                        if (item.ExecuteType != null)
                        {
                            type = Type.GetType(item.ExecuteType);
                            if (type != null)
                            {
                                action = Activator.CreateInstance(type) as IAction;
                                if (action != null)
                                {
                                    string[] args = new string[] { item.Param1, item.Param2, item.Param3, item.Param4, item.Param5, item.Param6, item.Param7, item.Param8, item.Param9, item.Param10 };
                                    this._listTask.Add(new Maticsoft.TimerTask.Timer(item, item.ExecuteTime, new Action<string[]>(action.Run), new Action<Maticsoft.TimerTask.Model.TaskTimer>(Task.CallBack), args));
                                }
                            }
                        }
                    });
                }
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(task);
                }
            }
        }

        public List<Maticsoft.TimerTask.Timer> CurrentTasks
        {
            get
            {
                return this._listTask;
            }
        }
    }
}

