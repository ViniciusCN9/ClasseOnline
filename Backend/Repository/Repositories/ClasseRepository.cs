using System;
using System.Collections.Generic;
using System.IO;
using Models.Entities;
using Newtonsoft.Json;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class ClasseRepository : IClasseRepository
    {
        public List<Classe> CarregarClasses(string[] codigos)
        {
            var resposta = new List<Classe>();
            var classes = CarregarClassesJson();

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

        public void CriarClasse(Classe classe)
        {

        }

        public void AtualizarClasse(string codigo, string nome)
        {
            throw new System.NotImplementedException();
        }

        public void RemoverClasse(string codigo)
        {
            throw new System.NotImplementedException();
        }

        private List<Classe> CarregarClassesJson()
        {
            var caminhoJson = @"C:\dev\ClasseOnline\Backend\Repository\Mocks\DynamoDbTable\classes.json";
            using (StreamReader streamReader = new StreamReader(caminhoJson))
            {
                var json = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Classe>>(json);
            }
        }
    }
}