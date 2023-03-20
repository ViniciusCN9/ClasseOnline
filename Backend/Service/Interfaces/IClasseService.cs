using System;
using System.Collections.Generic;
using Models.Entities;

namespace Service.Interfaces
{
    public interface IClasseService
    {
        List<Classe> CarregarClasses(Guid usuarioId);
        Classe CarregarClasse(string codigo, Guid usuarioId);
        Classe CriarClasse(string nome, Guid usuarioId);
        bool RegistrarAluno(string codigo, Guid usuarioId);
        void AtualizarClasse(string codigo, string nome);
        void RemoverClasse(string codigo);
    }
}