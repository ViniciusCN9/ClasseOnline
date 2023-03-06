using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public class Postagem : Base
    {
        public long Timestamp { get; set; }
        public Guid Usuario { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public List<Guid> Anexos { get; set; }
    }
}