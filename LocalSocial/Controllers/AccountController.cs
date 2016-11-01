//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Http.ModelBinding;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;
//using Microsoft.Owin.Security.Cookies;
//using Microsoft.Owin.Security.OAuth;
//using LocalSocial.Models;
//using LocalSocial.Providers;
//using LocalSocial.Results;
//using System.Linq;
//using System.Net;
//using LocalSocial.App_Start;
//using System.Data.Entity.Migrations;
//using Microsoft.AspNet.Http;
//using Microsoft.Owin;

//namespace LocalSocial.Controllers
//{
//    [Authorize]
//    [RoutePrefix("api")]
//    public class AccountController : ApiController
//    {
//        private LocalSocialContext db = new LocalSocialContext();
//        private const string LocalLoginProvider = "Local";
//        private UserManager _userManager;
//        //messages
//        private HttpError success_message = new HttpError("success");
//        private HttpError error_message = new HttpError();

//        public AccountController()
//        {
//        }

//        public AccountController(UserManager userManager,
//            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
//        {
//            UserManager = userManager;
//            AccessTokenFormat = accessTokenFormat;
//        }
//        public UserManager UserManager
//        {
//            get
//            {
//                return _userManager ?? Request.GetOwinContext().GetUserManager<UserManager>();
//            }
//            private set
//            {
//                _userManager = value;
//            }
//        }

//        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

//        // GET api/UserInfo
//        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
//        [Route("UserInfo")]
//        public UserInfoViewModel GetUserInfo()
//        {
//            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

//            return new UserInfoViewModel
//            {
//                Email = User.Identity.GetUserName(),
//                HasRegistered = externalLogin == null,
//                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
//            };
//        }

//        // POST api/Logout
//        [Route("Logout")]
//        public IHttpActionResult Logout()
//        {
//            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
//            return Content(HttpStatusCode.OK, success_message);
//        }

//        //[Route("ChangeLocation")]
//        //public IHttpActionResult ChangeLocation(ChangeLocationBindingModel model)
//        //{
//        //    if (!ModelState.IsValid)
//        //    {
//        //        error_message.Message = BadRequest(ModelState).ToString();
//        //        return Content(HttpStatusCode.BadRequest, error_message);
//        //    }
//        //    var id = User.Identity.GetUserId();
//        //    var user = from us in db.User
//        //               where us.Id == id
//        //               select us;
//        //    var selected_user = user.FirstOrDefault();
//        //    try
//        //    {
//        //        selected_user.LocationId = model.LocationId;

//        //        db.Set<User>().AddOrUpdate(selected_user);
//        //        db.SaveChanges();
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        error_message.Message = BadRequest().ToString();
//        //        return Content(HttpStatusCode.BadRequest, error_message);
//        //    }

//        //    return Content(HttpStatusCode.OK, success_message);

//        //}
//        // GET api/ManageInfo?returnUrl=%2F&generateState=true

//        [Route("ManageInfo")]
//        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
//        {
//            IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

//            if (user == null)
//            {
//                return null;
//            }

//            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

//            foreach (IdentityUserLogin linkedAccount in user.Logins)
//            {
//                logins.Add(new UserLoginInfoViewModel
//                {
//                    LoginProvider = linkedAccount.LoginProvider,
//                    ProviderKey = linkedAccount.ProviderKey
//                });
//            }

//            if (user.PasswordHash != null)
//            {
//                logins.Add(new UserLoginInfoViewModel
//                {
//                    LoginProvider = LocalLoginProvider,
//                    ProviderKey = user.UserName,
//                });
//            }

//            return new ManageInfoViewModel
//            {
//                LocalLoginProvider = LocalLoginProvider,
//                Email = user.UserName,
//                Logins = logins
//            };
//        }

//        //// POST api/ChangePassword
//        [Route("ChangePassword")]
//        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                error_message.Message = BadRequest(ModelState).ToString();
//                return Content(HttpStatusCode.BadRequest, error_message);
//            }

//            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
//                model.NewPassword);

//            if (!result.Succeeded)
//            {
//                error_message.Message = GetErrorResult(result).ToString();
//                return Content(HttpStatusCode.BadRequest, error_message);
//            }

//            return Content(HttpStatusCode.OK, success_message);
//        }

//        // POST api/SetPassword
//        [Route("SetPassword")]
//        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                error_message.Message = BadRequest(ModelState).ToString();
//                return Content(HttpStatusCode.BadRequest, error_message);
//            }

//            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

//            if (!result.Succeeded)
//            {
//                error_message.Message = GetErrorResult(result).ToString();
//                return Content(HttpStatusCode.BadRequest, error_message);
//            }

//            return Content(HttpStatusCode.OK, success_message);
//        }

//        // POST api/RemoveLogin
//        [Route("RemoveLogin")]
//        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            IdentityResult result;

//            if (model.LoginProvider == LocalLoginProvider)
//            {
//                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
//            }
//            else
//            {
//                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
//                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
//            }

//            if (!result.Succeeded)
//            {
//                return GetErrorResult(result);
//            }

//            return Ok();
//        }

//        // POST api/Register
//        [AllowAnonymous]
//        [Route("Register")]
//        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                error_message.Message = BadRequest(ModelState).ToString();
//                return Content(HttpStatusCode.BadRequest, error_message);
//            }

//            var user = new User() { UserName = model.Email, Email = model.Email, LocationId = model.LocationId };

//            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

//            if (!result.Succeeded)
//            {
//                error_message.Message = GetErrorResult(result).ToString();
//                return Content(HttpStatusCode.BadRequest, error_message);
//            }

//            return Content(HttpStatusCode.OK, success_message);
//        }
        
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && _userManager != null)
//            {
//                _userManager.Dispose();
//                _userManager = null;
//            }

//            base.Dispose(disposing);
//        }

//        #region Helpers

//        private IAuthenticationManager Authentication
//        {
//            get { return Request.GetOwinContext().Authentication; }
//        }

//        private IHttpActionResult GetErrorResult(IdentityResult result)
//        {
//            if (result == null)
//            {
//                return InternalServerError();
//            }

//            if (!result.Succeeded)
//            {
//                if (result.Errors != null)
//                {
//                    foreach (string error in result.Errors)
//                    {
//                        ModelState.AddModelError("", error);
//                    }
//                }

//                if (ModelState.IsValid)
//                {
//                    // No ModelState errors are available to send, so just return an empty BadRequest.
//                    return BadRequest();
//                }

//                return BadRequest(ModelState);
//            }

//            return null;
//        }

//        private class ExternalLoginData
//        {
//            public string LoginProvider { get; set; }
//            public string ProviderKey { get; set; }
//            public string UserName { get; set; }

//            public IList<Claim> GetClaims()
//            {
//                IList<Claim> claims = new List<Claim>();
//                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

//                if (UserName != null)
//                {
//                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
//                }

//                return claims;
//            }

//            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
//            {
//                if (identity == null)
//                {
//                    return null;
//                }

//                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

//                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
//                    || String.IsNullOrEmpty(providerKeyClaim.Value))
//                {
//                    return null;
//                }

//                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
//                {
//                    return null;
//                }

//                return new ExternalLoginData
//                {
//                    LoginProvider = providerKeyClaim.Issuer,
//                    ProviderKey = providerKeyClaim.Value,
//                    UserName = identity.FindFirstValue(ClaimTypes.Name)
//                };
//            }
//        }

//        private static class RandomOAuthStateGenerator
//        {
//            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

//            //public static string Generate(int strengthInBits)
//            //{
//            //    const int bitsPerByte = 8;

//            //    if (strengthInBits % bitsPerByte != 0)
//            //    {
//            //        throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
//            //    }

//            //    int strengthInBytes = strengthInBits / bitsPerByte;

//            //    byte[] data = new byte[strengthInBytes];
//            //    _random.GetBytes(data);
//            //    return HttpServerUtility.UrlTokenEncode(data);
//            //}
//        }

//        #endregion
//    }
//}
