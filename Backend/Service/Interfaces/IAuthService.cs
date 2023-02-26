using Models.Requests;
using Models.Response;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        LoginResponse Login(LoginRequest request);
    }
}