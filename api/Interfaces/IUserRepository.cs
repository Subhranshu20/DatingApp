using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs;
using api.Entity;
using api.Helpers;

namespace api.Interfaces
{
    public interface IUserRepository
    {
        void Update(ApiUser user);
        // Task<bool> SaveAllAsync();
        Task<IEnumerable<ApiUser>> GetUsersAsync();
        Task<ApiUser> GetUserByIdAsync(int id);
        Task<ApiUser> GetUserByUsernameAsync(string username);
        Task<MemberDto> GetMemberAsync(string username);
        Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        Task<string> GetUserGender(string username);

    }
}