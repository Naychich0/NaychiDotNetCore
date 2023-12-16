using Microsoft.AspNetCore.Mvc;
using NaychichoDotNetCore.MVCApp.Models;
using NaychichoDotNetCore.MVCApp.EFDbContext;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace NaychichoDotNetCore.MVCApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        //Get /List
        [ActionName("Index")]
        public IActionResult Index()
        {
            List<BlogDataModel> lst = _context.Blogs.ToList();
            return View("BlogIndex", lst);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BlogSave(BlogDataModel reqModel)
        {
            await _context.Blogs.AddAsync(reqModel);
            var result = await _context.SaveChangesAsync();
            string message = result >0 ? "Saving Successful." : "Saving Failed.";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            //return View("BlogCreate");
            return Redirect("/blog");
        }

        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            if(await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == id)
            {
                string message = "No data found.";
                TempData["Message"] = message;
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
               
            }
            var blog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.Blog_Id == id);
            return View("BlogEdit", blog);
        }
    }
}
