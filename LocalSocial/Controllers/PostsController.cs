using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using LocalSocial;
using LocalSocial.Models;

namespace LocalSocial.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private LocalSocialContext _context = new LocalSocialContext();

        // GET: api/Posts
        [HttpGet]
        public IEnumerable<Post> GetPost()
        {
            return new List<Post>
            {
                new Post {PostContent = "aaa"},
                new Post {PostContent = "Post2"}
            };
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