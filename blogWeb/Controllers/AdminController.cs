using blogWeb.Data;
using blogWeb.Models;
using blogWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace blogWeb.Controllers
{
    public class AdminController : Controller

    {
        IWebHostEnvironment env;
        AppDbContext db;
        public AdminController(AppDbContext _db,IWebHostEnvironment environment)
        {
            db = _db;
            env = environment;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddPost()
        {
            return View();

        }
        [HttpPost]
        public IActionResult AddPost(PostVM myPost)
        {
            if (ModelState.IsValid)
            {
                string ImageName = myPost.Image.FileName.ToString();
                var FolderPath = Path.Combine(env.WebRootPath, "Images");
                var CompletePicPath = Path.Combine(FolderPath, ImageName);
                myPost.Image.CopyTo(new FileStream(CompletePicPath,FileMode.Create));

                Post post = new Post();
                post.Title = myPost.Title;
                post.SubTitle=myPost.SubTitle;
                post.Date = myPost.Date;
                post.Slug = myPost.Slug;
                post.Content = myPost.Content;
                post.Image = ImageName;
                db.Tbl_Post.Add(post);
                db.SaveChanges();

                return RedirectToAction("AllPost","Admin");
            }

            
            return View();

        }
        public IActionResult AllPost()
        {
            var myAllPosts = db.Tbl_Post;
            return View(myAllPosts);
        }
        public IActionResult DeletePost(int id)
        {
            var PostToDelete = db.Tbl_Post.Find(id);
            if (PostToDelete != null)
            {
                db.Remove(PostToDelete);
                db.SaveChanges();
            }
            return RedirectToAction("AllPost", "Admin");

        }
        [HttpGet]
        public IActionResult UpdatePost(int Id)
        {
            var PosttoUpdate = db.Tbl_Post.Find(Id);
                return View(PosttoUpdate);
        }
        [HttpPost]
        public IActionResult UpdatePost(Post post)
        {
            db.Tbl_Post.Update(post);
            db.SaveChanges();
            return RedirectToAction("AllPost", "Admin");
        }

        public IActionResult CreateProfile()
        {
            return View();
        }
    }
}
