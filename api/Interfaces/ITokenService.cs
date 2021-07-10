using System.Threading.Tasks;
using api.Entity;

namespace api.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(ApiUser user);
    }
}