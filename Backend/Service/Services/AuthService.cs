using Models.Requests;
using Models.Response;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public LoginResponse Login(LoginRequest request)
        {
            var usuario = _authRepository.BuscarUsuario(request.Usuario, request.Senha);
            if (usuario is null)
                return new LoginResponse() { Sucesso = false };

            var token = _tokenService.GerarToken(usuario);

            return new LoginResponse()
            {
                Sucesso = true,
                Usuario = usuario.Nome,
                Funcao = usuario.Funcao,
                Token = token
            };
        }
    }
}