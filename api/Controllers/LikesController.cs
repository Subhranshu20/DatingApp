using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs;
using api.Entity;
using api.Extentions;
using api.Helpers;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    public class LikesController : ApiBaseController
    {
         private readonly IUnitOfWork _unitOfWork;
        
        public LikesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var sourceUserID = User.GetUserId();
            var likedUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _unitOfWork.LikesRepository.GetUserWithLikes(sourceUserID);
            if(likedUser == null) return NotFound();
            if(sourceUser.UserName == username) return BadRequest("You cannot like yourself");

            var userLike = await _unitOfWork.LikesRepository.GetUserLike(sourceUserID,likedUser.Id);
            if(userLike!=null) return BadRequest("You have already liked this user");
            userLike= new UserLike{
                SourceUserID=sourceUserID,
                LikeUserID=likedUser.Id
            };
            sourceUser.LikedUsers.Add(userLike);
            if(await _unitOfWork.Complete()) return Ok();
            return BadRequest("Failed to like user");

        }
        [HttpGet]
        public async Task<ActionResult<PagedList<LikeDto>>> GetUserLikes([FromQuery]LikesParams likeParams)
        {
            likeParams.UserId=User.GetUserId();
           var users= await _unitOfWork.LikesRepository.GetUserLikes(likeParams);
           Response.AddPaginationHeader(users.CurrentPage,users.PageSize,users.TotalCount,users.TotalPages);
           return Ok(users);
        }


    }
}