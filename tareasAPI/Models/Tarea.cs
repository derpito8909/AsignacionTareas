namespace tareasAPI.Models;
using System.ComponentModel.DataAnnotations;

public class Tarea
{
    [Key]
    public int IdTarea { get; set; }

    [StringLength(50, ErrorMessage = "El {0} no puede exceder los {1} caracteres. ")]
    public string Titulo { get; set; }

    [StringLength(250, ErrorMessage = "La {0} no puede exceder los {1} caracteres. ")]
    public string? Descripcion { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime FechaCreacion { get; set; }

    [StringLength(20, ErrorMessage = "El {0} no puede exceder los {1} caracteres. ")]
    public string Estado { get; set; }

    public string UsuarioId { get; set; }

    public Usuario Usuario { get; set; }
}
