using Models.Enum;

namespace Models.Entities
{
    public class Usuario : Base
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public Role Funcao { get; set; }
        public string[] Classes { get; set; }
    }
}