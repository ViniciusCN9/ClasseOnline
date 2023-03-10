using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Models.Entities;
using Newtonsoft.Json;
using Repository.Interfaces;
using Utils;

namespace Repository.Repositories
{
    public class ClasseRepository : IClasseRepository
    {
        private readonly IConfiguration _configuration;
        private string caminho;

        public ClasseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            caminho = _configuration.GetSection("CaminhoJsonClasses").Value;
        }

        public List<Classe> CarregarClasses(List<string> codigos)
        {
            var resposta = new List<Classe>();
            var classes = JsonUtil.CarregarEntidadeJson<Classe>(caminho);

            foreach (var classe in classes)
            {
                foreach (var codigo in codigos)
                {
                    if (classe.Codigo == codigo)
                        resposta.Add(classe);
                }
            }

            return resposta;
        }

        public Classe CarregarClasse(string codigo)
        {
            var resposta = new Classe();
            var classes = JsonUtil.CarregarEntidadeJson<Classe>(caminho);
            foreach (var classe in classes)
            {
                if (classe.Codigo == codigo)
                    resposta = classe;
            }

            return resposta;
        }

        public void CriarClasse(Classe classe)
        {
            var classes = JsonUtil.CarregarEntidadeJson<Classe>(caminho);
            classes.Add(classe);
            JsonUtil.EscreverEntidadeJson<Classe>(classes, caminho);
        }

        public void AtualizarClasse(Classe classeAtualizada)
        {
            var classesAtualizadas = new List<Classe>();
            var classes = JsonUtil.CarregarEntidadeJson<Classe>(caminho);
            foreach (var classe in classes)
            {
                if (classe.Codigo == classeAtualizada.Codigo)
                {
                    classe.Nome = classeAtualizada.Nome;
                    classe.Postagens = classeAtualizada.Postagens;
                    classe.Atividades = classeAtualizada.Atividades;
                }

                classesAtualizadas.Add(classe);
            }

            JsonUtil.EscreverEntidadeJson<Classe>(classesAtualizadas, caminho);
        }

        public void RemoverClasse(string codigo)
        {
            var classesAtualizadas = new List<Classe>();
            var classes = JsonUtil.CarregarEntidadeJson<Classe>(caminho);
            foreach (var classe in classes)
            {
                if (classe.Codigo != codigo)
                    classesAtualizadas.Add(classe);
            }

            JsonUtil.EscreverEntidadeJson<Classe>(classesAtualizadas, caminho);
        }
    }
}