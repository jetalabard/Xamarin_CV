using System.Threading;
using System.Threading.Tasks;

namespace Cv_Core.ConfigurationManagement
{
    public interface IConfigurationManager
    {
        Task<Configuration> GetAsync(CancellationToken cancellationToken);
    }
}
