using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MatrixAuth.API.Requests;
using MatrixAuth.Service.Interface;
using Matrix.Structures;
using System;
using System.Linq;

namespace MatrixAuth.API.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        protected readonly IMatrixAuthService _matrixAuthService;

        public UserController([FromServices] IMatrixAuthService matrixAuthService)
        {
            _matrixAuthService = matrixAuthService;
        }

        //[RequireHttps] //TODO: Get SSL certificate and use this for security
        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public ActionResult ValidateUser([FromBody]UserLogin request)
        {
            if(HttpContext.Request.Scheme != Uri.UriSchemeHttps)
            {
                Console.WriteLine("Warning: Request made from non-SSL origin! Please update this to use SSL as soon as it's available.");
            }

            MatrixLoginResponse ret = null;

            if (request.Password != null)
            {
                ret = _matrixAuthService.LoginWithPassword(request.Homeserver, request.UserName, request.Password);
            }
            else if (request.Token != null)
            {
                ret = _matrixAuthService.LoginWithToken(request.Homeserver, request.UserName, request.Token);
            }

            if (ret == null)
            {                
                return StatusCode(401);
            }

            return Json(ret);
        }

        //This is just for testing via Swagger
        [AllowAnonymous]
        [HttpGet]
        [Route("{username}")]
        public ActionResult GetUserLoginState([FromRoute] string username)
        {
            bool loggedIn = _matrixAuthService.UserExists(username);

            if(loggedIn)
            {
                return Json($"{username} is logged in");
            }

            return Json($"{username} is not logged in");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("active")]
        public ActionResult GetActiveUsers()
        {
            var userList = _matrixAuthService.ListUsers();

            if(userList != null)
            {
                return Json(userList);
            }

            return Json("Nobody is using the service");
        }
    }
}