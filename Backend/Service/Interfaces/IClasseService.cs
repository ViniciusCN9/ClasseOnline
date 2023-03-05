using System.Collections.Generic;
using Models.Entities;

namespace Service.Interfaces
{
    public interface IClasseService
    {
        List<Classe> CarregarClasses(int usuarioId);
        Classe CriarClasse(string nome, int usuarioId);
        void AtualizarClasse(string codigo, string nome);
        void RemoverClasse(string codigo);
    }
}