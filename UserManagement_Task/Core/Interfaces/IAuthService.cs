using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Interfaces
{
	public interface IAuthService
	{
        (bool Success, string Message, int AdminID) AuthenticateAdmin(AdminLoginDTO loginDto);

    }
}