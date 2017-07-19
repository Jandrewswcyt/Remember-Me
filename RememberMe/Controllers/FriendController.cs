using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RememberMe.Core.Models;
using AutoMapper;
using RememberMe.Persistence;
using Microsoft.EntityFrameworkCore;
using RememberMe.Controllers.Resource;
using System;
using RememberMe.Core.Interfaces;

namespace RememberMe.Controllers
{
    [Route("/api/friends")]
    public class FriendsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IFriendRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public FriendsController(IMapper mapper, IFriendRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFriend(int id)
        {
             var friend = await repository.GetFriend(id);
    
            if(friend == null)
            {
                return NotFound(); 
            }

            var friendResource = mapper.Map<Friend, SaveFriendResource>(friend); 

            return Ok(friendResource); 
        }


        // [HttpGet]
        // public async Task<IEnumerable<SaveFriendResource>> GetFriends()
        // {
        //     var friends = await repository.GetFriendsAsync();
        //     return mapper.Map<List<Friend>, List<SaveFriendResource>>(friends);
        // }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFriend([FromBody]FriendResource friendResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var friend = mapper.Map<FriendResource, Friend>(friendResource);
            friend.LastUpdated = DateTime.Now;
            repository.Add(friend);

            await unitOfWork.CompleteAsync(); 
         
            return Ok(friend);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFriend(int id, [FromBody]SaveFriendResource friendResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var friend = await repository.GetFriend(id); 

            if(friend == null)
            {
                return NotFound(); 
            }

            mapper.Map<SaveFriendResource, Friend>(friendResource,friend);
            friend.LastUpdated = DateTime.Now;
         
            await unitOfWork.CompleteAsync(); 
        
            return Ok(friend);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriend(int id)
        {
            var friend = await repository.GetFriend(id);
    
            if(friend == null)
            {
                return NotFound(); 
            }

            repository.Remove(friend); 
            await unitOfWork.CompleteAsync(); 

            return Ok(id);  
        }
    }
}