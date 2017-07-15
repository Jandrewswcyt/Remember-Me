using Microsoft.AspNetCore.Mvc;

namespace RememberMe.Controllers
{
    public class FriendController : Controller
    {
        public IActionResult Index()
        {
            return View();   
        }

        public IActionResult Error()
        {
            return View(); 
        }
    }
}