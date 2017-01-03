using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using auctionBoard.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace auctionBoard.Controllers
{
    public class DashboardController : Controller
    {
        private AuctionContext _context;
        public DashboardController(AuctionContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("curUserUsername") != null)
            {
                User RetrievedUser = _context.Users.SingleOrDefault(user => user.username == HttpContext.Session.GetString("curUserUsername"));
                ViewBag.curUser = RetrievedUser;                
                // List<Post> Posts = _context.Posts.Include(post => post.user).ToList();
                // List<Comment> Comments = _context.Comments.Include(comment => comment.post).Include(comment => comment.user).ToList();
                // ViewBag.Posts = Posts.OrderByDescending(x=> x.createdat);
                // ViewBag.Comments = Comments;
                if(TempData["error"] != null)
                {
                    ViewBag.error = TempData["error"];
                    ViewBag.isThere = true;
                }else{
                    ViewBag.error = "";
                    ViewBag.isThere = false;
                }
                List<User> sellers = _context.Users.Include(user => user.createditems).ToList();
                List<Item> allitems = _context.Items.ToList();
                ViewBag.sellers = sellers;
                ViewBag.items = allitems;
                return View("dashboard");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Route("getnew")]
        public IActionResult GetNew()
        {
            if (HttpContext.Session.GetString("curUserUsername") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("new");
        }
        [HttpPost]
        [Route("new")]
        public IActionResult PostNew(Item PassedItem)
        {
            if(ModelState.IsValid)
            {
                if(PassedItem.endat <= DateTime.Now)
                {
                    ViewBag.isThere = true;
                    ViewBag.error = "End date should be no earlier than today";
                    return View("new", PassedItem);
                }
                User thisUser = _context.Users.SingleOrDefault(user => user.username == HttpContext.Session.GetString("curUserUsername"));
                Item NewItem = new Item
                {
                    name = PassedItem.name,
                    description = PassedItem.description,
                    startingbid = PassedItem.startingbid,
                    endat = PassedItem.endat,
                    createdat = DateTime.Now,
                    updatedat = DateTime.Now,
                    sellerid = thisUser.id,
                    bidderid = thisUser.id
                };
                _context.Items.Add(NewItem);
                _context.SaveChanges();
                return RedirectToAction("dashboard");
            }
            User RetrievedUser = _context.Users.SingleOrDefault(user => user.username == HttpContext.Session.GetString("curUserUsername"));
            ViewBag.curUser = RetrievedUser;
            return View("new", PassedItem);
        }
        [HttpGet]
        [Route("item/{id}")]
        public IActionResult ShowOne(){
            Console.WriteLine("**************************************");
            Console.WriteLine(this.RouteData.Values["id"]);
            int id = Convert.ToInt32(this.RouteData.Values["id"]);
            Item thisitem = _context.Items.SingleOrDefault(item => item.id == id);
            User thisuser = _context.Users.SingleOrDefault(user => user.id == thisitem.sellerid);
            User thisbidder = _context.Users.SingleOrDefault(user => user.id == thisitem.bidderid);
            ViewBag.thisitem = thisitem;
            ViewBag.thisuser = thisuser;
            ViewBag.thisbidder = thisbidder;            
            return View("show");
        }
    }
}