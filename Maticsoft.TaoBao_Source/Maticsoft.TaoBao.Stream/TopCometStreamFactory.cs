namespace Maticsoft.TaoBao.Stream
{
    using System;

    public class TopCometStreamFactory
    {
        private Configuration configruation;

        public TopCometStreamFactory(Configuration configuration)
        {
            if (configuration == null)
            {
                throw new Exception("configuration is must not null!");
            }
            this.configruation = configuration;
        }

        public ITopCometStream GetInstance()
        {
            return new TopCometStreamImpl(this.configruation);
        }
    }
}

