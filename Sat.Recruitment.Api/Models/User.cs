using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Sat.Recruitment.Api.Utils.Enums;

namespace Sat.Recruitment.Api.Models
{
    public class User
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public UserType UserType { get; set; }
        [Required]
        public decimal Money { get; set; }
    }
}
