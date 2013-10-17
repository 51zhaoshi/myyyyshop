namespace Maticsoft.Model.Poll
{
    using System;
    using System.Collections.Generic;

    public class PollActionHelper
    {
        private Forms _formsHelper;
        private Options _optionsHelper;
        private List<Topics> _topicsHelper;

        public Forms FormsHelper
        {
            get
            {
                return this._formsHelper;
            }
            set
            {
                this._formsHelper = value;
            }
        }

        public Options OptionsHelper
        {
            get
            {
                return this._optionsHelper;
            }
            set
            {
                this._optionsHelper = value;
            }
        }

        public List<Topics> TopicsHelper
        {
            get
            {
                return this._topicsHelper;
            }
            set
            {
                this._topicsHelper = value;
            }
        }
    }
}

