using Models.Requests;
using Models.Response;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        public LoginResponse Login(LoginRequest request)
        {
            var usuario = _usuarioRepository.BuscarUsuario(request.Usuario, request.Senha);
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