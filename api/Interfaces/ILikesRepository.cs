using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs;
using api.Entity;
using api.Helpers;

namespace api.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserID,int likedUserID);
        Task<ApiUser> GetUserWithLikes(int userId);
        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
    }
}