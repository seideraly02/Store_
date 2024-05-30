using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Store.Models;

namespace Store.ModelViews
{
    public class ProductModelView
    {
        public Product Product { get; set; }
        public IFormFile FormFile { get; set; }
    }
}