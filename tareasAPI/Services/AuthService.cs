using tareasAPI.Models;
using tareasAPI.Services.interfaces;

namespace tareasAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserManager _userManager;
    private readonly ISignInManager _signInManager;

    private readonly ITokenService _tokenService;

    public AuthService(IUserManager userManager, ISignInManager signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<Usuario> RegisterAsync(Usuario usuario, string password)
    {
        var result = await _userManager.CreateAsync(usuario, password);
        if (!result.Succeeded)
            throw new Exception("Registro fallido");

        return usuario;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var usuario = await _userManager.FindByEmailAsync(email);
        if (usuario == null)
            throw new Exception("Usuario no encontrado");

        var result = await _signInManager.CheckPasswordSignInAsync(usuario, password, false);
        if (!result.Succeeded)
            throw new Exception("Credenciales inválidas");

        return await _tokenService.GenerateToken(usuario);
    }

    public async Task<Usuario> GetUserByEmailAsync(string userEmail)
    {
        return await _userManager.FindByEmailAsync(userEmail);
    }
}

