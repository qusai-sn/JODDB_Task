using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.DTOs;
using Core.Interfaces;

namespace Application.UseCases
{
	public class AddUserUseCase
	{
        private readonly IUserRepository _userRepository;
        
        public AddUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(UserDTO user)
        {
            _userRepository.Add(user);
        }


    }
}