using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Models.Entities;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class AnexoService : IAnexoService
    {
        private readonly IPostagemRepository _postagemRepository;
        private readonly IAnexoRepository _anexoRepository;
        private readonly IConfiguration _configuration;
        private string caminho;

        public AnexoService(IPostagemRepository postagemRepository, IAnexoRepository anexoRepository, IConfiguration configuration)
        {
            _postagemRepository = postagemRepository;
            _anexoRepository = anexoRepository;
            _configuration = configuration;
            caminho = _configuration.GetSection("CaminhoS3BucketPostagens").Value;
        }

        public List<Anexo> CarregarAnexos(Guid idPostagem)
        {
            var postagens = _postagemRepository.CarregarPostagem(idPostagem);
            return _anexoRepository.CarregarAnexos(postagens.Anexos);
        }

        public async void AdicionarAnexo(IFormFile arquivo)
        {
            var nome = arquivo.FileName;
            var url = Path.Combine(caminho, $"{Guid.NewGuid().ToString()}{Path.GetExtension(arquivo.FileName)}");
            using (var stream = File.Create(url))
            {
                await arquivo.CopyToAsync(stream);
            }

            var novoAnexo = new Anexo()
            {
                Nome = nome,
                Url = url
            };

            _anexoRepository.AdicionarAnexo(novoAnexo);
        }

        public void RemoverAnexo(Guid id)
        {
            var anexo = _anexoRepository.CarregarAnexo(id);
            if (File.Exists(anexo.Url))
                File.Delete(anexo.Url);

            _anexoRepository.RemoverAnexo(id);
        }
    }
}