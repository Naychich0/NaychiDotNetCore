using Microsoft.AspNetCore.Mvc;

namespace NaychichoDotNetCore.MVCApp.Controllers
{
    public class UserController : Controller
    {
        [ActionName("Index")]
        public IActionResult Index()
        {
            return View("BlogIndex");
        }
    }
}
