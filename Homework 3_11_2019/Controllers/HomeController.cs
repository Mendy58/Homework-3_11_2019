using Homework_3_11_2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework_3_11_2019.Controllers
{ 
    public class HomeController : Controller
    {
        PostsManager mng = new PostsManager(Properties.Settings.Default.PostConStr);

        public ActionResult Index()
        {
            HttpCookie FromRequest = Request.Cookies["MyPosts"];
            List<int> ids = new List<int>();
            if (FromRequest != null)
            {
                string userids = FromRequest.Value.ToString();
                ids = userids.Split(',').Select(Int32.Parse).ToList();
                //List<int> numbers = new List<int>(Array.ConvertAll(userids.Split(','), int.Parse));
                //ids = numbers;
            }

            List<Post> posts = mng.GetPosts();

            posts.ForEach(x => x.IsUsers = ids.Contains(x.Id));

            return View(posts);
        }

        public ActionResult About()
        {
             return View();
        }

        [HttpPost]
        public ActionResult deletepost(int id)
        {
            HttpCookie FromRequest = Request.Cookies["MyPosts"];
            List<int> ids = new List<int>();
            if (FromRequest != null)
            {
                string userids = FromRequest.Value;
                ids = userids.Split(',').Select(Int32.Parse).ToList();
            }
            ids.RemoveAll(x => x == id);
            mng.DeletePost(id);
            string updatedids = "";

            foreach(int b in ids)
            {
                updatedids += $",{b}";
            }

            HttpCookie cookie = new HttpCookie("MyPosts", updatedids);
            Response.Cookies.Add(cookie);

            return Redirect("~/Home/index");
        }

        public ActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPostdb(Post post)
        {
            post.DateCreated = DateTime.Now;
            HttpCookie FromRequest = Request.Cookies["MyPosts"];
            string ids;
            if(FromRequest != null)
            {
                ids = FromRequest.Value;
            }
            else
            {
                ids = "0";
            }
            int newid = mng.InsertPost(post);
            HttpCookie cookie = new HttpCookie("MyPosts", ids + $",{newid}");
            Response.Cookies.Add(cookie);

            return Redirect("~/Home/index");
        }
    }
}