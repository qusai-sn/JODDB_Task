using System.Web.Mvc;
using Core.DTOs;
using Application.Services;
using Core.Interfaces;
using Infrastructure.Repositories;
using Infrastructure;
using System;
using Infrastructure;

namespace YourNamespace.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController()
        {
            _authService = new AuthService(new AuthRepository(new joddbEntities()));
        }


        [HttpGet]
        public ActionResult AdminLogin()
        {
          
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(AdminLoginDTO loginDto)
        {

            var result = _authService.AuthenticateAdmin(loginDto);

            if (result.Success)
            {
                Session["AdminID"] = result.AdminID;
                Session["AdminEmail"] = loginDto.Email;
                return RedirectToAction("Index", "User");
            }

            ViewBag.ErrorMessage = result.Message;
            return View();
        }


        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("AdminLogin");
        }


    }
}
