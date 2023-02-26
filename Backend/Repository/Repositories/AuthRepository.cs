using System;
using System.Collections.Generic;
using System.IO;
using Models.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        public Usuario BuscarUsuario(string nome, string senha)
        {
            var usuarios = CarregarUsuariosJson();
            return usuarios.Find(e => e.Nome == nome && e.Senha == senha);
        }

        private List<Usuario> CarregarUsuariosJson()
        {
            var caminhoJson = @"C:\dev\ClasseOnline\Backend\Repository\Mocks\DynamoDbTable\usuarios.json";
            using (StreamReader streamReader = new StreamReader(caminhoJson))
            {
                var json = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Usuario>>(json);
            }
        }
    }
}