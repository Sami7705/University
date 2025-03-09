using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityModel.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }=string.Empty;
    }
}
