
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Models;


namespace Store.Controllers
{
    public class BrandsController : Controller
    {
        StoreContext _db { get; set; }

        public BrandsController(StoreContext db)
        {
            _db = db;
        }
        
        // GET
        
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            ViewBag.brands = _db.Brands.ToList();
            return View();
        }
        
        [HttpPost]
        [ActionName("Index")]
        [Authorize(Roles = "admin")]

        public IActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _db.Brands.Add(brand);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.brands = _db.Brands.ToList();
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult Remove(int id)
        {
            Brand brand = _db.Brands.FirstOrDefault(b => b.Id == id);
            if(brand != null)
            {
                _db.Brands.Remove(brand);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            Brand brand = _db.Brands.FirstOrDefault(b => b.Id == id);
            if (brand != null && brand.Name != null)
            {
                ViewBag.BrandEvent = "Edit";
                ViewBag.brand = brand;
                ViewBag.brands = _db.Brands.ToList();
                return View(nameof(Index));
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _db.Brands.Update(brand);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nameof(Index));
        }
    }
}