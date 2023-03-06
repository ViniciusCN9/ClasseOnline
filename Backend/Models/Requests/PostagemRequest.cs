using System;
using System.Collections.Generic;

namespace Models.Requests
{
    public class PostagemRequest
    {
        public Guid Usuario { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public List<Guid> Anexos { get; set; }
    }
}