using System.Collections.Generic;
using System.Threading.Tasks;
using RememberMe.Core.Models;

namespace RememberMe.Core.Interfaces
{
    public interface IFriendRepository
    {
        void Add(Friend friend);
        void Remove(Friend friend);

        Task<Friend> GetFriend(int id, bool includeAll = true); 
        Task<IEnumerable<Friend>> GetFriendsAsync();  
          
    }
}