namespace tareasAPI.Models;
using Microsoft.AspNetCore.Identity;

public class Usuario : IdentityUser
{
    public string NombreCompleto { get; set; }

    public string Rol { get; set; }
    public virtual ICollection<Tarea> Tareas { get; set; }
}
