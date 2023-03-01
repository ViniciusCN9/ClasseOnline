using Models.Entities;

namespace Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario BuscarUsuario(string nome, string senha);
        string[] BuscarClassses(int usuarioId);
    }
}