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
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Post>> GetMyPosts()
        {
            var userId = HttpContext.User.GetUserId();
            //var posts = _context.Post.AllAsync(x => x.UserId == userId);
            var posts = _context.Post.AsEnumerable();
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

        //// POST: api/Posts
        //[HttpPost]
        //public IActionResult PostPost([FromBody] Post post)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return HttpBadRequest(ModelState);
        //    }

        //    _context.Post.Add(post);
        //    try
        //    {
        //        _context.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (PostExists(post.Id))
        //        {
        //            return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("GetPost", new { id = post.Id }, post);
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