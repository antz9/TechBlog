using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechBlogWebAPI.Models
{
    public class ProfileModels
    {
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Organization { get; set; }
        public string City { get; set; }        
        public string College { get; set; }        
        public string AboutMe { get; set; }        
        public string Sex { get; set; }
    }
}