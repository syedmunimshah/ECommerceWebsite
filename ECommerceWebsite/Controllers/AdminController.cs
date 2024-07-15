﻿using ECommerceWebsite.Migrations;
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

    }
}
