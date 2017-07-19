using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RememberMe.Core.Interfaces;
using RememberMe.Core.Models;

namespace RememberMe.Persistence
{
    public class FriendRepository : IFriendRepository
    {
        private readonly RememberMeDbContext context;
        public FriendRepository(RememberMeDbContext context)
        {
            this.context = context;

        }

        public void Add(Friend friend)
        {
            context.Friends.Add(friend); 
        }

        public Task<Friend> GetFriend(int id, bool includeAll = true)
        {
            if(!includeAll)
                return context.Friends.FindAsync(id); 
            
            return context.Friends.FindAsync(id); //Change to include selected; 
        }

        public async Task<IEnumerable<Friend>> GetFriendsAsync()
        {
            return await context.Friends.ToListAsync();
        }

        public void Remove(Friend friend)
        {
            context.Remove(friend); 
        }

        public async Task<int> TotalFriends()
        {
            return await context.Friends.CountAsync(); 
        }

    }
}