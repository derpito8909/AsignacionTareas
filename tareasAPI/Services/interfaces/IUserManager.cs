using Microsoft.AspNetCore.Identity;
using tareasAPI.Models;

namespace tareasAPI.Services.interfaces;

public interface IUserManager
{
    Task<Usuario> FindByIdAsync(string id);

    Task<Usuario> FindByEmailAsync(string email);
    Task<IdentityResult> CreateAsync(Usuario user, string password);
    Task<IdentityResult> AddToRoleAsync(Usuario user, string role);

    Task<IList<string>> GetRolesAsync(Usuario user);

    Task<List<Usuario>> Users();

    Task<IdentityResult> DeleteAsync(Usuario user);
}
