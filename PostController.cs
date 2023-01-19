using Blog_Remastered.Models;
using Blog_Remastered.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blog_Remastered.Controllers
{
    public class PostController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        public PostController(ILogger<HomeController> logger, DataContext ctx, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _ctx = ctx;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index(int id)
        {
            if (id == 0)
            {
                return View(new Post());
            } else
            {
                Post post = _ctx.posts.Include(t => t.Author).Where(t => t.Id == id).First();
                if(post.Author.ApplicationUserId != _userManager.GetUserId(User))
                {
                    return View(new Post());
                }
                return View(post);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(Post post)
        {
            if (post.Id == 0)
            {
                post.Author = _ctx.authors.Where(t => t.ApplicationUserId == _userManager.GetUserId(User)).First();
                _ctx.posts.Add(post);
            } else
            {
                Post foundPost = _ctx.posts.Include(t => t.Author).Where(t => t.Id == post.Id).First();
                if(foundPost.Author.ApplicationUserId != _userManager.GetUserId(User))
                {
                    return LocalRedirect("/");
                }
                foundPost.Title = post.Title;
                foundPost.Body = post.Body;
            }
            await _ctx.SaveChangesAsync();
            return LocalRedirect("/");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Post post = _ctx.posts.First(t => t.Id == id);
            post.Deleted = DateTime.Now;
            await _ctx.SaveChangesAsync();
            return LocalRedirect("/");
        }
    }
}
