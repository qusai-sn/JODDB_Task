using System.Web.Mvc;
using Application.UseCases;
using Infrastructure.Repositories;
using Core.DTOs;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly AddUserUseCase _addUserUseCase;
        private readonly GetAllUsersUseCase _getAllUsersUseCase;

        public UserController()
        {
            var userRepository = new UserRepository();   

            _addUserUseCase = new AddUserUseCase(userRepository);
            _getAllUsersUseCase = new GetAllUsersUseCase(userRepository);
        }




        // GET: User
        public ActionResult Index()
        {
            var records = _getAllUsersUseCase.Execute();
            return View(records);
        }





        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                _addUserUseCase.Execute(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }
    }
}
