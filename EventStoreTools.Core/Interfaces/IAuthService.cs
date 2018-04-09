using EventStoreTools.Core.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventStoreTools.Core.Interfaces
{
    public interface IAuthService
    {
        Client Register(AuthParameters user);
        ClaimsIdentity Auth(AuthParameters user);
        Client GetCurrentClient(ClaimsPrincipal user);
        Task<bool> ClientExist(string login);
    }
}
