using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Models.Entities;
using Repository.Interfaces;
using Utils;

namespace Repository.Repositories
{
    public class PostagemRepository : IPostagemRepository
    {
        private readonly IConfiguration _configuration;
        private string caminho;

        public PostagemRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            caminho = _configuration.GetSection("CaminhoJsonPostagens").Value;
        }

        public List<Postagem> CarregarPostagens(List<Guid> postagemIds)
        {
            var resposta = new List<Postagem>();
            var postagens = JsonUtil.CarregarEntidadeJson<Postagem>(caminho);

            foreach (var postagem in postagens)
            {
                foreach (var postagemId in postagemIds)
                {
                    if (postagem.Id == postagemId)
                        resposta.Add(postagem);
                }
            }

            return resposta;
        }

        public Postagem CarregarPostagem(Guid id)
        {
            var postagens = JsonUtil.CarregarEntidadeJson<Postagem>(caminho);
            return postagens.FirstOrDefault(e => e.Id == id);
        }

        public void AdicionarPostagem(Postagem postagem)
        {
            var postagens = JsonUtil.CarregarEntidadeJson<Postagem>(caminho);
            postagens.Add(postagem);
            JsonUtil.EscreverEntidadeJson<Postagem>(postagens, caminho);
        }

        public void RemoverPostagem(Guid id)
        {
            var postagensAtualizadas = new List<Postagem>();
            var postagens = JsonUtil.CarregarEntidadeJson<Postagem>(caminho);
            foreach (var postagem in postagens)
            {
                if (postagem.Id != id)
                    postagensAtualizadas.Add(postagem);
            }

            JsonUtil.EscreverEntidadeJson<Postagem>(postagensAtualizadas, caminho);
        }
    }
}