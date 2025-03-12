using Core.DTOs;
using Core.Interfaces;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthService _authRepository;

        public AuthService(IAuthService authRepository)
        {
            _authRepository = authRepository;
        }

        public (bool Success, string Message, int AdminID) AuthenticateAdmin(AdminLoginDTO loginDto)
        {
            return _authRepository.AuthenticateAdmin(loginDto);
        }
    }
}
