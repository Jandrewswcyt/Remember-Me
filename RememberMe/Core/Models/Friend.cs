using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RememberMe.Core.Models
{
    public class Friend
    {
        public int Id {get; set; }
        [Required]
        [StringLength(255)]
        public string Name {get; set;} 
        public DateTime LastUpdated { get; set; }
        public ContactDetails ContactDetails { get; set; }


    }

}