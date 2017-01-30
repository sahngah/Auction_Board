using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using auctionBoard.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace auctionBoard.Controllers
{
    public class HomeController : Controller
    {
        private AuctionContext _context;
        public HomeController(AuctionContext context)
        {
            _context = context;
        }
        PasswordHasher<User> Hasher = new PasswordHasher<User>();
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("curUserUsername") != null)
            {
                return RedirectToAction("Dashboard", "Dashboard");
            } 
            @ViewBag.Error = "";
            @ViewBag.isThere = false;
            return View();
        }
        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterUser RegisterUser)
        {   
            if(HttpContext.Session.GetString("curUserUsername") != null)
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }
            if(ModelState.IsValid)
            {
                if(_context.Users.SingleOrDefault(user => user.username == RegisterUser.Username) != null)
                {
                    @ViewBag.Error = "Error: Email Already In Use";
                    @ViewBag.isThere = true;
                    return View();
                }
                User NewUser = new User
                {
                    firstname = RegisterUser.Firstname,
                    lastname = RegisterUser.Lastname,
                    username = RegisterUser.Username,
                    password = RegisterUser.Password,
                    createdat = DateTime.Now,
                    updatedat = DateTime.Now,
                    wallet = 1000
                };
                NewUser.password = Hasher.HashPassword(NewUser, RegisterUser.Password);
                _context.Users.Add(NewUser);
                _context.SaveChanges();
                HttpContext.Session.SetString("curUserUsername", NewUser.username);
                return RedirectToAction("Dashboard", "Dashboard");
            }
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginUser LoginUser)
        {
            if(ModelState.IsValid)
            {
                User thisuser = _context.Users.SingleOrDefault(user => user.username == LoginUser.Username);
                if(thisuser != null && 0 != Hasher.VerifyHashedPassword(thisuser, thisuser.password, LoginUser.Password))
                {
                    HttpContext.Session.SetString("curUserUsername", LoginUser.Username);
                    return RedirectToAction("Dashboard", "Dashboard");
                }
            }
            Console.WriteLine("urgh!!!!");
            @ViewBag.isThere = true;
            @ViewBag.Error = "Please check your email or password again.";
            return View("Index", LoginUser);
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
