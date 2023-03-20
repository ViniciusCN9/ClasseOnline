using System;
using System.Collections.Generic;
using Models.Entities;
using Repository.Interfaces;
using Service.Interfaces;
using Utils;

namespace Service.Services
{
    public class ClasseService : IClasseService
    {
        private readonly IClasseRepository _classeRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ClasseService(IClasseRepository classeRepository, IUsuarioRepository usuarioRepository)
        {
            _classeRepository = classeRepository;
            _usuarioRepository = usuarioRepository;
        }

        public List<Classe> CarregarClasses(Guid usuarioId)
        {
            var codigos = _usuarioRepository.BuscarClassses(usuarioId);
            return _classeRepository.CarregarClasses(codigos);
        }

        public Classe CarregarClasse(string codigo, Guid usuarioId)
        {
            var resposta = new Classe();
            if (_usuarioRepository.VerificarClasse(codigo, usuarioId))
                resposta = _classeRepository.CarregarClasse(codigo);

            return resposta;
        }

        public Classe CriarClasse(string nome, Guid usuarioId)
        {
            var codigo = GeradorCodigoClasseUtil.GerarCodigoClasse();
            var classe = new Classe() { Codigo = codigo, Nome = nome };

            _classeRepository.CriarClasse(classe);
            _usuarioRepository.RegistrarClasse(codigo, usuarioId);

            return classe;
        }

        public bool RegistrarAluno(string codigo, Guid usuarioId)
        {
            if (_usuarioRepository.VerificarClasse(codigo, usuarioId))
                return false;

            _usuarioRepository.RegistrarClasse(codigo, usuarioId);
            return true;
        }

        public void AtualizarClasse(string codigo, string nome)
        {
            var classe = _classeRepository.CarregarClasse(codigo);
            classe.Nome = nome;
            _classeRepository.AtualizarClasse(classe);
        }

        public void RemoverClasse(string codigo)
        {
            _usuarioRepository.DesregistrarClasse(codigo);
            _classeRepository.RemoverClasse(codigo);
        }
    }
}