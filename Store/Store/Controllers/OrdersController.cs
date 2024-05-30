using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Controllers
{
    public class OrdersController : Controller
    {
        private StoreContext _db;
        public OrdersController(StoreContext db)
        {
            _db = db;
        }
        [Authorize]
        public IActionResult Index()
        {
            List<Order> orders = 
                _db.Orders.Include(p => p.Product)
                    .ToList();
            return View(orders);
        }
        [Authorize(Roles = "user")]
        public IActionResult Create(int productId)
        {
            Product product = _db.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
                return View(new Order {Product = product});
            return NotFound();
        }
        
        [Authorize]
        [Authorize(Roles = "user")]
        [HttpPost]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _db.Orders.Add(order); 
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nameof(Create));
        }
        [Authorize]
        public IActionResult Remove(int orderId)
        {
            Order order = _db.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                _db.Orders.Remove(order);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}









