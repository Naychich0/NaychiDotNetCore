using Microsoft.AspNetCore.Mvc;

namespace NaychichoDotNetCore.MVCApp.Controllers
{
    public class BlogAjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
