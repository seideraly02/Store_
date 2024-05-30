using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Store.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MinLength(3,ErrorMessage = "Минимальная длина 3 символа")]
        [Remote("CheckNameCategory","Validation",ErrorMessage = "Категория уже существует")]

        public string Name { get; set; }

        
    }
}