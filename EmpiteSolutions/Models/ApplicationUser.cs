using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpiteSolutions.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
