namespace tareasAPI.Models;
using System.ComponentModel.DataAnnotations;

public class Rol
{
    [Key]
    public int IdRol { get; set; }

    [Required]
    [StringLength(100)]
    public required string Nombre { get; set; }

}
