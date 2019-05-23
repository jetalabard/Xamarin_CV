using System.Threading.Tasks;

namespace Cv_Forms.Controller
{
    public interface IAppHandler
    {
        Task<bool> LaunchApp(string uri);
    }
}
