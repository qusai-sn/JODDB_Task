using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Core.DTOs;
using Infrastructure;
using Infrastructure;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly joddbEntities _context;

        public AdminController()
        {
            _context = new joddbEntities();
        }

        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAdmin(AddAdminUserDTO adminDto)
        {

            if (_context.AdminUsers.Any(a => a.Email == adminDto.Email))
            {
                ViewBag.ErrorMessage = "Admin with this email already exists.";
                return View();
            }


            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(adminDto.Password, out passwordHash, out passwordSalt);


            var newAdmin = new AdminUser
            {
                Email = adminDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedAt = DateTime.UtcNow
            };


            _context.AdminUsers.Add(newAdmin);
            _context.SaveChanges();

            ViewBag.SuccessMessage = "Admin added successfully!";
            return View();
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
