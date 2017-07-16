using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using RememberMe.Models;

namespace RememberMe.Controllers.Resource
{
    public class FriendResource
    {
        public int Id { get; set; } 
        [Required]
        [StringLength(255)]
        public string Name {get; set; }

    }


}