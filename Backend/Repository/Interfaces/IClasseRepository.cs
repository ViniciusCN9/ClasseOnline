using System;
using System.Collections.Generic;
using Models.Entities;

namespace Repository.Interfaces
{
    public interface IClasseRepository
    {
        List<Classe> CarregarClasses(List<string> codigos);
        Classe CarregarClasse(string codigo);
        void CriarClasse(Classe classe);
        void AtualizarClasse(Classe classe);
        void RemoverClasse(string codigo);
    }
}