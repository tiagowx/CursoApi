using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Models.Users
{
    public class LoginViewModelInput
    {

        [Required(ErrorMessage = "Esse campo está inválido")]        
        public string Login{get;set;}
        [Required(ErrorMessage = "Esse campo está inválido")]
        public string Password{get;set;}
    }
}