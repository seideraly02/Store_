using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MinLength(3,ErrorMessage = "Минимальная длина наименования товара 3 символа")]
        [MaxLength(30,ErrorMessage = "Максимальная длина наименования товара 30 символав")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MinLength(3,ErrorMessage = "Минимальная длина наименования товара 3 символа")]
        [MaxLength(30,ErrorMessage = "Максимальная длина наименования товара 30 символав")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}