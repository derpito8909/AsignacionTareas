using tareasAPI.Models;

namespace tareasAPI.Services.interfaces;

public interface IAuthService
{
    Task<Usuario> RegisterAsync(Usuario usuario, string password);
    Task<string> LoginAsync(string email, string password);
    Task<Usuario> GetUserByEmailAsync(string userEmail);
}
