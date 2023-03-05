using System.Collections.Generic;
using Models.Entities;

namespace Repository.Interfaces
{
    public interface IClasseRepository
    {
        List<Classe> CarregarClasses(List<string> codigos);
        void CriarClasse(Classe classe);
        void AtualizarClasse(string codigo, string nome);
        void RemoverClasse(string codigo);
    }
}