namespace Maticsoft.DBUtility
{
    using System;
    using System.Data.Common;
    using System.Threading;

    public class CommandInfo
    {
        private EventHandler _solicitationEvent;
        public string CommandText;
        public Maticsoft.DBUtility.EffentNextType EffentNextType;
        public object OriginalData;
        public DbParameter[] Parameters;
        public object ShareObject;

        private event EventHandler _solicitationEvent
        {
            add
            {
                EventHandler handler2;
                EventHandler handler = this._solicitationEvent;
                do
                {
                    handler2 = handler;
                    EventHandler handler3 = (EventHandler) Delegate.Combine(handler2, value);
                    handler = Interlocked.CompareExchange<EventHandler>(ref this._solicitationEvent, handler3, handler2);
                }
                while (handler != handler2);
            }
            remove
            {
                EventHandler handler2;
                EventHandler handler = this._solicitationEvent;
                do
                {
                    handler2 = handler;
                    EventHandler handler3 = (EventHandler) Delegate.Remove(handler2, value);
                    handler = Interlocked.CompareExchange<EventHandler>(ref this._solicitationEvent, handler3, handler2);
                }
                while (handler != handler2);
            }
        }

        public event EventHandler SolicitationEvent
        {
            add
            {
                this._solicitationEvent += value;
            }
            remove
            {
                this._solicitationEvent -= value;
            }
        }

        public CommandInfo()
        {
        }

        public CommandInfo(string sqlText, DbParameter[] para)
        {
            this.CommandText = sqlText;
            this.Parameters = para;
        }

        public CommandInfo(string sqlText, DbParameter[] para, Maticsoft.DBUtility.EffentNextType type)
        {
            this.CommandText = sqlText;
            this.Parameters = para;
            this.EffentNextType = type;
        }

        public void OnSolicitationEvent()
        {
            if (this._solicitationEvent != null)
            {
                this._solicitationEvent(this, new EventArgs());
            }
        }
    }
}

