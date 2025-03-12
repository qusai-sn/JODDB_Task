using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.DTOs;
using Core.Interfaces;
using Infrastructure;


namespace Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
    {
        private readonly joddbEntities _context = new joddbEntities();


        public UserRepository() { }

        public UserDTO GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == id);

            if (user == null) return null;

            return new UserDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Name = user.Name,
                Email = user.Email,
                MobileNumber = user.MobileNumber,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                ProfilePicture = user.ProfilePicture
            };
        }

        public List<UserDTO> GetAll()
        {
            return _context.Users.Select(user => new UserDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Name = user.Name,
                Email = user.Email,
                MobileNumber = user.MobileNumber,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                ProfilePicture = user.ProfilePicture
            }).ToList();
        }

        public void Add(UserDTO userDto)
        {
            try
            {
                var user = new User
                {
                    Username = userDto.Username,
                    Name = userDto.Name,
                    Email = userDto.Email,
                    MobileNumber = userDto.MobileNumber,

                    PasswordHash = new byte[32],  // Dummy placeholder
                    PasswordSalt = new byte[32],  // Dummy placeholder

                    CreatedAt = DateTime.Now,
                    UpdatedAt = null,
                    ProfilePicture = userDto.ProfilePicture
                };

                _context.Users.Add(user);
                _context.SaveChanges();
            }



            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
                throw;
            }

        }



        public void Update(UserDTO userDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == userDto.UserID);
            if (user == null) return;

            user.Username = userDto.Username;
            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.MobileNumber = userDto.MobileNumber;
            user.UpdatedAt = userDto.UpdatedAt;
            user.ProfilePicture = userDto.ProfilePicture;

            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == id);
            if (user == null) return;

            _context.Users.Remove(user);
            _context.SaveChanges();
        }



    }

}
 