using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using LocalSocial;
using LocalSocial.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LocalSocial.Controllers
{
    [Route("api/[controller]")]
    public class UserFriendsController : Controller
    {
        private readonly LocalSocialContext _context;
        private readonly UserManager<User> _userManager;
        public UserFriendsController(LocalSocialContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //[Route("friends")]
        //[HttpPost]
        //[Authorize]
        //public async Task<IEnumerable<User>> GetFriends()
        //{
        //    var userId = HttpContext.User.GetUserId();
        //    var user = _context.User.FirstOrDefault(x => x.Id == userId);
        //    return user.Friends.Friends.AsEnumerable();
        //}
        [Route("friends")]
        [HttpPost]
        [Authorize]
        public async Task<IEnumerable<User>> FindFriends(UserBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.User.GetUserId();
                var user = _context.User.FirstOrDefault(x => x.Id == userId);
                //wszyscy poza biezacym uzytkownikiem
                var friends = (from u in _context.User
                               where u.Id != userId
                               select u);
                return friends.AsEnumerable();
            }
            return null;
        }
        //[Route("addfriend/{UserId:string}")]
        [Route("addfriend")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddFriend(string UserId)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.User.GetUserId();
                var user = _context.User.FirstOrDefault(x => x.Id == userId);
                var friend = _context.User.FirstOrDefault(x => x.Id == UserId);
                if (user != null && friend != null)
                {
                    user.Friends.Friends.Add(friend);
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
            return HttpBadRequest();
        }
        //[Route("removefriend/{Id:string}")]
        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> RemoveFriend(string Id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userId = HttpContext.User.GetUserId();
        //        var user = _context.User.FirstOrDefault(x => x.Id == userId);
        //        var friend = _context.User.FirstOrDefault(x => x.Id == Id);
        //        if (user != null && friend != null)
        //        {
        //            user.Friends.Friends.Remove(friend);
        //        }
        //        await _context.SaveChangesAsync();
        //        return Ok();
        //    }
        //    return HttpBadRequest();
        //}
    }
}
