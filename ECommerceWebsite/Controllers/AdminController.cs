using ECommerceWebsite.Migrations;
using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            var row = _myContext.tbl_admin.FirstOrDefault(x => x.admin_id == int.Parse(adminId)); 
            return View(row);
        }



        [HttpPost]
        public IActionResult Profile(Models.Admin admin)
        {
            _myContext.tbl_admin.Update(admin);
            _myContext.SaveChanges();
            return RedirectToAction("Profile");
        }



        [HttpPost]
        public IActionResult ChangeProfileImage(IFormFile admin_image, Models.Admin admin)
        {
            string ImagePath = Path.Combine(_env.WebRootPath,"images",admin_image.FileName);
            FileStream fs = new FileStream(ImagePath,FileMode.Create);
            admin_image.CopyTo(fs);
            admin.admin_image = admin_image.FileName;
            _myContext.tbl_admin.Update(admin);
            _myContext.SaveChanges();
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult getallCustomer()
        {
            return View(_myContext.tbl_customer.ToList());
        }
        [HttpGet]
        public IActionResult customerDetails(int id)
        {
            var cus = _myContext.tbl_customer.FirstOrDefault(x => x.customer_id == id);

            return View(cus);
        }
        public IActionResult updatecustomer(int id)
        {
            var upda= _myContext.tbl_customer.Find(id);
            return View(upda);
        }
        [HttpPost]
        public IActionResult updatecustomer(Customer customer,IFormFile customer_img)
        {
            string ImagePath = Path.Combine(_env.WebRootPath, "Customer_images", customer_img.FileName);
            FileStream fs = new FileStream(ImagePath, FileMode.Create);
            customer_img.CopyTo(fs);
            customer.customer_img = customer_img.FileName;
            _myContext.tbl_customer.Update(customer);
            _myContext.SaveChanges();
            return RedirectToAction("getallCustomer");
        }
        public IActionResult deletecustomer(int id)
        {
            var customer=_myContext.tbl_customer.Find(id);
            _myContext.tbl_customer.Remove(customer);
            _myContext.SaveChanges();
            return RedirectToAction("getallCustomer");
        }
        public IActionResult deletepermission(int id)
        {
            var dele = _myContext.tbl_customer.FirstOrDefault(x=>x.customer_id==id);
            return View(dele);
        }


        public IActionResult fetchCategory()
        {
            return View(_myContext.tbl_categories.ToList());
        }

           public IActionResult addCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult addCategory(Category cat)
        {
            _myContext.tbl_categories.Add(cat);
            _myContext.SaveChanges();
            return RedirectToAction("fetchCategory");
        }

        public IActionResult updateCategory(int id)
        {
            var category=_myContext.tbl_categories.Find(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult updateCategory(Category cat)
        {
            _myContext.tbl_categories.Update(cat);
            _myContext.SaveChanges();
            return RedirectToAction("fetchCategory");
        }
        public IActionResult deletepermissionCategory(int id)
        {
            var dele = _myContext.tbl_categories.FirstOrDefault(x => x.category_id == id);
            return View(dele);
        }
        public IActionResult deleteCategory(int id)
        {
            var category=_myContext.tbl_categories.Find(id);
            _myContext.tbl_categories.Remove(category);
            _myContext.SaveChanges();

            return RedirectToAction("fetchCategory");
        }

        public IActionResult FetchAllProduct()
        {
            return View(_myContext.tbl_product.ToList());
            
        }
        public IActionResult addProduct() {

            ViewBag.cat = new SelectList(_myContext.tbl_categories, "category_id", "category_name");
            return View();
        }
        [HttpPost]
        public IActionResult addProduct(Product product,IFormFile product_image)
        {
           
                string imagePath = Path.Combine(_env.WebRootPath, "Product_images", product_image.FileName);
                FileStream fs = new FileStream(imagePath, FileMode.Create);
                product_image.CopyTo(fs);
                product.product_image = product_image.FileName;

                _myContext.tbl_product.Add(product);
                _myContext.SaveChanges();
                return RedirectToAction("FetchAllProduct");
        }
      
        public IActionResult productDetails(int id)
        {
            var product = _myContext.tbl_product.Include(p=>p.Category).FirstOrDefault(p=>p.product_id==id);
            return View(product);
        }
    
        public IActionResult productdeletepermission(int id)
        {
            var dele = _myContext.tbl_product.FirstOrDefault(x => x.product_id == id);
            return View(dele);
        }
        public IActionResult deleteproduct(int id)
        {
            var category = _myContext.tbl_product.Find(id);
            _myContext.tbl_product.Remove(category);
            _myContext.SaveChanges();

            return RedirectToAction("FetchAllProduct");
        }
        public IActionResult productupdate(int id)
        {
           
            ViewBag.cat = new SelectList(_myContext.tbl_categories, "category_id", "category_name");
            var product = _myContext.tbl_product.Find(id);
            ViewBag.selectedCategoryId = product.cat_id;
            return View(product);
        }
       
        [HttpPost]
        public IActionResult productupdate(Product product,IFormFile product_image)
        {
            string ImagePath = Path.Combine(_env.WebRootPath, "Product_images", product_image.FileName);
            FileStream fs = new FileStream(ImagePath, FileMode.Create);
            product_image.CopyTo(fs);
            product.product_image = product_image.FileName;
            _myContext.tbl_product.Update(product);
            _myContext.SaveChanges();
            return RedirectToAction("FetchAllProduct");
        }

        public IActionResult FetchAllFeedback()
        {
            var feedback = _myContext.tbl_feedback.ToList();
            return View(feedback);

        }
        public IActionResult feedbackdeletepermission(int id)
        {
            var dele = _myContext.tbl_feedback.FirstOrDefault(x => x.feedback_id == id);
            return View(dele);
        }
        public IActionResult deletefeedback(int id)
        {
            var feedback = _myContext.tbl_feedback.Find(id);
            _myContext.tbl_feedback.Remove(feedback);
            _myContext.SaveChanges();

            return RedirectToAction("FetchAllFeedback");
        }
        public IActionResult FetchAllCart()
        {

            var cart = _myContext.tbl_cart.Include(x => x.products).Include(x => x.Customers).ToList();
            return View(cart);

        }
        public IActionResult Cartdeletepermission(int id)
        {
            //var dele = _myContext.tbl_cart.FirstOrDefault(x => x.cart_id == id);
            var dele = _myContext.tbl_cart.Include(x => x.products).Include(x => x.Customers).FirstOrDefault(x=>x.cart_id==id);
            return View(dele);
        }
        public IActionResult deleteCart(int id)
        {
            var cart = _myContext.tbl_cart.Find(id);
            _myContext.tbl_cart.Remove(cart);
            _myContext.SaveChanges();

            return RedirectToAction("FetchAllCart");
        }


    }

}
