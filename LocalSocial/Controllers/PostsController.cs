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
        [Route("add")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPost([FromBody] PostBindingModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Post.Add(
                    new Post()
                    {
                        Title = model.Title,
                        Description = model.Description,
                        AddDate = DateTime.Now,
                        Latitude = model.Latitude,
                        Longitude = model.Longitude,
                        UserId = HttpContext.User.GetUserId()
                    });
                await _context.SaveChangesAsync();
                return Ok();
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
            var posts = _context.Post.AsQueryable().Where(x => x.UserId == userId);
            return posts;
        }

        //// GET: api/Posts/5
        //[HttpGet("{id}", Name = "GetPost")]
        //public IActionResult GetPost([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return HttpBadRequest(ModelState);
        //    }

        //    Post post = _context.Post.Single(m => m.Id == id);

        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return Ok(post);
        //}

        //// PUT: api/Posts/5
        //[HttpPut("{id}")]
        //public IActionResult PutPost(int id, [FromBody] Post post)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return HttpBadRequest(ModelState);
        //    }

        //    if (id != post.Id)
        //    {
        //        return HttpBadRequest();
        //    }

        //    _context.Entry(post).State = EntityState.Modified;

        //    try
        //    {
        //        _context.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PostExists(id))
        //        {
        //            return HttpNotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
        //}

        //// DELETE: api/Posts/5
        //[HttpDelete("{id}")]
        //public IActionResult DeletePost(int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return HttpBadRequest(ModelState);
        //    }

        //    Post post = _context.Post.Single(m => m.Id == id);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    _context.Post.Remove(post);
        //    _context.SaveChanges();

        //    return Ok(post);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _context.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool PostExists(int id)
        //{
        //    return _context.Post.Count(e => e.Id == id) > 0;
        //}
    }
}