using System.Collections.Generic;
using Models.Entities;

namespace Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario BuscarUsuario(string nome, string senha);
        List<string> BuscarClassses(int usuarioId);
        void RegistrarClasse(string codigo, int usuarioId);
        void DesregistrarClasse(string codigo);
    }
}