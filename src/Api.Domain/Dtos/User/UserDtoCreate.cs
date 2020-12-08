using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class UserDtoCreate
    {
       [Required( ErrorMessage = "Nome é campo obrigátorio")]
       [StringLength(60, ErrorMessage = "Nome deve ter no máximno {1} caracteres")]
       public string Nome { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Email deve ter no máximo {1} caracteres")]
        [EmailAddress(ErrorMessage ="Email inválido")]
       public string Email { get; set; }
    }
}
