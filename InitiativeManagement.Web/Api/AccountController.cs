using InitiativeManagement.Common;
using InitiativeManagement.Service;
using InitiativeManagement.Web.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
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
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.OK, false);
            }
        }

        [HttpGet]
        [Route("permission")]
        [AllowAnonymous]
        public HttpResponseMessage GetUserRoles(HttpRequestMessage request)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            if (user == null)
                return request.CreateResponse(HttpStatusCode.OK, false);

            //var userIdentity = (ClaimsIdentity)User.Identity;

            //var claims = userIdentity.Claims;

            //var roleClaimType = userIdentity.RoleClaimType;

            //var roles = claims.Where(c => c.Type == roleClaimType).Select(x => x.Value).ToList();

            var roles = _applicationGroupService.GetRolesByUserId(user.Id).Select(x => x.Name).ToList();

            return request.CreateResponse(HttpStatusCode.OK, roles);
        }
    }
}