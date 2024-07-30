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
            return View();
        }
        public IActionResult CustomerLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CustomerLogin(string customer_email,string customer_password)
        {
            _myContext.tbl_customer.FirstOrDefault(x=>x.customer_email==customer_email && x.customer_password==customer_password);
           
            return View();
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
