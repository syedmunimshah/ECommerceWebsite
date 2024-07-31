using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebsite.Controllers
{
    public class CustomerController : Controller
    {
        private readonly myContext _myContext;

        public CustomerController(myContext myContext)
        {
            _myContext = myContext;
        }
        public IActionResult Index()
        {
            List <Category> categories= _myContext.tbl_categories.ToList();
            ViewBag.cat=categories;
            ViewBag.checkSession = HttpContext.Session.GetString("Customersession");
            return View();
        }
        public IActionResult CustomerLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CustomerLogin(string customer_email,string customer_password)
        {
            var row  = _myContext.tbl_customer.FirstOrDefault(x=>x.customer_email==customer_email && x.customer_password==customer_password);
            if (row != null)
            {
                HttpContext.Session.SetString("Customersession", row.customer_id.ToString());
                return RedirectToAction("Index");
            }
            else 
            {
                ViewBag.message = "Incorrect User  Or Password";
                return View();
            }

        }
        public IActionResult CustomerRegister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CustomerRegister(Customer customer)
        {
            _myContext.tbl_customer.Add(customer);
            _myContext.SaveChanges();
            return RedirectToAction("CustomerLogin");
          
        }
    }
}
