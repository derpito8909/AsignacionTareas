using Microsoft.AspNetCore.Identity;
using tareasAPI.Models;
using tareasAPI.Services.interfaces;

namespace tareasAPI.Services;

public class CustomSignInManager : ISignInManager
{
    private readonly SignInManager<Usuario> _signInManager;

    public CustomSignInManager(SignInManager<Usuario> signInManager)
    {
        _signInManager = signInManager;
    }
    public Task<SignInResult> CheckPasswordSignInAsync(Usuario user, string password, bool lockoutOnFailure)
    {
        return _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
    }
}
