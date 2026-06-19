using ASPDOTNETCOREEF_LoginUsingSession.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPDOTNETCOREEF_LoginUsingSession.Controllers
{
    public class HomeController : Controller
    {
        private readonly FcDotnetContext _context;

        public HomeController(FcDotnetContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("UserName") != null)
            {
                return RedirectToAction("Dashborad");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(TblUser user)
        {
            var myuser = _context.TblUsers.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
            if(myuser != null)
            {
                HttpContext.Session.SetString("UserName", myuser.Email);
                return RedirectToAction("Dashborad");
            }
            else
            {
                ViewBag.Message = "Invalid email or password";
            }
            return View();
        }

        public IActionResult Dashborad()
        {
            if(HttpContext.Session.GetString("UserName") != null)
            {
                ViewBag.mySession = HttpContext.Session.GetString("UserName").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                //HttpContext.Session.Remove("UserName");
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
