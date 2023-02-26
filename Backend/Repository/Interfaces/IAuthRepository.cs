using Models.Entities;

namespace Repository.Interfaces
{
    public interface IAuthRepository
    {
        Usuario BuscarUsuario(string nome, string senha);
    }
}