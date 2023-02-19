using blogWeb.Data;
using blogWeb.Models;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using PagedList.Mvc;

namespace blogWeb.Controllers
{
	public class HomeController : Controller
	{
		private readonly AppDbContext db;
		public HomeController(AppDbContext _db)
		{
			db = _db;
		}
		public IActionResult Index(string? searchquery, int  pageNumber =1)
		{
			SharedLayOutData();
			if(!string.IsNullOrEmpty(searchquery))
			{
                IEnumerable<Post> Posts = db.Tbl_Post.Where(x => x.Content.Contains(searchquery));
				return View(Posts);
            }

			IEnumerable<Post> mypost = db.Tbl_Post;
			ViewBag.TotalNumPages=Math.Ceiling(mypost.Count() / 2.0);
			ViewBag.CurrentPage = pageNumber;
			mypost = mypost.Skip((pageNumber - 1) * 10).Take(10).ToList();

			return View(mypost);
		}





		[Route("Home/Post/{Slug}")]
		public IActionResult Post(string Slug)

		{
			SharedLayOutData();
			var DetailedPost = db.Tbl_Post.Where(x => x.Slug == Slug).FirstOrDefault();		
			//var DetailedPost = db.Tbl_Post.Find(id);		;- this is used to "find" only by ID
			return View(DetailedPost);
		}

		//this function create data for shared layout which will show in popular post and
		// profile info
		public void SharedLayOutData()
		{

			ViewBag.Post = db.Tbl_Post;
			ViewBag.Profile = db.Tbl_Profile.FirstOrDefault();

		}
	}
}
