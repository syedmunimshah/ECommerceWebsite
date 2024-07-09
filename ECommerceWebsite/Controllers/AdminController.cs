using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebsite.Controllers
{

    public class AdminController : Controller
    {
        private readonly myContext _myContext;
        public AdminController(myContext myContext)
        {
                _myContext = myContext;
        }
        public IActionResult Index()
        {
            string admin_session = HttpContext.Session.GetString("admin_session");
            if (admin_session!=null) 
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
        public IActionResult Login(string adminEmail,string adminPassword)
        {
            var row = _myContext.tbl_admin.FirstOrDefault(x => x.admin_email == adminEmail);
            if (row!=null && row.admin_password==adminPassword) {

                HttpContext.Session.SetString("admin_session",row.admin_id.ToString());
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

           var adminId= HttpContext.Session.GetString("admin_session");
            var row = _myContext.tbl_admin.Where(x=>x.admin_id==int.Parse(adminId));

            return View(row);
        }

        [HttpPost]
        public IActionResult Profile(Admin admin)
        {
            _myContext.tbl_admin.Update(admin);
            _myContext.SaveChanges();
            return RedirectToAction("Profile");
        }
    }
}
