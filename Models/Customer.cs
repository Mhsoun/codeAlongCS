using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace codeAlongCS.Models
{
    public class Customer
    {
        public string Id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage ="your string should be 10 char")]
        [DisplayName("Enter your names")]
        public string Name { get; set; }
        public string Telephone { get; set; }
    }
}