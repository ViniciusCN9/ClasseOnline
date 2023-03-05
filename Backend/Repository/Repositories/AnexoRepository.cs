using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Models.Entities;
using Repository.Interfaces;
using Utils;

namespace Repository.Repositories
{
    public class AnexoRepository : IAnexoRepository
    {
        private readonly IConfiguration _configuration;
        private string caminho;

        public AnexoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            caminho = _configuration.GetSection("CaminhoJsonAnexos").Value;
        }

        public List<Anexo> CarregarAnexos(List<Guid> anexoIds)
        {
            var resposta = new List<Anexo>();
            var anexos = JsonUtil.CarregarEntidadeJson<Anexo>(caminho);

            foreach (var anexo in anexos)
            {
                foreach (var anexoId in anexoIds)
                {
                    if (anexo.Id == anexoId)
                        resposta.Add(anexo);
                }
            }

            return resposta;
        }

        public Anexo CarregarAnexo(Guid id)
        {
            var anexos = JsonUtil.CarregarEntidadeJson<Anexo>(caminho);
            return anexos.FirstOrDefault(e => e.Id == id);
        }

        public void AdicionarAnexo(Anexo anexo)
        {
            var anexos = JsonUtil.CarregarEntidadeJson<Anexo>(caminho);
            anexos.Add(anexo);
            JsonUtil.EscreverEntidadeJson<Anexo>(anexos, caminho);
        }

        public void RemoverAnexo(Guid id)
        {
            var anexosAtualizados = new List<Anexo>();
            var anexos = JsonUtil.CarregarEntidadeJson<Anexo>(caminho);
            foreach (var anexo in anexos)
            {
                if (anexo.Id != id)
                    anexosAtualizados.Add(anexo);
            }

            JsonUtil.EscreverEntidadeJson<Anexo>(anexosAtualizados, caminho);
        }
    }
}