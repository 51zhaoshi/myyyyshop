namespace Maticsoft.Web.Components
{
    using System;

    public class WapDataJson
    {
        private int topicid;
        private string topicvlaue;
        private int type;

        public string TopicClaue
        {
            get
            {
                return this.topicvlaue;
            }
            set
            {
                this.topicvlaue = value;
            }
        }

        public int TopicID
        {
            get
            {
                return this.topicid;
            }
            set
            {
                this.topicid = value;
            }
        }

        public int Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
    }
}

