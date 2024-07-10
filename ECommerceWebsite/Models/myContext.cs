﻿using Microsoft.EntityFrameworkCore;

namespace ECommerceWebsite.Models
{
    public class myContext:DbContext
    {
        public myContext(DbContextOptions<myContext> options):base(options) 
        {
            
        }
        public DbSet<Admin> tbl_admin { get; set; }
        public DbSet<Customer>tbl_customer { get; set; }
        public DbSet<Category> tbl_categories { get; set; }
        public DbSet<Product> tbl_product { get; set; }
        public DbSet<Cart> tbl_cart { get; set; }
        public DbSet<Feedback> tbl_feedback { get; set; }
        public DbSet<Faqs> tbl_faqs { get; set; }
    }
}