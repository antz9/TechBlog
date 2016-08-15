using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TechBlogWebAPI.Models
{
    public class Login
    {
        [Required] 
        [DataType(DataType.EmailAddress)]       
        public string EmailId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }  
        
            
    }
}