namespace Maticsoft.TimerTask.Model
{
    using System;

    [Serializable]
    public class TaskTimer
    {
        private int _executenumber;
        private DateTime _executetime;
        private string _executetype;
        private int _id;
        private decimal _interval;
        private bool _issingle = true;
        private string _param1;
        private string _param10;
        private string _param2;
        private string _param3;
        private string _param4;
        private string _param5;
        private string _param6;
        private string _param7;
        private string _param8;
        private string _param9;

        public int ExecuteNumber
        {
            get
            {
                return this._executenumber;
            }
            set
            {
                this._executenumber = value;
            }
        }

        public DateTime ExecuteTime
        {
            get
            {
                return this._executetime;
            }
            set
            {
                this._executetime = value;
            }
        }

        public string ExecuteType
        {
            get
            {
                return this._executetype;
            }
            set
            {
                this._executetype = value;
            }
        }

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

        public decimal Interval
        {
            get
            {
                return this._interval;
            }
            set
            {
                this._interval = value;
            }
        }

        public bool IsSingle
        {
            get
            {
                return this._issingle;
            }
            set
            {
                this._issingle = value;
            }
        }

        public string Param1
        {
            get
            {
                return this._param1;
            }
            set
            {
                this._param1 = value;
            }
        }

        public string Param10
        {
            get
            {
                return this._param10;
            }
            set
            {
                this._param10 = value;
            }
        }

        public string Param2
        {
            get
            {
                return this._param2;
            }
            set
            {
                this._param2 = value;
            }
        }

        public string Param3
        {
            get
            {
                return this._param3;
            }
            set
            {
                this._param3 = value;
            }
        }

        public string Param4
        {
            get
            {
                return this._param4;
            }
            set
            {
                this._param4 = value;
            }
        }

        public string Param5
        {
            get
            {
                return this._param5;
            }
            set
            {
                this._param5 = value;
            }
        }

        public string Param6
        {
            get
            {
                return this._param6;
            }
            set
            {
                this._param6 = value;
            }
        }

        public string Param7
        {
            get
            {
                return this._param7;
            }
            set
            {
                this._param7 = value;
            }
        }

        public string Param8
        {
            get
            {
                return this._param8;
            }
            set
            {
                this._param8 = value;
            }
        }

        public string Param9
        {
            get
            {
                return this._param9;
            }
            set
            {
                this._param9 = value;
            }
        }

        public string[] Params
        {
            get
            {
                return new string[] { this.Param1, this.Param2, this.Param3, this.Param4, this.Param5, this.Param6, this.Param7, this.Param8, this.Param9, this.Param10 };
            }
            set
            {
                if ((value != null) && (value.Length >= 1))
                {
                    if (value.Length > 0)
                    {
                        this.Param1 = value[0];
                    }
                    if (value.Length > 1)
                    {
                        this.Param2 = value[1];
                    }
                    if (value.Length > 2)
                    {
                        this.Param3 = value[2];
                    }
                    if (value.Length > 3)
                    {
                        this.Param4 = value[3];
                    }
                    if (value.Length > 4)
                    {
                        this.Param5 = value[4];
                    }
                    if (value.Length > 5)
                    {
                        this.Param6 = value[5];
                    }
                    if (value.Length > 6)
                    {
                        this.Param7 = value[6];
                    }
                    if (value.Length > 7)
                    {
                        this.Param8 = value[7];
                    }
                    if (value.Length > 8)
                    {
                        this.Param9 = value[8];
                    }
                    if (value.Length > 9)
                    {
                        this.Param10 = value[9];
                    }
                }
            }
        }
    }
}

