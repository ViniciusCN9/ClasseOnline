using System;
using System.Collections.Generic;
using Models.Entities;

namespace Repository.Interfaces
{
    public interface IAnexoRepository
    {
        List<Anexo> CarregarAnexos(List<Guid> anexoIds);
        Anexo CarregarAnexo(Guid id);
        void AdicionarAnexo(Anexo anexo);
        void RemoverAnexo(Guid id);
    }
}