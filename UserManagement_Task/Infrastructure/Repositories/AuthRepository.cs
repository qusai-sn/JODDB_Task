using System;
using System.Linq;
using System.Security.Cryptography;
using Core.DTOs;
using Core.Interfaces;
using Infrastructure;

namespace Infrastructure.Repositories
{
    public class AuthRepository : IAuthService
    {

        private readonly joddbEntities _context;


        public AuthRepository(joddbEntities context)
        {
            _context = context;
        }


        public (bool Success, string Message, int AdminID) AuthenticateAdmin(AdminLoginDTO loginDto)
        {

            var admin = _context.AdminUsers.FirstOrDefault(a => a.Email == loginDto.Email);

            if (admin == null)
                return (false, "Admin not found.", 0);


            if (!VerifyPassword(loginDto.Password, admin.PasswordHash, admin.PasswordSalt))
                return (false, "Invalid password.", 0);


            return (true, "Login successful.", admin.AdminID);


        }

        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA256(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}
