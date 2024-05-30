using System.ComponentModel.DataAnnotations;

namespace Store.ModelViews
{
    public class LoginModelView
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}