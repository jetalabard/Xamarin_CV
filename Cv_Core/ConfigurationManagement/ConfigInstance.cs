using System;
using System.Collections.Generic;
using System.Text;

namespace Cv_Core.ConfigurationManagement
{
    public class ConfigInstance
    {
        private static ConfigInstance _Instance;
        private Configuration config;

        public bool IsDev
        {
            get
            {
                return config.Dev;
            }
        }

        public string DownloadDirectoryName
        {
            get
            {
                return config.DownloadDirectoryName;
            }
        }

        public string DatabaseUrl
        {
            get
            {
                return config.DatabaseUrl;
            }
        }

        public string ImageDirectoryName
        {
            get
            {
                return config.ImageDirectoryName;
            }
        }

        public string ImageUrl
        {
            get
            {
                return config.ImageUrl;
            }
        }


        public ConfigInstance(Configuration config)
        {
            this.config = config;
        }

        public static ConfigInstance GetInstance(Configuration config = null)
        {
            if(_Instance == null || config != null)
            {
                _Instance = new ConfigInstance(config);
            }
            return _Instance;
        }


    }
}
