using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using tareasAPI.Models;
using tareasAPI.Services.interfaces;

namespace tareasAPI.Services;

public class CustomUserManager : IUserManager
{
    private readonly UserManager<Usuario> _userManager;

    public CustomUserManager(UserManager<Usuario> userManager)
    {
        _userManager = userManager;
    }

    public Task<Usuario> FindByIdAsync(string id)
    {
        return _userManager.FindByIdAsync(id);
    }

    public Task<Usuario> FindByEmailAsync(string email)
    {
        return _userManager.FindByEmailAsync(email);
    }

    public Task<IdentityResult> CreateAsync(Usuario user, string password)
    {
        return _userManager.CreateAsync(user, password);
    }

    public Task<IdentityResult> AddToRoleAsync(Usuario user, string role)
    {
        return _userManager.AddToRoleAsync(user, role);
    }

    public async Task<IList<string>> GetRolesAsync(Usuario user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public Task<List<Usuario>> Users()
    {
        return _userManager.Users.ToListAsync();
    }

    public Task<IdentityResult> DeleteAsync(Usuario user)
    {
        return _userManager.DeleteAsync(user);
    }
}
