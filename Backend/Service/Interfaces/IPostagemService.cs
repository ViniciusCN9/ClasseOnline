using System;
using System.Collections.Generic;
using Models.Entities;
using Models.Requests;

namespace Service.Interfaces
{
    public interface IPostagemService
    {
        List<Postagem> CarregarPostagens(string codigo);
        void AdicionarPostagem(PostagemRequest request, string codigo);
        void RemoverPostagem(Guid id, string codigo);
    }
}