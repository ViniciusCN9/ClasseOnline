using System.Collections.Generic;
using Models.Entities;
using Repository.Interfaces;
using Service.Interfaces;

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

    }
}