using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CappWebServer.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username required.", AllowEmptyStrings = false)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required.", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Senha { get; set; }

        public bool Lembrar { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Senha", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmarSenha { get; set; }
    }

}
