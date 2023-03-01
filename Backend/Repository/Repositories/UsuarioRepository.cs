using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Models.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public Usuario BuscarUsuario(string nome, string senha)
        {
            var usuarios = CarregarUsuariosJson();
            return usuarios.Find(e => e.Nome == nome && e.Senha == senha);
        }

        public string[] BuscarClassses(int usuarioId)
        {
            var usuarios = CarregarUsuariosJson();
            var usuario = usuarios.FirstOrDefault(e => e.Id == usuarioId);
            return usuario.Classes;
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