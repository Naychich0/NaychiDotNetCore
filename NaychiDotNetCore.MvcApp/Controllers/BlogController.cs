using Microsoft.AspNetCore.Mvc;

namespace NaychiDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        [ActionName("Index")]
        public IActionResult BlogIndex()
        {
            return View("BlogIndex");
        }
    }
}
