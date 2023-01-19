using Blog_Remastered.Data;
using Blog_Remastered.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Remastered.Controllers
{
    public class HomeController :  Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(SignInManager<IdentityUser> signInManager, ILogger<HomeController> logger, DataContext ctx, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _ctx = ctx;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_ctx.posts.Where(t => t.Deleted == null).ToList());
        }

        public async Task<IActionResult> Post(int id)
        {
            Post post = _ctx.posts.Where(t => t.Id == id).First();
            post.PageViews += 1;
            await _ctx.SaveChangesAsync();
            return View(_ctx.posts.Include(t => t.Author).Include(t => t.Comments).ThenInclude(t => t.Author).Where(t => t.Id == id).First());
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect("/");
        }
        [HttpPost]
        public async Task<IActionResult> Comment(Comment comment)
        {
            comment.Author = _ctx.authors.Where(t => t.ApplicationUserId == _userManager.GetUserId(User)).First();
            Post? foundPost = _ctx.posts.Include(t => t.Comments).Where(t=> t.Id == comment.PostIdFromQuery).First();
            if(foundPost != null)
            {
                foundPost.Comments.Add(comment);
                await _ctx.SaveChangesAsync();
            }
            return RedirectToAction("Post", new { id = comment.PostIdFromQuery });
        }
    }
}
