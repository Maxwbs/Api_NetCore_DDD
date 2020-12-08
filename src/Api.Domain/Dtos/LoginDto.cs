using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    [Serializable]
    public class LoginDto
    {
        [Required(ErrorMessage = "Email é obrigatorio para login.")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(100, ErrorMessage = "Email deve ter no máximo {1} caractere.")]
        public string email { get; set; }
    }
}
