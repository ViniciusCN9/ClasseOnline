using System.Collections.Generic;
using Models.Entities;
using Repository.Interfaces;
using Service.Interfaces;
using Service.Utils;

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

        public List<Classe> CarregarClasses(int usuarioId)
        {
            var codigos = _usuarioRepository.BuscarClassses(usuarioId);
            return _classeRepository.CarregarClasses(codigos);
        }

        public Classe CriarClasse(string nome)
        {
            var codigo = GeradorCodigoClasseUtil.GerarCodigoClasse();
            var classe = new Classe() { Codigo = codigo, Nome = nome };

            _classeRepository.CriarClasse(classe);

            return classe;
        }
    }
}