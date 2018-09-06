using System.Threading.Tasks;
using Action.Common.Auth;

namespace Actio.Services.Identity.Services
{
    public interface IUserService
    {
         Task RegisterAsync(string email, string password, string name);
         Task<JsonWebToken> LoginAsync(string email, string passwor);
    }
}