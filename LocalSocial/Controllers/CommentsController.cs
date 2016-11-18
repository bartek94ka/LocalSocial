using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalSocial.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

namespace LocalSocial.Controllers
{
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly LocalSocialContext _context;
        private readonly UserManager<User> _userManager;
        public CommentsController(LocalSocialContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
    }
}
