using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        UserDTO GetById(int id);
        List<UserDTO> GetAll();
        void Add(UserDTO user);
        void Update(UserDTO user);
        void Delete(int id);
    }

}
