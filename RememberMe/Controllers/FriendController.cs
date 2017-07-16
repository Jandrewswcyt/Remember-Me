using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RememberMe.Models;
using AutoMapper;
using RememberMe.Persistence;
using Microsoft.EntityFrameworkCore;
using RememberMe.Controllers.Resource; 

namespace RememberMe.Controllers
{
    [Route("/api/friends")]
    public class FriendsController : Controller
    {
        private readonly RememberMeDbContext context;
        private readonly IMapper mapper;

        public FriendsController(RememberMeDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;

        }
        [HttpGet]
        public async Task<IEnumerable<FriendResource>> GetFriends()
        {
            var friends = await context.Friends.ToListAsync();
            return mapper.Map<List<Friend>,List<FriendResource>>(friends);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateFriend([FromBody]FriendResource friendResource)
        {
            var vehicle = mapper.Map<FriendResource, Friend>(friendResource); 
            return Ok(vehicle); 
        }
    }
}