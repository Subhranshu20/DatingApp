using api.Data;
using api.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class BuggyController : ApiBaseController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet("Auth")]
        public ActionResult<string> GetSecret()
        {

            return "Secret Text";
        }
        
        //  [Authorize]
        [HttpGet("not-found")]
        public ActionResult<ApiUser> GetNotFound()
        {

            var thing = _context.Users.Find(-1);
            if (thing==null) return NotFound();
            return Ok(thing);
        }
        // [Authorize]
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {

            var thing = _context.Users.Find(-1);
            var thingtoreturn = thing.ToString();
            return thingtoreturn;
        }
        // [Authorize]
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {

            // return BadRequest("This was not a good request");
             return BadRequest();
        }

    }
}