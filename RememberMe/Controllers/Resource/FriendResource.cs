using System;
using RememberMe.Core.Models;

namespace RememberMe.Controllers.Resource
{
    public class FriendResource
    {
        public int Id {get; set; }
        public string Name {get; set;} 
        public DateTime LastUpdated { get; set; }
        public ContactDetails ContactDetails { get; set; }
    }
}