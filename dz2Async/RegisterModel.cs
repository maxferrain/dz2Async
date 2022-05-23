using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kursovaya_work.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Required field")]
        [DisplayName("Login")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DisplayName("LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password should consist of not less 8 symbols, 1 number, 1 upper and 1 lower letter")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Required field")]
        [DisplayName("Confirm password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password should consist of not less 8 symbols, 1 number, 1 upper and 1 lower letter")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Required field,")]
        [DisplayName("Passport")]
        [RegularExpression("^1?(\\d{10})", ErrorMessage = "Passport should consist of 10 numbers")]
        public string Passport { get; set; }

        [DisplayName("Download avatar")]
        [DataType(DataType.Upload)]
        public IFormFile Avatar { get; set; }
    }
}
