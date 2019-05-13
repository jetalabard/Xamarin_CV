using System;
using System.IO;
using System.Threading.Tasks;

namespace Cv_Core.ConfigurationManagement
{
    public interface IConfigurationStreamProvider : IDisposable
    {
        Task<Stream> GetStreamAsync();
    }
}
