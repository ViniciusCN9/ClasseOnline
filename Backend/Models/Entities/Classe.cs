using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public class Classe
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public List<Guid> Postagens { get; set; }
        public List<Guid> Atividades { get; set; }
    }
}