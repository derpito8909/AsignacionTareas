using Microsoft.AspNetCore.Identity;
using tareasAPI.Models;

namespace tareasAPI.Services.interfaces;

public interface ISignInManager
{
    Task<SignInResult> CheckPasswordSignInAsync(Usuario user, string password, bool lockoutOnFailure);
}
