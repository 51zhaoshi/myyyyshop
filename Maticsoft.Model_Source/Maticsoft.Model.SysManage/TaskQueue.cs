namespace Maticsoft.Model.SysManage
{
    using System;

    [Serializable]
    public class TaskQueue
    {
        private int _id;
        private DateTime? _rundate;
        private int _status;
        private int _taskid;
        private int _type;

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public DateTime? RunDate
        {
            get
            {
                return this._rundate;
            }
            set
            {
                this._rundate = value;
            }
        }

        public int Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        public int TaskId
        {
            get
            {
                return this._taskid;
            }
            set
            {
                this._taskid = value;
            }
        }

        public int Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}

