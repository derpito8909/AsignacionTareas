using System.Security.Authentication;
using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using tareasAPI.Data;
using tareasAPI.Models;
using tareasAPI.Services;
using tareasAPI.Services.interfaces;

namespace tareasAPI.controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TareaController : ControllerBase
{
    private readonly IRepository<Tarea> _repository;

    public TareaController(IRepository<Tarea> repository)
    {
        _repository = repository;
    }

    // Crear nueva tarea
    [Authorize(Roles = "Admin, Supervisor")]
    [HttpPost]
    public async Task<IActionResult> CrearTarea([FromBody] TareaDto tareaDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var tarea = new Tarea
            {
                Titulo = tareaDto.Titulo,
                Descripcion = tareaDto.Descripcion,
                FechaCreacion = DateTime.UtcNow,
                Estado = "Pendiente",
                UsuarioId = tareaDto.UsuarioId
            };
            await _repository.AddAsync(tarea);
            return Ok("Tarea creada exitosamente.");
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
        catch (AuthenticationRequiredException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message });
        }


    }

    // Obtener todas las tareas
    [Authorize(Roles = "Admin, Supervisor")]
    [HttpGet]
    public async Task<IActionResult> ObtenerTareas()
    {

        try
        {
            var tareas = await _repository.GetAllAsync();
            return Ok(tareas);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
        catch (AuthenticationRequiredException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message });
        }

    }

    // Actualizar tarea
    [Authorize(Policy = "RequireSupervisorRole")]
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarTarea(int id, [FromBody] TareaDto tareaDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var tarea = await _repository.GetByIdAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }
            tarea.Titulo = tareaDto.Titulo;
            tarea.Descripcion = tareaDto.Descripcion;
            tarea.Estado = tareaDto.Estado;

            await _repository.UpdateAsync(tarea);
            return Ok("Tarea actualizada exitosamente.");
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
        catch (AuthenticationRequiredException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message });
        }

    }

    // Eliminar tarea
    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarTarea(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var tarea = await _repository.DeleteAsync(id);

            if (!tarea)
            {
                return NotFound();
            }
            return Ok("Tarea eliminada exitosamente.");
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
        catch (AuthenticationRequiredException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message });
        }

    }

    // Actualizar estado de tarea (Empleado actualiza el estado)
    [Authorize(Policy = "RequireEmployeeRole")]
    [HttpPut("actualizar-estado/{id}")]
    public async Task<IActionResult> ActualizarEstadoTarea(int id, [FromBody] string estado)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var tarea = await _repository.GetByIdAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }
            tarea.Estado = estado;
            await _repository.UpdateAsync(tarea);
            return Ok("Estado de la tarea actualizado.");
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
        catch (AuthenticationRequiredException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message });
        }

    }
}
