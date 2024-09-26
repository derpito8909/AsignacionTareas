using System.ComponentModel.DataAnnotations;

namespace tareasAPI.Data;

public class UsuarioDto
{
    public string NombreCompleto { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }
    public string Rol { get; set; }
}
