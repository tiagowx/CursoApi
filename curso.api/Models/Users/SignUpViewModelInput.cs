using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace curso.api.Models.Users
{
    public class SignUpViewModelInput
    {
        [Required(ErrorMessage = "Esse campo está inválido")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Esse campo está inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Esse campo está inválido")]
        public string Pasword { get; set; }


    }
}