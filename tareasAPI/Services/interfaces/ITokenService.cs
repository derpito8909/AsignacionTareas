using tareasAPI.Models;

namespace tareasAPI.Services.interfaces;

public interface ITokenService
{
    Task<string> GenerateToken(Usuario user);
}
