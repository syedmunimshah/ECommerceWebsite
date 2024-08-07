using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceWebsite.Controllers
{
    public class CustomerController : Controller
    {
        private readonly myContext _myContext;
        private readonly IWebHostEnvironment _env;

        public CustomerController(myContext myContext, IWebHostEnvironment env)
        {
            _myContext = myContext;
            _env = env;
        }
        public IActionResult Index()
        {
            List <Category> categories= _myContext.tbl_categories.ToList();
            ViewBag.cat=categories;
            List <Product> product=_myContext.tbl_product.ToList();
            ViewBag.pro = product;
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
        
       public IActionResult CustomerLogout()
        {
            HttpContext.Session.Remove("Customersession");

            return RedirectToAction("Index");
        }
        public IActionResult CustomerProfile()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Customersession")))
                {
                  return RedirectToAction("CustomerLogin");
            }
            else
            {
                List<Category> categories = _myContext.tbl_categories.ToList();
                ViewBag.cat = categories;
                ViewBag.checkSession = HttpContext.Session.GetString("Customersession");
                var CustomerId = HttpContext.Session.GetString("Customersession");
                var row = _myContext.tbl_customer.FirstOrDefault(x=>x.customer_id==int.Parse(CustomerId));
                return View(row);
            }
        
        }
        [HttpPost]
        public IActionResult CustomerProfile(Customer customer,IFormFile customer_img)
        {


            List<Category> categories = _myContext.tbl_categories.ToList();
            ViewBag.cat = categories;
            var CustomerId = HttpContext.Session.GetString("Customersession");

            if (CustomerId == null)
            {
                return RedirectToAction("CustomerLogin");
            }

            string ImagePath = Path.Combine(_env.WebRootPath, "Customer_images", customer_img.FileName);
            FileStream fs = new FileStream(ImagePath, FileMode.Create);
            customer_img.CopyTo(fs);
            customer.customer_img = customer_img.FileName;

            customer.customer_id = int.Parse(CustomerId);  
            _myContext.tbl_customer.Update(customer);
            _myContext.SaveChanges();

            return View(customer);


        }


        public IActionResult feedback()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Customersession")))
            {
                return RedirectToAction("CustomerLogin");
            }
            else
            {
                List<Category> categories = _myContext.tbl_categories.ToList();
                ViewBag.cat = categories;
                ViewBag.checkSession = HttpContext.Session.GetString("Customersession");
                return View();
            }
        }
        [HttpPost]
        public IActionResult feedback(Feedback feedback)
        {
            List<Category> categories = _myContext.tbl_categories.ToList();
            ViewBag.cat = categories;
            _myContext.tbl_feedback.Add(feedback);
            _myContext.SaveChanges();
             ViewBag.feedback = "Thanks Your Feedback";
            return View();
        }
        //fetchAllProduct
        public IActionResult ProductPage()
        {
            List<Category> categories = _myContext.tbl_categories.ToList();
            ViewBag.cat = categories;
            List<Product> product = _myContext.tbl_product.ToList();
            ViewBag.pro = product; 
            return View();
        }
        public IActionResult productDetails(int id)
        {
            List<Category> categories = _myContext.tbl_categories.ToList();
            ViewBag.cat = categories;
            var prod = _myContext.tbl_product.Where(x=>x.product_id==id).ToList();
            return View(prod);
        }

        public IActionResult AddToCart(dynamic product_id, Cart cart)
        { string login = HttpContext.Session.GetString("Customersession");
           
            if (login!=null) {
             
                cart.prod_id = product_id;
                cart.cust_id = int.Parse(login);
                cart.cart_quantity = 1;
                cart.cart_status = 0;
                _myContext.tbl_cart.Add(cart);
                _myContext.SaveChanges();
                TempData["message"] = "Product Successfully Added in Cart";
                return RedirectToAction("ProductPage");
            }
            else { return RedirectToAction("CustomerLogin"); }
           
        }
    }
}
