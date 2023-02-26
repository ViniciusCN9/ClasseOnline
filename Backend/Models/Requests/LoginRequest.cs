using Models.Enum;

namespace Models.Requests
{
    public class LoginRequest
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}