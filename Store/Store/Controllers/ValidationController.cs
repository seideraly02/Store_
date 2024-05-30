using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Store.Models;

namespace Store.Controllers
{
    public class ValidationController : Controller
    {
        private StoreContext _db;

        public ValidationController(StoreContext db)
        {
            _db = db;
        }

        // GET
        public bool CheckNameBrand(string name)
        {
            return !_db.Brands.Any(b => b.Name.ToUpper() == name.ToUpper());
        }
        public bool CheckNameCategory(string name)
        {
            return !_db.Categories.Any(c => c.Name.ToUpper() == name.ToUpper());
        }
        public bool CheckEmailUser(string email)
        {
            return !_db.Users.Any(c => c.Email.ToUpper() == email.ToUpper());
        }
    }
}