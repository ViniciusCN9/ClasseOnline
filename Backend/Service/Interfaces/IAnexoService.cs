using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Models.Entities;

namespace Service.Interfaces
{
    public interface IAnexoService
    {
        List<Anexo> CarregarAnexos(Guid idPostagem);
        Anexo CarregarAnexo(Guid id);
        void AdicionarAnexo(IFormFile arquivo);
        void RemoverAnexo(Guid id);
    }
}