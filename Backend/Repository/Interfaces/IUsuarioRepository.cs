using System;
using System.Collections.Generic;
using Models.Entities;

namespace Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario BuscarUsuario(string nome, string senha);
        List<string> BuscarClassses(Guid usuarioId);
        bool VerificarClasse(string codigo, Guid usuarioId);
        void RegistrarClasse(string codigo, Guid usuarioId);
        void DesregistrarClasse(string codigo);
    }
}