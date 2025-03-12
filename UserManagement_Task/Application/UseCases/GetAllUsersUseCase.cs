using Core.DTOs;
using Core.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.UseCases
{
	public class GetAllUsersUseCase
	{
         private readonly IUserRepository _userRepository;

        public GetAllUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public List<UserDTO> Execute()
        {
            return _userRepository.GetAll();
        }


    }

}