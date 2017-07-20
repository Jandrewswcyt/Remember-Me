using System;
using Xunit;
using RememberMe.Core.Interfaces;
using RememberMe.Core.Models;
using RememberMe.Controllers;
using AutoMapper;
using RememberMe.Mapping;
using System.Threading.Tasks;
using RememberMe.Controllers.Resource;
using Microsoft.EntityFrameworkCore;
using RememberMe.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace RememberMe.Test
{
    public class FriendRepositoryTests
    {
        [Fact]
        public void Add_WhenCalled_AddsFriendToRepository()
        {
            //Arrange
            UnitOfWork work; 
            var repo = GetRepoContextWithData(out work);
            //Act
            repo.Add(new Friend{Id = 4, Name = "Manjit"}); 
            work.Complete(); 
            //Assert
            var friend = repo.GetFriend(4).Result; 
            Assert.Equal( friend.Name, "Manjit"); 
        }

        [Fact]
        public void Add_WhenCalled_HasCorrectContactDetails()
        {
            //Arrange 
            UnitOfWork work; 
            var repo = GetRepoContextWithData(out work);
            ContactDetails cdOne = new ContactDetails {Phone = "07776668888", Email = "FooBlah@BlahFoo.com"}; 
            //Act
            repo.Add(new Friend{Id = 4, Name ="Dominic", ContactDetails = cdOne});
            work.Complete(); 
            //Assert
            var friend = repo.GetFriend(4).Result; 
            Assert.Equal(friend.ContactDetails.Email, cdOne.Email); 
            Assert.Equal(friend.ContactDetails.Phone, cdOne.Phone);  
        }

        [Fact]
        public async void Repository_WillImplicitly_UpdateFriendDetails()
        {
            //Arrange 
            UnitOfWork work; 
            var repo = GetRepoContextWithData(out work);
            ContactDetails cdOne = new ContactDetails {Phone = "07776668888", Email = "FooBlah@BlahFoo.com"}; 
            ContactDetails cdTwo = new ContactDetails {Phone = "2525252", Email = "NewFoo@Gmail.com"}; 
            //Act
            repo.Add(new Friend{Id = 4, Name = "James Brown",ContactDetails = cdOne}); 
            work.Complete(); 
            var friend = repo.GetFriend(4).Result; 
            friend.ContactDetails = cdTwo; 
            friend.Name = "Jim Brown";
            work.Complete(); 
        
            var friendUpdated = repo.GetFriend(4).Result;  
            //Assert
            int totalFriends = await repo.TotalFriends(); 
            Assert.Equal(totalFriends, 4); 
            Assert.Equal(friendUpdated.Name, friend.Name ) ; 
            Assert.Equal(friendUpdated.ContactDetails.Email, friend.ContactDetails.Email);
            Assert.Equal(friendUpdated.ContactDetails.Phone, friend.ContactDetails.Phone);
        }
        private  FriendRepository GetRepoContextWithData(out UnitOfWork unitOfWork)
        {
            var options = new DbContextOptionsBuilder<RememberMeDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options; 
            var context = new RememberMeDbContext(options); 

            var repo = new FriendRepository(context); 
            ContactDetails cdOne = new ContactDetails {Phone = "07776668888", Email = "FooBlah@BlahFoo.com" }; 

            repo.Add(new Friend {Id = 1, Name = "Sally", ContactDetails = cdOne} );
            repo.Add(new Friend {Id = 2, Name = "Jon"} );
            repo.Add(new Friend {Id = 3, Name = "Theo"} );

            unitOfWork = new UnitOfWork(context);
            unitOfWork.Complete();  
            return repo;  
        }

    }

}
