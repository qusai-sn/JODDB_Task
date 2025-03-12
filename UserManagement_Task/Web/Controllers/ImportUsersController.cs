using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;  // ✅ Required for Stopwatch
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using Infrastructure;

namespace Web.Controllers
{
    public class ImportUsersController : Controller
    {
        private readonly joddbEntities _context;

        public ImportUsersController()
        {
            _context = new joddbEntities();
        }

        // GET: ImportUsers
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportUsers(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                ViewBag.ErrorMessage = "Please select a valid Excel file.";
                return View("Index");
            }

            Stopwatch stopwatch = new Stopwatch(); // ✅ Start Timer
            stopwatch.Start();

            try
            {
                List<User> usersToInsert = new List<User>();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(file.InputStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        ViewBag.ErrorMessage = "Invalid Excel file.";
                        return View("Index");
                    }

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Skip header
                    {
                        string name = worksheet.Cells[row, 2].Text.Trim();
                        string email = worksheet.Cells[row, 3].Text.Trim();
                        string mobileNo = worksheet.Cells[row, 4].Text.Trim();

                        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
                            continue; // Skip invalid rows

                        byte[] passwordHash, passwordSalt;
                        GeneratePasswordHash("DefaultPassword123!", out passwordHash, out passwordSalt);

                        // ✅ Generate a truly unique username
                        string username = GenerateUniqueUsername();

                        usersToInsert.Add(new User
                        {
                            Username = username,
                            Name = name,
                            Email = email,
                            MobileNumber = mobileNo,
                            PasswordHash = passwordHash,
                            PasswordSalt = passwordSalt,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }

                BulkInsertUsers(usersToInsert);

                stopwatch.Stop(); // ✅ Stop Timer
                ViewBag.ImportTime = stopwatch.Elapsed.TotalSeconds.ToString("0.00"); // ✅ Store elapsed time
                ViewBag.SuccessMessage = $"{usersToInsert.Count} users imported successfully in {ViewBag.ImportTime} seconds!";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error importing users: " + ex.Message;
            }

            return View("Index");
        }

        
        private string GenerateUniqueUsername()
        {
            string username;
       
            username = "user_" + Guid.NewGuid().ToString("N").Substring(0, 16); // Generate random username
          

            return username;
        }

         private void GeneratePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        /// ✅ **Efficient Bulk Insert to SQL**
        private void BulkInsertUsers(List<User> users)
        {
            using (SqlConnection conn = new SqlConnection(_context.Database.Connection.ConnectionString))
            {
                conn.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                {
                    bulkCopy.DestinationTableName = "Users";

                    bulkCopy.ColumnMappings.Add("Username", "Username");  
                    bulkCopy.ColumnMappings.Add("Name", "Name");
                    bulkCopy.ColumnMappings.Add("Email", "Email");
                    bulkCopy.ColumnMappings.Add("MobileNumber", "MobileNumber");
                    bulkCopy.ColumnMappings.Add("PasswordHash", "PasswordHash"); 
                    bulkCopy.ColumnMappings.Add("PasswordSalt", "PasswordSalt");
                    bulkCopy.ColumnMappings.Add("CreatedAt", "CreatedAt");

                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("Username", typeof(string)); 
                    dataTable.Columns.Add("Name", typeof(string));
                    dataTable.Columns.Add("Email", typeof(string));
                    dataTable.Columns.Add("MobileNumber", typeof(string));
                    dataTable.Columns.Add("PasswordHash", typeof(byte[]));
                    dataTable.Columns.Add("PasswordSalt", typeof(byte[]));
                    dataTable.Columns.Add("CreatedAt", typeof(DateTime));

                    foreach (var user in users)
                    {
                        dataTable.Rows.Add(user.Username, user.Name, user.Email, user.MobileNumber, user.PasswordHash, user.PasswordSalt, user.CreatedAt);
                    }

                    bulkCopy.WriteToServer(dataTable);
                }
            }
        }
    }
}
