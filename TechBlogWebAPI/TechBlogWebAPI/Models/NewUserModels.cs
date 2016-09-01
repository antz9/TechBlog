using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TechBlogWebAPI.Models
{
    public class NewUserModels
    {
        public byte[] ProfilePicture { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string  EmailId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string  ChoosePassword { get; set; }        
    }
}