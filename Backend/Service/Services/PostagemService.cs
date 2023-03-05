using System;
using System.Collections.Generic;
using Models.Entities;
using Models.Requests;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class PostagemService : IPostagemService
    {
        private readonly IClasseRepository _classeRepository;
        private readonly IPostagemRepository _postagemRepository;

        public PostagemService(IClasseRepository classeRepository, IPostagemRepository postagemRepository)
        {
            _classeRepository = classeRepository;
            _postagemRepository = postagemRepository;
        }

        public List<Postagem> CarregarPostagens(string codigo)
        {
            var classe = _classeRepository.CarregarClasse(codigo);
            return _postagemRepository.CarregarPostagens(classe.Postagens);
        }

        public void AdicionarPostagem(PostagemRequest request, string codigo)
        {
            var novaPostagem = new Postagem()
            {
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Usuario = request.Usuario,
                Titulo = request.Titulo,
                Texto = request.Texto,
                Anexos = request.Anexos
            };

            var classe = _classeRepository.CarregarClasse(codigo);
            classe.Postagens.Add(novaPostagem.Id);
            _classeRepository.AtualizarClasse(classe);

            _postagemRepository.AdicionarPostagem(novaPostagem);
        }

        public void RemoverPostagem(Guid id, string codigo)
        {
            var classe = _classeRepository.CarregarClasse(codigo);
            classe.Postagens.Remove(id);
            _classeRepository.AtualizarClasse(classe);

            _postagemRepository.RemoverPostagem(id);
        }
    }
}