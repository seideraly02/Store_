using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Store.ModelViews
{
    public class RegisterModelView
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.EmailAddress)]
        [Remote("CheckEmailUser","Validation",ErrorMessage = "Такой Email уже зарегистрирован")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage = "Длина пароля должна быть не менее 8 символов и не более 72 символов!")]
        [MaxLength(72,ErrorMessage = "Длина пароля должна быть не менее 6 символов и не более 72 символов!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage = "Длина пароля должна быть не менее 8 символов и не более 72 символов!")]
        [MaxLength(72,ErrorMessage = "Длина пароля должна быть не менее 6 символов и не более 72 символов!")]
        [Compare("Password",ErrorMessage = "Пароль не совпадает")]
        public string ConfigPassword { get; set; }
    }
}