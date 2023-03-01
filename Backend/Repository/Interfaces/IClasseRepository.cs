using System.Collections.Generic;
using Models.Entities;

namespace Repository.Interfaces
{
    public interface IClasseRepository
    {
        List<Classe> CarregarClasses(string[] codigos);
        string CriarClasse(string nome);
        void AtualizarClasse(string codigo, string nome);
        void RemoverClasse(string codigo);
    }
}