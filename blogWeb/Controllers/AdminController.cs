using blogWeb.Data;
using blogWeb.Models;
using blogWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

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
            if (HttpContext.Session.GetString("LoginFlag") != null)
            {
                ViewBag.NumberOfPosts = db.Tbl_Post.Count();
                ViewBag.NumberOfUsers = db.Tbl_Profile.Count();
                DisplayData();
                return View();
            }
            else
            return RedirectToAction("Login", "Admin");
        }

        [HttpGet]
        public IActionResult AddPost()
        {
            if (HttpContext.Session.GetString("LoginFlag") != null)
            {
                DisplayData();
                return View();
            }
            else
                //return RedirectToAction("Login", "Admin");
                return Redirect("/Admin/Login?ReturnUrl=/Admin/AddPost");
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPost(PostVM myPost)
        {
            DisplayData();
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
        [HttpGet]
        public IActionResult AllPost()
        {
            if (HttpContext.Session.GetString("LoginFlag") != null)
            {
                DisplayData();
                var myAllPosts = db.Tbl_Post;
                return View(myAllPosts);
            }
            else
                return RedirectToAction("Login", "Admin");
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

            DisplayData();
            var PosttoUpdate = db.Tbl_Post.Find(Id);
                return View(PosttoUpdate);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost(Post post)
        {
            
            db.Tbl_Post.Update(post);
            db.SaveChanges();
            return RedirectToAction("AllPost", "Admin");
        }

        public IActionResult CreateProfile()
        {
            if (HttpContext.Session.GetString("LoginFlag") != null)
            {
                DisplayData();
                return View();
            }
            else
                return RedirectToAction("Login", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProfile(ProfileVM profileVM)
        {
            DisplayData();
            if (ModelState.IsValid)
            {
                string ImageName = profileVM.Image.FileName.ToString();
                var FolderPath=Path.Combine(env.WebRootPath, "Images");
                var CompleteImagePath=Path.Combine(FolderPath,ImageName);
                profileVM.Image.CopyTo(new FileStream(CompleteImagePath,FileMode.Create));

                Profile profile = new Profile();
                profile.Name = profileVM.Name;
                profile.FatherName = profileVM.FatherName;
                profile.Bio=profileVM.Bio;
                profile.Image = ImageName;
                profile.username = profileVM.username;
                profile.Password = profileVM.Password;

                db.Tbl_Profile.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Home", "Index");
            }
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVM loginVM, string? ReturnUrl)
        {
            if(ModelState.IsValid)
            {
                var result = db.Tbl_Profile.Where(opt => opt.username.Equals(loginVM.username)
                && opt.Password.Equals(loginVM.password)).FirstOrDefault();
            if(result!=null)
                {   //session is like a global variable data can be accessed in allproject
                    HttpContext.Session.SetInt32("ProfileId",result.Id);
                    HttpContext.Session.SetString("LoginFlag", "true");
                   if(ReturnUrl==null)
                    { 
                        return RedirectToAction("Index", "Admin"); 
                    }
                   else
                    {

                        return Redirect(ReturnUrl);
                    }
                   
                }

            // taking msg from here to view on invalid attempt
                ViewData["LoginFlag"] = "invalid username or password";
                return View();
            }
            
            return View();
        }

        //will be used to display data as partial view except of .cshtml data
        public void DisplayData()
        {
            ViewBag.Profile = db.Tbl_Profile.Where(x => x.Id.Equals(HttpContext.Session
                .GetInt32("ProfileId"))).AsNoTracking().FirstOrDefault();
            //AsNoTracking() stops the tracking of id its extention method currently using for reading onl
        }


        public IActionResult UpdateProfile(int Id)
        {
            DisplayData();
            var myProfile = db.Tbl_Profile.Find(Id);
            ProfileVM pVm = new ProfileVM();
            pVm.Id = myProfile.Id;
            pVm.Name = myProfile.Name;
            pVm.FatherName = myProfile.FatherName;
            pVm.Bio = myProfile.Bio;
            pVm.username = myProfile.username;
            pVm.Password = myProfile.Password;

            pVm.ConfirmPassword = myProfile.Password;
            ViewData["ImageName"] = myProfile.Image;

            return View(pVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProfile(ProfileVM myProfile, string? oldpic)
        {
            DisplayData();
            string imageName = null;
            if (ModelState.IsValid)
            {
                if (myProfile.Image != null)
                {
                    imageName = myProfile.Image.FileName.ToString();
                    var FolderPath = Path.Combine(env.WebRootPath, "images");
                    var Imagepath = Path.Combine(FolderPath, imageName);
                    myProfile.Image.CopyTo(new FileStream(Imagepath, FileMode.Create));
                }
                Profile originalProfile = new Profile();
                originalProfile.Id = myProfile.Id;
                originalProfile.Name = myProfile.Name;
                originalProfile.FatherName = myProfile.FatherName;
                originalProfile.Bio = myProfile.Bio;
                originalProfile.username = myProfile.username;
                originalProfile.Password = myProfile.Password;
                
                if(!string.IsNullOrEmpty(imageName))
                {
                    originalProfile.Image = imageName;
                }
                else
                {
                    originalProfile.Image = oldpic;
                }
                db.Tbl_Profile.Update(originalProfile);
                db.SaveChanges();
                return View("Index", "Admin");
            }

            if (!string.IsNullOrEmpty(imageName))
            {
                ViewData["ImageName"] = imageName;
            }
            else
            {
                ViewData["ImageName"] = oldpic;
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Admin");
        }
        }
}
