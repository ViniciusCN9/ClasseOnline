using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Models.Entities;
using Repository.Interfaces;
using Utils;

namespace Repository.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration _configuration;
        private string caminho;

        public UsuarioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            caminho = _configuration.GetSection("CaminhoJsonUsuarios").Value;
        }

        public Usuario BuscarUsuario(string nome, string senha)
        {
            var usuarios = JsonUtil.CarregarEntidadeJson<Usuario>(caminho);
            return usuarios.Find(e => e.Nome == nome && e.Senha == senha);
        }

        public List<string> BuscarClassses(Guid usuarioId)
        {
            var usuarios = JsonUtil.CarregarEntidadeJson<Usuario>(caminho);
            var usuario = usuarios.FirstOrDefault(e => e.Id == usuarioId);
            return usuario.Classes;
        }

        public bool VerificarClasse(string codigo, Guid usuarioId)
        {
            var resposta = false;
            var usuarios = JsonUtil.CarregarEntidadeJson<Usuario>(caminho);
            foreach (var usuario in usuarios)
            {
                if (usuario.Id == usuarioId)
                    resposta = usuario.Classes.Contains(codigo);

            }
            return resposta;
        }

        public void RegistrarClasse(string codigo, Guid usuarioId)
        {
            var usuariosAtualizados = new List<Usuario>();
            var usuarios = JsonUtil.CarregarEntidadeJson<Usuario>(caminho);
            foreach (var usuario in usuarios)
            {
                if (usuario.Id == usuarioId)
                    usuario.Classes.Add(codigo);

                usuariosAtualizados.Add(usuario);
            }

            JsonUtil.EscreverEntidadeJson<Usuario>(usuariosAtualizados, caminho);
        }

        public void DesregistrarClasse(string codigo)
        {
            var usuariosAtualizados = new List<Usuario>();
            var usuarios = JsonUtil.CarregarEntidadeJson<Usuario>(caminho);
            foreach (var usuario in usuarios)
            {
                if (usuario.Classes.Contains(codigo))
                    usuario.Classes.Remove(codigo);

                usuariosAtualizados.Add(usuario);
            }

            JsonUtil.EscreverEntidadeJson<Usuario>(usuariosAtualizados, caminho);
        }
    }
}