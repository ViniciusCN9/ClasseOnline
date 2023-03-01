using System.Collections.Generic;
using Models.Entities;

namespace Service.Interfaces
{
    public interface IClasseService
    {
        List<Classe> CarregarClasses(int usuarioId);
    }
}