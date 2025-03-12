using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Core.DTOs;
using Core.Interfaces;

namespace Application.UseCases
{
	public class ImportUsersFromExcelUseCase
	{
        private readonly IUserRepository _userRepository;

        ImportUsersFromExcelUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public void Execute(Stream excelFileStream)
        {
            //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //using var reader = ExcelReaderFactory.CreateReader(excelFileStream);
            //var result = reader.AsDataSet();
            //var table = result.Tables[0];

            //var users = new List<UserDTO>();
            //for (int i = 1; i < table.Rows.Count; i++) // Assuming first row is headers
            //{
            //    var row = table.Rows[i];

            //    var user = new UserDTO
            //    {
            //        Username = row[0].ToString(),
            //        Name = row[1].ToString(),
            //        Email = row[2].ToString(),
            //        MobileNumber = row[3].ToString()
            //    };

            //    users.Add(user);
            //}

            //foreach (var user in users)
            //{
            //    _userRepository.Add(user);
            //}
        }
    }

}




