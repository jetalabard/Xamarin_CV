using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Cv_Core.ConfigurationManagement;

namespace TalabardJeremyCv.Controller.Configuration
{
    public class AndroidConfigurationStreamProviderFactory : IConfigurationStreamProviderFactory
    {
        private readonly Func<Context> _contextProvider;

        public AndroidConfigurationStreamProviderFactory(Func<Context> contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public IConfigurationStreamProvider Create()
        {
            return new AndroidConfigurationStreamProvider(_contextProvider);
        }
    }
}