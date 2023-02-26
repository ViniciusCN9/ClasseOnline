using Models.Enum;

namespace Models.Response
{
    public class LoginResponse
    {
        public bool Sucesso { get; set; }
        public string Usuario { get; set; }
        public Role Funcao { get; set; }
        public string Token { get; set; }
    }
}