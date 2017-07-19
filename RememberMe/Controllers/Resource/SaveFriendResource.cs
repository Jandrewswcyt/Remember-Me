using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using RememberMe.Core.Models;

namespace RememberMe.Controllers.Resource
{
    public class SaveFriendResource
    {
        public int Id { get; set; } 
        [Required]
        [StringLength(255)]
        public string Name {get; set; }
        public ContactDetailsResource ContactDetails { get; set; }
        public DateTime LastUpdated { get; set; }

    }


}