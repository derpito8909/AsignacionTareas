using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tareasAPI.Data;
using tareasAPI.Models;
using tareasAPI.Services.interfaces;

namespace tareasAPI.controllers;

[Authorize(Policy = "RequireAdminRole")]
[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUserManager _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsuarioController(IUserManager userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // Crear nuevo usuario
    [HttpPost]
    public async Task<IActionResult> CrearUsuario([FromBody] UsuarioDto usuarioDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var usuario = new Usuario { UserName = usuarioDto.Email, Email = usuarioDto.Email, NombreCompleto = usuarioDto.NombreCompleto };
            var result = await _userManager.CreateAsync(usuario, usuarioDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("Usuario creado exitosamente.");
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

    // Obtener todos los usuarios
    [HttpGet]
    public async Task<IActionResult> ObtenerUsuarios()
    {
        try
        {
            var usuarios = await _userManager.Users();
            return Ok(usuarios);
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

    // Asignar rol a un usuario
    [HttpPut("asignar-rol/{id}")]
    public async Task<IActionResult> AsignarRol(string id, [FromBody] string rol)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            var result = await _userManager.AddToRoleAsync(usuario, rol);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("Rol asignado exitosamente.");
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

    // Eliminar usuario
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarUsuario(string id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(usuario);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("Usuario eliminado exitosamente.");
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
