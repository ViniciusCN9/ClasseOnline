using System;
using System.Collections.Generic;
using Models.Entities;

namespace Repository.Interfaces
{
    public interface IPostagemRepository
    {
        List<Postagem> CarregarPostagens(List<Guid> postagemIds);
        Postagem CarregarPostagem(Guid id);
        void AdicionarPostagem(Postagem postagem);
        void RemoverPostagem(Guid id);
    }
}