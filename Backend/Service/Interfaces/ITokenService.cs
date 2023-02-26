using Models.Entities;

namespace Service.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
    }
}