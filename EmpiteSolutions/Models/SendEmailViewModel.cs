using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmpiteSolutions.Models
{
    public class SendEmailViewModel
    {
        [EmailAddress]
        [Required]
        public string ToEmail { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string EmailBody { get; set; }

    }
}
