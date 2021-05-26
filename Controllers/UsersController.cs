
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UserManagmentSystem.DAL;
using UserManagmentSystem.Models;

namespace UserManagmentSystem.Controllers
{


    public class UsersController : Controller
    {

        private readonly SystemContext _context;

        public UsersController(SystemContext context)
        {
            _context = context;
        }


        // GET: Users/Login
        [HttpGet]
        public IActionResult Login()
        {
           
            return View();
        }


        // POST: Users/Login
        [HttpPost]
        public IActionResult Login(IFormCollection frmobj)
        {
           
            string userId = frmobj["userid"];
            User user = _context.Users.Find(userId);
            string password = frmobj["pwd"];

            if (user == null)
            {
                string msg = "User does not exist";

                ViewData["msg"] = msg;
                return View();
            }

            if (userId.Equals(user.UserID) && password.Equals(user.Password))
            {
                TempData["UserID"] = user.UserID;
 
                return RedirectToAction("PersonalAndCompanyDetails", "Users", new {id = user.UserID });
                
            }
            else
            {
                string msg = "User does not exist or password incorrect";

                ViewData["msg"] = msg;
                return View();
            }

        }

        // GET: Users/PersonalAndCompanyDetails/{id}
        [HttpGet]
        public IActionResult PersonalAndCompanyDetails(string id)
        {

           
            User user = _context.Users.Find(id);

            TempData["id"] = id; 
            return View(user);
        }


        // POST: Users/PersonalAndCompanyDetails
        [HttpPost]
        [ActionName("PersonalAndCompanyDetails")]
        public IActionResult PersonalAndCompanyDetailsPost(IFormCollection frmobj)
        {
            var userId = TempData["id"] as string; 
             User userEntity = _context.Users.Find(userId);
           
            userEntity.FirstName = frmobj["firstName"];      
            userEntity.LastName = frmobj["lastName"];
            userEntity.DateOfBirth = frmobj["birthDate"]; 
            userEntity.Email = frmobj["email"];
            userEntity.PhoneNumber = frmobj["phoneNumber"];
            userEntity.LastLoginDate = frmobj["lastLogin"];
            userEntity.CompanyID = frmobj["companyId"];
            userEntity.CompanyName = frmobj["companyName"];

            _context.Users.Update(userEntity);
                _context.SaveChanges();
                return RedirectToAction("BankAccountss", "Users", new { id = userEntity.UserID });
        }

        // GET: Users/BankAccountss/
        [HttpGet]
        public IActionResult BankAccountss(string id)
        {
            User user = _context.Users.Find(id);
          
            return View(user);
        }


        //POST: Users/BankAccountssInsert 
        [HttpPost]
        public IActionResult BankAccountssInsert([FromBody] List<Bank> banks)
        {
            //Truncate Table, delete all old records.

             var rows = from bank in _context.Banks
                             select bank;

               foreach (var bank in rows)
               {
                   _context.Banks.Remove(bank);
               }
               _context.SaveChanges();


               foreach (Bank bank in banks)
               {
                   _context.Banks.Add(bank);
               }
               _context.SaveChanges();


            return RedirectToAction("BankAccountss", "Users", new { id = banks[0].UserID });

        }

    }
}
