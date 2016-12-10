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
using Geolocation;

namespace LocalSocial.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly LocalSocialContext _context;
        private readonly UserManager<User> _userManager;

        public PostsController(LocalSocialContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Route("inrange")]
        [HttpPost]
        [Authorize]
        public IEnumerable<Post> GetPostsInRange([FromBody] Location model)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.User.GetUserId();
                var user = _context.User.FirstOrDefault(x => x.Id == userId);
                //CoordinateBoundaries boundaries = new CoordinateBoundaries(model.Latitude, model.Longitude, user.SearchRange/1.6);
                //var posts = _context.Post.AsQueryable();
                //var result = posts.Where(
                //        x => x.Latitude >= boundaries.MinLatitude && x.Latitude <= boundaries.MaxLatitude)
                //    .Where(x => x.Longitude >= boundaries.MinLongitude && x.Longitude <= boundaries.MaxLongitude);
                //range - dystans w kilometrach
                //GeoCalculation.GetDistance(...) - zwraca dystans w milach => 1 mila = 1.6 km
                var result = (from p in _context.Post
                              let range = user.SearchRange / 1000
                              where
                              range >=
                              GeoCalculator.GetDistance(model.Latitude, model.Longitude, p.Latitude, p.Longitude, 5) / 1.6
                              orderby p.AddDate descending
                              select p);
                //var result = _context.Post.Include(x => x.user).Where(x => x.user.SearchRange / 100 >= GeoCalculator.GetDistance(model.Latitude, model.Longitude, x.Latitude, x.Longitude, 5) / 1.6);
                return result;
            }
            return null;
        }
        [Route("add")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPost([FromBody] PostBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.User.GetUserId();
                var user = _context.User.FirstOrDefault(x => x.Id == userId);
                _context.Post.Add(
                    new Post()
                    {
                        Title = model.Title,
                        Description = model.Description,
                        AddDate = DateTime.Now,
                        Latitude = model.Latitude,
                        Longitude = model.Longitude,
                        _UserId = userId,
                        user = user

                    });
                await _context.SaveChangesAsync();
                return Ok();
            }
            return HttpBadRequest();
        }

        [Route("post/{Id:int}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPost(int Id)
        {
            Post post = await _context.Post.Include(x=>x.Comments).FirstAsync(x => x.Id == Id);
            if (post != null)
            {
                return Ok(post);
            }
            return HttpBadRequest();
        }
        
        [Route("edit/{Id:int}")]
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditPost(int Id, [FromBody] PostBindingModel model)
        {
            if (ModelState.IsValid)
            {
                Post post = await _context.Post.FirstAsync(x => x.Id == Id);
                if (post != null)
                {
                    post.Title = model.Title;
                    post.Description = model.Description;
                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    catch
                    {
                        return HttpBadRequest();
                    }
                }
            }
            return HttpBadRequest();
        }

        [Route("delete/{Id:int}")]
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePost(int Id)
        {
            Post post = await _context.Post.FirstAsync(x => x.Id == Id);
            if (post != null)
            {
                try
                {
                    _context.Remove(post);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch
                {
                    return HttpBadRequest();
                }
            }
            return HttpBadRequest();
        }
        // GET: api/Posts
        [Route("all")]
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var userId = HttpContext.User.GetUserId();
            //var posts = _context.Post.AllAsync(x => x.UserId == userId);
            var posts = _context.Post.AsEnumerable();
            return posts;
        }

        [Route("myposts")]
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Post>> GetMyPosts()
        {
            var userId = HttpContext.User.GetUserId();
            var posts = _context.Post.AsQueryable().Where(x => x._UserId == userId).OrderByDescending(p=>p.AddDate);
            return posts;
        }
    }
}