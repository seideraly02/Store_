using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Store.Models;
using Store.ModelViews;

namespace Store.Controllers
{
    public enum Filter
    {
        NameAsc, 
        NameDesc, 
        BrandAsc,
        BrandDesc,
        DateAsc, 
        DateDesc, 
        CategoryAsc, 
        CategoryDesc, 
        PriceAsc,
        PriceDesc
    }
    public class ProductsController : Controller
    {
        private StoreContext _db;
        private CreateFile _createFile;
        private IHostEnvironment _environment;

        public ProductsController(StoreContext db, CreateFile file, IHostEnvironment environment)
        {
            _db = db;
            _createFile = file;
            _environment = environment;
        }

        // GET
        public async Task<IActionResult> Index(Filter filter = Filter.NameAsc)
        {
            ViewBag.FilterName = filter == Filter.NameAsc ? Filter.NameDesc : Filter.NameAsc;
            ViewBag.FilterBrand =  filter == Filter.BrandAsc ? Filter.BrandDesc : Filter.BrandAsc;
            ViewBag.FilterDate = filter == Filter.DateAsc ? Filter.DateDesc : Filter.DateAsc;
            ViewBag.FilterCategory = filter == Filter.CategoryAsc ? Filter.CategoryDesc : Filter.CategoryAsc;
            ViewBag.FilterPrice = filter == Filter.PriceAsc ? Filter.PriceDesc : Filter.PriceAsc;
            
            IQueryable<Product> products;
            switch (filter)
            {
                case Filter.NameDesc:
                    products = _db.Products.OrderByDescending(p => p.Name);
                    break;
                case Filter.BrandAsc:
                    products = _db.Products.OrderBy(p => p.BrandId);
                    break;
                case Filter.BrandDesc:
                    products = _db.Products.OrderByDescending(p => p.BrandId);
                    break;
                case Filter.DateAsc:
                    products = _db.Products.OrderBy(p => p.CreateDate);
                    break;
                case Filter.DateDesc:
                    products = _db.Products.OrderByDescending(p => p.CreateDate);
                    break;
                case Filter.CategoryAsc:
                    products = _db.Products.OrderBy(p => p.CategoryId);
                    break;
                case Filter.CategoryDesc:
                    products = _db.Products.OrderByDescending(p => p.CategoryId);
                    break;
                case Filter.PriceAsc:
                    products = _db.Products.OrderBy(p => p.Price);
                    break;
                case Filter.PriceDesc:
                    products = _db.Products.OrderByDescending(p => p.Price);
                    break;
                default:
                    products = _db.Products.OrderBy(p => p.Name);
                    break;
            }
            
            return View(await products.AsNoTracking().ToListAsync());
        }
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewBag.Category = _db.Categories;
            ViewBag.Brand = _db.Brands;
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create(ProductModelView model)
        {
            if (ModelState.IsValid)
            {
                string photoPath = "productImages/error.jpg";
                if (model.FormFile != null)
                {
                    string filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/productImages/");
                    photoPath = $"productImages/{model.FormFile.FileName}";
                    _createFile.FileCreat(filePath,model.FormFile.FileName,model.FormFile);
                }
                Product product = new Product
                {
                    Name = model.Product.Name,
                    Price = model.Product.Price,
                    Description = model.Product.Description,
                    PhotoPath = photoPath,
                    CategoryId = model.Product.CategoryId,
                    BrandId = model.Product.BrandId
                };
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }
        

        public IActionResult Details(int id)
        {
            Product product = _db.Products.FirstOrDefault(p => p.Id == id);
            Brand brand = _db.Brands.FirstOrDefault(b => b.Id == product.BrandId);
            Category category = _db.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
            ViewData["brandName"] = brand.Name;
            ViewData["categoryName"] = category.Name;
            ViewData["price12"] = Math.Round(product.Price / 12,2);

            return View(product);
        }
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                Product product = _db.Products
                    .FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    ProductModelView productModelView = new ProductModelView()
                    {
                        Product = product
                    };
                    ViewBag.Category = _db.Categories;
                    ViewBag.Brand = _db.Brands;
                    return View(productModelView);
                }
            }
            return NotFound();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Edit(ProductModelView model)
        {
            if (ModelState.IsValid)
            {
               
                if (model.FormFile != null)
                {
                    string filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/productImages/");
                    string photoPath = $"productImages/{model.FormFile.FileName}";
                    _createFile.FileCreat(filePath,model.FormFile.FileName,model.FormFile);
                    model.Product.PhotoPath = photoPath;
                }
                
                model.Product.UpdateDate = DateTime.Now;
                _db.Products.Update(model.Product);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin")]
        public IActionResult Remove(int id, string key = null)
        {
            Product product = _db.Products.FirstOrDefault(p => p.Id == id);
            _db.Products.Remove(product);
            _db.SaveChanges();
            if (key != null)
            {
                return RedirectToAction("Create");
            }
            return RedirectToAction("Index");
        }
    }

    
    
}