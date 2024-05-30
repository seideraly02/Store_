using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MinLength(3,ErrorMessage = "Минимальная длина наименования товара 3 символа")]
        [MaxLength(30,ErrorMessage = "Максимальная длина наименования товара 30 символав")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(50,1000000,ErrorMessage = "Минимальная цена 50 единиц" )]
        public double Price { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MinLength(3,ErrorMessage = "Минимальная длина 5 символа")]
        [MaxLength(250,ErrorMessage = "Максимальная длина 250 символав")]
        public string Description  { get; set; }
        public string PhotoPath { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; } 
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
       
    }
    
}