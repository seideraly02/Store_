using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Store.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MinLength(3,ErrorMessage = "Минимальная длина 3 символа")]
        [Remote("CheckNameBrand","Validation",ErrorMessage = "Такой бренд уже существует")]
        public string Name { get; set; }
        
    }
}