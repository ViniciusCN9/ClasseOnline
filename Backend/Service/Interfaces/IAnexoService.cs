using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models.Entities;

namespace Service.Interfaces
{
    public interface IAnexoService
    {
        List<Anexo> CarregarAnexos(Guid idPostagem);
        Anexo CarregarAnexo(Guid id);
        Task<List<Anexo>> AdicionarAnexo(IFormFileCollection arquivos);
        void RemoverAnexo(Guid id);
    }
}