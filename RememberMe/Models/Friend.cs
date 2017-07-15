using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RememberMe.Models
{
    public class Friend
    {
        public int Id {get; set; }
        [Required]
        [StringLength(255)]
        public string Name {get; set;}
        public ICollection<string> NickNames {get; set; } = new Collection<string>();  
        public DateTime DateCreated { get; set; }
        public DateTime LastTimeUpdated { get; set; }


    }

    public class FriendContactDetails
    {
        public int PhoneNumber { get; set; }  
        [StringLength(255)] 
        public string EmailAddress {get; set; }
    }
}