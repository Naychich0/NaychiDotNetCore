using Microsoft.AspNetCore.Mvc;
using NaychiDotNetCore.MvcApp.Models;

namespace AEHKLMNSTZDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        [ActionName("Index")]
        public IActionResult BlogIndex()
        {
            AppDbContext db = new AppDbContext();
            List<BlogDataModel> lst = db.Blogs.ToList();
            return View("BlogIndex",lst);
        }
    }
}
