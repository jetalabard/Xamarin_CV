using System;
using Android.Content;
using Cv_Core.ConfigurationManagement;

namespace Cv_Forms.Droid.Controller.Configuration
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