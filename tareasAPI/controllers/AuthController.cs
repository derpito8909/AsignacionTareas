using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tareasAPI.Data;
using tareasAPI.Models;
using tareasAPI.Services.interfaces;

namespace tareasAPI.controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var usuario = new Usuario
            {
                NombreCompleto = registerDto.NombreCompleto,
                UserName = registerDto.Email,
                Email = registerDto.Email,
                Rol = registerDto.Rol
            };

            await _authService.RegisterAsync(usuario, registerDto.Password);
            return Ok("Usuario registrado exitosamente");
        }
        catch (System.Exception ex)
        {

            return StatusCode(500, new { Message = ex.Message });
        }

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var token = await _authService.LoginAsync(loginDto.Email, loginDto.Password);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message });
        }
    }

}
