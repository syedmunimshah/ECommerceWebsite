using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebsite.Controllers
{

    public class AdminController : Controller
    {
        private readonly myContext _myContext;
        private readonly IWebHostEnvironment _env;
        public AdminController(myContext myContext, IWebHostEnvironment env)
        {
            _myContext = myContext;
            _env = env;
        }
        public IActionResult Index()
        {
            string admin_session = HttpContext.Session.GetString("admin_session");
            if (admin_session != null)
            {
                return View();
            }
            else {
                return RedirectToAction("Login");
            }

        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string adminEmail, string adminPassword)
        {
            var row = _myContext.tbl_admin.FirstOrDefault(x => x.admin_email == adminEmail);
            if (row != null && row.admin_password == adminPassword) {

                HttpContext.Session.SetString("admin_session", row.admin_id.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Incorrect User  Or Password";
                return View();
            }




        }


        public IActionResult logout()
        {
            HttpContext.Session.Remove("admin_session");
            return RedirectToAction("Login");
        }

        public IActionResult Profile()
        {
            var adminId = HttpContext.Session.GetString("admin_session");
            var row = _myContext.tbl_admin.FirstOrDefault(x => x.admin_id == int.Parse(adminId)); // Return a single object
            return View(row);
        }



        [HttpPost]
        public IActionResult Profile(Admin admin)
        {
            _myContext.tbl_admin.Update(admin);
            _myContext.SaveChanges();
            return RedirectToAction("Profile");
        }



        [HttpPost]
        public IActionResult ChangeProfileImage(IFormFile admin_image, Admin admin)
        {
            if (admin_image == null || string.IsNullOrEmpty(admin_image.FileName))
            {
                // Handle the error appropriately
                ViewBag.message = "Invalid image file.";
                return View("Profile", admin); // Return to the profile view with the current admin data
            }

            string webRootPath = _env?.WebRootPath;
            if (string.IsNullOrEmpty(webRootPath))
            {
                // Handle the error appropriately
                ViewBag.message = "Web root path is not configured.";
                return View("Profile", admin); // Return to the profile view with the current admin data
            }

            string imagePath = Path.Combine(webRootPath, "images", admin_image.FileName);
            using (FileStream fs = new FileStream(imagePath, FileMode.Create))
            {
                admin_image.CopyTo(fs);
            }

            admin.admin_image = admin_image.FileName;
            _myContext.tbl_admin.Update(admin);
            _myContext.SaveChanges();

            return RedirectToAction("Profile");
        }




    }
}
