using InitiativeManagement.Common;
using InitiativeManagement.Service;
using InitiativeManagement.Web.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace InitiativeManagement.Web.Api
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IApplicationGroupService _applicationGroupService;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IApplicationGroupService applicationGroupService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _applicationGroupService = applicationGroupService;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<HttpResponseMessage> Login(HttpRequestMessage request, string userName, string password, bool rememberMe)
        {
            if (!ModelState.IsValid)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(userName, password, rememberMe, shouldLockout: false);
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Authorize]
        [Route("logout")]
        public HttpResponseMessage Logout(HttpRequestMessage request)
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return request.CreateResponse(HttpStatusCode.OK, new { success = true });
        }

        [HttpGet]
        [Route("users")]
        [Authorize]
        public async Task<HttpResponseMessage> GetUsers(HttpRequestMessage request)
        {
            try
            {
                var users = await UserManager.Users.Where(u => !u.IsAccountAdmin).Select(item => new CustomSelectList
                {
                    Text = item.FullName,
                    Value = item.Id
                }).ToListAsync();

                return request.CreateResponse(HttpStatusCode.OK, users);
            }
            catch
            {
                return request.CreateResponse(HttpStatusCode.OK, new List<string>());
            }
        }

        [HttpGet]
        [Route("permission")]
        public async Task<HttpResponseMessage> GetUserRolesAsync(HttpRequestMessage request)
        {
            try
            {
                //var user = UserManager.FindById(User.Identity.GetUserId());

                //var roles = _applicationGroupService.GetRolesByUserId(user.Id).Select(x => x.Name).ToList();
                var roles = await UserManager.GetRolesAsync(User.Identity.GetUserId());

                return request.CreateResponse(HttpStatusCode.OK, roles);
            }
            catch
            {
                return request.CreateResponse(HttpStatusCode.OK, new List<string>());
            }
        }
    }
}